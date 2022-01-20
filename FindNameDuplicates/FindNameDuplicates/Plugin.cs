using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using Invelos.DVDProfilerPlugin;

namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    [ComVisible(true)]
    [Guid("6E02321E-1226-417E-947B-11BC88C7676E")]
    public class Plugin : IDVDProfilerPlugin, IDVDProfilerPluginInfo
    {
        internal static Settings Settings;

        private readonly String SettingsFile;

        internal static readonly String ErrorFile;

        private readonly String ApplicationPath;

        private IDVDProfilerAPI Api;

        private const Int32 MenuId = 1;

        private MainForm MainForm;

        private String MenuTokenISCP = "";

        private WindowHandle WindowHandle;

        static Plugin()
        {
            ErrorFile = Environment.GetEnvironmentVariable("TEMP") + @"\FindNameDuplicatesCrash.xml";
        }

        public Plugin()
        {
            ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Doena Soft\Find Name Duplicates\";
            SettingsFile = ApplicationPath + "FindNameDuplicatesSettings.xml";
        }

        #region IDVDProfilerPlugin Members
        public void Load(IDVDProfilerAPI api)
        {
            WindowHandle = new WindowHandle();
            Api = api;
            if (Directory.Exists(ApplicationPath) == false)
            {
                Directory.CreateDirectory(ApplicationPath);
            }
            if (File.Exists(SettingsFile))
            {
                try
                {
                    Settings = Settings.Deserialize(SettingsFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, SettingsFile, ex.Message)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CreateSettings();
            MenuTokenISCP = Api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , "Collection", Texts.PluginName, MenuId);
            Api.RegisterForEvent(PluginConstants.EVENTID_DVDEditSave);
        }

        public void Unload()
        {
            Api.UnregisterMenuItem(MenuTokenISCP);
            try
            {
                Settings.Serialize(SettingsFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, SettingsFile, ex.Message)
                    , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Api = null;
        }

        public void HandleEvent(Int32 EventType, Object EventData)
        {
            if (EventType == PluginConstants.EVENTID_CustomMenuClick)
            {
                this.HandleMenuClick((Int32)EventData);
            }
            if (EventType == PluginConstants.EVENTID_DVDEditSave)
            {
                if (MainForm != null)
                {
                    MainForm.UpdateProfile();
                }
            }
        }
        #endregion

        #region IDVDProfilerPluginInfo Members
        public String GetName()
        {
            //if(Settings.DefaultValues.UiCulture!=0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginName", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return (Texts.PluginName);
        }

        public String GetDescription()
        {
            //if(Settings.DefaultValues.UiCulture != 0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginDescription", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return (Texts.PluginDescription);
        }

        public String GetAuthorName()
        {
            return ("Doena Soft.");
        }

        public String GetAuthorWebsite()
        {
            //if(Settings.DefaultValues.UiCulture != 0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginUrl", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return (Texts.PluginUrl);
        }

        public Int32 GetPluginAPIVersion()
        {
            return (PluginConstants.API_VERSION);
        }

        public Int32 GetVersionMajor()
        {
            Version version;

            version = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version;
            return (version.Major);
        }

        public Int32 GetVersionMinor()
        {
            Version version;

            version = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version;
            return (version.Minor * 100 + version.Build * 10 + version.Revision);
        }
        #endregion

        private void HandleMenuClick(Int32 MenuEventID)
        {
            if (MenuEventID == MenuId)
            {
                if (MainForm == null)
                {
                    MainForm = new MainForm(Api);
                    MainForm.FormClosed += this.OnMainFormClosed;
                    MainForm.Show(WindowHandle);
                }
                else
                {
                    MainForm.Focus();
                }
            }
        }

        private void OnMainFormClosed(Object sender, FormClosedEventArgs e)
        {
            MainForm.FormClosed -= this.OnMainFormClosed;
            MainForm.Dispose();
            MainForm = null;
        }

        private static void CreateSettings()
        {
            if (Settings == null)
            {
                Settings = new Settings();
            }
            if (Settings.MainForm == null)
            {
                Settings.MainForm = new SizableForm();
            }
            if (Settings.DefaultValues == null)
            {
                Settings.DefaultValues = new DefaultValues();
            }
        }

        #region Plugin Registering
        [DllImport("user32.dll")]
        public extern static int SetParent(int child, int parent);

        [ComImport(), Guid("0002E005-0000-0000-C000-000000000046")]
        internal class StdComponentCategoriesMgr { }

        [ComRegisterFunction()]
        public static void RegisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("6E02321E-1226-417E-947B-11BC88C7676E");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.RegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }

        [ComUnregisterFunction()]
        public static void UnregisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("6E02321E-1226-417E-947B-11BC88C7676E");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.UnRegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }
        #endregion
    }
}
