using System.Collections.Generic;
using System.Windows.Forms;
using MFiles.MFWS.Structs;

namespace ThreeDViewCS {
    partial class ThreeDViewForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.ComponentModel.ComponentResourceManager resources = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreeDViewForm));
            this.CreationFilter_panel = new System.Windows.Forms.Panel();
            this.Visibility_pictureBox = new System.Windows.Forms.PictureBox();
            this.Creation_pictureBox = new System.Windows.Forms.PictureBox();
            this.PropertiesFilter_panel = new System.Windows.Forms.Panel();
            this.OpenFile_button = new System.Windows.Forms.PictureBox();
            this.RemoveTo_button = new System.Windows.Forms.PictureBox();
            this.RemoveFrom_button = new System.Windows.Forms.PictureBox();
            this.AddTo_button = new System.Windows.Forms.PictureBox();
            this.AddFrom_button = new System.Windows.Forms.PictureBox();
            this.MakeRoot_button = new System.Windows.Forms.PictureBox();
            this.GPU_panel = new System.Windows.Forms.Panel();
            this.GLSLVersion_text = new System.Windows.Forms.Label();
            this.GLVersion_text = new System.Windows.Forms.Label();
            this.GLRenderer_text = new System.Windows.Forms.Label();
            this.GLVendor_text = new System.Windows.Forms.Label();
            this.OpenDetails_button = new System.Windows.Forms.PictureBox();
            this.Details_panel = new System.Windows.Forms.Panel();
            this.DrawingSurface = new ThreeDViewCS.DrawingSurface();
            this.ExportToExcel_button = new System.Windows.Forms.PictureBox();
            this.DefaultView_menuItem = new System.Windows.Forms.PictureBox();
            this.MenuStrip = new System.Windows.Forms.Panel();
            this.Background_menuItem = new System.Windows.Forms.PictureBox();
            this.GPU_menuItem = new System.Windows.Forms.PictureBox();
            this.Logout_menuItem = new System.Windows.Forms.PictureBox();
            this.Properties_menuItem = new System.Windows.Forms.PictureBox();
            this.UpdateProperties_menuItem = new System.Windows.Forms.PictureBox();
            this.Filter_menuItem = new System.Windows.Forms.PictureBox();
            this.CreationFilter_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visibility_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Creation_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenFile_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveTo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveFrom_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFrom_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MakeRoot_button)).BeginInit();
            this.GPU_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenDetails_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportToExcel_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultView_menuItem)).BeginInit();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Background_menuItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPU_menuItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logout_menuItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Properties_menuItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateProperties_menuItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filter_menuItem)).BeginInit();
            this.SuspendLayout();
            // 
            // CreationFilter_panel
            // 
            this.CreationFilter_panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreationFilter_panel.AutoSize = true;
            this.CreationFilter_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CreationFilter_panel.Controls.Add(this.Visibility_pictureBox);
            this.CreationFilter_panel.Controls.Add(this.Creation_pictureBox);
            this.CreationFilter_panel.Location = new System.Drawing.Point(520, 32);
            this.CreationFilter_panel.Margin = new System.Windows.Forms.Padding(0);
            this.CreationFilter_panel.Name = "CreationFilter_panel";
            this.CreationFilter_panel.Size = new System.Drawing.Size(32, 16);
            this.CreationFilter_panel.TabIndex = 16;
            this.CreationFilter_panel.Visible = false;
            // 
            // Visibility_pictureBox
            // 
            this.Visibility_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("Visibility_pictureBox.Image")));
            this.Visibility_pictureBox.Location = new System.Drawing.Point(-2, 0);
            this.Visibility_pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.Visibility_pictureBox.Name = "Visibility_pictureBox";
            this.Visibility_pictureBox.Size = new System.Drawing.Size(16, 16);
            this.Visibility_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Visibility_pictureBox.TabIndex = 2;
            this.Visibility_pictureBox.TabStop = false;
            // 
            // Creation_pictureBox
            // 
            this.Creation_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("Creation_pictureBox.Image")));
            this.Creation_pictureBox.Location = new System.Drawing.Point(16, 0);
            this.Creation_pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.Creation_pictureBox.Name = "Creation_pictureBox";
            this.Creation_pictureBox.Size = new System.Drawing.Size(16, 16);
            this.Creation_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Creation_pictureBox.TabIndex = 1;
            this.Creation_pictureBox.TabStop = false;
            // 
            // PropertiesFilter_panel
            // 
            this.PropertiesFilter_panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PropertiesFilter_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PropertiesFilter_panel.Location = new System.Drawing.Point(552, 32);
            this.PropertiesFilter_panel.Margin = new System.Windows.Forms.Padding(0);
            this.PropertiesFilter_panel.Name = "PropertiesFilter_panel";
            this.PropertiesFilter_panel.Size = new System.Drawing.Size(27, 100);
            this.PropertiesFilter_panel.TabIndex = 22;
            this.PropertiesFilter_panel.Visible = false;
            // 
            // OpenFile_button
            // 
            this.OpenFile_button.BackColor = System.Drawing.Color.Black;
            this.OpenFile_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenFile_button.BackgroundImage")));
            this.OpenFile_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenFile_button.Location = new System.Drawing.Point(709, 481);
            this.OpenFile_button.Margin = new System.Windows.Forms.Padding(0);
            this.OpenFile_button.Name = "OpenFile_button";
            this.OpenFile_button.Size = new System.Drawing.Size(64, 64);
            this.OpenFile_button.TabIndex = 6;
            this.OpenFile_button.TabStop = false;
            this.OpenFile_button.Visible = false;
            this.OpenFile_button.MouseHover += new System.EventHandler(this.OpenFile_button_MouseHover);
            this.OpenFile_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenFile_button_MouseUp);
            // 
            // RemoveTo_button
            // 
            this.RemoveTo_button.BackColor = System.Drawing.Color.Black;
            this.RemoveTo_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveTo_button.BackgroundImage")));
            this.RemoveTo_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RemoveTo_button.Location = new System.Drawing.Point(837, 481);
            this.RemoveTo_button.Margin = new System.Windows.Forms.Padding(0);
            this.RemoveTo_button.Name = "RemoveTo_button";
            this.RemoveTo_button.Size = new System.Drawing.Size(64, 64);
            this.RemoveTo_button.TabIndex = 5;
            this.RemoveTo_button.TabStop = false;
            this.RemoveTo_button.Visible = false;
            this.RemoveTo_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RemoveTo_button_MouseUp);
            this.RemoveTo_button.MouseHover += new System.EventHandler(this.RemoveTo_button_MouseHover);
            // 
            // RemoveFrom_button
            // 
            this.RemoveFrom_button.BackColor = System.Drawing.Color.Black;
            this.RemoveFrom_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveFrom_button.BackgroundImage")));
            this.RemoveFrom_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RemoveFrom_button.Location = new System.Drawing.Point(773, 481);
            this.RemoveFrom_button.Margin = new System.Windows.Forms.Padding(0);
            this.RemoveFrom_button.Name = "RemoveFrom_button";
            this.RemoveFrom_button.Size = new System.Drawing.Size(64, 64);
            this.RemoveFrom_button.TabIndex = 4;
            this.RemoveFrom_button.TabStop = false;
            this.RemoveFrom_button.Visible = false;
            this.RemoveFrom_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RemoveFrom_button_MouseUp);
            this.RemoveFrom_button.MouseHover += new System.EventHandler(this.RemoveFrom_button_MouseHover);
            // 
            // AddTo_button
            // 
            this.AddTo_button.BackColor = System.Drawing.Color.Black;
            this.AddTo_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddTo_button.BackgroundImage")));
            this.AddTo_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddTo_button.Location = new System.Drawing.Point(837, 417);
            this.AddTo_button.Margin = new System.Windows.Forms.Padding(0);
            this.AddTo_button.Name = "AddTo_button";
            this.AddTo_button.Size = new System.Drawing.Size(64, 64);
            this.AddTo_button.TabIndex = 3;
            this.AddTo_button.TabStop = false;
            this.AddTo_button.Visible = false;
            this.AddTo_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddTo_button_MouseUp);
            this.AddTo_button.MouseHover += new System.EventHandler(this.AddTo_button_MouseHover);
            // 
            // AddFrom_button
            // 
            this.AddFrom_button.BackColor = System.Drawing.Color.Black;
            this.AddFrom_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddFrom_button.BackgroundImage")));
            this.AddFrom_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddFrom_button.Location = new System.Drawing.Point(773, 417);
            this.AddFrom_button.Margin = new System.Windows.Forms.Padding(0);
            this.AddFrom_button.Name = "AddFrom_button";
            this.AddFrom_button.Size = new System.Drawing.Size(64, 64);
            this.AddFrom_button.TabIndex = 2;
            this.AddFrom_button.TabStop = false;
            this.AddFrom_button.Visible = false;
            this.AddFrom_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddFrom_button_MouseUp);
            this.AddFrom_button.MouseHover += new System.EventHandler(this.AddFrom_button_MouseHover);
            // 
            // MakeRoot_button
            // 
            this.MakeRoot_button.BackColor = System.Drawing.Color.Black;
            this.MakeRoot_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MakeRoot_button.BackgroundImage")));
            this.MakeRoot_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MakeRoot_button.Location = new System.Drawing.Point(709, 417);
            this.MakeRoot_button.Margin = new System.Windows.Forms.Padding(0);
            this.MakeRoot_button.Name = "MakeRoot_button";
            this.MakeRoot_button.Size = new System.Drawing.Size(64, 64);
            this.MakeRoot_button.TabIndex = 1;
            this.MakeRoot_button.TabStop = false;
            this.MakeRoot_button.Visible = false;
            this.MakeRoot_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeRoot_button_MouseUp);
            this.MakeRoot_button.MouseHover += new System.EventHandler(this.MakeRoot_button_MouseHover);
            // 
            // GPU_panel
            // 
            this.GPU_panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GPU_panel.Controls.Add(this.GLSLVersion_text);
            this.GPU_panel.Controls.Add(this.GLVersion_text);
            this.GPU_panel.Controls.Add(this.GLRenderer_text);
            this.GPU_panel.Controls.Add(this.GLVendor_text);
            this.GPU_panel.Location = new System.Drawing.Point(680, 32);
            this.GPU_panel.Name = "GPU_panel";
            this.GPU_panel.Size = new System.Drawing.Size(200, 80);
            this.GPU_panel.TabIndex = 19;
            this.GPU_panel.Visible = false;
            // 
            // GLSLVersion_text
            // 
            this.GLSLVersion_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GLSLVersion_text.Location = new System.Drawing.Point(0, 60);
            this.GLSLVersion_text.Margin = new System.Windows.Forms.Padding(0);
            this.GLSLVersion_text.Name = "GLSLVersion_text";
            this.GLSLVersion_text.Size = new System.Drawing.Size(200, 20);
            this.GLSLVersion_text.TabIndex = 7;
            this.GLSLVersion_text.Text = "GLSL_VERSION";
            this.GLSLVersion_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GLVersion_text
            // 
            this.GLVersion_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GLVersion_text.Location = new System.Drawing.Point(0, 40);
            this.GLVersion_text.Margin = new System.Windows.Forms.Padding(0);
            this.GLVersion_text.Name = "GLVersion_text";
            this.GLVersion_text.Size = new System.Drawing.Size(200, 20);
            this.GLVersion_text.TabIndex = 6;
            this.GLVersion_text.Text = "GL_VERSION";
            this.GLVersion_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GLRenderer_text
            // 
            this.GLRenderer_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GLRenderer_text.Location = new System.Drawing.Point(0, 0);
            this.GLRenderer_text.Margin = new System.Windows.Forms.Padding(0);
            this.GLRenderer_text.Name = "GLRenderer_text";
            this.GLRenderer_text.Size = new System.Drawing.Size(200, 20);
            this.GLRenderer_text.TabIndex = 5;
            this.GLRenderer_text.Text = "GL_RENDERER";
            this.GLRenderer_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GLVendor_text
            // 
            this.GLVendor_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GLVendor_text.Location = new System.Drawing.Point(0, 20);
            this.GLVendor_text.Margin = new System.Windows.Forms.Padding(0);
            this.GLVendor_text.Name = "GLVendor_text";
            this.GLVendor_text.Size = new System.Drawing.Size(200, 20);
            this.GLVendor_text.TabIndex = 4;
            this.GLVendor_text.Text = "GL_VENDOR";
            this.GLVendor_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenDetails_button
            // 
            this.OpenDetails_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenDetails_button.BackgroundImage")));
            this.OpenDetails_button.Location = new System.Drawing.Point(773, 545);
            this.OpenDetails_button.Margin = new System.Windows.Forms.Padding(0);
            this.OpenDetails_button.Name = "OpenDetails_button";
            this.OpenDetails_button.Size = new System.Drawing.Size(128, 10);
            this.OpenDetails_button.TabIndex = 20;
            this.OpenDetails_button.TabStop = false;
            this.OpenDetails_button.Visible = false;
            this.OpenDetails_button.MouseHover += new System.EventHandler(this.OpenDetails_button_MouseHover);
            // 
            // Details_panel
            // 
            this.Details_panel.AutoSize = true;
            this.Details_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Details_panel.BackColor = System.Drawing.Color.Transparent;
            this.Details_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Details_panel.BackgroundImage")));
            this.Details_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Details_panel.Location = new System.Drawing.Point(801, 162);
            this.Details_panel.Margin = new System.Windows.Forms.Padding(0);
            this.Details_panel.Name = "Details_panel";
            this.Details_panel.Size = new System.Drawing.Size(0, 0);
            this.Details_panel.TabIndex = 21;
            this.Details_panel.Visible = false;
            // 
            // DrawingSurface
            // 
            this.DrawingSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingSurface.Location = new System.Drawing.Point(0, 0);
            this.DrawingSurface.Margin = new System.Windows.Forms.Padding(0);
            this.DrawingSurface.Name = "DrawingSurface";
            this.DrawingSurface.Size = new System.Drawing.Size(1264, 681);
            this.DrawingSurface.TabIndex = 0;
            this.DrawingSurface.Text = "drawingSurface";
            this.DrawingSurface.Visible = false;
            this.DrawingSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_Down);
            this.DrawingSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_Up);
            // 
            // ExportToExcel_button
            // 
            this.ExportToExcel_button.BackColor = System.Drawing.Color.Black;
            this.ExportToExcel_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ExportToExcel_button.BackgroundImage")));
            this.ExportToExcel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExportToExcel_button.Location = new System.Drawing.Point(901, 481);
            this.ExportToExcel_button.Margin = new System.Windows.Forms.Padding(0);
            this.ExportToExcel_button.Name = "ExportToExcel_button";
            this.ExportToExcel_button.Size = new System.Drawing.Size(64, 64);
            this.ExportToExcel_button.TabIndex = 23;
            this.ExportToExcel_button.TabStop = false;
            this.ExportToExcel_button.Visible = false;
            this.ExportToExcel_button.Click += new System.EventHandler(this.ExportToExcel_button_Click);
            this.ExportToExcel_button.MouseHover += new System.EventHandler(this.ExportToExcel_button_MouseHover);
            // 
            // DefaultView_menuItem
            // 
            this.DefaultView_menuItem.BackColor = System.Drawing.Color.Black;
            this.DefaultView_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DefaultView_menuItem.BackgroundImage")));
            this.DefaultView_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DefaultView_menuItem.Location = new System.Drawing.Point(96, 0);
            this.DefaultView_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.DefaultView_menuItem.Name = "DefaultView_menuItem";
            this.DefaultView_menuItem.Size = new System.Drawing.Size(32, 32);
            this.DefaultView_menuItem.TabIndex = 24;
            this.DefaultView_menuItem.TabStop = false;
            this.DefaultView_menuItem.Click += new System.EventHandler(this.DefaultView_button_Click);
            this.DefaultView_menuItem.MouseHover += new System.EventHandler(this.DefaultView_menuItem_MouseHover);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MenuStrip.BackColor = System.Drawing.Color.Black;
            this.MenuStrip.Controls.Add(this.Background_menuItem);
            this.MenuStrip.Controls.Add(this.GPU_menuItem);
            this.MenuStrip.Controls.Add(this.Logout_menuItem);
            this.MenuStrip.Controls.Add(this.Properties_menuItem);
            this.MenuStrip.Controls.Add(this.DefaultView_menuItem);
            this.MenuStrip.Controls.Add(this.UpdateProperties_menuItem);
            this.MenuStrip.Controls.Add(this.Filter_menuItem);
            this.MenuStrip.Location = new System.Drawing.Point(520, 0);
            this.MenuStrip.Margin = new System.Windows.Forms.Padding(0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(224, 32);
            this.MenuStrip.TabIndex = 25;
            this.MenuStrip.Visible = false;
            // 
            // Background_menuItem
            // 
            this.Background_menuItem.BackColor = System.Drawing.Color.Black;
            this.Background_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Background_menuItem.BackgroundImage")));
            this.Background_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Background_menuItem.Location = new System.Drawing.Point(128, 0);
            this.Background_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.Background_menuItem.Name = "Background_menuItem";
            this.Background_menuItem.Size = new System.Drawing.Size(32, 32);
            this.Background_menuItem.TabIndex = 31;
            this.Background_menuItem.TabStop = false;
            this.Background_menuItem.Click += new System.EventHandler(this.Background_menuItem_Click);
            this.Background_menuItem.MouseHover += new System.EventHandler(this.Background_menuItem_MouseHover);
            // 
            // GPU_menuItem
            // 
            this.GPU_menuItem.BackColor = System.Drawing.Color.Black;
            this.GPU_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GPU_menuItem.BackgroundImage")));
            this.GPU_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GPU_menuItem.Location = new System.Drawing.Point(160, 0);
            this.GPU_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.GPU_menuItem.Name = "GPU_menuItem";
            this.GPU_menuItem.Size = new System.Drawing.Size(32, 32);
            this.GPU_menuItem.TabIndex = 30;
            this.GPU_menuItem.TabStop = false;
            this.GPU_menuItem.Click += new System.EventHandler(this.GPU_menuItem_Click);
            this.GPU_menuItem.MouseHover += new System.EventHandler(this.GPU_menuItem_MouseHover);
            // 
            // Logout_menuItem
            // 
            this.Logout_menuItem.BackColor = System.Drawing.Color.Black;
            this.Logout_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Logout_menuItem.BackgroundImage")));
            this.Logout_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Logout_menuItem.Location = new System.Drawing.Point(192, 0);
            this.Logout_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.Logout_menuItem.Name = "Logout_menuItem";
            this.Logout_menuItem.Size = new System.Drawing.Size(32, 32);
            this.Logout_menuItem.TabIndex = 29;
            this.Logout_menuItem.TabStop = false;
            this.Logout_menuItem.Click += new System.EventHandler(this.logout_menuItem_Click);
            this.Logout_menuItem.MouseHover += new System.EventHandler(this.Logout_menuItem_MouseHover);
            // 
            // Properties_menuItem
            // 
            this.Properties_menuItem.BackColor = System.Drawing.Color.Black;
            this.Properties_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Properties_menuItem.BackgroundImage")));
            this.Properties_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Properties_menuItem.Location = new System.Drawing.Point(32, 0);
            this.Properties_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.Properties_menuItem.Name = "Properties_menuItem";
            this.Properties_menuItem.Size = new System.Drawing.Size(32, 32);
            this.Properties_menuItem.TabIndex = 28;
            this.Properties_menuItem.TabStop = false;
            this.Properties_menuItem.Click += new System.EventHandler(this.Properties_menuItem_Click);
            this.Properties_menuItem.MouseHover += new System.EventHandler(this.Properties_menuItem_MouseHover);
            // 
            // UpdateProperties_menuItem
            // 
            this.UpdateProperties_menuItem.BackColor = System.Drawing.Color.Black;
            this.UpdateProperties_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpdateProperties_menuItem.BackgroundImage")));
            this.UpdateProperties_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdateProperties_menuItem.Location = new System.Drawing.Point(64, 0);
            this.UpdateProperties_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.UpdateProperties_menuItem.Name = "UpdateProperties_menuItem";
            this.UpdateProperties_menuItem.Size = new System.Drawing.Size(32, 32);
            this.UpdateProperties_menuItem.TabIndex = 27;
            this.UpdateProperties_menuItem.TabStop = false;
            this.UpdateProperties_menuItem.Visible = false;
            this.UpdateProperties_menuItem.Click += new System.EventHandler(this.UpdatePropertiesToolStripMenuItem_Click);
            this.UpdateProperties_menuItem.MouseHover += new System.EventHandler(this.UpdateProperties_menuItem_MouseHover);
            // 
            // Filter_menuItem
            // 
            this.Filter_menuItem.BackColor = System.Drawing.Color.Black;
            this.Filter_menuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Filter_menuItem.BackgroundImage")));
            this.Filter_menuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Filter_menuItem.Location = new System.Drawing.Point(0, 0);
            this.Filter_menuItem.Margin = new System.Windows.Forms.Padding(0);
            this.Filter_menuItem.Name = "Filter_menuItem";
            this.Filter_menuItem.Size = new System.Drawing.Size(32, 32);
            this.Filter_menuItem.TabIndex = 26;
            this.Filter_menuItem.TabStop = false;
            this.Filter_menuItem.Click += new System.EventHandler(this.Filter_menuItem_click);
            this.Filter_menuItem.MouseHover += new System.EventHandler(this.Filter_menuItem_MouseHover);
            // 
            // ThreeDViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.ExportToExcel_button);
            this.Controls.Add(this.PropertiesFilter_panel);
            this.Controls.Add(this.Details_panel);
            this.Controls.Add(this.OpenDetails_button);
            this.Controls.Add(this.GPU_panel);
            this.Controls.Add(this.AddFrom_button);
            this.Controls.Add(this.OpenFile_button);
            this.Controls.Add(this.MakeRoot_button);
            this.Controls.Add(this.AddTo_button);
            this.Controls.Add(this.RemoveTo_button);
            this.Controls.Add(this.CreationFilter_panel);
            this.Controls.Add(this.RemoveFrom_button);
            this.Controls.Add(this.DrawingSurface);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(3840, 2160);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "ThreeDViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3DView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.CreationFilter_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Visibility_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Creation_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenFile_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveTo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveFrom_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFrom_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MakeRoot_button)).EndInit();
            this.GPU_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OpenDetails_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportToExcel_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultView_menuItem)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Background_menuItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPU_menuItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logout_menuItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Properties_menuItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateProperties_menuItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filter_menuItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DrawingSurface DrawingSurface;
        private Panel CreationFilter_panel;
        private PictureBox Creation_pictureBox;

        public void AddCreationFilterCheckBoxes() {
            this.CreationFilter_panel.AutoSize = true;
            int index = 0;
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                // 
                // CreationFilter_checkbox
                //
                CheckBox CreationFilter_checkBox = new CheckBox();
                CreationFilter_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                CreationFilter_checkBox.Location = new System.Drawing.Point(20, 16 + (20*index));
                CreationFilter_checkBox.Margin = new System.Windows.Forms.Padding(0);
                CreationFilter_checkBox.Name = pair.Value + "_checkBox";
                CreationFilter_checkBox.AutoSize = true;
                CreationFilter_checkBox.TabIndex = index;
                CreationFilter_checkBox.TabStop = true;
                CreationFilter_checkBox.Text = pair.Value;
                CreationFilter_checkBox.UseVisualStyleBackColor = true;
                CreationFilter_checkBox.Visible = true;
                CreationFilter_checkBox.Checked = true;
                CreationFilter_checkBox.CheckedChanged += new System.EventHandler((s, ev) => {
                    // Define filtering behaviour
                    threeDView.ToggleCreationFilterType(pair.Key);
                });

                CreationFilter_checkBox.ResumeLayout(true);
                CreationFilter_checkBox.PerformLayout();
                this.CreationFilter_panel.Controls.Add(CreationFilter_checkBox);
                CreationFilter_panel.ResumeLayout(true);
                CreationFilter_panel.PerformLayout();

                TextBox filterValue = new TextBox();
                filterValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                filterValue.TextAlign = HorizontalAlignment.Left;
                filterValue.Location = new System.Drawing.Point(CreationFilter_checkBox.Location.X + CreationFilter_checkBox.Size.Width, CreationFilter_checkBox.Location.Y);
                filterValue.Size = new System.Drawing.Size(150, 20);

                filterValue.AcceptsReturn = false;
                filterValue.AcceptsTab = false;
                filterValue.AllowDrop = false;
                filterValue.Margin = System.Windows.Forms.Padding.Empty;

                filterValue.ResumeLayout(true);
                filterValue.PerformLayout();
                this.CreationFilter_panel.Controls.Add(filterValue);
                CreationFilter_panel.ResumeLayout(true);
                CreationFilter_panel.PerformLayout();
                filterValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                filterValue.Location = new System.Drawing.Point(CreationFilter_panel.Size.Width - filterValue.Size.Width, CreationFilter_checkBox.Location.Y);
                filterValue.LostFocus += (s, e) => {
                    string filterText = filterValue.Text.ToLower();
                    if (filterText.Length < 3) {
                        foreach (MFilesObject mFilesObject in threeDView.DownloadedObjects) {
                            if (mFilesObject.Type == pair.Key)
                                threeDView.ToggleRender(mFilesObject.Type, mFilesObject.ID, true);
                        }
                        //System.Console.WriteLine("Reset");
                        return;
                    }
                    // has only spaces?
                    bool onlySpaces = true;
                    foreach (char character in filterText)
                        if (character != ' ') {
                            onlySpaces = false;
                            break;
                        }
                    if (onlySpaces)
                        return;

                    string displayText = "";
                    bool foundMatch = false;
                    List<MFilesObject> unmatchedObjects = new List<MFilesObject>();

                    foreach (MFilesObject mFilesObject in threeDView.DownloadedObjects) {
                        if (mFilesObject.Type != pair.Key)
                            continue;
                        //System.Console.WriteLine("Searching trough: " + mFilesObject.Title);
                        if (mFilesObject.Title.Length >= filterText.Length && mFilesObject.Title.ToLower().Contains(filterText)) {
                            foundMatch = true;
                            //continue;
                        }
                        if (!foundMatch) {
                            PropertyValue[] properties = MFiles.GetProperties(mFilesObject);
                            foreach (PropertyValue property in properties) {
                                if (MFiles.Properties[property.PropertyDef] != "" &&
                                    threeDView.PropertiesFilter[property.PropertyDef]) {
                                    displayText = property.TypedValue.DisplayValue.ToLower();
                                    //System.Console.WriteLine("  " + displayText);
                                    if (displayText.Length >= filterText.Length)
                                        if (displayText.Contains(filterText)) {
                                            foundMatch = true;
                                            break;
                                        }
                                }
                            }
                        }
                        //System.Console.WriteLine("Match " + (foundMatch ? "found" : " not found"));
                        threeDView.ToggleRender(mFilesObject.Type, mFilesObject.ID, foundMatch);
                        foundMatch = false;
                    }
                };

                index += 1;
            }

            //this.CreationFilter_panel.Size = new System.Drawing.Size(this.CreationFilter_panel.Width, CreationFilter_label.Size.Height + (20 * index));
        }

        //private Label VisibilityFilter_label;
        //private Panel VisibilityFilter_panel;

        public void AddVisibilityFilterCheckBoxes() {
            int index = 0;
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                // 
                // VisibilityFilter_checkbox
                //
                CheckBox VisibilityFilter_checkBox = new CheckBox();
                VisibilityFilter_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                VisibilityFilter_checkBox.Location = new System.Drawing.Point(0, 16 + (20 * index));
                VisibilityFilter_checkBox.Margin = new System.Windows.Forms.Padding(0);
                VisibilityFilter_checkBox.Name = pair.Value + "_checkBox";
                VisibilityFilter_checkBox.Size = new System.Drawing.Size(20, 20);
                VisibilityFilter_checkBox.TabIndex = index;
                VisibilityFilter_checkBox.TabStop = true;
                VisibilityFilter_checkBox.Text = "";//pair.Value;
                VisibilityFilter_checkBox.UseVisualStyleBackColor = true;
                VisibilityFilter_checkBox.Visible = true;
                VisibilityFilter_checkBox.Checked = true;
                VisibilityFilter_checkBox.CheckedChanged += new System.EventHandler((s, ev) => {
                    // Define filtering behaviour
                    threeDView.ToggleVisibilityFilterType(pair.Key, VisibilityFilter_checkBox.Checked);
                });

                VisibilityFilter_checkBox.ResumeLayout(true);
                VisibilityFilter_checkBox.PerformLayout();
                this.CreationFilter_panel.Controls.Add(VisibilityFilter_checkBox);
                CreationFilter_panel.ResumeLayout(true);
                CreationFilter_panel.PerformLayout();
                index += 1;
            }

            //this.VisibilityFilter_panel.Size = new System.Drawing.Size(this.VisibilityFilter_panel.Width, VisibilityFilter_label.Size.Height + (20 * index));
        }

        public void AddPropertiesFilterCheckBoxes() {
            this.PropertiesFilter_panel.AutoSize = true;
            int propertiesPerPage = 10;
            int index = 0;
            bool setVisible = true;
            int prop = 1;
            foreach (KeyValuePair<int, string> pair in MFiles.Properties) {
                // 
                // PropertiesFilter_checkbox
                //
                CheckBox propertiesFilter_checkbox = new CheckBox();
                propertiesFilter_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                propertiesFilter_checkbox.Margin = new System.Windows.Forms.Padding(0);
                propertiesFilter_checkbox.Name = pair.Value + "_checkBox";
                //propertiesFilter_checkbox.Size = new System.Drawing.Size(200, 20);
                propertiesFilter_checkbox.TabIndex = index;
                propertiesFilter_checkbox.TabStop = true;
                propertiesFilter_checkbox.AutoSize = true;
                propertiesFilter_checkbox.Text = pair.Value;
                propertiesFilter_checkbox.Location = new System.Drawing.Point(0, (20 * index));
                propertiesFilter_checkbox.UseVisualStyleBackColor = true;
                propertiesFilter_checkbox.Visible = setVisible;
                propertiesFilter_checkbox.Checked = true;
                threeDView.PropertiesFilter[pair.Key] = propertiesFilter_checkbox.Checked;
                propertiesFilter_checkbox.CheckedChanged += new System.EventHandler((s, ev) => {
                    // Define filtering behaviour
                    threeDView.TogglePropertiesFilter(pair.Key, propertiesFilter_checkbox.Checked);
                });
                this.PropertiesFilter_panel.Controls.Add(propertiesFilter_checkbox);
                index += 1;
                if (index >= propertiesPerPage) {
                    index = 0;
                    setVisible = false;
                }
                //System.Console.WriteLine(prop + pair.Value);
                prop += 1;
            }
            //System.Console.WriteLine(this.PropertiesFilter_panel.Controls.Count);

            int extraControlsInList = 3;
            int getMaxPages() {
                return (int)System.Math.Ceiling((PropertiesFilter_panel.Controls.Count - extraControlsInList) / (double)propertiesPerPage);
            }
            string getCurrentPageText(int pMaxPages) {
                return (propertiesFilterPage + 1) + " / " + pMaxPages;
            }

            Label currentPage = new Label();
            currentPage.Anchor = AnchorStyles.Top;
            currentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            currentPage.Margin = new System.Windows.Forms.Padding(0);
            currentPage.Location = new System.Drawing.Point((PropertiesFilter_panel.Size.Width / 2) - 20, (20 * 10));
            currentPage.Size = new System.Drawing.Size(40, 20);
            currentPage.Visible = true;
            currentPage.Text = 1 + "/" + getMaxPages();
            this.PropertiesFilter_panel.Controls.Add(currentPage);


            Button previous = new Button();
            previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            previous.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            previous.Margin = new System.Windows.Forms.Padding(0);
            previous.Location = new System.Drawing.Point(10, (20 * 10));
            previous.Size = new System.Drawing.Size(20, 20);
            previous.Visible = true;
            previous.Text = "<";
            previous.MouseClick += new MouseEventHandler((s, e) => {
                int maxPages = getMaxPages();
                if (propertiesFilterPage > 0) {
                    propertiesFilterPage -= 1;
                    for (int i = 0; i < PropertiesFilter_panel.Controls.Count - extraControlsInList; i++) {
                        PropertiesFilter_panel.Controls[i].Visible = (i >= (propertiesFilterPage * propertiesPerPage) && i < (1 + propertiesFilterPage) * propertiesPerPage);
                    }
                }
                currentPage.Text = getCurrentPageText(maxPages);
            });
            this.PropertiesFilter_panel.Controls.Add(previous);
            
            Button next = new Button();
            next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            previous.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            next.Margin = new System.Windows.Forms.Padding(0);
            next.Location = new System.Drawing.Point(PropertiesFilter_panel.Size.Width - 30, (20 * 10));
            next.Size = new System.Drawing.Size(20, 20);
            next.Visible = true;
            next.Text = ">";
            next.MouseClick += new MouseEventHandler((s, e) => {
                int maxPages = getMaxPages();
                if (propertiesFilterPage < (maxPages - 1)) {
                    propertiesFilterPage += 1;
                    for (int i = 0; i < PropertiesFilter_panel.Controls.Count - extraControlsInList; i++) {
                        PropertiesFilter_panel.Controls[i].Visible = (i >= (propertiesFilterPage * propertiesPerPage) && i < (1 + propertiesFilterPage) * propertiesPerPage);
                    }
                }
                currentPage.Text = getCurrentPageText(maxPages);
            });
            this.PropertiesFilter_panel.Controls.Add(next);
            PropertiesFilter_panel.Size = new System.Drawing.Size(PropertiesFilter_panel.Size.Width, propertiesPerPage * 10);
        }

        private PictureBox Visibility_pictureBox;
        private PictureBox MakeRoot_button;
        private PictureBox AddFrom_button;
        private PictureBox AddTo_button;
        private PictureBox RemoveFrom_button;
        private PictureBox RemoveTo_button;
        private PictureBox OpenFile_button;
        private Panel GPU_panel;
        private Label GLSLVersion_text;
        private Label GLVersion_text;
        private Label GLRenderer_text;
        private Label GLVendor_text;
        private PictureBox OpenDetails_button;
        private Panel Details_panel;
        private Panel PropertiesFilter_panel;
        private PictureBox ExportToExcel_button;
        private PictureBox DefaultView_menuItem;
        private Panel MenuStrip;
        private PictureBox Filter_menuItem;
        private PictureBox Logout_menuItem;
        private PictureBox Properties_menuItem;
        private PictureBox UpdateProperties_menuItem;
        private PictureBox GPU_menuItem;
        private PictureBox Background_menuItem;
    }
    public class DrawingSurface : Control {
        protected override void OnPaint(PaintEventArgs pEvent) {
            // don't call base.OnPaint(e) to prevent forground painting
            // base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pEvent) {
            // don't call base.OnPaintBackground(e) to prevent background painting
            //base.OnPaintBackground(pevent);
        }
    }

}