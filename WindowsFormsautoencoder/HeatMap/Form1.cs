using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AI;
using AI.DataStructs.Algebraic;
using AI.ML.Classifiers;
using AI.ML.NeuralNetwork.CoreNNW;
using AI.ML.NeuralNetwork.CoreNNW.Activations;
using AI.ML.NeuralNetwork.CoreNNW.Datasets;
using AI.ML.NeuralNetwork.CoreNNW.Layers;
using AI.ML.NeuralNetwork.CoreNNW.Loss;
using AI.ML.NeuralNetwork.CoreNNW.Optimizers;
using AI.ML.NeuralNetwork.CoreNNW.Train;
using AI.ML.SeqPredict;
using AI.ML.Datasets;
using static System.Resources.ResXFileRef;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using AI.ComputerVision;
using Matrix = AI.DataStructs.Algebraic.Matrix;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing.Imaging;

namespace HeatMap
{
    public partial class Form1 : Form
    {
        List<Matrix> inputs = new List<Matrix>();
        List<Matrix> outputs = new List<Matrix>();

        DataSetNoReccurent noReccurent;
        NNW nnW = new NNW();
        public Form1()
        {
            InitializeComponent();
        }
        void DataLoad(string MAP)
        {
            LoadImages(MAP, inputs);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            LoadImages(MAP, outputs);
        }

        private void LoadImages(string directoryPath, List<Matrix> imageList)
        {
            string[] paths = Directory.GetFiles(directoryPath);
            for (int i = 0; i < 20000; i++) // paths.Length
            {
                using (Bitmap bitmap = new Bitmap(paths[i]))
                {
                    imageList.Add(bitmap.ToMatrix(32, 32));
                }

                Console.WriteLine(i + " - " + paths.Length);
            }
        }
        void Tr()
        {
            GraphCPU graph = new GraphCPU(false);
            Trainer trainer = new Trainer(graph);
            trainer.Train(50, 1, 0.0001f, nnW, noReccurent);
            nnW.Save("nn2.txt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //nnW = NNW.Load("nn.txt");

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    DataLoad(selectedPath);

                    noReccurent = new DataSetNoReccurent(inputs.ToArray(), inputs.ToArray(), new LossMeanSqrSqrt());

                    nnW.AddNewLayer(new Shape(32, 32, 1), new ConvolutionalLayer(new ReLU(0.01), 4) { IsSame = true });
                    nnW.AddNewLayer(new MaxPooling());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 8) { IsSame = true });
                    nnW.AddNewLayer(new MaxPooling());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 16) { IsSame = true });
                    nnW.AddNewLayer(new MaxPooling());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 32) { IsSame = true });
                    nnW.AddNewLayer(new MaxPooling());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 64) { IsSame = true });
                    nnW.AddNewLayer(new UpSampling2DBicubic());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 32) { IsSame = true });
                    nnW.AddNewLayer(new UpSampling2DBicubic());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 16) { IsSame = true });
                    nnW.AddNewLayer(new UpSampling2DBicubic());
                    nnW.AddNewLayer(new ConvolutionalLayer(new ReLU(0.01), 8) { IsSame = true });
                    nnW.AddNewLayer(new UpSampling2DBicubic());
                    nnW.AddNewLayer(new ConvolutionalLayer(new LinearUnit(), 1) { IsSame = true });

                    Console.WriteLine(nnW);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Tr();

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                Bitmap userImages = new Bitmap(selectedFile);

                Matrix outp = nnW.Forward(new NNValue(userImages.ToMatrix(32, 32)), new GraphCPU(false)).ToMatrix();

                Bitmap image1 = ImgConverter.ToBitmap(userImages.ToMatrix(32, 32));
                Bitmap image2 = ImgConverter.ToBitmap(outp);

                pictureBox1.Image = image1;
                pictureBox2.Image = GetDifferenceImage(image1, image2);
            }

        }
        int hode = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            hode--;
            if (hode < 0)
            {
                hode = 0;
            }
            button3.Name = $"Length: {paths.Length}, Hode: {hode}";

            Bitmap userImages = new Bitmap(paths[hode]);

            Matrix outp = nnW.Forward(new NNValue(userImages.ToMatrix(32, 32)), new GraphCPU(false)).ToMatrix();

            Bitmap image1 = ImgConverter.ToBitmap(userImages.ToMatrix(32, 32));
            Bitmap image2 = ImgConverter.ToBitmap(outp);

            pictureBox1.Image = image1;
            pictureBox2.Image = GetDifferenceImage(image1, image2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hode++;
            if (hode > paths.Length)
            {
                hode = paths.Length;
            }
            button3.Name = $"Length: {paths.Length}, Hode: {hode}";

            Bitmap userImages = new Bitmap(paths[hode]);

            Matrix outp = nnW.Forward(new NNValue(userImages.ToMatrix(32, 32)), new GraphCPU(false)).ToMatrix();

            Bitmap image1 = ImgConverter.ToBitmap(userImages.ToMatrix(32, 32));
            Bitmap image2 = ImgConverter.ToBitmap(outp);

            pictureBox1.Image = image1;
            pictureBox2.Image = GetDifferenceImage(image1, image2);
        }

        string[] paths;
        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    paths = Directory.GetFiles(selectedPath);
                    ;
                }
            }
        }


        public static Bitmap GetDifferenceImage(Bitmap img1, Bitmap img2)
        {
            if (img1.Width != img2.Width || img1.Height != img2.Height)
                throw new ArgumentException("Images must be of the same size");

            Bitmap diffImg = new Bitmap(img1.Width, img1.Height, PixelFormat.Format24bppRgb);

            BitmapData img1Data = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData img2Data = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData diffImgData = diffImg.LockBits(new Rectangle(0, 0, diffImg.Width, diffImg.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int bytesPerPixel = 3;
            int height = img1.Height;
            int widthInBytes = img1.Width * bytesPerPixel;
            byte[] img1Buffer = new byte[img1Data.Stride * img1.Height];
            byte[] img2Buffer = new byte[img2Data.Stride * img2.Height];
            byte[] diffBuffer = new byte[diffImgData.Stride * diffImgData.Height];

            System.Runtime.InteropServices.Marshal.Copy(img1Data.Scan0, img1Buffer, 0, img1Buffer.Length);
            System.Runtime.InteropServices.Marshal.Copy(img2Data.Scan0, img2Buffer, 0, img2Buffer.Length);

            img1.UnlockBits(img1Data);
            img2.UnlockBits(img2Data);

            for (int y = 0; y < height; y++)
            {
                int img1Offset = y * img1Data.Stride;
                int img2Offset = y * img2Data.Stride;
                int diffOffset = y * diffImgData.Stride;

                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    byte bDiff = (byte)Math.Abs(img1Buffer[img1Offset + x] - img2Buffer[img2Offset + x]);
                    byte gDiff = (byte)Math.Abs(img1Buffer[img1Offset + x + 1] - img2Buffer[img2Offset + x + 1]);
                    byte rDiff = (byte)Math.Abs(img1Buffer[img1Offset + x + 2] - img2Buffer[img2Offset + x + 2]);

                    diffBuffer[diffOffset + x] = bDiff;
                    diffBuffer[diffOffset + x + 1] = gDiff;
                    diffBuffer[diffOffset + x + 2] = rDiff;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(diffBuffer, 0, diffImgData.Scan0, diffBuffer.Length);
            diffImg.UnlockBits(diffImgData);

            return diffImg;
        }
    }
}
