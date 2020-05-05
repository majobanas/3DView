using System.Windows.Forms;
using System.Collections.Generic;

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
            this.CreationFilter_label = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Filter_menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile_button = new System.Windows.Forms.PictureBox();
            this.RemoveTo_button = new System.Windows.Forms.PictureBox();
            this.RemoveFrom_button = new System.Windows.Forms.PictureBox();
            this.AddTo_button = new System.Windows.Forms.PictureBox();
            this.AddFrom_button = new System.Windows.Forms.PictureBox();
            this.MakeRoot_button = new System.Windows.Forms.PictureBox();
            this.DrawingSurface = new ThreeDViewCS.DrawingSurface();
            this.CreationFilter_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visibility_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Creation_pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenFile_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveTo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveFrom_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFrom_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MakeRoot_button)).BeginInit();
            this.SuspendLayout();
            // 
            // CreationFilter_panel
            // 
            this.CreationFilter_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreationFilter_panel.Controls.Add(this.Visibility_pictureBox);
            this.CreationFilter_panel.Controls.Add(this.Creation_pictureBox);
            this.CreationFilter_panel.Controls.Add(this.CreationFilter_label);
            this.CreationFilter_panel.Location = new System.Drawing.Point(1120, 24);
            this.CreationFilter_panel.Margin = new System.Windows.Forms.Padding(0);
            this.CreationFilter_panel.Name = "CreationFilter_panel";
            this.CreationFilter_panel.Size = new System.Drawing.Size(144, 267);
            this.CreationFilter_panel.TabIndex = 16;
            this.CreationFilter_panel.Visible = false;
            // 
            // Visibility_pictureBox
            // 
            this.Visibility_pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Creation_pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Creation_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("Creation_pictureBox.Image")));
            this.Creation_pictureBox.Location = new System.Drawing.Point(18, 0);
            this.Creation_pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.Creation_pictureBox.Name = "Creation_pictureBox";
            this.Creation_pictureBox.Size = new System.Drawing.Size(16, 16);
            this.Creation_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Creation_pictureBox.TabIndex = 1;
            this.Creation_pictureBox.TabStop = false;
            // 
            // CreationFilter_label
            // 
            this.CreationFilter_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreationFilter_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreationFilter_label.Location = new System.Drawing.Point(0, 0);
            this.CreationFilter_label.Margin = new System.Windows.Forms.Padding(0);
            this.CreationFilter_label.Name = "CreationFilter_label";
            this.CreationFilter_label.Size = new System.Drawing.Size(144, 13);
            this.CreationFilter_label.TabIndex = 0;
            this.CreationFilter_label.Text = "Filter";
            this.CreationFilter_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Filter_menuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "Menu strip";
            // 
            // Filter_menuItem
            // 
            this.Filter_menuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Filter_menuItem.Name = "Filter_menuItem";
            this.Filter_menuItem.Size = new System.Drawing.Size(45, 20);
            this.Filter_menuItem.Text = "Filter";
            this.Filter_menuItem.Click += new System.EventHandler(this.Filter_menuItem_click);
            // 
            // OpenFile_button
            // 
            this.OpenFile_button.BackColor = System.Drawing.Color.Black;
            this.OpenFile_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenFile_button.BackgroundImage")));
            this.OpenFile_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenFile_button.Location = new System.Drawing.Point(801, 88);
            this.OpenFile_button.Margin = new System.Windows.Forms.Padding(0);
            this.OpenFile_button.Name = "OpenFile_button";
            this.OpenFile_button.Size = new System.Drawing.Size(64, 64);
            this.OpenFile_button.TabIndex = 6;
            this.OpenFile_button.TabStop = false;
            this.OpenFile_button.Visible = false;
            this.OpenFile_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenFile_button_MouseUp);
            // 
            // RemoveTo_button
            // 
            this.RemoveTo_button.BackColor = System.Drawing.Color.Black;
            this.RemoveTo_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveTo_button.BackgroundImage")));
            this.RemoveTo_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RemoveTo_button.Location = new System.Drawing.Point(929, 88);
            this.RemoveTo_button.Margin = new System.Windows.Forms.Padding(0);
            this.RemoveTo_button.Name = "RemoveTo_button";
            this.RemoveTo_button.Size = new System.Drawing.Size(64, 64);
            this.RemoveTo_button.TabIndex = 5;
            this.RemoveTo_button.TabStop = false;
            this.RemoveTo_button.Visible = false;
            this.RemoveTo_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RemoveTo_button_MouseUp);
            // 
            // RemoveFrom_button
            // 
            this.RemoveFrom_button.BackColor = System.Drawing.Color.Black;
            this.RemoveFrom_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveFrom_button.BackgroundImage")));
            this.RemoveFrom_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RemoveFrom_button.Location = new System.Drawing.Point(929, 24);
            this.RemoveFrom_button.Margin = new System.Windows.Forms.Padding(0);
            this.RemoveFrom_button.Name = "RemoveFrom_button";
            this.RemoveFrom_button.Size = new System.Drawing.Size(64, 64);
            this.RemoveFrom_button.TabIndex = 4;
            this.RemoveFrom_button.TabStop = false;
            this.RemoveFrom_button.Visible = false;
            this.RemoveFrom_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RemoveFrom_button_MouseUp);
            // 
            // AddTo_button
            // 
            this.AddTo_button.BackColor = System.Drawing.Color.Black;
            this.AddTo_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddTo_button.BackgroundImage")));
            this.AddTo_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddTo_button.Location = new System.Drawing.Point(865, 88);
            this.AddTo_button.Margin = new System.Windows.Forms.Padding(0);
            this.AddTo_button.Name = "AddTo_button";
            this.AddTo_button.Size = new System.Drawing.Size(64, 64);
            this.AddTo_button.TabIndex = 3;
            this.AddTo_button.TabStop = false;
            this.AddTo_button.Visible = false;
            this.AddTo_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddTo_button_MouseUp);
            // 
            // AddFrom_button
            // 
            this.AddFrom_button.BackColor = System.Drawing.Color.Black;
            this.AddFrom_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddFrom_button.BackgroundImage")));
            this.AddFrom_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddFrom_button.Location = new System.Drawing.Point(865, 24);
            this.AddFrom_button.Margin = new System.Windows.Forms.Padding(0);
            this.AddFrom_button.Name = "AddFrom_button";
            this.AddFrom_button.Size = new System.Drawing.Size(64, 64);
            this.AddFrom_button.TabIndex = 2;
            this.AddFrom_button.TabStop = false;
            this.AddFrom_button.Visible = false;
            this.AddFrom_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddFrom_button_MouseUp);
            // 
            // MakeRoot_button
            // 
            this.MakeRoot_button.BackColor = System.Drawing.Color.Black;
            this.MakeRoot_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MakeRoot_button.BackgroundImage")));
            this.MakeRoot_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MakeRoot_button.Location = new System.Drawing.Point(801, 24);
            this.MakeRoot_button.Margin = new System.Windows.Forms.Padding(0);
            this.MakeRoot_button.Name = "MakeRoot_button";
            this.MakeRoot_button.Size = new System.Drawing.Size(64, 64);
            this.MakeRoot_button.TabIndex = 1;
            this.MakeRoot_button.TabStop = false;
            this.MakeRoot_button.Visible = false;
            this.MakeRoot_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeRoot_button_MouseUp);
            // 
            // DrawingSurface
            // 
            this.DrawingSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingSurface.Location = new System.Drawing.Point(0, 24);
            this.DrawingSurface.Margin = new System.Windows.Forms.Padding(0);
            this.DrawingSurface.Name = "DrawingSurface";
            this.DrawingSurface.Size = new System.Drawing.Size(1264, 657);
            this.DrawingSurface.TabIndex = 0;
            this.DrawingSurface.Text = "drawingSurface";
            this.DrawingSurface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_Click);
            this.DrawingSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_Down);
            this.DrawingSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_Up);
            // 
            // ThreeDViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.AddFrom_button);
            this.Controls.Add(this.OpenFile_button);
            this.Controls.Add(this.MakeRoot_button);
            this.Controls.Add(this.AddTo_button);
            this.Controls.Add(this.RemoveTo_button);
            this.Controls.Add(this.CreationFilter_panel);
            this.Controls.Add(this.RemoveFrom_button);
            this.Controls.Add(this.DrawingSurface);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "3DView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3DView";
            this.CreationFilter_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Visibility_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Creation_pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenFile_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveTo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemoveFrom_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFrom_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MakeRoot_button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DrawingSurface DrawingSurface;
        private Label CreationFilter_label;
        private Panel CreationFilter_panel;
        private PictureBox Creation_pictureBox;

        public void AddCreationFilterCheckBoxes() {
            int index = 0;
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                // 
                // CreationFilter_radiobutton
                //
                CheckBox CreationFilter_checkBox = new CheckBox();
                CreationFilter_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                CreationFilter_checkBox.Location = new System.Drawing.Point(20, CreationFilter_label.Size.Height + (20*index));
                CreationFilter_checkBox.Margin = new System.Windows.Forms.Padding(0);
                CreationFilter_checkBox.Name = pair.Value + "_checkBox";
                CreationFilter_checkBox.Size = new System.Drawing.Size(200, 20);
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
                this.CreationFilter_panel.Controls.Add(CreationFilter_checkBox);
                index += 1;
            }

            this.CreationFilter_panel.Size = new System.Drawing.Size(this.CreationFilter_panel.Width, CreationFilter_label.Size.Height + (20 * index));
        }

        //private Label VisibilityFilter_label;
        //private Panel VisibilityFilter_panel;

        public void AddVisibilityFilterCheckBoxes() {
            int index = 0;
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                // 
                // VisibilityFilter_radiobutton
                //
                CheckBox VisibilityFilter_checkBox = new CheckBox();
                VisibilityFilter_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                VisibilityFilter_checkBox.Location = new System.Drawing.Point(0, CreationFilter_label.Size.Height + (20 * index));
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
                this.CreationFilter_panel.Controls.Add(VisibilityFilter_checkBox);
                index += 1;
            }

            //this.VisibilityFilter_panel.Size = new System.Drawing.Size(this.VisibilityFilter_panel.Width, VisibilityFilter_label.Size.Height + (20 * index));
        }

        private PictureBox Visibility_pictureBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem Filter_menuItem;
        private PictureBox MakeRoot_button;
        private PictureBox AddFrom_button;
        private PictureBox AddTo_button;
        private PictureBox RemoveFrom_button;
        private PictureBox RemoveTo_button;
        private PictureBox OpenFile_button;
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