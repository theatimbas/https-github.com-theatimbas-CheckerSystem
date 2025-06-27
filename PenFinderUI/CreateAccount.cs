using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class CreateAccount : Form
    {
        private readonly LibraryDataService libraryService;
        public CreateAccount(LibraryDataService libraryService)
        {
            InitializeComponent();
            this.libraryService = libraryService;
        }
        private void CreateAccount_Load(object sender, EventArgs e)
        {
            txtUsername.Focus(); 
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password are required.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool success = libraryService.RegisterAccount(username, password);

            if (success)
            {
                MessageBox.Show("Account created successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose another.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblPenFinder_Click(object sender, EventArgs e)
        {
            // No action needed — used for branding or decoration.
        }
    }
}
