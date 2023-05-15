using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintingProgram
{
    public partial class MainForm : Form
    {
        private bool isDrawing;
        private Point previousPoint;
        private Pen currentPen;
        private Bitmap drawingBitmap;

        public MainForm()
        {
            InitializeComponent();
            isDrawing = false;
            currentPen = new Pen(Color.Black, 2); // Výchozí barva pera: černá, tloušťka: 2
            drawingBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            previousPoint = e.Location;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    g.DrawLine(currentPen, previousPoint, e.Location);
                }
                previousPoint = e.Location;
                Invalidate(); // Vynucení překreslení plátna
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentPen.Color = colorDialog.Color;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(Color.White);
            }
            Invalidate(); // Vynucení překreslení plátna
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    drawingBitmap.Save(saveFileDialog.FileName);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawingBitmap, Point.Empty);
            base.OnPaint(e);
        }
    }
}
