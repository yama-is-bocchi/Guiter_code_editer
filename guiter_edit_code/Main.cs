using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace guiter_edit_code
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }
        private string? File_path;
        private List<string> song_lists = new List<string>();
        private List<string> cur_num_lists = new List<string>();
        private List<string> cur_sharp_lists = new List<string>();

        private string? cur_key;

        private void root_foldes_btn_MouseClick(object sender, MouseEventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath.Replace("guiter_edit_code.exe", "") + @"codes\";
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                File_path = ofd.FileName;
                //ラベルに歌詞を表示
                write_song();

                insert_cur_arr();

            }
        }

        private void Init()
        {
            song_label.Text = "";
            test.Text = "";
            cur_key = "";
            song_lists = new List<string>();
            cur_num_lists = new List<string>();
        }

        private void write_song()
        {

            Init();
            if (File_path == null) return;
            StreamReader sr = new StreamReader(File_path);//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() + "";
                    string[] values = line.Split("\n");

                    List<string> lists = new List<string>();
                    lists.AddRange(values);
                    foreach (string liner in lists)
                    {
                        if (liner.Contains("Original Key："))
                        {

                            key_label.Text = liner;
                            string p_l = liner;
                            cur_key = extract_p_key(ref p_l);
                            if (cur_key.Length > 2)
                            {
                                cur_key = cur_key.Substring(0, 2);
                            }
                            test.Text = cur_key;
                        }
                        song_label.Text += liner + "\n";
                        song_lists.Add(liner);
                    }

                }
                sr.Close();
            }
        }

        private string extract_p_key(ref string p_line)
        {
            string? ret_str = p_line;

            string[] ret_list = ret_str.Split(" / ");

            return ret_list[0].Replace("Original Key：", "");


        }

        private void add_btn_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = @"add_program\Scrayping_guitar_code.exe";
            Process? process = Process.Start(p);
            if (process == null) return;
            process.WaitForExit();
        }

        private void song_label_TextChanged(object sender, EventArgs e)
        {
            if (song_label.Text.Length > 0)
            {
                edit_panel.Visible = true;
            }
            else
            {
                edit_panel.Visible = false;
            }
        }

        private void insert_cur_arr()
        {
            if (cur_key == null) return;
            bool existed = false;
            List<string> temp = new List<string>();

            for (int i = 0; i < 18; i++)
            {
                if (Common.readOnlycode[i] == cur_key)
                {
                    existed = true;
                    cur_num_lists.Add(Common.readOnlycode[i]);

                }
                else if (existed == true)
                {
                    cur_num_lists.Add(Common.readOnlycode[i]);
                }
                else
                {
                    temp.Add(Common.readOnlycode[i]);
                }
            }

            if (temp.Count > 0)
            {
                foreach (string element in temp)
                {
                    cur_num_lists.Add((string)element);
                }
            }

            foreach (string s in cur_num_lists)
            {
                if (s.Contains("#") || s.Contains("♭"))
                {
                    cur_sharp_lists.Add(s);
                }
            }
        }

        private int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        private List<string> replace_num()
        {
            string? pre_line = null;
            List<string> ret_list = new List<string>();


            int eng_count;
            int bigspace_count;
            int smallspace_count;
            int count;

            foreach (string item in song_lists)
            {

                eng_count = 0;
                bigspace_count = 0;
                smallspace_count = 0;
                count = 0;
                string work = item;

                foreach (string num in Common.num_List)
                {
                    if (cur_num_lists[count].Contains("♭"))
                    {
                        work = work.Replace(cur_num_lists[count], num);
                    }
                    count++;

                }
                count = 0;
                foreach (string num in Common.num_List)
                {
                    if (cur_num_lists[count].Contains("#"))
                    {
                        work = work.Replace(cur_num_lists[count], num);
                    }
                    count++;

                }
                count = 0;

                foreach (string num in Common.num_List)
                {
                        work = work.Replace(cur_num_lists[count], num);
                    
                    count++;

                }


                eng_count = (Regex.Replace(item, "[^a-zA-Z]", "")).Length;
                bigspace_count = CountChar(item, '　');
                smallspace_count = CountChar(item, ' ');

                if (pre_line == null)
                {
                    ret_list.Add(item);
                }
                else if (item.Contains("#") || item.Contains("♭") || item.Contains("/") || item.Contains("7"))
                {
                    ret_list.Add(work);
                }
                else if (eng_count > 8)
                {
                    ret_list.Add(item);
                }
                else if (eng_count < 5)
                {
                    ret_list.Add(work);
                }
                else if (bigspace_count > 4 || smallspace_count > 4)
                {
                    ret_list.Add(work);
                }
                else
                {
                    ret_list.Add(item);
                }

                pre_line = item;
            }


            return ret_list;
        }

        private void convertnum_btn_MouseClick(object sender, MouseEventArgs e)
        {
            List<string> write_list = replace_num();
            song_label.Text = "";
            if (File_path == null) return;
            using (StreamWriter sw = new StreamWriter(File_path.Replace(".txt", "_numver.txt"), false,
                                Encoding.GetEncoding("utf-8")))
            {
                foreach (string item in write_list)
                {

                    sw.WriteLine(item);
                    song_label.Text += item + "\n";

                }

                sw.Close();
            }
            test.Text = "完了しました";
        }
    }
}
