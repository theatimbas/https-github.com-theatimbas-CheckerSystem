using System;
using System.Windows.Forms;
using ELibraryDataLogic;

namespace PenFinderUI
{
    public partial class GenresBooks : Form
    {
        private readonly LibraryDataService libraryService;
        private readonly string username;
        public GenresBooks(string username, LibraryDataService sharedService)
        {
            InitializeComponent();
            this.username = username;
            this.libraryService = sharedService;
        }
        private void GenresBooks_Load(object sender, EventArgs e)
        {
            txtGenre.Focus();
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            string genre = txtGenre.Text.Trim();

            if (string.IsNullOrWhiteSpace(genre))
            {
                MessageBox.Show("Please enter a genre.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBooks.Items.Clear();
            var books = libraryService.GetBooksByGenre(genre);

            if (books == null || books.Count == 0)
            {
                listBooks.Items.Add("No books found in this genre.");
                return;
            }

            foreach (var book in books)
                listBooks.Items.Add(book);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string genre = txtGenre.Text.Trim();
            string bookTitle = txtAdd.Text.Trim();

            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(bookTitle))
            {
                MessageBox.Show("Please enter both a genre and a book title.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var favorites = libraryService.GetFavorites(username);
            if (favorites.Exists(b => b.Equals(bookTitle, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("This book is already in your favorites.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool added = libraryService.AddFavorite(username, bookTitle);
            if (added)
            {
                MessageBox.Show("Book successfully added to favorites!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var favForm = new Favorites(username, libraryService);
                favForm.ShowDialog();
                txtAdd.Clear();
                txtGenre.Clear();
                listBooks.Items.Clear();
                txtGenre.Focus();
            }
            else
            {
                MessageBox.Show("Failed to add book to favorites.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
