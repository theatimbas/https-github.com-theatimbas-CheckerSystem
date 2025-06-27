namespace PenFinderUI
{
    partial class Favorites
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panel1;
        private Label lblFavorites;
        private ListBox listFavorites;
        private TextBox txtRemove;
        private Button btnRemove;
        private Button btnReturn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnReturn = new Button();
            txtRemove = new TextBox();
            btnRemove = new Button();
            listFavorites = new ListBox();
            lblFavorites = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Plum;
            panel1.Controls.Add(btnReturn);
            panel1.Controls.Add(txtRemove);
            panel1.Controls.Add(btnRemove);
            panel1.Controls.Add(listFavorites);
            panel1.Controls.Add(lblFavorites);
            panel1.Location = new Point(6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(544, 440);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(326, 399);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(101, 33);
            btnReturn.TabIndex = 4;
            btnReturn.Text = "Back";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // txtRemove
            // 
            txtRemove.Location = new Point(81, 356);
            txtRemove.Name = "txtRemove";
            txtRemove.Size = new Size(211, 23);
            txtRemove.TabIndex = 3;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(326, 350);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(101, 32);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // listFavorites
            // 
            listFavorites.BackColor = Color.Plum;
            listFavorites.FormattingEnabled = true;
            listFavorites.ItemHeight = 15;
            listFavorites.Location = new Point(64, 84);
            listFavorites.Name = "listFavorites";
            listFavorites.Size = new Size(326, 244);
            listFavorites.TabIndex = 1;
            // 
            // lblFavorites
            // 
            lblFavorites.AutoSize = true;
            lblFavorites.Font = new Font("Ravie", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFavorites.Location = new Point(167, 23);
            lblFavorites.Name = "lblFavorites";
            lblFavorites.Size = new Size(152, 30);
            lblFavorites.TabIndex = 0;
            lblFavorites.Text = "Favorites";
            // 
            // Favorites
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 444);
            Controls.Add(panel1);
            Name = "Favorites";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Favorites";
            Load += Favorites_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }
    }
}
