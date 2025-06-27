using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class DeleteAccount : Form
    {
        private readonly string username;
        private readonly LibraryDataService service;
        public DeleteAccount(string username, LibraryDataService sharedService)
        {
            InitializeComponent();
            this.username = username;
            this.service = sharedService;
        }
        private void DeleteAccount_Load(object sender, EventArgs e)
        {
            lblWarning.Text = $"Are you sure you want to delete your account, {username}?";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "This action is irreversible. Proceed?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                service.DeleteAccountWithUsername(username);
                Hide();
                new MainFrame().Show();
                Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
