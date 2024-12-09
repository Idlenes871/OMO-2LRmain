namespace LR2Form
{
    partial class BaseForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LearnBTN = new Button();
            LearningRateTrackbar = new TrackBar();
            LearningRateLabel = new Label();
            EpochNumberLabel = new Label();
            EpochNumberTrackbar = new TrackBar();
            TrackbarPanel = new Panel();
            AutosaveCheckBox = new CheckBox();
            Output = new RichTextBox();
            ShowstatsBTN = new Button();
            ShowgraphBTN = new Button();
            LoadBTN = new Button();
            SaveBTN = new Button();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            ResetBTN = new Button();
            Canvas = new PictureBox();
            ResetCanvasBTN = new Button();
            ThicknessTrackbar = new TrackBar();
            ThicknessLabel = new Label();
            ClassComboBox = new ComboBox();
            RecoginzeBTN = new Button();
            PunishBTN = new Button();
            ((System.ComponentModel.ISupportInitialize)LearningRateTrackbar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EpochNumberTrackbar).BeginInit();
            TrackbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThicknessTrackbar).BeginInit();
            SuspendLayout();
            // 
            // LearnBTN
            // 
            LearnBTN.Font = new Font("Segoe UI", 7F);
            LearnBTN.Location = new Point(312, 13);
            LearnBTN.Name = "LearnBTN";
            LearnBTN.Size = new Size(99, 60);
            LearnBTN.TabIndex = 0;
            LearnBTN.Text = "(До)обучить с заданными праметрами";
            LearnBTN.UseVisualStyleBackColor = true;
            LearnBTN.Click += LearnBTN_Click;
            // 
            // LearningRateTrackbar
            // 
            LearningRateTrackbar.Location = new Point(11, 47);
            LearningRateTrackbar.Maximum = 1000;
            LearningRateTrackbar.Minimum = 1;
            LearningRateTrackbar.Name = "LearningRateTrackbar";
            LearningRateTrackbar.Size = new Size(186, 56);
            LearningRateTrackbar.TabIndex = 1;
            LearningRateTrackbar.Value = 1;
            LearningRateTrackbar.Scroll += LearningRateTrackbar_Scroll;
            // 
            // LearningRateLabel
            // 
            LearningRateLabel.AutoSize = true;
            LearningRateLabel.Location = new Point(11, 13);
            LearningRateLabel.Name = "LearningRateLabel";
            LearningRateLabel.Size = new Size(186, 20);
            LearningRateLabel.TabIndex = 2;
            LearningRateLabel.Text = "Скорость обучения: 0,000";
            // 
            // EpochNumberLabel
            // 
            EpochNumberLabel.AutoSize = true;
            EpochNumberLabel.Location = new Point(203, 13);
            EpochNumberLabel.Name = "EpochNumberLabel";
            EpochNumberLabel.Size = new Size(103, 20);
            EpochNumberLabel.TabIndex = 3;
            EpochNumberLabel.Text = "Число эпох: 0";
            // 
            // EpochNumberTrackbar
            // 
            EpochNumberTrackbar.Location = new Point(203, 47);
            EpochNumberTrackbar.Maximum = 100;
            EpochNumberTrackbar.Minimum = 1;
            EpochNumberTrackbar.Name = "EpochNumberTrackbar";
            EpochNumberTrackbar.Size = new Size(103, 56);
            EpochNumberTrackbar.TabIndex = 4;
            EpochNumberTrackbar.Value = 1;
            EpochNumberTrackbar.Scroll += EpochNumberTrackbar_Scroll;
            // 
            // TrackbarPanel
            // 
            TrackbarPanel.Controls.Add(AutosaveCheckBox);
            TrackbarPanel.Controls.Add(EpochNumberTrackbar);
            TrackbarPanel.Controls.Add(LearningRateTrackbar);
            TrackbarPanel.Controls.Add(LearnBTN);
            TrackbarPanel.Controls.Add(EpochNumberLabel);
            TrackbarPanel.Controls.Add(LearningRateLabel);
            TrackbarPanel.Location = new Point(12, 12);
            TrackbarPanel.Name = "TrackbarPanel";
            TrackbarPanel.Size = new Size(422, 110);
            TrackbarPanel.TabIndex = 5;
            // 
            // AutosaveCheckBox
            // 
            AutosaveCheckBox.AutoSize = true;
            AutosaveCheckBox.Checked = true;
            AutosaveCheckBox.CheckState = CheckState.Checked;
            AutosaveCheckBox.Location = new Point(312, 79);
            AutosaveCheckBox.Name = "AutosaveCheckBox";
            AutosaveCheckBox.Size = new Size(99, 24);
            AutosaveCheckBox.TabIndex = 7;
            AutosaveCheckBox.Text = "Автосохр.";
            AutosaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            Output.Location = new Point(8, 128);
            Output.Name = "Output";
            Output.ReadOnly = true;
            Output.Size = new Size(426, 313);
            Output.TabIndex = 6;
            Output.Text = "";
            // 
            // ShowstatsBTN
            // 
            ShowstatsBTN.Location = new Point(440, 25);
            ShowstatsBTN.Name = "ShowstatsBTN";
            ShowstatsBTN.Size = new Size(100, 30);
            ShowstatsBTN.TabIndex = 7;
            ShowstatsBTN.Text = "Статистика";
            ShowstatsBTN.UseVisualStyleBackColor = true;
            ShowstatsBTN.Click += ShowstatsBTN_Click;
            // 
            // ShowgraphBTN
            // 
            ShowgraphBTN.Location = new Point(440, 55);
            ShowgraphBTN.Name = "ShowgraphBTN";
            ShowgraphBTN.Size = new Size(100, 30);
            ShowgraphBTN.TabIndex = 8;
            ShowgraphBTN.Text = "Графики";
            ShowgraphBTN.UseVisualStyleBackColor = true;
            ShowgraphBTN.Click += ShowgraphBTN_Click;
            // 
            // LoadBTN
            // 
            LoadBTN.Location = new Point(546, 25);
            LoadBTN.Name = "LoadBTN";
            LoadBTN.Size = new Size(100, 30);
            LoadBTN.TabIndex = 9;
            LoadBTN.Text = "Загрузить";
            LoadBTN.UseVisualStyleBackColor = true;
            LoadBTN.Click += LoadBTN_Click;
            // 
            // SaveBTN
            // 
            SaveBTN.Location = new Point(546, 55);
            SaveBTN.Name = "SaveBTN";
            SaveBTN.Size = new Size(100, 30);
            SaveBTN.TabIndex = 10;
            SaveBTN.Text = "Сохранить";
            SaveBTN.UseVisualStyleBackColor = true;
            SaveBTN.Click += SaveBTN_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = ".json-файлы|*.json";
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = ".json-файлы|*.json";
            // 
            // ResetBTN
            // 
            ResetBTN.Location = new Point(652, 25);
            ResetBTN.Name = "ResetBTN";
            ResetBTN.Size = new Size(100, 60);
            ResetBTN.TabIndex = 11;
            ResetBTN.Text = "Сброс обучения";
            ResetBTN.UseVisualStyleBackColor = true;
            ResetBTN.Click += ResetBTN_Click;
            // 
            // Canvas
            // 
            Canvas.BorderStyle = BorderStyle.Fixed3D;
            Canvas.Location = new Point(614, 128);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(256, 256);
            Canvas.SizeMode = PictureBoxSizeMode.StretchImage;
            Canvas.TabIndex = 12;
            Canvas.TabStop = false;
            Canvas.MouseDown += Canvas_MouseDown;
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseUp += Canvas_MouseUp;
            // 
            // ResetCanvasBTN
            // 
            ResetCanvasBTN.Location = new Point(770, 390);
            ResetCanvasBTN.Name = "ResetCanvasBTN";
            ResetCanvasBTN.Size = new Size(100, 60);
            ResetCanvasBTN.TabIndex = 13;
            ResetCanvasBTN.Text = "Сброс рисунка";
            ResetCanvasBTN.UseVisualStyleBackColor = true;
            ResetCanvasBTN.Click += ResetCanvasBTN_Click;
            // 
            // ThicknessTrackbar
            // 
            ThicknessTrackbar.Location = new Point(622, 390);
            ThicknessTrackbar.Maximum = 5;
            ThicknessTrackbar.Minimum = 1;
            ThicknessTrackbar.Name = "ThicknessTrackbar";
            ThicknessTrackbar.Size = new Size(103, 56);
            ThicknessTrackbar.TabIndex = 8;
            ThicknessTrackbar.Value = 2;
            ThicknessTrackbar.Scroll += ThicknessTrackbar_Scroll;
            // 
            // ThicknessLabel
            // 
            ThicknessLabel.AutoSize = true;
            ThicknessLabel.Location = new Point(731, 390);
            ThicknessLabel.Name = "ThicknessLabel";
            ThicknessLabel.Size = new Size(33, 20);
            ThicknessLabel.TabIndex = 8;
            ThicknessLabel.Text = "2px";
            // 
            // ClassComboBox
            // 
            ClassComboBox.DisplayMember = "1";
            ClassComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ClassComboBox.FormattingEnabled = true;
            ClassComboBox.Items.AddRange(new object[] { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К" });
            ClassComboBox.Location = new Point(440, 128);
            ClassComboBox.Name = "ClassComboBox";
            ClassComboBox.Size = new Size(100, 28);
            ClassComboBox.TabIndex = 14;
            // 
            // RecoginzeBTN
            // 
            RecoginzeBTN.Location = new Point(440, 162);
            RecoginzeBTN.Name = "RecoginzeBTN";
            RecoginzeBTN.Size = new Size(100, 30);
            RecoginzeBTN.TabIndex = 15;
            RecoginzeBTN.Text = "Распознать";
            RecoginzeBTN.UseVisualStyleBackColor = true;
            RecoginzeBTN.Click += RecoginzeBTN_Click;
            // 
            // PunishBTN
            // 
            PunishBTN.Enabled = false;
            PunishBTN.Location = new Point(440, 198);
            PunishBTN.Name = "PunishBTN";
            PunishBTN.Size = new Size(100, 30);
            PunishBTN.TabIndex = 16;
            PunishBTN.Text = "Наказать";
            PunishBTN.UseVisualStyleBackColor = true;
            PunishBTN.Click += PunishBTN_Click;
            // 
            // BaseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 453);
            Controls.Add(PunishBTN);
            Controls.Add(RecoginzeBTN);
            Controls.Add(ClassComboBox);
            Controls.Add(ThicknessLabel);
            Controls.Add(ThicknessTrackbar);
            Controls.Add(ResetCanvasBTN);
            Controls.Add(Canvas);
            Controls.Add(ResetBTN);
            Controls.Add(SaveBTN);
            Controls.Add(LoadBTN);
            Controls.Add(ShowgraphBTN);
            Controls.Add(ShowstatsBTN);
            Controls.Add(Output);
            Controls.Add(TrackbarPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "BaseForm";
            Text = "ЛР2";
            ((System.ComponentModel.ISupportInitialize)LearningRateTrackbar).EndInit();
            ((System.ComponentModel.ISupportInitialize)EpochNumberTrackbar).EndInit();
            TrackbarPanel.ResumeLayout(false);
            TrackbarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Canvas).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThicknessTrackbar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LearnBTN;
        private TrackBar LearningRateTrackbar;
        private Label LearningRateLabel;
        private Label EpochNumberLabel;
        private TrackBar EpochNumberTrackbar;
        private Panel TrackbarPanel;
        private RichTextBox Output;
        private CheckBox AutosaveCheckBox;
        private Button ShowstatsBTN;
        private Button ShowgraphBTN;
        private Button LoadBTN;
        private Button SaveBTN;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button ResetBTN;
        private PictureBox Canvas;
        private Button ResetCanvasBTN;
        private TrackBar ThicknessTrackbar;
        private Label ThicknessLabel;
        private ComboBox ClassComboBox;
        private Button RecoginzeBTN;
        private Button PunishBTN;
    }
}
