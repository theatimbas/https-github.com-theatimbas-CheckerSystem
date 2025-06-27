using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class Dashboard : Form
    {
        private readonly LibraryDataService libraryService;
        private readonly string currentUser;
        public Dashboard(string user, LibraryDataService sharedService)
        {
            InitializeComponent();
            currentUser = user;
            libraryService = sharedService;
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblgreeting.Text = $"Welcome, {currentUser}!";
        }
        private void btnBrowseGenres_Click(object sender, EventArgs e)
        {
            var genresForm = new GenresBooks(currentUser, libraryService);
            genresForm.ShowDialog();
        }
        private void btnFavorites_Click(object sender, EventArgs e)
        {
            var favForm = new Favorites(currentUser, libraryService);
            favForm.ShowDialog();
        }
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            var updateForm = new UpdatePassword(currentUser, libraryService);
            updateForm.ShowDialog();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteAccount(currentUser, libraryService);
            deleteForm.ShowDialog();
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Do you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question );

            if (confirm == DialogResult.Yes)
            {
                Hide(); 
                new MainFrame().Show(); 
            }
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
