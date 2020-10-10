using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.DVDProfilerXML;
using DoenaSoft.DVDProfiler.DVDProfilerXML.Version400;
using Invelos.DVDProfilerPlugin;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    internal partial class MainForm : Form
    {
        private class DuplicateHash : Dictionary<Person, Dictionary<CompletePerson, List<DVDName>>>
        {
        }

        #region Fields
        private IDVDProfilerAPI Api;

        private ProgressWindow ProgressWindow;

        private Boolean CanClose;

        private Collection Collection;

        private DuplicateHash CastDuplicates;

        private DuplicateHash CrewDuplicates;
        #endregion

        #region Delegates
        private delegate void ProgressBarDelegate();

        private delegate String GetProfileDataDelegate(String id);

        private delegate void ThreadFinishedDelegate(Collection collection);
        #endregion

        #region Constructor
        public MainForm(IDVDProfilerAPI api)
        {
            this.Api = api;
            this.CanClose = true;
            InitializeComponent();
        }
        #endregion

        private void OnProcessButtonClick(Object sender, EventArgs e)
        {
            try
            {
                this.CanClose = false;
                this.UseWaitCursor = true;
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;
                this.CastBasicNameListBox.Items.Clear();
                this.CastFullNameListBox.Items.Clear();
                this.CastDVDNameListBox.Items.Clear();
                this.CrewBasicNameListBox.Items.Clear();
                this.CrewFullNameListBox.Items.Clear();
                this.CrewDVDNameListBox.Items.Clear();
                this.CastDuplicates = new DuplicateHash();
                this.CrewDuplicates = new DuplicateHash();
                if (this.Collection == null)
                {
                    Thread thread;
                    Object[] allIds;

                    this.ProgressWindow = new ProgressWindow();
                    this.ProgressWindow.ProgressBar.Minimum = 0;
                    this.ProgressWindow.ProgressBar.Step = 1;
                    this.ProgressWindow.CanClose = false;
                    allIds = (Object[])(this.Api.GetAllProfileIDs());
                    this.ProgressWindow.ProgressBar.Maximum = allIds.Length;
                    this.ProgressWindow.Show();
                    if (TaskbarManager.IsPlatformSupported)
                    {
                        TaskbarManager.Instance.OwnerHandle = this.Handle;
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                        TaskbarManager.Instance.SetProgressValue(0, this.ProgressWindow.ProgressBar.Maximum);
                    }
                    thread = new Thread(new ParameterizedThreadStart(this.ThreadRun));
                    thread.IsBackground = false;
                    thread.Start(new Object[] { allIds });
                }
                else
                {
                    this.ThreadFinished(this.Collection);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ExceptionXml exceptionXml;

                    MessageBox.Show(this, String.Format(MessageBoxTexts.CriticalError, ex.Message, Plugin.ErrorFile)
                        , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (File.Exists(Plugin.ErrorFile))
                    {
                        File.Delete(Plugin.ErrorFile);
                    }
                    exceptionXml = new ExceptionXml(ex);
                    DVDProfilerSerializer<ExceptionXml>.Serialize(Plugin.ErrorFile, exceptionXml);
                }
                catch (Exception inEx)
                {
                    MessageBox.Show(this, String.Format(MessageBoxTexts.FileCantBeWritten, Plugin.ErrorFile, inEx.Message), MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ThreadRun(Object param)
        {
            Collection collection;

            collection = null;
            try
            {
                Object[] allIds;
                List<DVD> dvdList;

                allIds = (Object[])(((Object[])param)[0]);
                dvdList = new List<DVD>(allIds.Length);
                for (Int32 i = 0; i < allIds.Length; i++)
                {
                    DVD dvd;
                    String xml;

                    xml = (String)(this.Invoke(new GetProfileDataDelegate(this.GetProfileData), allIds[i]));
                    dvd = DVDProfilerSerializer<DVD>.FromString(xml, DVD.DefaultEncoding);
                    dvdList.Add(dvd);
                    this.Invoke(new ProgressBarDelegate(this.UpdateProgressBar));
                }
                collection = new Collection();
                collection.DVDList = dvdList.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Invoke(new ThreadFinishedDelegate(this.ThreadFinished), collection);
            }
        }

        private void ThreadFinished(Collection collection)
        {
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                TaskbarManager.Instance.OwnerHandle = IntPtr.Zero;
            }
            if (this.ProgressWindow != null)
            {
                this.ProgressWindow.CanClose = true;
                this.ProgressWindow.Close();
                this.ProgressWindow.Dispose();
                this.ProgressWindow = null;
            }
            this.Collection = collection;
            if (this.Collection != null)
            {
                if (collection.DVDList != null && collection.DVDList.Length >= 0)
                {
                    List<Person> duplicates;

                    foreach (DVD dvd in collection.DVDList)
                    {
                        DVDName dvdName;

                        dvdName = new DVDName(dvd, true);
                        if ((dvd.CastList != null) && (dvd.CastList.Length > 0))
                        {
                            for (Int32 i = 0; i < dvd.CastList.Length; i++)
                            {
                                IPerson xmlPerson;

                                xmlPerson = dvd.CastList[i] as IPerson;
                                this.ProcessXmlPerson(dvdName, xmlPerson, this.CastDuplicates);
                            }
                        }
                        dvdName = new DVDName(dvd, false);
                        if ((dvd.CrewList != null) && (dvd.CrewList.Length > 0))
                        {
                            for (Int32 i = 0; i < dvd.CrewList.Length; i++)
                            {
                                IPerson xmlPerson;

                                xmlPerson = dvd.CrewList[i] as IPerson;
                                this.ProcessXmlPerson(dvdName, xmlPerson, this.CrewDuplicates);
                            }
                        }
                    }
                    duplicates = new List<Person>();
                    foreach (KeyValuePair<Person, Dictionary<CompletePerson, List<DVDName>>> keyValue in this.CastDuplicates)
                    {
                        if (keyValue.Value.Count > 1)
                        {
                            duplicates.Add(keyValue.Key);
                        }
                    }
                    duplicates.Sort(ComparePerson);
                    this.CastBasicNameListBox.Items.AddRange(duplicates.ToArray());
                    duplicates = new List<Person>();
                    foreach (KeyValuePair<Person, Dictionary<CompletePerson, List<DVDName>>> keyValue in this.CrewDuplicates)
                    {
                        if (keyValue.Value.Count > 1)
                        {
                            duplicates.Add(keyValue.Key);
                        }
                    }
                    duplicates.Sort(ComparePerson);
                    this.CrewBasicNameListBox.Items.AddRange(duplicates.ToArray());
                }
                MessageBox.Show(this, MessageBoxTexts.Done, MessageBoxTexts.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Enabled = true;
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            this.CanClose = true;
        }

        private void ProcessXmlPerson(DVDName dvdName, IPerson xmlPerson, DuplicateHash duplicates)
        {
            if (xmlPerson != null)
            {
                Person person;
                CompletePerson completePerson;

                //InvertedNamePerson invertedNamePerson;

                person = new Person(xmlPerson);
                completePerson = new CompletePerson(xmlPerson);
                this.ProcessPerson(person, completePerson, dvdName, duplicates);
                if ((this.CheckCreditedAsCheckBox.Checked) && (String.IsNullOrEmpty(xmlPerson.CreditedAs) == false))
                {
                    CreditedAsPerson creditedAsPerson;

                    creditedAsPerson = new CreditedAsPerson(xmlPerson);
                    this.ProcessPerson(creditedAsPerson, completePerson, dvdName, duplicates);
                }
                if (this.CheckWithoutMiddleNameCheckBox.Checked)
                {
                    NoMiddleNamePerson noMiddleNamePerson;

                    noMiddleNamePerson = new NoMiddleNamePerson(xmlPerson);
                    if (person.ToString() != noMiddleNamePerson.ToString())
                    {
                        this.ProcessPerson(noMiddleNamePerson, completePerson, dvdName, duplicates);
                    }
                }
                //invertedNamePerson = new InvertedNamePerson(xmlPerson);
                //if (person.ToString() != invertedNamePerson.ToString())
                //{
                //    this.ProcessPerson(invertedNamePerson, completePerson, dvdName);
                //}
            }
        }

        private static Int32 ComparePerson(Person left, Person right)
        {
            return (left.ToString().CompareTo(right.ToString()));
        }

        private void ProcessPerson(Person person, CompletePerson completePerson, DVDName dvdName, DuplicateHash duplicates)
        {
            if (String.IsNullOrEmpty(completePerson.XmlPerson.CreditedAs) == false)
            {
                dvdName = new DVDName(dvdName, "(as " + completePerson.XmlPerson.CreditedAs + ")");
            }
            if (duplicates.ContainsKey(person))
            {
                if (duplicates[person].ContainsKey(completePerson))
                {
                    if (duplicates[person][completePerson].Contains(dvdName) == false)
                    {
                        duplicates[person][completePerson].Add(dvdName);
                    }
                }
                else
                {
                    duplicates[person].Add(completePerson, new List<DVDName>());
                    duplicates[person][completePerson].Add(dvdName);
                }
            }
            else
            {
                duplicates.Add(person, new Dictionary<CompletePerson, List<DVDName>>());
                duplicates[person] = new Dictionary<CompletePerson, List<DVDName>>();
                duplicates[person].Add(completePerson, new List<DVDName>());
                duplicates[person][completePerson].Add(dvdName);
            }
        }

        private void OnCastBasicNameListBoxSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.CastBasicNameListBox.SelectedIndex != -1)
            {
                Person person;
                Dictionary<CompletePerson, List<DVDName>> duplicates;
                List<Person> list;

                this.CastFullNameListBox.Items.Clear();
                this.CastDVDNameListBox.Items.Clear();
                person = (Person)(this.CastBasicNameListBox.Items[this.CastBasicNameListBox.SelectedIndex]);
                duplicates = this.CastDuplicates[person];
                list = new List<Person>();
                foreach (CompletePerson completePerson in duplicates.Keys)
                {
                    list.Add(completePerson);
                }
                list.Sort(ComparePerson);
                this.CastFullNameListBox.Items.AddRange(list.ToArray());
            }
        }

        private void OnCastFullNameListBoxSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.CastFullNameListBox.SelectedIndex != -1)
            {
                Person person;
                CompletePerson completePerson;
                List<DVDName> dvds;

                this.CastDVDNameListBox.Items.Clear();
                person = (Person)(this.CastBasicNameListBox.Items[this.CastBasicNameListBox.SelectedIndex]);
                completePerson = (CompletePerson)(this.CastFullNameListBox.Items[this.CastFullNameListBox.SelectedIndex]);
                dvds = this.CastDuplicates[person][completePerson];
                dvds.Sort(CompareDVDName);
                this.CastDVDNameListBox.Items.AddRange(dvds.ToArray());
            }
        }

        private void OnCrewBasicNameListBoxSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.CrewBasicNameListBox.SelectedIndex != -1)
            {
                Person person;
                Dictionary<CompletePerson, List<DVDName>> duplicates;
                List<Person> list;

                this.CrewFullNameListBox.Items.Clear();
                this.CrewDVDNameListBox.Items.Clear();
                person = (Person)(this.CrewBasicNameListBox.Items[this.CrewBasicNameListBox.SelectedIndex]);
                duplicates = this.CrewDuplicates[person];
                list = new List<Person>();
                foreach (CompletePerson completePerson in duplicates.Keys)
                {
                    list.Add(completePerson);
                }
                list.Sort(ComparePerson);
                this.CrewFullNameListBox.Items.AddRange(list.ToArray());
            }
        }

        private void OnCrewFullNameListBoxSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.CrewFullNameListBox.SelectedIndex != -1)
            {
                Person person;
                CompletePerson completePerson;
                List<DVDName> dvds;

                this.CrewDVDNameListBox.Items.Clear();
                person = (Person)(this.CrewBasicNameListBox.Items[this.CrewBasicNameListBox.SelectedIndex]);
                completePerson = (CompletePerson)(this.CrewFullNameListBox.Items[this.CrewFullNameListBox.SelectedIndex]);
                dvds = this.CrewDuplicates[person][completePerson];
                dvds.Sort(CompareDVDName);
                this.CrewDVDNameListBox.Items.AddRange(dvds.ToArray());
            }
        }

        private static Int32 CompareDVDName(DVDName left, DVDName right)
        {
            return (left.ToString().CompareTo(right.ToString()));
        }

        private void OnMainFormLoad(Object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.CheckCreditedAsCheckBox.Checked = Plugin.Settings.DefaultValues.CheckCreditedAs;
            this.CheckWithoutMiddleNameCheckBox.Checked = Plugin.Settings.DefaultValues.CheckMissingMiddleName;
            this.ReloadCheckBox.Checked = Plugin.Settings.DefaultValues.ReloadAfterSavingProfile;
            if (Plugin.Settings.MainForm.WindowState == FormWindowState.Normal)
            {
                this.Left = Plugin.Settings.MainForm.Left;
                this.Top = Plugin.Settings.MainForm.Top;
                this.Width = Plugin.Settings.MainForm.Width;
                this.Height = Plugin.Settings.MainForm.Height;
            }
            else
            {
                this.Left = Plugin.Settings.MainForm.RestoreBounds.X;
                this.Top = Plugin.Settings.MainForm.RestoreBounds.Y;
                this.Width = Plugin.Settings.MainForm.RestoreBounds.Width;
                this.Height = Plugin.Settings.MainForm.RestoreBounds.Height;
            }
            if (Plugin.Settings.MainForm.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = Plugin.Settings.MainForm.WindowState;
            }
            this.ResumeLayout();
            if (Plugin.Settings.DefaultValues.CurrentVersion != this.GetType().Assembly.GetName().Version.ToString())
            {
                this.OpenReadMe();
                Plugin.Settings.DefaultValues.CurrentVersion = this.GetType().Assembly.GetName().Version.ToString();
            }
        }

        private void OnMainFormClosing(Object sender, FormClosingEventArgs e)
        {
            if (this.CanClose == false)
            {
                e.Cancel = true;
                return;
            }
            Plugin.Settings.MainForm.Left = this.Left;
            Plugin.Settings.MainForm.Top = this.Top;
            Plugin.Settings.MainForm.Width = this.Width;
            Plugin.Settings.MainForm.Height = this.Height;
            Plugin.Settings.MainForm.WindowState = this.WindowState;
            Plugin.Settings.MainForm.RestoreBounds = this.RestoreBounds;
            Plugin.Settings.DefaultValues.CheckCreditedAs = this.CheckCreditedAsCheckBox.Checked;
            Plugin.Settings.DefaultValues.CheckMissingMiddleName = this.CheckWithoutMiddleNameCheckBox.Checked;
            Plugin.Settings.DefaultValues.ReloadAfterSavingProfile = this.ReloadCheckBox.Checked;
        }

        private void CheckForNewVersion()
        {
            OnlineAccess.Init("Doena Soft.", "FindNameDuplicates");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", this, "FindNameDuplicates", this.GetType().Assembly);
        }

        private void OnAboutToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox(this.GetType().Assembly))
            {
                aboutBox.ShowDialog();
            }
        }

        private void OnHelpToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.OpenReadMe();
        }

        private void OpenReadMe()
        {
            String helpFile;

            helpFile = (new FileInfo(this.GetType().Assembly.Location)).DirectoryName + @"\Readme\readme.txt";
            if (File.Exists(helpFile))
            {
                using (HelpForm helpForm = new HelpForm(helpFile))
                {
                    helpForm.Text = "Read Me";
                    helpForm.ShowDialog(this);
                }
            }
        }

        private void UpdateProgressBar()
        {
            this.ProgressWindow.ProgressBar.PerformStep();
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressValue(this.ProgressWindow.ProgressBar.Value, this.ProgressWindow.ProgressBar.Maximum);
            }
        }

        private String GetProfileData(String id)
        {
            IDVDInfo dvdInfo;
            String xml;

            this.Api.DVDByProfileID(out dvdInfo, id, -1, -1);
            xml = dvdInfo.GetXML(true);
            return (xml);
        }

        private void OnCrewDVDNameListBoxMouseDoubleClick(Object sender, MouseEventArgs e)
        {
        }

        private void OnCastDVDNameListBoxMouseDoubleClick(Object sender, MouseEventArgs e)
        {
            if (this.CastDVDNameListBox.SelectedIndex != -1)
            {
                DVDName dvdName;

                dvdName = (DVDName)(this.CastDVDNameListBox.Items[this.CastDVDNameListBox.SelectedIndex]);
                this.Api.SelectDVDByProfileID(dvdName.Id);
            }
        }

        internal void UpdateProfile()
        {
            if (this.ReloadCheckBox.Checked)
            {
                this.Collection = null;
                this.OnProcessButtonClick(this, EventArgs.Empty);
            }
        }

        private void OnCheckForUpdateToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.CheckForNewVersion();
        }
    }
}