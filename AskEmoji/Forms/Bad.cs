using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AskEmoji.Forms
{
    public partial class Bad : Form
    {
        private string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Res", "img", "bad.png");

        public Bad()
        {
            InitializeComponent();
            LoadImage();
            pictureBox1.Click += PictureBox1_Click;
        }

        private void LoadImage()
        {
            if (File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Selecione uma nova imagem";
                dlg.Filter = "Imagens (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(dlg.FileName, imagePath, true);
                        LoadImage();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao substituir a imagem: " + ex.Message);
                    }
                }
            }
        }
    }
}
