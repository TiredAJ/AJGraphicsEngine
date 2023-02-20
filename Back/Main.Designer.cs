namespace BasicGraphicsEngine
{
    partial class frm_Main
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
            pbx_DisplayCanvas = new PictureBox();
            btn_Start = new Button();
            btn_Stop = new Button();
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).BeginInit();
            SuspendLayout();
            // 
            // pbx_DisplayCanvas
            // 
            pbx_DisplayCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbx_DisplayCanvas.BackColor = SystemColors.ButtonHighlight;
            pbx_DisplayCanvas.BorderStyle = BorderStyle.Fixed3D;
            pbx_DisplayCanvas.Location = new Point(14, 14);
            pbx_DisplayCanvas.Margin = new Padding(3, 4, 3, 4);
            pbx_DisplayCanvas.Name = "pbx_DisplayCanvas";
            pbx_DisplayCanvas.Size = new Size(887, 501);
            pbx_DisplayCanvas.SizeMode = PictureBoxSizeMode.Zoom;
            pbx_DisplayCanvas.TabIndex = 0;
            pbx_DisplayCanvas.TabStop = false;
            pbx_DisplayCanvas.Paint += pbx_DisplayCanvas_Paint;
            pbx_DisplayCanvas.MouseMove += pbx_DisplayCanvas_MouseMove;
            // 
            // btn_Start
            // 
            btn_Start.Location = new Point(907, 14);
            btn_Start.Margin = new Padding(3, 4, 3, 4);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(147, 60);
            btn_Start.TabIndex = 1;
            btn_Start.Text = "Start";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.Location = new Point(907, 82);
            btn_Stop.Margin = new Padding(3, 4, 3, 4);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(147, 60);
            btn_Stop.TabIndex = 2;
            btn_Stop.Text = "Stop";
            btn_Stop.UseVisualStyleBackColor = true;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1063, 529);
            Controls.Add(btn_Stop);
            Controls.Add(btn_Start);
            Controls.Add(pbx_DisplayCanvas);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frm_Main";
            Text = "Form1";
            Load += frm_Main_Load;
            MouseMove += frm_Main_MouseMove;
            Resize += frm_Main_Resize;
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_Start;
        public PictureBox pbx_DisplayCanvas;
        private Button btn_Stop;
    }
}