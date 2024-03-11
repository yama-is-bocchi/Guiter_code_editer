namespace guiter_edit_code
{
    partial class Main
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
            Main_panel = new Panel();
            test = new Label();
            key_label = new Label();
            edit_panel = new Panel();
            convertnum_btn = new Button();
            song_label = new Label();
            add_btn = new Button();
            txt_select_btn = new Button();
            Main_panel.SuspendLayout();
            edit_panel.SuspendLayout();
            SuspendLayout();
            // 
            // Main_panel
            // 
            Main_panel.AutoScroll = true;
            Main_panel.BackColor = Color.Black;
            Main_panel.Controls.Add(test);
            Main_panel.Controls.Add(key_label);
            Main_panel.Controls.Add(edit_panel);
            Main_panel.Controls.Add(song_label);
            Main_panel.Controls.Add(add_btn);
            Main_panel.Controls.Add(txt_select_btn);
            Main_panel.Dock = DockStyle.Fill;
            Main_panel.Location = new Point(0, 0);
            Main_panel.Margin = new Padding(4, 3, 4, 3);
            Main_panel.Name = "Main_panel";
            Main_panel.Size = new Size(979, 514);
            Main_panel.TabIndex = 0;
            // 
            // test
            // 
            test.AutoSize = true;
            test.ForeColor = Color.LightGreen;
            test.Location = new Point(29, 426);
            test.Name = "test";
            test.Size = new Size(0, 15);
            test.TabIndex = 8;
            // 
            // key_label
            // 
            key_label.AutoSize = true;
            key_label.Font = new Font("MS UI Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            key_label.ForeColor = Color.LimeGreen;
            key_label.Location = new Point(3, 378);
            key_label.Name = "key_label";
            key_label.Size = new Size(0, 15);
            key_label.TabIndex = 7;
            // 
            // edit_panel
            // 
            edit_panel.Controls.Add(convertnum_btn);
            edit_panel.Location = new Point(12, 12);
            edit_panel.Margin = new Padding(4, 3, 4, 3);
            edit_panel.Name = "edit_panel";
            edit_panel.Size = new Size(211, 100);
            edit_panel.TabIndex = 6;
            edit_panel.Visible = false;
            // 
            // convertnum_btn
            // 
            convertnum_btn.Cursor = Cursors.Hand;
            convertnum_btn.FlatStyle = FlatStyle.Flat;
            convertnum_btn.ForeColor = Color.LimeGreen;
            convertnum_btn.Location = new Point(4, 11);
            convertnum_btn.Margin = new Padding(4, 3, 4, 3);
            convertnum_btn.Name = "convertnum_btn";
            convertnum_btn.Size = new Size(94, 68);
            convertnum_btn.TabIndex = 4;
            convertnum_btn.Text = "数値に変換";
            convertnum_btn.UseVisualStyleBackColor = true;
            convertnum_btn.MouseClick += convertnum_btn_MouseClick;
            // 
            // song_label
            // 
            song_label.AutoSize = true;
            song_label.Font = new Font("MS UI Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            song_label.ForeColor = Color.LimeGreen;
            song_label.Location = new Point(327, 12);
            song_label.Margin = new Padding(4, 0, 4, 0);
            song_label.Name = "song_label";
            song_label.Size = new Size(0, 19);
            song_label.TabIndex = 3;
            song_label.TextChanged += song_label_TextChanged;
            // 
            // add_btn
            // 
            add_btn.Cursor = Cursors.Hand;
            add_btn.FlatStyle = FlatStyle.Flat;
            add_btn.ForeColor = Color.LimeGreen;
            add_btn.Location = new Point(12, 247);
            add_btn.Margin = new Padding(4, 3, 4, 3);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(202, 68);
            add_btn.TabIndex = 2;
            add_btn.Text = "テキスト追加プログラム開始";
            add_btn.UseVisualStyleBackColor = true;
            add_btn.MouseClick += add_btn_MouseClick;
            // 
            // txt_select_btn
            // 
            txt_select_btn.Cursor = Cursors.Hand;
            txt_select_btn.FlatStyle = FlatStyle.Flat;
            txt_select_btn.ForeColor = Color.LimeGreen;
            txt_select_btn.Location = new Point(12, 137);
            txt_select_btn.Margin = new Padding(4, 3, 4, 3);
            txt_select_btn.Name = "txt_select_btn";
            txt_select_btn.Size = new Size(202, 68);
            txt_select_btn.TabIndex = 1;
            txt_select_btn.Text = "編集対象テキストを選択";
            txt_select_btn.UseVisualStyleBackColor = true;
            txt_select_btn.MouseClick += root_foldes_btn_MouseClick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(979, 514);
            Controls.Add(Main_panel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Main";
            Text = "Main";
            Main_panel.ResumeLayout(false);
            Main_panel.PerformLayout();
            edit_panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Main_panel;
        private Button add_btn;
        private Button txt_select_btn;
        private Label song_label;
        private Panel edit_panel;
        private Button convertnum_btn;
        private Label key_label;
        private Label test;
    }
}
