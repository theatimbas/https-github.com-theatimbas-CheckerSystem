using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class LogIn : Form
    {
        public string LoggedInUsername { get; private set; } = "";
        private readonly LibraryDataService libraryService;
        public LogIn(LibraryDataService libraryService)
        {
            InitializeComponent();
            this.libraryService = libraryService;
        }
        private void LogIn_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (libraryService.Login(username, password))
            {
                LoggedInUsername = username;
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear(); 
                txtPassword.Focus();
            }
        }
    }
}
