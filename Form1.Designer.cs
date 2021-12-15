
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
            this.buttonChangeColor = new System.Windows.Forms.Button();
            this.transparencyValue = new System.Windows.Forms.NumericUpDown();
            this.mirrorValue = new System.Windows.Forms.NumericUpDown();
            this.checkTransparency = new System.Windows.Forms.CheckBox();
            this.checkMirror = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkBoxAdditionalLight = new System.Windows.Forms.CheckBox();
            this.lightX = new System.Windows.Forms.NumericUpDown();
            this.lightY = new System.Windows.Forms.NumericUpDown();
            this.lightZ = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lightValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirrorValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightValue)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(240, 462);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(595, 44);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(841, 462);
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
            this.labelTime.Location = new System.Drawing.Point(12, 472);
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
            // checkBoxAdditionalLight
            // 
            this.checkBoxAdditionalLight.AutoSize = true;
            this.checkBoxAdditionalLight.Location = new System.Drawing.Point(41, 30);
            this.checkBoxAdditionalLight.Name = "checkBoxAdditionalLight";
            this.checkBoxAdditionalLight.Size = new System.Drawing.Size(306, 29);
            this.checkBoxAdditionalLight.TabIndex = 5;
            this.checkBoxAdditionalLight.Text = "Дополнительный источник света";
            this.checkBoxAdditionalLight.UseVisualStyleBackColor = true;
            this.checkBoxAdditionalLight.CheckedChanged += new System.EventHandler(this.checkBoxAdditionalLight_CheckedChanged);
            // 
            // lightX
            // 
            this.lightX.Enabled = false;
            this.lightX.Location = new System.Drawing.Point(68, 81);
            this.lightX.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightX.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.lightX.Name = "lightX";
            this.lightX.Size = new System.Drawing.Size(69, 31);
            this.lightX.TabIndex = 6;
            this.lightX.Value = new decimal(new int[] {
            7,
            0,
            0,
            -2147483648});
            this.lightX.ValueChanged += new System.EventHandler(this.lightX_ValueChanged);
            // 
            // lightY
            // 
            this.lightY.Enabled = false;
            this.lightY.Location = new System.Drawing.Point(175, 81);
            this.lightY.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightY.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.lightY.Name = "lightY";
            this.lightY.Size = new System.Drawing.Size(69, 31);
            this.lightY.TabIndex = 6;
            this.lightY.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.lightY.ValueChanged += new System.EventHandler(this.lightY_ValueChanged);
            // 
            // lightZ
            // 
            this.lightZ.Enabled = false;
            this.lightZ.Location = new System.Drawing.Point(282, 81);
            this.lightZ.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.lightZ.Name = "lightZ";
            this.lightZ.Size = new System.Drawing.Size(69, 31);
            this.lightZ.TabIndex = 6;
            this.lightZ.ValueChanged += new System.EventHandler(this.lightZ_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Z:";
            // 
            // lightValue
            // 
            this.lightValue.DecimalPlaces = 1;
            this.lightValue.Enabled = false;
            this.lightValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lightValue.Location = new System.Drawing.Point(259, 127);
            this.lightValue.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lightValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lightValue.Name = "lightValue";
            this.lightValue.Size = new System.Drawing.Size(69, 31);
            this.lightValue.TabIndex = 6;
            this.lightValue.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.lightValue.ValueChanged += new System.EventHandler(this.lightValue_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Интенсивность света:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lightY);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.checkBoxAdditionalLight);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lightX);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lightValue);
            this.groupBox3.Controls.Add(this.lightZ);
            this.groupBox3.Location = new System.Drawing.Point(608, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 173);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Свет";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 518);
            this.Controls.Add(this.groupBox3);
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
            ((System.ComponentModel.ISupportInitialize)(this.lightX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightValue)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxAdditionalLight;
        private System.Windows.Forms.NumericUpDown lightX;
        private System.Windows.Forms.NumericUpDown lightY;
        private System.Windows.Forms.NumericUpDown lightZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown lightValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

