using System.Diagnostics;

namespace csv2sql
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox3 = new TextBox();
            label4 = new Label();
            button4 = new Button();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(48, 20);
            label1.Name = "label1";
            label1.Size = new Size(92, 21);
            label1.TabIndex = 0;
            label1.Text = "Work folder";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(48, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(581, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "c:\\temp";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(48, 250);
            label2.Name = "label2";
            label2.Size = new Size(124, 21);
            label2.TabIndex = 2;
            label2.Text = "Default datatype";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(48, 274);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(581, 23);
            textBox2.TabIndex = 3;
            textBox2.Text = "nvarchar(100)";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(48, 372);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 70;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(581, 392);
            dataGridView1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(48, 343);
            label3.Name = "label3";
            label3.Size = new Size(153, 21);
            label3.TabIndex = 5;
            label3.Text = "Data type exceptions";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(299, 338);
            button1.Name = "button1";
            button1.Size = new Size(75, 29);
            button1.TabIndex = 6;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(219, 339);
            button2.Name = "button2";
            button2.Size = new Size(75, 29);
            button2.TabIndex = 7;
            button2.Text = "Load";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(173, 835);
            button3.Name = "button3";
            button3.Size = new Size(380, 35);
            button3.TabIndex = 8;
            button3.Text = "Start";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(48, 104);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(581, 23);
            textBox3.TabIndex = 10;
            textBox3.Text = "c:\\example.csv";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(48, 80);
            label4.Name = "label4";
            label4.Size = new Size(64, 21);
            label4.TabIndex = 9;
            label4.Text = "CSV file";
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(638, 100);
            button4.Name = "button4";
            button4.Size = new Size(75, 29);
            button4.TabIndex = 11;
            button4.Text = "Choose";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(44, 153);
            label5.Name = "label5";
            label5.Size = new Size(78, 21);
            label5.TabIndex = 12;
            label5.Text = "Batch size";
            label5.Click += label5_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(48, 177);
            numericUpDown1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 13;
            numericUpDown1.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(638, 38);
            button5.Name = "button5";
            button5.Size = new Size(75, 29);
            button5.TabIndex = 14;
            button5.Text = "Choose";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 1008);
            Controls.Add(button5);
            Controls.Add(numericUpDown1);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private DataGridView dataGridView1;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox3;
        private Label label4;
        private Button button4;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Button button5;
    }


}