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
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).BeginInit();
            SuspendLayout();
            // 
            // pbx_DisplayCanvas
            // 
            pbx_DisplayCanvas.Location = new Point(12, 12);
            pbx_DisplayCanvas.Name = "pbx_DisplayCanvas";
            pbx_DisplayCanvas.Size = new Size(776, 426);
            pbx_DisplayCanvas.TabIndex = 0;
            pbx_DisplayCanvas.TabStop = false;
            pbx_DisplayCanvas.Paint += OnPaint;
            // 
            // btn_Start
            // 
            btn_Start.Location = new Point(794, 12);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(129, 51);
            btn_Start.TabIndex = 1;
            btn_Start.Text = "Start";
            btn_Start.UseVisualStyleBackColor = true;
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 450);
            Controls.Add(btn_Start);
            Controls.Add(pbx_DisplayCanvas);
            Name = "frm_Main";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbx_DisplayCanvas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbx_DisplayCanvas;
        private Button btn_Start;
    }
}