using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class Favorites : Form
    {
        private readonly LibraryDataService libraryService;
        private readonly string username;
        public Favorites(string username, LibraryDataService sharedService)
        {
            InitializeComponent();
            this.username = username;
            this.libraryService = sharedService;
            Text = "Your Favorites";
        }
        private void Favorites_Load(object sender, EventArgs e)
        {
            LoadFavorites();
        }
        public void LoadFavorites()
        {
            listFavorites.Items.Clear();

            var favorites = libraryService.GetFavorites(username);

            if (favorites == null || favorites.Count == 0)
            {
                listFavorites.Items.Add("No favorites yet.");
                return;
            }

            foreach (var book in favorites)
                listFavorites.Items.Add(book);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string titleToRemove = txtRemove.Text.Trim();

            if (string.IsNullOrWhiteSpace(titleToRemove))
            {
                MessageBox.Show("Please type the book title to remove.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (titleToRemove.Equals("No favorites yet.", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid book title.");
                return;
            }

            bool success = libraryService.RemoveFavorite(username, titleToRemove);

            if (success)
            {
                MessageBox.Show("Book removed from favorites.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemove.Clear();
                LoadFavorites();
            }
            else
            {
                MessageBox.Show("Book not found in your favorites.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
