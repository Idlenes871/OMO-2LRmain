using MyML;
using MyML.Functions;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames;
using MathNet.Numerics.LinearAlgebra;

namespace LR2Form
{
    public partial class BaseForm : Form
    {
        private Random rnd = new Random(0);

        private double LearningRateStep { get; set; }
        public double LearningRate { get; private set; }
        public double EpochNumber { get; private set; }

        private NN nn { get; set; }
        private IActFunction func { get; set; }
        private string trainPath { get; set; }
        private string testPath { get; set; }

        private void UpdateLearningRateLabel()
        {
            LearningRateLabel.Text = $"Скорость обучения: {LearningRate.ToString("F3")}";
        }
        private void UpdateEpochNumberLabel()
        {
            EpochNumberLabel.Text = $"Число эпох: {EpochNumber}";
        }

        private List<Control> inputContols = [];

        private void DisableInput()
        {
            foreach (Control control in inputContols)
                control.Enabled = false;
        }
        private void EnableInput()
        {
            foreach (Control control in inputContols)
                control.Enabled = true;
        }

        private void CreateNN()
        {
            int[] nnparams = [32 * 32, 32*8, 32*4, 32, 10];
            func = new Sigmoid();
            nn = new NN(nnparams, func);
        }

        public BaseForm()
        {
            CreateNN();

            trainPath = "training_data.csv";
            testPath = "validation_data.csv";

            InitializeComponent();
            LearningRateStep = 0.001;
            LearningRateTrackbar.Value = 20;
            EpochNumberTrackbar.Value = 4;

            LearningRate = LearningRateStep * LearningRateTrackbar.Value;
            EpochNumber = EpochNumberTrackbar.Value;
            UpdateLearningRateLabel();
            UpdateEpochNumberLabel();

            ClassComboBox.SelectedIndex = 0;

            inputContols.Add(LearningRateTrackbar);
            inputContols.Add(EpochNumberTrackbar);
            inputContols.Add(LearnBTN);
            inputContols.Add(ShowstatsBTN);
            inputContols.Add(ShowgraphBTN);
            inputContols.Add(RecoginzeBTN);
            inputContols.Add(PunishBTN);

            InitCanvas();
        }

        private void LearningRateTrackbar_Scroll(object sender, EventArgs e)
        {
            LearningRate = LearningRateTrackbar.Value * LearningRateStep;
            UpdateLearningRateLabel();
        }

        private void EpochNumberTrackbar_Scroll(object sender, EventArgs e)
        {
            EpochNumber = EpochNumberTrackbar.Value;
            UpdateEpochNumberLabel();
        }

        private void LearnBTN_Click(object sender, EventArgs e)
        {
            DisableInput();
            Output.Text = "";

            Task.Run(() =>
            {
                string pathBase = DateTime.Now.ToString("yyyy.MM.dd_HH_mm_ss");
                int epochBase = nn.EpochCount;
                Output.Invoke(() =>
                {
                    Output.Text += $"Текущий опыт: {epochBase} эпох.\n";
                    Output.Text += $"Начало дообучения на {EpochNumber} эпох\n";
                });

                for (int i = 1; i <= EpochNumber; i++)
                {
                    Output.Invoke(() =>
                    {
                        Output.Text += $"Начало эпохи №{epochBase + i}: {DateTime.Now.ToString("HH:mm:ss")}\n";
                    });
                    nn.EpochFromFiles(trainPath, testPath, LearningRate);
                    if (AutosaveCheckBox.Checked)
                    {
                        nn.ExportToJson($"{pathBase}_epoch_{epochBase + i}_autosave.json");
                        Output.Invoke(() =>
                        {
                            Output.Text += $"Автосохранение прошло успешно\n";
                        });
                    }
                    var stats = nn.EpochStats[nn.EpochCount - 1];
                    Output.Invoke(() =>
                    {
                        Output.Text += $"Потери на тренировочной выборке: {stats.TrainingStats.Loss.ToString("F4")}\n";
                        Output.Text += $"Потери на валидационной выборке: {stats.ValidationStats.Loss.ToString("F4")}\n";
                    });
                }
                Output.Invoke(() =>
                {
                    Output.Text += $"ОБУЧЕНИЕ ЗАВЕРШЕНО\n";
                    EnableInput();
                });
            });
        }

