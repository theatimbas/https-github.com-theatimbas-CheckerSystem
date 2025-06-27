namespace PenFinderUI
{
    partial class GenresBooks
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblGenre = new Label();
            listGenres = new ListBox();
            listBooks = new ListBox();
            txtGenre = new TextBox();
            btnEnter = new Button();
            btnAdd = new Button();
            txtAdd = new TextBox();
            btnReturn = new Button();
            SuspendLayout();
            // 
            // lblGenre
            // 
            lblGenre.AutoSize = true;
            lblGenre.Font = new Font("Ravie", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGenre.Location = new Point(128, 13);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(357, 26);
            lblGenre.TabIndex = 0;
            lblGenre.Text = "Available Genres and Books";
            // 
            // listGenres
            // 
            listGenres.FormattingEnabled = true;
            listGenres.ItemHeight = 15;
            listGenres.Location = new Point(53, 57);
            listGenres.Name = "listGenres";
            listGenres.Size = new Size(201, 259);
            listGenres.TabIndex = 1;
            // 
            // listBooks
            // 
            listBooks.FormattingEnabled = true;
            listBooks.ItemHeight = 15;
            listBooks.Location = new Point(308, 57);
            listBooks.Name = "listBooks";
            listBooks.Size = new Size(210, 259);
            listBooks.TabIndex = 2;
            // 
            // txtGenre
            // 
            txtGenre.Location = new Point(110, 347);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(182, 23);
            txtGenre.TabIndex = 3;
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(344, 341);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(92, 32);
            btnEnter.TabIndex = 4;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(344, 393);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(92, 29);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtAdd
            // 
            txtAdd.Location = new Point(110, 393);
            txtAdd.Name = "txtAdd";
            txtAdd.Size = new Size(182, 23);
            txtAdd.TabIndex = 7;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(12, 436);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(92, 30);
            btnReturn.TabIndex = 8;
            btnReturn.Text = "Back";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // GenresBooks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.photo_2025_06_23_11_42_09;
            ClientSize = new Size(581, 475);
            Controls.Add(btnReturn);
            Controls.Add(txtAdd);
            Controls.Add(btnAdd);
            Controls.Add(btnEnter);
            Controls.Add(txtGenre);
            Controls.Add(listBooks);
            Controls.Add(listGenres);
            Controls.Add(lblGenre);
            Name = "GenresBooks";
            Text = "GenresBooks";
            Load += GenresBooks_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGenre;
        private ListBox listGenres;
        private ListBox listBooks;
        private TextBox txtGenre;
        private Button btnEnter;
        private Button btnAdd;
        private TextBox txtAdd;
        private Button btnReturn;
    }
}
