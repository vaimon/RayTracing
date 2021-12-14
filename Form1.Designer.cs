
namespace RayTracing
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCompute = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.transparencyValue = new System.Windows.Forms.NumericUpDown();
            this.mirrorValue = new System.Windows.Forms.NumericUpDown();
            this.checkTransparency = new System.Windows.Forms.CheckBox();
            this.checkMirror = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonChangeColor = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirrorValue)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 457);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(749, 44);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(719, 404);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(148, 44);
            this.btnCompute.TabIndex = 1;
            this.btnCompute.Text = "Отрендерить";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(767, 467);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(68, 25);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "Время:";
            this.labelTime.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.canvas);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 439);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сцена";
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(3, 27);
            this.canvas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(587, 409);
            this.canvas.TabIndex = 10;
            this.canvas.TabStop = false;
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonChangeColor);
            this.groupBox2.Controls.Add(this.transparencyValue);
            this.groupBox2.Controls.Add(this.mirrorValue);
            this.groupBox2.Controls.Add(this.checkTransparency);
            this.groupBox2.Controls.Add(this.checkMirror);
            this.groupBox2.Location = new System.Drawing.Point(608, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 257);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки объекта";
            // 
            // transparencyValue
            // 
            this.transparencyValue.DecimalPlaces = 2;
            this.transparencyValue.Enabled = false;
            this.transparencyValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.transparencyValue.Location = new System.Drawing.Point(225, 202);
            this.transparencyValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.transparencyValue.Name = "transparencyValue";
            this.transparencyValue.Size = new System.Drawing.Size(122, 31);
            this.transparencyValue.TabIndex = 1;
            // 
            // mirrorValue
            // 
            this.mirrorValue.DecimalPlaces = 2;
            this.mirrorValue.Enabled = false;
            this.mirrorValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.mirrorValue.Location = new System.Drawing.Point(225, 136);
            this.mirrorValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mirrorValue.Name = "mirrorValue";
            this.mirrorValue.Size = new System.Drawing.Size(122, 31);
            this.mirrorValue.TabIndex = 1;
            // 
            // checkTransparency
            // 
            this.checkTransparency.AutoSize = true;
            this.checkTransparency.Location = new System.Drawing.Point(27, 203);
            this.checkTransparency.Name = "checkTransparency";
            this.checkTransparency.Size = new System.Drawing.Size(156, 29);
            this.checkTransparency.TabIndex = 0;
            this.checkTransparency.Text = "Прозрачность";
            this.checkTransparency.UseVisualStyleBackColor = true;
            // 
            // checkMirror
            // 
            this.checkMirror.AutoSize = true;
            this.checkMirror.Location = new System.Drawing.Point(27, 137);
            this.checkMirror.Name = "checkMirror";
            this.checkMirror.Size = new System.Drawing.Size(149, 29);
            this.checkMirror.TabIndex = 0;
            this.checkMirror.Text = "Зеркальность";
            this.checkMirror.UseVisualStyleBackColor = true;
            // 
            // buttonChangeColor
            // 
            this.buttonChangeColor.Location = new System.Drawing.Point(111, 60);
            this.buttonChangeColor.Name = "buttonChangeColor";
            this.buttonChangeColor.Size = new System.Drawing.Size(148, 42);
            this.buttonChangeColor.TabIndex = 2;
            this.buttonChangeColor.Text = "Изменить цвет";
            this.buttonChangeColor.UseVisualStyleBackColor = true;
            this.buttonChangeColor.Click += new System.EventHandler(this.buttonChangeColor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 518);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.progressBar);
            this.Name = "Form1";
            this.Text = "Корнуэльская комната";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirrorValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown transparencyValue;
        private System.Windows.Forms.NumericUpDown mirrorValue;
        private System.Windows.Forms.CheckBox checkTransparency;
        private System.Windows.Forms.CheckBox checkMirror;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonChangeColor;
    }
}

