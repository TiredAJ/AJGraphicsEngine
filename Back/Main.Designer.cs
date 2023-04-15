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
            if(disposing && (components != null))
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
            btn_Pause = new Button();
            btn_Reset = new Button();
            flp_FlowPanel = new FlowLayoutPanel();
            lblFrameTime = new Label();
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).BeginInit();
            SuspendLayout();
            // 
            // pbx_DisplayCanvas
            // 
            pbx_DisplayCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbx_DisplayCanvas.BackColor = SystemColors.ButtonHighlight;
            pbx_DisplayCanvas.BorderStyle = BorderStyle.Fixed3D;
            pbx_DisplayCanvas.Location = new Point(0, 0);
            pbx_DisplayCanvas.Margin = new Padding(3, 4, 3, 4);
            pbx_DisplayCanvas.Name = "pbx_DisplayCanvas";
            pbx_DisplayCanvas.Size = new Size(833, 528);
            pbx_DisplayCanvas.TabIndex = 0;
            pbx_DisplayCanvas.TabStop = false;
            pbx_DisplayCanvas.Paint += pbx_DisplayCanvas_Paint;
            pbx_DisplayCanvas.MouseDoubleClick += MouseDoubleClickFnc;
            pbx_DisplayCanvas.MouseDown += MouseDownFnc;
            pbx_DisplayCanvas.MouseMove += MouseMoveFnc;
            pbx_DisplayCanvas.MouseUp += MouseUpFnc;
            // 
            // btn_Start
            // 
            btn_Start.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Start.Location = new Point(972, 14);
            btn_Start.Margin = new Padding(3, 4, 3, 4);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(147, 30);
            btn_Start.TabIndex = 1;
            btn_Start.Text = "Start";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Pause
            // 
            btn_Pause.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Pause.Enabled = false;
            btn_Pause.Location = new Point(972, 90);
            btn_Pause.Margin = new Padding(3, 4, 3, 4);
            btn_Pause.Name = "btn_Pause";
            btn_Pause.Size = new Size(147, 30);
            btn_Pause.TabIndex = 2;
            btn_Pause.Text = "Pause";
            btn_Pause.UseVisualStyleBackColor = true;
            btn_Pause.Click += btn_Pause_Click;
            // 
            // btn_Reset
            // 
            btn_Reset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Reset.Enabled = false;
            btn_Reset.Location = new Point(972, 52);
            btn_Reset.Margin = new Padding(3, 4, 3, 4);
            btn_Reset.Name = "btn_Reset";
            btn_Reset.Size = new Size(147, 30);
            btn_Reset.TabIndex = 3;
            btn_Reset.Text = "Reset";
            btn_Reset.UseVisualStyleBackColor = true;
            btn_Reset.Click += btn_Reset_Click;
            // 
            // flp_FlowPanel
            // 
            flp_FlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            flp_FlowPanel.AutoScroll = true;
            flp_FlowPanel.BorderStyle = BorderStyle.FixedSingle;
            flp_FlowPanel.FlowDirection = FlowDirection.TopDown;
            flp_FlowPanel.Location = new Point(839, 127);
            flp_FlowPanel.Name = "flp_FlowPanel";
            flp_FlowPanel.Size = new Size(286, 401);
            flp_FlowPanel.TabIndex = 4;
            flp_FlowPanel.WrapContents = false;
            // 
            // lblFrameTime
            // 
            lblFrameTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblFrameTime.AutoSize = true;
            lblFrameTime.Location = new Point(839, 19);
            lblFrameTime.Name = "lblFrameTime";
            lblFrameTime.Size = new Size(83, 20);
            lblFrameTime.TabIndex = 5;
            lblFrameTime.Text = "FrameTime";
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1128, 529);
            Controls.Add(lblFrameTime);
            Controls.Add(flp_FlowPanel);
            Controls.Add(btn_Reset);
            Controls.Add(btn_Pause);
            Controls.Add(btn_Start);
            Controls.Add(pbx_DisplayCanvas);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frm_Main";
            Text = "Form1";
            FormClosing += frm_Main_FormClosing;
            Load += frm_Main_Load;
            MouseDoubleClick += MouseDoubleClickFnc;
            MouseDown += MouseDownFnc;
            MouseMove += MouseMoveFnc;
            MouseUp += MouseUpFnc;
            Resize += frm_Main_Resize;
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_Start;
        public PictureBox pbx_DisplayCanvas;
        private Button btn_Pause;
        private Button btn_Reset;
        private FlowLayoutPanel flp_FlowPanel;
        private Label lblFrameTime;
    }
}