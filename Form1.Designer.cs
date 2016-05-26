namespace osproject
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.memory_panel = new System.Windows.Forms.Panel();
            this.algorithm_choose_box = new System.Windows.Forms.ComboBox();
            this.next_step_button = new System.Windows.Forms.Button();
            this.input_size_label = new System.Windows.Forms.Label();
            this.memory_text = new System.Windows.Forms.TextBox();
            this.submit_button = new System.Windows.Forms.Button();
            this.algorithm_choose_label = new System.Windows.Forms.Label();
            this.instruction_displayer = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // memory_panel
            // 
            this.memory_panel.Location = new System.Drawing.Point(12, 12);
            this.memory_panel.Margin = new System.Windows.Forms.Padding(0);
            this.memory_panel.Name = "memory_panel";
            this.memory_panel.Size = new System.Drawing.Size(399, 443);
            this.memory_panel.TabIndex = 0;
            this.memory_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.memory_panel_Paint);
            // 
            // algorithm_choose_box
            // 
            this.algorithm_choose_box.FormattingEnabled = true;
            this.algorithm_choose_box.Items.AddRange(new object[] {
            "首次适应算法",
            "最佳适应算法"});
            this.algorithm_choose_box.Location = new System.Drawing.Point(478, 130);
            this.algorithm_choose_box.Name = "algorithm_choose_box";
            this.algorithm_choose_box.Size = new System.Drawing.Size(121, 20);
            this.algorithm_choose_box.TabIndex = 1;
            this.algorithm_choose_box.Text = "最佳适应算法";
            this.algorithm_choose_box.SelectedIndexChanged += new System.EventHandler(this.algorithm_choose_box_SelectedIndexChanged);
            // 
            // next_step_button
            // 
            this.next_step_button.Location = new System.Drawing.Point(498, 156);
            this.next_step_button.Name = "next_step_button";
            this.next_step_button.Size = new System.Drawing.Size(75, 23);
            this.next_step_button.TabIndex = 2;
            this.next_step_button.Text = "next step";
            this.next_step_button.UseVisualStyleBackColor = true;
            this.next_step_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // input_size_label
            // 
            this.input_size_label.AutoSize = true;
            this.input_size_label.Location = new System.Drawing.Point(445, 47);
            this.input_size_label.Name = "input_size_label";
            this.input_size_label.Size = new System.Drawing.Size(191, 12);
            this.input_size_label.TabIndex = 3;
            this.input_size_label.Text = "Please input total memory size:";
            // 
            // memory_text
            // 
            this.memory_text.Location = new System.Drawing.Point(488, 62);
            this.memory_text.Name = "memory_text";
            this.memory_text.Size = new System.Drawing.Size(100, 21);
            this.memory_text.TabIndex = 4;
            this.memory_text.Text = "640";
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(498, 89);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(75, 23);
            this.submit_button.TabIndex = 5;
            this.submit_button.Text = "submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // algorithm_choose_label
            // 
            this.algorithm_choose_label.AutoSize = true;
            this.algorithm_choose_label.Location = new System.Drawing.Point(462, 115);
            this.algorithm_choose_label.Name = "algorithm_choose_label";
            this.algorithm_choose_label.Size = new System.Drawing.Size(161, 12);
            this.algorithm_choose_label.TabIndex = 6;
            this.algorithm_choose_label.Text = "Please choose an algorithm";
            // 
            // instruction_displayer
            // 
            this.instruction_displayer.Location = new System.Drawing.Point(436, 196);
            this.instruction_displayer.Name = "instruction_displayer";
            this.instruction_displayer.Size = new System.Drawing.Size(200, 243);
            this.instruction_displayer.TabIndex = 7;
            this.instruction_displayer.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 467);
            this.Controls.Add(this.instruction_displayer);
            this.Controls.Add(this.algorithm_choose_label);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.memory_text);
            this.Controls.Add(this.input_size_label);
            this.Controls.Add(this.next_step_button);
            this.Controls.Add(this.algorithm_choose_box);
            this.Controls.Add(this.memory_panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel memory_panel;
        private System.Windows.Forms.ComboBox algorithm_choose_box;
        private System.Windows.Forms.Button next_step_button;
        private System.Windows.Forms.Label input_size_label;
        private System.Windows.Forms.TextBox memory_text;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.Label algorithm_choose_label;
        private System.Windows.Forms.RichTextBox instruction_displayer;
    }
}

