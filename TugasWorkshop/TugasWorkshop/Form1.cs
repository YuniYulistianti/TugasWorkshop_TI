using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TugasWorkshop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap copyBitmap = new Bitmap((Bitmap)pictureBox1.Image);
            ProcessImage(copyBitmap);
            pictureBox2.Image = copyBitmap;
        }

        public bool ProcessImage(Bitmap bmp)
        {
            int numColors = 5; // Update the number of colors
            Color[] quantizedColors = GetQuantizedColors(numColors);

            int stripWidth = bmp.Width / numColors;

            for (int i = 0; i < numColors; i++)
            {
                Color stripColor = quantizedColors[i];

                for (int x = i * stripWidth; x < (i + 1) * stripWidth && x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);

                        // Calculate the weighted average to blend the original pixel color and the desired color
                        int alpha = (pixelColor.A + stripColor.A) / 2;
                        int red = (pixelColor.R + stripColor.R) / 2;
                        int green = (pixelColor.G + stripColor.G) / 2;
                        int blue = (pixelColor.B + stripColor.B) / 2;

                        bmp.SetPixel(x, y, Color.FromArgb(alpha, red, green, blue));
                    }
                }
            }

            return true;
        }

        private Color[] GetQuantizedColors(int numColors)
        {
            // You can customize the colors here to be any set of colors you want
            Color[] quantizedColors = new Color[]
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Purple,
                Color.Orange,
            };

            return quantizedColors;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFile.FileName);
            }
        }
    }
}
