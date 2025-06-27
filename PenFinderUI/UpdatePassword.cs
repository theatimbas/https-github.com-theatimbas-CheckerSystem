using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class UpdatePassword : Form
    {
        private readonly string username;
        private readonly LibraryDataService service;
        public UpdatePassword(string username, LibraryDataService sharedService)
        {
            InitializeComponent();
            this.username = username;
            this.service = sharedService;
        }
        private void UpdatePassword_Load(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = true;
            txtNewPassword.Focus();
        }
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("New password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            service.UpdatePassword(username, newPassword);
            MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