        private void ShowstatsBTN_Click(object sender, EventArgs e)
        {
            var estats = nn.EpochStats;
            string format = "F4";

            if (estats.Count < 1)
            {
                Output.Text = "Нейросеть ещё не обучена. Данных нет";
                return;
            }

            Output.Text = $"Нейросеть прошла {nn.EpochCount} эпох обучения\n";
            string nnparams = "";
            for (int i = 0; i < nn.LayerSizes.Length; i++)
            {
                nnparams += nn.LayerSizes[i].ToString();
                if (i < nn.LayerSizes.Length - 1)
                    nnparams += ":";
            }
            Output.Text += $"Размеры слоёв, начиная с входного:\n{nnparams}\n\n";

            Output.Text += "Потери на тренировочной выборке:\n";
            for (int i = 0; i < nn.EpochCount; i++)
            {
                var stats = estats[i].TrainingStats;
                Output.Text += $"{stats.Loss.ToString(format)}; ";
            }
            Output.Text += "\n";
            Output.Text += "Потери на валидационной выборке:\n";
            for (int i = 0; i < nn.EpochCount; i++)
            {
                var stats = estats[i].ValidationStats;
                Output.Text += $"{stats.Loss.ToString(format)}; ";
            }
            Output.Text += "\n\n";
            Output.Text += "ACCURACY:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Accuracy для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Accuracy[i].ToString(format)}; ";
                }
                Output.Text += "\n";
                Output.Text += $"Accuracy для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Accuracy[i].ToString(format)}; ";
                }
                Output.Text += "\n\n";
            }
            Output.Text += "\nPRECISION:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Precision для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Precision[i].ToString(format)}; ";
                }
                Output.Text += "\n";
                Output.Text += $"Precision для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Precision[i].ToString(format)}; ";
                }
                Output.Text += "\n\n";
            }
            Output.Text += "\nRECALL:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Recall для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Recall[i].ToString(format)}; ";
                }
                Output.Text += "\n";
                Output.Text += $"Recall для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += $"{stats.Recall[i].ToString(format)}; ";
                }
                Output.Text += "\n\n";
            }
        }

        private void ShowgraphBTN_Click(object sender, EventArgs e)
        {
            var estats = nn.EpochStats;
            string format = "F4";

            if (estats.Count < 1)
            {
                Output.Text = "Нейросеть ещё не обучена. Данных нет";
                return;
            }

            string Point(int x, double y)
            {
                return $"({x};{y.ToString(format)})";
            }

            Output.Text = "Потери на тренировочной выборке:\n";
            for (int i = 0; i < nn.EpochCount; i++)
            {
                var stats = estats[i].TrainingStats;
                Output.Text += Point(i + 1, stats.Loss);
            }
            Output.Text += "\n";
            Output.Text += "Потери на валидационной выборке:\n";
            for (int i = 0; i < nn.EpochCount; i++)
            {
                var stats = estats[i].ValidationStats;
                Output.Text += Point(i + 1, stats.Loss);
            }
            Output.Text += "\n\n";
            Output.Text += "ACCURACY:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Accuracy для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n";
                Output.Text += $"Accuracy для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n\n";
            }
            Output.Text += "\nPRECISION:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Precision для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n";
                Output.Text += $"Precision для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n\n";
            }
            Output.Text += "\nRECALL:\n";
            for (int i = 0; i < nn.OutputSize; i++)
            {
                Output.Text += $"Recall для класса {i} в тестовой выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n";
                Output.Text += $"Recall для класса {i} в валидационной выборке:\n";
                for (int j = 0; j < nn.EpochCount; j++)
                {
                    var stats = estats[j].TrainingStats;
                    Output.Text += Point(j + 1, stats.Accuracy[i]);
                }
                Output.Text += "\n\n";
            }
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                nn.ExportToJson(saveFileDialog.FileName);
            }
        }

        private void LoadBTN_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                nn = NN.BuildFromJson(openFileDialog.FileName, func);
                Output.Text = $"Нейросеть прошла {nn.EpochCount} эпох обучения\n";
                string nnparams = "";
                for (int i = 0; i < nn.LayerSizes.Length; i++)
                {
                    nnparams += nn.LayerSizes[i].ToString();
                    if (i < nn.LayerSizes.Length - 1)
                        nnparams += ":";
                }
                Output.Text += $"Размеры слоёв, начиная с входного:\n{nnparams}\n\n";
            }
        }

        private void ResetBTN_Click(object sender, EventArgs e)
        {
            PunishBTN.Enabled = false;
            CreateNN();
            Output.Text = "Обучение сброшено";
        }

        private bool IsDrawing { get; set; }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrawing = false;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrawing = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDrawing)
                return;

            float absW = Canvas.Width;
            float absH = Canvas.Height;
            float relX = e.X / absW;
            float relY = e.Y / absH;

            graphics.DrawEllipse(pen, relX*64, relY*64, 1, 1);
            Canvas.Image = bitmap;
        }

        private Graphics graphics;
        private Pen pen = new Pen(Color.Black, 2f);
        private Bitmap bitmap { get; set; }

        private void InitCanvas()
        {
            bitmap = new Bitmap(64, 64);
            graphics = Graphics.FromImage(bitmap);
            Canvas.Image = bitmap;
        }
        private void ThicknessTrackbar_Scroll(object sender, EventArgs e)
        {
            ThicknessLabel.Text = $"{ThicknessTrackbar.Value}px";
            pen.Width = (float)ThicknessTrackbar.Value;
        }

        private void ResetCanvasBTN_Click(object sender, EventArgs e)
        {
            InitCanvas();
        }

        private string[] classes = ["А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К"];

        private Matrix<double> InputFromCanvas()
        {
            Matrix<double> inputVector = Matrix<double>.Build.Dense(nn.InputSize, 1);

            Bitmap bitmap = new Bitmap(Canvas.Image);

            int i = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    if (pixel.A / 255 > 0.5)
                        inputVector[i, 0] = 1;
                    i++;
                }
            }
            string testString = "";
            for (int j = 0; j < inputVector.RowCount; j++)
                testString += ((int)inputVector[j, 0]).ToString();

            return inputVector;
        }
        private string OutputPresentation(Matrix<double> output)
        {
            string result = "";
            for (int i = 0; i < output.RowCount; i++)
                result += $"{output[i, 0].ToString("F3")} : {classes[i]}\n";
            return result;
        }

        Matrix<double> outputVector;
        private void RecoginzeBTN_Click(object sender, EventArgs e)
        {
            var input = InputFromCanvas();
            outputVector = nn.Run(input);
            int decidedClass = nn.DecideBySoftmax(outputVector);
            Output.Text = $"Предсказанное значение: {classes[decidedClass]}\n\n";
            Output.Text += $"Вектор результатов:\n{OutputPresentation(outputVector)}";
            PunishBTN.Enabled = true;
        }

        private void PunishBTN_Click(object sender, EventArgs e)
        {
            var input = InputFromCanvas();
            nn.Learn(outputVector, nn.BuildIdeal(ClassComboBox.SelectedIndex), LearningRate, out double _);
            outputVector = nn.Run(input);
            int decidedClass = nn.DecideBySoftmax(outputVector);
            Output.Text = $"Предсказанное значение: {classes[decidedClass]}\n\n";
            Output.Text += $"Вектор результатов:\n{OutputPresentation(outputVector)}";

        }
    }
}
