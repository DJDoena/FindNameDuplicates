namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ProcessButton = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckWithoutMiddleNameCheckBox = new System.Windows.Forms.CheckBox();
            this.CheckCreditedAsCheckBox = new System.Windows.Forms.CheckBox();
            this.CrewTab = new System.Windows.Forms.TabControl();
            this.CastTab = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CastDVDNameListBox = new System.Windows.Forms.ListBox();
            this.CastFullNameListBox = new System.Windows.Forms.ListBox();
            this.CastBasicNameListBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CrewDVDNameListBox = new System.Windows.Forms.ListBox();
            this.CrewFullNameListBox = new System.Windows.Forms.ListBox();
            this.CrewBasicNameListBox = new System.Windows.Forms.ListBox();
            this.ReloadCheckBox = new System.Windows.Forms.CheckBox();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.CrewTab.SuspendLayout();
            this.CastTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(12, 50);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(86, 23);
            this.ProcessButton.TabIndex = 3;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.OnProcessButtonClick);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(792, 24);
            this.MenuStrip.TabIndex = 10;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.AboutToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.OnHelpToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // CheckWithoutMiddleNameCheckBox
            // 
            this.CheckWithoutMiddleNameCheckBox.AutoSize = true;
            this.CheckWithoutMiddleNameCheckBox.Location = new System.Drawing.Point(12, 27);
            this.CheckWithoutMiddleNameCheckBox.Name = "CheckWithoutMiddleNameCheckBox";
            this.CheckWithoutMiddleNameCheckBox.Size = new System.Drawing.Size(229, 17);
            this.CheckWithoutMiddleNameCheckBox.TabIndex = 11;
            this.CheckWithoutMiddleNameCheckBox.Text = "Additionally check for missing Middle Name";
            this.CheckWithoutMiddleNameCheckBox.UseVisualStyleBackColor = true;
            // 
            // CheckCreditedAsCheckBox
            // 
            this.CheckCreditedAsCheckBox.AutoSize = true;
            this.CheckCreditedAsCheckBox.Checked = true;
            this.CheckCreditedAsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckCreditedAsCheckBox.Location = new System.Drawing.Point(290, 27);
            this.CheckCreditedAsCheckBox.Name = "CheckCreditedAsCheckBox";
            this.CheckCreditedAsCheckBox.Size = new System.Drawing.Size(192, 17);
            this.CheckCreditedAsCheckBox.TabIndex = 12;
            this.CheckCreditedAsCheckBox.Text = "Additionally check for \"credited as\"";
            this.CheckCreditedAsCheckBox.UseVisualStyleBackColor = true;
            // 
            // CrewTab
            // 
            this.CrewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrewTab.Controls.Add(this.CastTab);
            this.CrewTab.Controls.Add(this.tabPage2);
            this.CrewTab.Location = new System.Drawing.Point(12, 79);
            this.CrewTab.Name = "CrewTab";
            this.CrewTab.SelectedIndex = 0;
            this.CrewTab.Size = new System.Drawing.Size(768, 275);
            this.CrewTab.TabIndex = 13;
            // 
            // CastTab
            // 
            this.CastTab.Controls.Add(this.label4);
            this.CastTab.Controls.Add(this.label3);
            this.CastTab.Controls.Add(this.label2);
            this.CastTab.Controls.Add(this.CastDVDNameListBox);
            this.CastTab.Controls.Add(this.CastFullNameListBox);
            this.CastTab.Controls.Add(this.CastBasicNameListBox);
            this.CastTab.Location = new System.Drawing.Point(4, 22);
            this.CastTab.Name = "CastTab";
            this.CastTab.Padding = new System.Windows.Forms.Padding(3);
            this.CastTab.Size = new System.Drawing.Size(760, 249);
            this.CastTab.TabIndex = 0;
            this.CastTab.Text = "Cast";
            this.CastTab.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(537, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "DVD:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "<FirstName> {Middle Name} [Last Name]\r\n(Birth Year):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Basic Name:";
            // 
            // CastDVDNameListBox
            // 
            this.CastDVDNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CastDVDNameListBox.FormattingEnabled = true;
            this.CastDVDNameListBox.Location = new System.Drawing.Point(540, 47);
            this.CastDVDNameListBox.Name = "CastDVDNameListBox";
            this.CastDVDNameListBox.Size = new System.Drawing.Size(214, 186);
            this.CastDVDNameListBox.TabIndex = 12;
            this.CastDVDNameListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnCastDVDNameListBoxMouseDoubleClick);
            // 
            // CastFullNameListBox
            // 
            this.CastFullNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CastFullNameListBox.FormattingEnabled = true;
            this.CastFullNameListBox.Location = new System.Drawing.Point(274, 47);
            this.CastFullNameListBox.Name = "CastFullNameListBox";
            this.CastFullNameListBox.Size = new System.Drawing.Size(251, 186);
            this.CastFullNameListBox.TabIndex = 11;
            this.CastFullNameListBox.SelectedIndexChanged += new System.EventHandler(this.OnCastFullNameListBoxSelectedIndexChanged);
            // 
            // CastBasicNameListBox
            // 
            this.CastBasicNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CastBasicNameListBox.FormattingEnabled = true;
            this.CastBasicNameListBox.Location = new System.Drawing.Point(9, 47);
            this.CastBasicNameListBox.Name = "CastBasicNameListBox";
            this.CastBasicNameListBox.Size = new System.Drawing.Size(250, 186);
            this.CastBasicNameListBox.TabIndex = 10;
            this.CastBasicNameListBox.SelectedIndexChanged += new System.EventHandler(this.OnCastBasicNameListBoxSelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.CrewDVDNameListBox);
            this.tabPage2.Controls.Add(this.CrewFullNameListBox);
            this.tabPage2.Controls.Add(this.CrewBasicNameListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 249);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Crew";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(537, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "DVD:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 26);
            this.label6.TabIndex = 20;
            this.label6.Text = "<FirstName> {Middle Name} [Last Name]\r\n(Birth Year):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Basic Name:";
            // 
            // CrewDVDNameListBox
            // 
            this.CrewDVDNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrewDVDNameListBox.FormattingEnabled = true;
            this.CrewDVDNameListBox.Location = new System.Drawing.Point(540, 47);
            this.CrewDVDNameListBox.Name = "CrewDVDNameListBox";
            this.CrewDVDNameListBox.Size = new System.Drawing.Size(214, 186);
            this.CrewDVDNameListBox.TabIndex = 18;
            this.CrewDVDNameListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnCrewDVDNameListBoxMouseDoubleClick);
            // 
            // CrewFullNameListBox
            // 
            this.CrewFullNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CrewFullNameListBox.FormattingEnabled = true;
            this.CrewFullNameListBox.Location = new System.Drawing.Point(274, 47);
            this.CrewFullNameListBox.Name = "CrewFullNameListBox";
            this.CrewFullNameListBox.Size = new System.Drawing.Size(251, 186);
            this.CrewFullNameListBox.TabIndex = 17;
            this.CrewFullNameListBox.SelectedIndexChanged += new System.EventHandler(this.OnCrewFullNameListBoxSelectedIndexChanged);
            // 
            // CrewBasicNameListBox
            // 
            this.CrewBasicNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CrewBasicNameListBox.FormattingEnabled = true;
            this.CrewBasicNameListBox.Location = new System.Drawing.Point(9, 47);
            this.CrewBasicNameListBox.Name = "CrewBasicNameListBox";
            this.CrewBasicNameListBox.Size = new System.Drawing.Size(250, 186);
            this.CrewBasicNameListBox.TabIndex = 16;
            this.CrewBasicNameListBox.SelectedIndexChanged += new System.EventHandler(this.OnCrewBasicNameListBoxSelectedIndexChanged);
            // 
            // ReloadCheckBox
            // 
            this.ReloadCheckBox.AutoSize = true;
            this.ReloadCheckBox.Checked = true;
            this.ReloadCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReloadCheckBox.Location = new System.Drawing.Point(556, 27);
            this.ReloadCheckBox.Name = "ReloadCheckBox";
            this.ReloadCheckBox.Size = new System.Drawing.Size(149, 17);
            this.ReloadCheckBox.TabIndex = 14;
            this.ReloadCheckBox.Text = "Reload after saving profile";
            this.ReloadCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.checkForUpdateToolStripMenuItem.Text = "&Check for Update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.OnCheckForUpdateToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 366);
            this.Controls.Add(this.ReloadCheckBox);
            this.Controls.Add(this.CrewTab);
            this.Controls.Add(this.CheckCreditedAsCheckBox);
            this.Controls.Add(this.CheckWithoutMiddleNameCheckBox);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.ProcessButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "MainForm";
            this.Text = "Find Name Duplicates";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnMainFormClosing);
            this.Load += new System.EventHandler(this.OnMainFormLoad);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.CrewTab.ResumeLayout(false);
            this.CastTab.ResumeLayout(false);
            this.CastTab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox CheckWithoutMiddleNameCheckBox;
        private System.Windows.Forms.CheckBox CheckCreditedAsCheckBox;
        private System.Windows.Forms.TabControl CrewTab;
        private System.Windows.Forms.TabPage CastTab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox CastDVDNameListBox;
        private System.Windows.Forms.ListBox CastFullNameListBox;
        private System.Windows.Forms.ListBox CastBasicNameListBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox CrewDVDNameListBox;
        private System.Windows.Forms.ListBox CrewFullNameListBox;
        private System.Windows.Forms.ListBox CrewBasicNameListBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.CheckBox ReloadCheckBox;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
    }
}

