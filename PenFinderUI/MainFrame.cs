using System;
using System.Windows.Forms;
using ELibraryDataLogic;
using PFinderCommon;

namespace PenFinderUI
{
    public partial class MainFrame : Form
    {
        private readonly IFinderDataService dataService = new PenFinderDB();
        private readonly LibraryDataService libraryService;

        public MainFrame()
        {
            InitializeComponent();
            libraryService = new LibraryDataService(dataService); 
        }
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            using var createForm = new CreateAccount(libraryService);

            var result = createForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            using var loginForm = new LogIn(libraryService);

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Hide();
                using var dashboard = new Dashboard(loginForm.LoggedInUsername, libraryService);
                dashboard.ShowDialog();
                Show(); 
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
