
namespace Petri_Nets
{
    partial class TransitionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yLocation = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xLocation = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PriorityNumber = new System.Windows.Forms.NumericUpDown();
            this.allowTransition = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTransition = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLocation)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yLocation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xLocation);
            this.groupBox1.Location = new System.Drawing.Point(12, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 76);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // yLocation
            // 
            this.yLocation.Location = new System.Drawing.Point(29, 46);
            this.yLocation.Name = "yLocation";
            this.yLocation.Size = new System.Drawing.Size(118, 23);
            this.yLocation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "X:";
            // 
            // xLocation
            // 
            this.xLocation.Location = new System.Drawing.Point(29, 17);
            this.xLocation.Name = "xLocation";
            this.xLocation.Size = new System.Drawing.Size(118, 23);
            this.xLocation.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PriorityNumber);
            this.groupBox2.Controls.Add(this.allowTransition);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nameTransition);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 116);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // PriorityNumber
            // 
            this.PriorityNumber.Location = new System.Drawing.Point(54, 42);
            this.PriorityNumber.Name = "PriorityNumber";
            this.PriorityNumber.Size = new System.Drawing.Size(157, 23);
            this.PriorityNumber.TabIndex = 6;
            // 
            // allowTransition
            // 
            this.allowTransition.AutoSize = true;
            this.allowTransition.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.allowTransition.Location = new System.Drawing.Point(6, 71);
            this.allowTransition.Name = "allowTransition";
            this.allowTransition.Size = new System.Drawing.Size(112, 19);
            this.allowTransition.TabIndex = 4;
            this.allowTransition.Text = "Allow transition:";
            this.allowTransition.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Priority:";
            // 
            // nameTransition
            // 
            this.nameTransition.Location = new System.Drawing.Point(54, 16);
            this.nameTransition.Name = "nameTransition";
            this.nameTransition.Size = new System.Drawing.Size(159, 23);
            this.nameTransition.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TransitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 219);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TransitionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLocation)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown yLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown xLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox nameTransition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox allowTransition;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown PriorityNumber;
    }
}