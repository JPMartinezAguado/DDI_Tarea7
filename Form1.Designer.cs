namespace DDI_Tarea7
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            timer1 = new System.Windows.Forms.Timer(components);
            btnSalto = new Button();
            btnSiguiente = new Button();
            axWindowsMediaPlayer2 = new AxWMPLib.AxWindowsMediaPlayer();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            timer2 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            // 
            // btnSalto
            // 
            btnSalto.BackColor = Color.Transparent;
            btnSalto.FlatAppearance.BorderColor = Color.White;
            btnSalto.FlatStyle = FlatStyle.Flat;
            btnSalto.Font = new Font("Viner Hand ITC", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSalto.ForeColor = Color.White;
            btnSalto.Location = new Point(995, 32);
            btnSalto.Name = "btnSalto";
            btnSalto.Size = new Size(187, 52);
            btnSalto.TabIndex = 0;
            btnSalto.Text = "Saltar Intro";
            btnSalto.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSalto.UseVisualStyleBackColor = false;
            btnSalto.Click += btnSalto_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSiguiente.BackColor = Color.Transparent;
            btnSiguiente.FlatAppearance.BorderColor = Color.White;
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Snap ITC", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.Snow;
            btnSiguiente.Location = new Point(541, 551);
            btnSiguiente.Margin = new Padding(0);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(112, 81);
            btnSiguiente.TabIndex = 1;
            btnSiguiente.Text = ">>";
            btnSiguiente.TextAlign = ContentAlignment.TopCenter;
            btnSiguiente.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // axWindowsMediaPlayer2
            // 
            axWindowsMediaPlayer2.Enabled = true;
            axWindowsMediaPlayer2.Location = new Point(-4, -16);
            axWindowsMediaPlayer2.Margin = new Padding(0);
            axWindowsMediaPlayer2.Name = "axWindowsMediaPlayer2";
            axWindowsMediaPlayer2.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer2.OcxState");
            axWindowsMediaPlayer2.Size = new Size(1268, 757);
            axWindowsMediaPlayer2.TabIndex = 4;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Cooper Black", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(118, 285);
            label1.Name = "label1";
            label1.Size = new Size(982, 119);
            label1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.fondojuego100;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1250, 726);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            BackgroundImage = Properties.Resources.FONDOPORTADA;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1248, 733);
            Controls.Add(pictureBox1);
            Controls.Add(btnSalto);
            Controls.Add(label1);
            Controls.Add(btnSiguiente);
            Controls.Add(axWindowsMediaPlayer2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Button btnSalto;
        private Button btnSiguiente;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer2;
        private Label label1;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer2;
    }
}
