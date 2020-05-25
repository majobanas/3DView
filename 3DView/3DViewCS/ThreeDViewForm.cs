using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using MFiles.MFWS.Structs;

namespace ThreeDViewCS {
    public partial class ThreeDViewForm : Form {
        // 3DView
        public ThreeDView threeDView = null;
        // Quick action
        private bool quickActionVisible = false;
        private Control[] quickActionButtons;
        private bool openDetailsRotation = false;
        // Properties filter
        private int propertiesFilterPage = 0;
        // Properties / Details page
        private List<KeyValuePair<int, TextBox>> updateProperty = new List<KeyValuePair<int, TextBox>>();

        public ThreeDViewForm() {
            InitializeComponent();
            quickActionButtons = new[] { MakeRoot_button, AddFrom_button, AddTo_button, OpenFile_button, RemoveFrom_button, RemoveTo_button, ExportToExcel_button };

            threeDView = new ThreeDView(Parser.applicationPath);
            this.Show();
            this.Deactivate += new EventHandler((s, e) => { threeDView.IsInFocus = false; });
            this.Activated += new EventHandler((s, e) => { threeDView.IsInFocus = true; });
            OpenLoginDialog();
        }

        private void Logout() {
            ToggleUIVisibility(false);
            // Clean the login.txt
            string path = Parser.applicationPath + "assets/login.txt";
            File.WriteAllText(path, Convert.ToBase64String(Encoding.ASCII.GetBytes("")));

            Application.Exit();

        }

        private void logout_menuItem_Click(object sender, EventArgs e) {
            Logout();
        }

        private void OpenLoginDialog(bool pAskForNewCredentials = false) {
            string path = Parser.applicationPath + "assets/login.txt";
            if (!pAskForNewCredentials) {
                string[] loadedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(File.ReadAllText(path))).Split('\n');
                if (loadedCredentials.Length > 2) {
                    Login_button_Click(loadedCredentials[0], loadedCredentials[1], loadedCredentials[2]);
                    return;
                }
            }


            Form loginDialog = new Form();
            loginDialog.Owner = this;
            loginDialog.Text = "Login";
            loginDialog.MaximizeBox = false;
            loginDialog.MinimizeBox = false;
            loginDialog.ControlBox = false;
            loginDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            loginDialog.Size = new System.Drawing.Size(260, 230);
            loginDialog.StartPosition = FormStartPosition.CenterScreen;
            // ------------URL------------------------
            Label URLTitle = new Label();
            loginDialog.Controls.Add(URLTitle);
            URLTitle.Location = new System.Drawing.Point(20, 5);
            URLTitle.Size = new System.Drawing.Size(200, 12);
            URLTitle.Text = "URL";

            TextBox URL = new TextBox();
            loginDialog.Controls.Add(URL);
            URL.Location = new System.Drawing.Point(20, 22);
            URL.Size = new System.Drawing.Size(200, 20);
            URL.Text = "http://185.2.41.77/REST/";
            // -------------Username-----------------------
            Label usernameTitle = new Label();
            loginDialog.Controls.Add(usernameTitle);
            usernameTitle.Location = new System.Drawing.Point(20, 47);
            usernameTitle.Size = new System.Drawing.Size(200, 12);
            usernameTitle.Text = "Username";

            TextBox username = new TextBox();
            loginDialog.Controls.Add(username);
            username.Location = new System.Drawing.Point(20, 64);
            username.Size = new System.Drawing.Size(200, 30);
            username.Text = "";
            // ----------Password---------------------
            Label passwordTitle = new Label();
            loginDialog.Controls.Add(passwordTitle);
            passwordTitle.Location = new System.Drawing.Point(20, 89);
            passwordTitle.Size = new System.Drawing.Size(200, 12);
            passwordTitle.Text = "Password";

            TextBox password = new TextBox();
            loginDialog.Controls.Add(password);
            password.Location = new System.Drawing.Point(20, 106);
            password.Size = new System.Drawing.Size(200, 30);
            password.UseSystemPasswordChar = true;
            password.Text = "";
            // --------------Remember---------------------
            CheckBox rememberCredentials = new CheckBox();
            loginDialog.Controls.Add(rememberCredentials);
            rememberCredentials.Location = new System.Drawing.Point(20, 128);
            rememberCredentials.Size = new System.Drawing.Size(200, 20);
            rememberCredentials.Text = "Remember my credentials";
            rememberCredentials.Checked = true;
            // ---------------------------------------

