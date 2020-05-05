using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MFiles.MFWS.Structs;
using ThreeDViewCLR;

namespace ThreeDViewCS {
    public partial class ThreeDViewForm : Form {
        // 3DView
        public ThreeDView threeDView = null;

        private bool quickActionVisible = false;
        private Control[] quickActionButtons;

        public ThreeDViewForm() {
            InitializeComponent();
            quickActionButtons = new[] { MakeRoot_button, AddFrom_button, AddTo_button, OpenFile_button, RemoveFrom_button, RemoveTo_button };

            threeDView = new ThreeDView(Parser.applicationPath);
            this.Show();
            OpenLoginDialog();
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
            username.Text = "ADMIN";
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
            password.Text = "cxcxx";
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
                if (rememberCredentials.Checked) {
                    SaveLoginCredentials(path, URL.Text, username.Text, password.Text);
                }
                Login_button_Click(URL.Text, username.Text, password.Text);
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

                if (MFiles.LoggedIn) {
                    MFiles.ReadTypes();
                    MFiles.ReadTypeIcons();
                    threeDView.InitializeCreationFilterTypes();
                    AddCreationFilterCheckBoxes();
                    threeDView.InitializeVisibilityFilterTypes();
                    AddVisibilityFilterCheckBoxes();
                    threeDView.InitializeView(DrawingSurface.Handle);
                    /*threeDView.SetConfigInteger(
                        "load_models",
                        (Parser.vaultGUID == "{72B08B17-FE4E-4DDB-92D9-77624FA1FC6E}") ? 1 : 0);*/
                    threeDView.LoadEntryObjects(Parser.entryObjects);
                } else {
                    OpenLoginDialog(true);
                }
            }
        }

        private void DrawingSurface_Down(object sender, MouseEventArgs pEvent) {
            if (threeDView.Initialized) {
                if (pEvent.Button == MouseButtons.Right) {
                    if (threeDView.SelectedObject != null) {
                        ShowQuickAction(threeDView.IsRootSelected(), MFiles.HasFiles(threeDView.SelectedObject));
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

        private void DrawingSurface_Up(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Right) {
                if (threeDView.SelectedObject != null) {
                    // Empty
                }
            }
        }

        private void DrawingSurface_Click(object sender, MouseEventArgs pEvent) {
        }

        private void Filter_menuItem_click(object sender, EventArgs pEvent) {
            CreationFilter_panel.Visible = !CreationFilter_panel.Visible;
            Debug.Log("Creation / Visibility Filter.Visible = :" + CreationFilter_panel.Visible);
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
                    threeDView.AddFrom(threeDView.SelectedObject);
                    HideQuickAction();
                }
            }
        }
        private void AddTo_button_MouseUp(object sender, MouseEventArgs pEvent) {
            if (pEvent.Button == MouseButtons.Left) {
                if (threeDView.SelectedObject != null) {
                    threeDView.AddTo(threeDView.SelectedObject);
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
                    if (pIsRootSelected && !pHasFiles) {
                        xOffset += quickActionButtons[0].Size.Width;
                    }
                }
                if (cursor.Y + size.Height > this.DrawingSurface.Size.Height)
                    yOffset = -size.Height;foreach (Control control in quickActionButtons) {
                    control.Visible = true;
                }
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
                
            } else {
                HideQuickAction();
            }
        }

        private void HideQuickAction() {
            foreach (Control control in quickActionButtons) {
                control.Visible = false;
            }
            quickActionVisible = false;
        }

        private void OpenFile() {
            ObjectFile[] files = MFiles.GetFiles(threeDView.SelectedObject);
            if (files.Length == 0) return;

            string filePath = Parser.applicationPath + "downloads/" + files[0].Name + "." + files[0].Extension;

            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath,
                Parser.StreamToBytes(
                    MFiles.GetFileContent(threeDView.SelectedObject, files[0].ID)));

            DialogResult dialogResult = MessageBox.Show(
                "Do you want to edit the file?\n\nYes - Writable\nNo - Read-only",
                "Select opening mode, please.",
                MessageBoxButtons.YesNo);
            bool writable = dialogResult == DialogResult.Yes;

            if (writable) {
                MFCheckOutStatus checkOutStatus = MFiles.GetCheckOutStatus(threeDView.SelectedObject);
                if (checkOutStatus != MFCheckOutStatus.CheckedOut) {
                    MFiles.SetCheckOutStatus(threeDView.SelectedObject, MFCheckOutStatus.CheckedOutToMe);
                    ObjectVersion objectVersion = MFiles.GetObjectVersion(threeDView.SelectedObject);
                    Console.WriteLine("Checked out user name: " + objectVersion.CheckedOutToUserName);
                    Console.WriteLine("Checked out to: " + objectVersion.CheckedOutTo);
                    Console.WriteLine("Checked out: " + objectVersion.ObjectCheckedOut);
                    Console.WriteLine("Checked out to this user: " + objectVersion.ObjectCheckedOutToThisUser);
                    //Console.WriteLine("After checkout: " + MFiles.GetCheckOutStatus(threeDView.SelectedObject));
                } else {
                    writable = false;
                }
            }

            Processes.Start(filePath, writable, (o, e) => {
                try {
                    if (DialogResult.Yes == MessageBox.Show(
                            "Do you want to upload your changes?",
                            "",
                            MessageBoxButtons.YesNo)) {
                        MFiles.UploadFileContent(threeDView.SelectedObject, files[0].ID, Path.GetFullPath(filePath));
                    }
                } catch (Exception pException) {
                    Console.WriteLine(pException.ToString());
                }
                MFiles.SetCheckOutStatus(threeDView.SelectedObject, MFCheckOutStatus.CheckedIn);
                //Console.WriteLine("After checkin: " + MFiles.GetCheckOutStatus(threeDView.SelectedObject));
            });
        }
    }
}
