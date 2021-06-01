﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form1 : Form
    {
        Bitmap image;
        PreviousSteps PrevSteps;
        public float[,] StructElementMatrix;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            фильтрыToolStripMenuItem.Enabled = false;
            button6.Enabled = false;
            button2.Enabled = false;
            button7.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;

            PrevSteps = new PreviousSteps();
            StructElementMatrix = new float[5, 5];
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    StructElementMatrix[i, j] = 1;
                }
            }
        }

        private void PictureBoxRefresh()
        {
            pictureBox2.Image = PrevSteps.Images[0];
            pictureBox3.Image = PrevSteps.Images[1];
            pictureBox4.Image = PrevSteps.Images[2];
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
            файлToolStripMenuItem.Enabled = true;
            фильтрыToolStripMenuItem.Enabled = true;
            button6.Enabled = true;
            button2.Enabled = true;
            button7.Enabled = true;
            button5.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            фильтрыToolStripMenuItem.Enabled = true;
            button6.Enabled = true;
            button2.Enabled = true;
            button7.Enabled = true;
            button5.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filter)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
            {
                PrevSteps.AddImage(image);
                image = newImage;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            файлToolStripMenuItem.Enabled = false;
            фильтрыToolStripMenuItem.Enabled = false;
            button6.Enabled = false;
            button2.Enabled = false;
            button7.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                pictureBox1.Image = image;
                PictureBoxRefresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеГауссToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new LetteringFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(0, 50, 0);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(0, -50, 0);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(-50, 0, 0);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(50, 0, 0);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(0, 0, Math.PI / 4);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Filter filter = new MoveFilter(0, 0, -Math.PI / 4);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new WavesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеВДвиженииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new MotionBlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void выделениеГраницToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new SharrFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void выделениеГраницToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filter filter = new PryittFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            dialog.Title = "Save an Image File";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)dialog.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (dialog.FilterIndex)
                {
                    case 1:
                        try { this.image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg); }
                        catch(Exception ee )
                        {
                            MessageBox.Show("Невозможно сохранить файл, которого нет)))");
                            return;
                        }
                        break;

                    case 2:
                        this.image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            if(PrevSteps.Images[0] != null)
            {
                //Bitmap pic = image;
                pictureBox1.Image = PrevSteps.Images[0];
                image = PrevSteps.Images[0];
                //PrevSteps.AddImage(pic);
                PictureBoxRefresh();
            }
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            if (PrevSteps.Images[1] != null)
            {
                //Bitmap pic = image;
                pictureBox1.Image = PrevSteps.Images[1];
                image = PrevSteps.Images[1];
                //PrevSteps.AddImage(pic);
                PictureBoxRefresh();
            }
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            if (PrevSteps.Images[2] != null)
            {
                //Bitmap pic = image;
                pictureBox1.Image = PrevSteps.Images[2];
                image = PrevSteps.Images[2];
                //PrevSteps.AddImage(pic);
                PictureBoxRefresh();
            }
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линРастяжГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new LinearGistFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void бинарноеИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new BinaryFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Dilation(StructElementMatrix);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Erosion(StructElementMatrix);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размыканиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Opening(StructElementMatrix);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void замыканиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Closing(StructElementMatrix);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Grad();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(StructElementMatrix, this);
            form2.Show();
        }

        private void фильтрыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