            Button login = new Button();
            loginDialog.Controls.Add(login);
            login.Location = new System.Drawing.Point(20, 150);
            login.Size = new System.Drawing.Size(80, 20);
            login.Text = "Login";
            login.Click += new EventHandler((s, e) => {
                loginDialog.Hide();
                Login_button_Click(URL.Text, username.Text, password.Text);
                if (rememberCredentials.Checked) {
                    SaveLoginCredentials(path, URL.Text, username.Text, password.Text);
                }
                loginDialog.Close();
            });
            // ---------------------------------------

            Button cancel = new Button();
            loginDialog.Controls.Add(cancel);
            cancel.Location = new System.Drawing.Point(140, 150);
            cancel.Size = new System.Drawing.Size(80, 20);
            cancel.Text = "Cancel";
            cancel.Click += new EventHandler((s, e) => {
                loginDialog.Close();
                Application.Exit();
            });


            loginDialog.Show();
        }

        private void SaveLoginCredentials(string pPath, string pURL, string pUsername, string pPassword) {
            string credentials = "";
            credentials += pURL + "\n";
            credentials += pUsername + "\n";
            credentials += pPassword;
            File.WriteAllText(pPath, Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials)));
        }

        private void Login_button_Click(string pURL, string pUsername, string pPassword) {
            if (!MFiles.LoggedIn) {
                MFiles.Login(pURL, pUsername, pPassword, Parser.vaultGUID);
                this.Text = "3DView    [ " + MFiles.VaultName + " ]    [ " + MFiles.ClientName + " ]";

                if (MFiles.LoggedIn) {
                    // Object types
                    MFiles.ReadTypes();
                    MFiles.ReadTypeIcons();
                    threeDView.InitializeCreationFilterTypes();
                    AddCreationFilterCheckBoxes();
                    threeDView.InitializeVisibilityFilterTypes();
                    AddVisibilityFilterCheckBoxes();
                    // Properties
                    MFiles.ReadProperties();
                    threeDView.InitializePropertiesFilter();
                    AddPropertiesFilterCheckBoxes();
                    // C++
                    threeDView.InitializeView(DrawingSurface.Handle);
                    // GPU info
                    FillGPUPanel();
                    // Enable picking objects in fullscreen mode
                    this.WindowState = FormWindowState.Normal;
                    this.CenterToScreen();
                    //--------------------------------------------------
                    threeDView.LoadEntryObjects(Parser.entryObjects);
                    ToggleUIVisibility(true);
                } else {
                    OpenLoginDialog(true);
                }
            }
        }

        private void ToggleUIVisibility(bool pBool) {
            MenuStrip.Visible = pBool;
            DrawingSurface.Visible = pBool;
            this.MaximizeBox = pBool;
            //this.MinimizeBox = pBool;
        }


        private void DrawingSurface_Down(object sender, MouseEventArgs pEvent) {
            if (threeDView.Initialized) {
                if (pEvent.Button == MouseButtons.Right) {
                    if (threeDView.SelectedObject != null) {
                        ShowQuickAction(threeDView.IsRootSelected(), MFiles.HasFiles(threeDView.SelectedObject[0]));
                    }
                }
                PropertiesFilter_panel.Visible = false;
                GPU_panel.Visible = false;
                CreationFilter_panel.Visible = false;
            }
        }

        private void DrawingSurface_Up(object sender, MouseEventArgs pEvent) {
            if (threeDView.Initialized) {
                updateProperty.Clear();
                if (pEvent.Button == MouseButtons.Right) {

                    if (threeDView.SelectedObject != null) {
                        // Empty
                    }
                }
                if (pEvent.Button == MouseButtons.Left) {
                    if (!quickActionVisible) {
                        threeDView.SelectedObject = threeDView.SelectObject();
                        if (threeDView.SelectedObject != null) {
                            Debug.Log("C# ThreeDViewForm.DrawingSurface_click: " + threeDView.SelectedObject.ToString());
                        }
                    }
                    HideQuickAction();
                }
            }
        }


        private void MakeRoot_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.MakeRoot();
                    HideQuickAction();
                }
            }
        }
        private void AddFrom_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.AddFrom(threeDView.SelectedObject[0]);
                    HideQuickAction();
                }
            }
        }
        private void AddTo_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.AddTo(threeDView.SelectedObject[0]);
                    HideQuickAction();
                }
            }

        }
        private void RemoveFrom_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.RemoveFrom();
                    HideQuickAction();
                }
            }
        }
        private void RemoveTo_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.RemoveTo();
                    HideQuickAction();
                }
            }
        }

        private void OpenFile_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    OpenFile();
                    HideQuickAction();
                }
            }

        }

        // -------------------------------------------------------------- Complex functions

        private void ShowQuickAction(bool pIsRootSelected, bool pHasFiles) {
            if (threeDView.Initialized) {
                if (!quickActionVisible) {
                    quickActionVisible = true;
                    foreach (Control control in quickActionButtons) {
                        control.Visible = true;
                    }
                    System.Drawing.Point cursor = PointToClient(Cursor.Position);
                    System.Drawing.Size size =
                        new System.Drawing.Size(
                            quickActionButtons[0].Size.Width * quickActionButtons.Length / 2,
                            quickActionButtons[0].Size.Height * 2);
                    // Calculate offset from window border
                    int xOffset = 0;
                    int yOffset = 0;
                    if (cursor.X + size.Width > this.DrawingSurface.Size.Width) {
                        xOffset = -size.Width;
                        // Reduce offset if buttons arent going to be visible
                        //if (pIsRootSelected && !pHasFiles) {
                        //    xOffset += quickActionButtons[0].Size.Width;
                        //}
                    }
                    if (cursor.Y > this.DrawingSurface.Size.Height / 2)
                    //if (cursor.Y + size.Height > this.DrawingSurface.Size.Height)
                        yOffset = -size.Height;
                    // Set all buttons to visible
                    foreach (Control control in quickActionButtons) {
                        control.Visible = true;
                    }
                    OpenDetails_button.Visible = true;
                    // Set MakeRoot button visibility
                    int firstRowOffset = 0;
                    if (pIsRootSelected) {
                        quickActionButtons[0].Visible = false;
                        firstRowOffset = quickActionButtons[0].Size.Width;
                    }
                    // Set first row positions
                    quickActionButtons[0].Location = new System.Drawing.Point(cursor.X + xOffset, cursor.Y + yOffset);
                    quickActionButtons[1].Location = new System.Drawing.Point(quickActionButtons[0].Location.X + quickActionButtons[0].Size.Width - firstRowOffset, quickActionButtons[0].Location.Y);
                    quickActionButtons[2].Location = new System.Drawing.Point(quickActionButtons[1].Location.X + quickActionButtons[1].Size.Width, quickActionButtons[1].Location.Y);
                    quickActionButtons[3].Location = new System.Drawing.Point(quickActionButtons[0].Location.X, quickActionButtons[0].Location.Y + quickActionButtons[0].Size.Height);
                    // Set OpenFile button visibility
                    int secondRowOffset = 0;
                    if (!pHasFiles) {
                        quickActionButtons[3].Visible = false;
                        secondRowOffset = quickActionButtons[3].Size.Width;
                    }
                    // Set second row positions
                    quickActionButtons[4].Location = new System.Drawing.Point(quickActionButtons[3].Location.X + quickActionButtons[3].Size.Width - secondRowOffset, quickActionButtons[3].Location.Y);
                    quickActionButtons[5].Location = new System.Drawing.Point(quickActionButtons[4].Location.X + quickActionButtons[4].Size.Width, quickActionButtons[4].Location.Y);
                    // ExportToExcel
                    quickActionButtons[6].Location = new System.Drawing.Point(quickActionButtons[5].Location.X + quickActionButtons[5].Size.Width, quickActionButtons[5].Location.Y);
                    quickActionButtons[6].Visible = false;
                    // Set details position

                    if (yOffset >= 0) {
                        OpenDetails_button.Location = new System.Drawing.Point(quickActionButtons[4].Location.X, quickActionButtons[4].Location.Y + quickActionButtons[4].Size.Height);
                        if (openDetailsRotation) {
                            OpenDetails_button.BackgroundImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                            openDetailsRotation = false;
                        }
                    } else {
                        OpenDetails_button.Location = new System.Drawing.Point(quickActionButtons[1].Location.X, quickActionButtons[1].Location.Y - OpenDetails_button.Size.Height);
                        if (!openDetailsRotation) {
                            OpenDetails_button.BackgroundImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                            openDetailsRotation = true;
                        }
                    }
                } else {
                    HideQuickAction();
                }
            }
        }

        private void HideQuickAction() {
            if (threeDView.Initialized) { 
                foreach (Control control in quickActionButtons) {
                    control.Visible = false;
                }
                OpenDetails_button.Visible = false;
                UpdateProperties_menuItem.Visible = false;
                Details_panel.Visible = false;
                //
                quickActionVisible = false;
            }
        }

        private void OpenFile() {
            ObjectFile[] files = MFiles.GetFiles(threeDView.SelectedObject[0]);
            if (files.Length == 0) return;

            string filePath = Parser.applicationPath + "downloads/" + files[0].Name + "." + files[0].Extension;

            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath,
                Parser.StreamToBytes(
                    MFiles.GetFileContent(threeDView.SelectedObject[0], files[0].ID)));
            // Write / Read dialog
            bool writable = DialogResult.Yes == MessageBox.Show(
                this,
                "Do you want to edit the file?\n\n" +
                "Yes - Writable\n" + 
                "No - Read-only",
                "Select opening mode, please.",
                MessageBoxButtons.YesNo);

            if (writable) {
                MFCheckOutStatus checkOutStatus = MFiles.GetCheckOutStatus(threeDView.SelectedObject[0]);
                if (checkOutStatus != MFCheckOutStatus.CheckedOut) {
                    threeDView.SelectedObject[0] = MFiles.SetCheckOutStatus(threeDView.SelectedObject[0], MFCheckOutStatus.CheckedOutToMe);
                    ObjectVersion objectVersion = MFiles.GetObjectVersion(threeDView.SelectedObject[0]);
                    //Console.WriteLine("Checked out user name: " + objectVersion.CheckedOutToUserName);
                    //Console.WriteLine("Checked out to: " + objectVersion.CheckedOutTo);
                    //Console.WriteLine("Checked out: " + objectVersion.ObjectCheckedOut);
                    //Console.WriteLine("Checked out to this user: " + objectVersion.ObjectCheckedOutToThisUser);
                    //Console.WriteLine("--After checkout: " + MFiles.GetCheckOutStatus(threeDView.SelectedObject[0]));
                } else {
                    writable = false;
                }
            }

            Processes.Start(filePath, writable, 
                // Writable Exited Callback
                (s, e) => {
                    try {
                        // Upload changes? dialog
                        if (DialogResult.Yes == MessageBox.Show(
                            "Are you sure you want to upload your changes?",
                            "",
                            MessageBoxButtons.YesNo)) {
                                threeDView.SelectedObject[0] = MFiles.UploadFileContent(threeDView.SelectedObject[0], files[0].ID, Path.GetFullPath(filePath));
                                MFiles.SetCheckOutStatus(threeDView.SelectedObject[0], MFCheckOutStatus.CheckedIn);
                        }
                    } catch (Exception pException) {
                        Console.WriteLine("WrtableExitedCallback threw CATCH\n" + pException.ToString());
                    }
                    //Console.WriteLine("After checkin: " + MFiles.GetCheckOutStatus(threeDView.SelectedObject));               
                }
            );
        }
       
        private void ExportToExcel_button_Click(object sender, EventArgs e) {
            List<List<ExcelExporter.Properties>> properties = new List<List<ExcelExporter.Properties>>();
            foreach (MFilesObject mFilesObject in threeDView.SelectedObject) {
                properties.Add(new List<ExcelExporter.Properties>());
                PropertyValue[] propertyValues = MFiles.GetProperties(mFilesObject);
                foreach (PropertyValue propertyValue in propertyValues) {
                    if (MFiles.Properties[propertyValue.PropertyDef] != "" &&
                        threeDView.PropertiesFilter[propertyValue.PropertyDef] &&
                        propertyValue.TypedValue.DisplayValue != "") {
                        properties[properties.Count - 1].Add(new ExcelExporter.Properties(MFiles.Properties[propertyValue.PropertyDef], propertyValue.TypedValue.DisplayValue));
                    }
                }
            }
            string fullPath = Path.GetFullPath(Parser.applicationPath + "downloads/");
            //Console.WriteLine(fullPath);
            ExcelExporter.GenerateExcel(properties, fullPath, threeDView.SelectedObject);
            properties.Clear();


        }

        private void FillGPUPanel() {
            this.GLVendor_text.Text = threeDView.GetConfigString("gl_vendor");
            this.GLRenderer_text.Text = threeDView.GetConfigString("gl_renderer");
            this.GLVersion_text.Text = threeDView.GetConfigString("gl_version");
            this.GLSLVersion_text.Text = threeDView.GetConfigString("glsl_version");
        }

        private void OpenDetails_button_MouseHover(object sender, EventArgs e) {
            
            if (threeDView.SelectedObject != null) {
                UpdateProperties_menuItem.Visible = true;
                quickActionButtons[6].Visible = true;
                Details_panel.Controls.Clear();
                int index = 0;
                int yLocation = 0;
                int xLocation = 0;
                PropertyValue[] properties = MFiles.GetProperties(threeDView.SelectedObject[0]);
                foreach (PropertyValue property in properties) {
                    if (threeDView.PropertiesFilter[property.PropertyDef] == true) {
                        if (MFiles.Properties[property.PropertyDef] != "" && property.TypedValue.DisplayValue != "") {

                            Label propertyName = new Label();
                            propertyName.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            propertyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            propertyName.AutoSize = true;
                            propertyName.Location = new System.Drawing.Point(3, yLocation);
                            propertyName.Text = MFiles.Properties[property.PropertyDef];// + ":    " + property.TypedValue.DisplayValue;
                            propertyName.ForeColor = System.Drawing.Color.Gray;
                            propertyName.ResumeLayout(true);
                            propertyName.PerformLayout();
                            Details_panel.Controls.Add(propertyName);
                            Details_panel.ResumeLayout(true);
                            Details_panel.PerformLayout();

                            TextBox propertyValue = new TextBox();
                            propertyValue.ForeColor = System.Drawing.Color.Black;
                            propertyValue.BackColor = System.Drawing.Color.White;
                            if (property.TypedValue.DataType != MFDataType.Text) {
                                propertyValue.ReadOnly = true;
                                propertyValue.ForeColor = System.Drawing.Color.White;
                                propertyValue.BackColor = System.Drawing.Color.Black;
                            }
                            propertyValue.BorderStyle = BorderStyle.None;
                            propertyValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            propertyValue.AutoSize = false;
                            propertyValue.Size = new System.Drawing.Size(200, propertyName.Size.Height);
                            propertyValue.TextAlign = HorizontalAlignment.Center;
                            propertyValue.Text = property.TypedValue.DisplayValue;
                            propertyValue.Location = new System.Drawing.Point(propertyName.Size.Width, yLocation);

                            propertyValue.ResumeLayout(true);
                            propertyValue.PerformLayout();
                            Details_panel.Controls.Add(propertyValue);
                            Details_panel.ResumeLayout(true);
                            Details_panel.PerformLayout();
                            propertyValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                            propertyValue.Location = new System.Drawing.Point(Details_panel.Size.Width - propertyValue.Size.Width, yLocation);
                            propertyValue.TextChanged += (s, ev) => {
                                KeyValuePair<int, TextBox> kvp = new KeyValuePair<int, TextBox>(property.PropertyDef, propertyValue);
                                if (!updateProperty.Contains(kvp))
                                    updateProperty.Add(kvp);
                            };

                            yLocation = propertyValue.Location.Y + propertyValue.Size.Height;
                            index += 1;
                        }
                    }
                }
                OpenDetails_button.Visible = false;
                Details_panel.Visible = true;
                if (!openDetailsRotation) {
                    Details_panel.Location = new System.Drawing.Point(quickActionButtons[0].Location.X, OpenDetails_button.Location.Y);
                } else {
                    Details_panel.Location = new System.Drawing.Point(quickActionButtons[0].Location.X, quickActionButtons[0].Location.Y - Details_panel.Size.Height);
                }
                System.Drawing.Point mousePosition = this.PointToClient(Cursor.Position);
                if (DrawingSurface.Size.Width - mousePosition.X < Details_panel.Size.Width)
                    Details_panel.Location = new System.Drawing.Point(DrawingSurface.Size.Width - Details_panel.Size.Width, Details_panel.Location.Y);
            }
        }

        private void Filter_menuItem_click(object sender, EventArgs pEvent) {
            HideQuickAction();
            PropertiesFilter_panel.Visible = false;
            GPU_panel.Visible = false;
            CreationFilter_panel.Visible = !CreationFilter_panel.Visible;
            Debug.Log("Creation / Visibility Filter.Visible = :" + CreationFilter_panel.Visible);
        }

        private void Properties_menuItem_Click(object sender, EventArgs pEvent) {
            HideQuickAction();
            CreationFilter_panel.Visible = false;
            GPU_panel.Visible = false;
            PropertiesFilter_panel.Visible = !PropertiesFilter_panel.Visible;
        }

        private void GPU_menuItem_Click(object sender, EventArgs pEvent) {
            HideQuickAction();
            CreationFilter_panel.Visible = false;
            PropertiesFilter_panel.Visible = false;
            GPU_panel.Visible = !GPU_panel.Visible;
        }

        private void Background_menuItem_Click(object sender, EventArgs pEvent) {
            threeDView.CycleSkybox();
        }

        private void UpdatePropertiesToolStripMenuItem_Click(object sender, EventArgs pEvent) {
            string content = "";
            foreach (KeyValuePair<int, TextBox>  pair in updateProperty) {
                content += pair.Value.Parent.Controls[pair.Value.Parent.Controls.IndexOf(pair.Value) - 1].Text + "->" + pair.Value.Text + "\n\n";
            }
            if (content == "") {
                updateProperty.Clear();
                return;
            }
            
            if (DialogResult.Yes == MessageBox.Show(
                content,
                "Are you sure you want to perform the following changes?",
                MessageBoxButtons.YesNo)) {
                foreach (KeyValuePair<int, TextBox> pair in updateProperty) {
                    MFilesObject mFilesObject = MFiles.SetCheckOutStatus(threeDView.SelectedObject[0], MFCheckOutStatus.CheckedOutToMe);
                    PropertyValue prop = MFiles.GetProperty(mFilesObject, pair.Key);
                    prop.TypedValue.Value = pair.Value.Text;
                    MFiles.SetProperty(mFilesObject, pair.Key, prop);
                    threeDView.SelectedObject[0] = MFiles.SetCheckOutStatus(mFilesObject, MFCheckOutStatus.CheckedIn);
                }
            }
            updateProperty.Clear();
        }

        private void MakeRoot_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(MakeRoot_button, "Make the selected object root of the scene");
        }

        private void OpenFile_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(OpenFile_button, "Open the file attached to this object");
        }

        private void ExportToExcel_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(ExportToExcel_button, "Summarize the selected objects into an Excel document");
        }

        private void AddFrom_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(AddFrom_button, "Show \"Relationships-From\" of the selected object");
        }

        private void AddTo_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(AddTo_button, "Show \"Relationships-To\" of the selected object");
        }

        private void RemoveFrom_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(RemoveFrom_button, "Hide \"Relationships-From\" of the selected object");
        }

        private void RemoveTo_button_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(RemoveTo_button, "Hide \"Relationships-To\" of the selected object");
        }

        private void DefaultView_button_Click(object sender, EventArgs e) {
            threeDView.SetDefaultView();
        }

        private void Filter_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(Filter_menuItem, "Object filtering options");
        }

        private void Properties_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(Properties_menuItem, "Metadata filtering options");
        }

        private void UpdateProperties_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(UpdateProperties_menuItem, "Upload metadata changes");
        }

        private void DefaultView_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(DefaultView_menuItem, "Move camera to default orientation");
        }

        private void Background_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(Background_menuItem, "Change background");
        }

        private void GPU_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(GPU_menuItem, "Show GPU information");
        }

        private void Logout_menuItem_MouseHover(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(Logout_menuItem, "Logout from account");
        }
    }
}
