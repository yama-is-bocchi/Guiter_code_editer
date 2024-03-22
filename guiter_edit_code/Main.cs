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
        private List<string> cur_code_lists = new List<string>();
        private List<string> cur_roma_lists = new List<string>();
        private Control[]? active_btn;

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
                if (active_btn != null)
                {
                    active_btn[0].BackColor = Color.Black;
                    active_btn[0].ForeColor = Color.LimeGreen;
                    active_btn = null;
                }
                Init();
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                File_path = ofd.FileName;
                //ラベルに歌詞を表示
                read_song();
            }
        }

        private void Init()
        {
            song_label.Text = "";
            test.Text = "";
            cur_key = "";
            cur_code_lists.Clear();
            cur_roma_lists.Clear();
            song_lists.Clear();
            song_lists = new List<string>();
        }

        private void read_song()
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
                            song_lists.Add(liner);
                        }
                        else
                        {
                            song_label.Text += liner + "\n";
                            song_lists.Add(liner);
                        }
                    }

                }
                sr.Close();
            }

            //現在のコードリストを取得
            string? minus_or_measure;

            if (cur_key == null) return;

            if (cur_key.Contains("m"))
            {
                minus_or_measure = "minus";
                minus_radio.Checked = true;
            }
            else
            {
                minus_or_measure = "measure";
                measure_radio.Checked = true;
            }

            bool first = true;
            int count = 0;
            sr = new StreamReader(@"res\" + minus_or_measure + "_code.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() + "";
                    string[] values = line.Split("\n");

                    List<string> lists = new List<string>();
                    lists.AddRange(values);
                    foreach (string liner in lists)
                    {
                        string[] work = liner.Split(",");

                        if (first == true)
                        {
                            count = 0;
                            foreach (string code in work)
                            {
                                if (code == cur_key)
                                {
                                    first = false;
                                    cur_code_lists.Add(code);
                                    test.Text += code + "\n";
                                    break;
                                }

                                count++;
                            }
                            if (first == true)
                            {
                                cur_code_lists.Add("");
                                test.Text += "" + "\n";
                            }
                        }
                        else
                        {

                            test.Text += work[count] + "\n";
                            cur_code_lists.Add(work[count]);

                        }
                    }

                }
                sr.Close();
            }

            sr = new StreamReader(@"res\roma_num.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() + "";
                    string[] values = line.Split("\n");



                    List<string> lists = new List<string>();
                    lists.AddRange(values);
                    foreach (string liner in lists)
                    {
                        string[] work = liner.Split(",");



                        test.Text += work[count] + "\n";
                        cur_roma_lists.Add(work[count]);


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


        private int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        private List<string> replace_num()
        {
            List<string> ret_list = new List<string>();

            foreach (string item in song_lists)
            {
                string work = item;
                int eng_count = (Regex.Replace(item, "[^a-z]", "")).Length;
                int bigspace_count = CountChar(item, '　');
                int smallspace_count = CountChar(item, ' ');
                //ギターコードかどうか
                if (item.Contains("Original Key："))
                {
                    ret_list.Add(key_label.Text);
                }
                else if ((item.Contains("#") || item.Contains("7") || item.Contains("♭"))
                    || (bigspace_count > 4 || smallspace_count > 4) && eng_count < 6)
                {
                    int count = 0;

                    foreach (string element in cur_code_lists)
                    {

                        if (element.Contains("♭") && element != "")
                        {
                            work = work.Replace(element, cur_roma_lists[count]);
                        }

                        count++;
                    }

                    count = 0;

                    foreach (string element in cur_code_lists)
                    {
                        if (element.Contains("#") && element != "")
                        {
                            work = work.Replace(element, cur_roma_lists[count]);
                        }

                        count++;
                    }

                    count = 0;

                    foreach (string element in cur_code_lists)
                    {
                        if (element != "")
                        {
                            work = work.Replace(element, cur_roma_lists[count]);

                        }

                        count++;
                    }

                    ret_list.Add(work);

                }
                else
                {
                    
                        ret_list.Add(item);
                    
                }

            }
            return ret_list;
        }


        private List<string> replace_code(ref List<string> pre_list)
        {
            List<string> ret_list = new List<string>();

            foreach (string item in song_lists)
            {
                string work = item;
                int eng_count = (Regex.Replace(item, "[^a-z]", "")).Length;
                int bigspace_count = CountChar(item, '　');
                int smallspace_count = CountChar(item, ' ');
                //ギターコードかどうか
                if (item.Contains("Original Key："))
                {
                    ret_list.Add(key_label.Text);

                }
                else if ((item.Contains("#") || item.Contains("7") || item.Contains("♭"))
                    || (bigspace_count > 4 || smallspace_count > 4) && eng_count < 6)
                {
                    int count = 0;
                    foreach (string element in cur_code_lists)
                    {

                        if (element.Contains("♭") && pre_list[count] != "" && element != null)
                        {
                            work = work.Replace(pre_list[count], element.Replace("♭", "").ToLower() + "♭");
                        }
                        else if (element == null && pre_list[count] != null)
                        {
                            work = work.Replace(pre_list[count], "");
                        }
                        count++;
                    }

                    count = 0;
                    foreach (string element in cur_code_lists)
                    {

                        if (element.Contains("#") && pre_list[count] != "" && element != null)
                        {
                            work = work.Replace(pre_list[count], element.Replace("#", "").ToLower() + "#");
                        }
                        else if (element == null && pre_list[count] != null)
                        {
                            work = work.Replace(pre_list[count], "");
                        }
                        count++;
                    }

                    count = 0;
                    foreach (string element in cur_code_lists)
                    {

                        if (pre_list[count] != "" && !element.Contains("♭") && !element.Contains("#") && element != null)
                        {
                            work = work.Replace(pre_list[count], element.ToLower());
                        }
                        else if (element == null && pre_list[count] != "")
                        {
                            work = work.Replace(pre_list[count], "");
                        }
                        count++;
                    }
                    work = work.ToUpper();

                    work = work.Replace("M", "m");

                    ret_list.Add(work);
                }
                else
                {

                    ret_list.Add(item);

                }

            }
            song_lists = ret_list;

            return ret_list;
        }

        private void convertnum_btn_MouseClick(object sender, MouseEventArgs e)
        {
            List<string> write_list = replace_num();
            song_label.Text = "";
            if (File_path == null) return;

            using (StreamWriter sw = new StreamWriter(File_path.Replace(".txt","_"+cur_key+"_ローマ数字ver.txt"), false,
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
            comp_message.Start();
        }

        private void Code_MouseClick(object sender, MouseEventArgs e)
        {
            if (active_btn != null)
            {
                active_btn[0].BackColor = Color.Black;
                active_btn[0].ForeColor = Color.LimeGreen;
            }

            ((Button)sender).BackColor = Color.LimeGreen;
            ((Button)sender).ForeColor = Color.Black;

            active_btn = edit_panel.Controls.Find(((Button)sender).Name, true);

            string minus_or_measure = "";

            if (minus_radio.Checked == true) minus_or_measure = "m";

            key_label.Text = key_label.Text.Replace("Original Key：" + cur_key, "Original Key：" + ((Button)sender).Text + minus_or_measure);
            cur_key = ((Button)sender).Text + minus_or_measure;


            if (cur_key.Contains("m"))
            {
                minus_or_measure = "minus";
            }
            else
            {
                minus_or_measure = "measure";
            }

            string temp_key = cur_key.Replace("m","");

            bool first = true;
            int count = 0;

            List<string> temp_code_list = cur_code_lists;
            cur_code_lists = new List<string>();
            cur_roma_lists = new List<string>();
            test.Text = "";

            StreamReader sr = new StreamReader(@"res\" + minus_or_measure + "_code.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() + "";
                    string[] values = line.Split("\n");

                    List<string> lists = new List<string>();
                    lists.AddRange(values);
                    foreach (string liner in lists)
                    {
                        string[] work = liner.Split(",");

                        if (first == true)
                        {
                            count = 0;
                            foreach (string code in work)
                            {
                                
                                if (code == temp_key)
                                {
                                    first = false;
                                    cur_code_lists.Add(code);
                                    test.Text += code + "\n";
                                    break;
                                }
                             
                                count++;
                            }

                            if (first == true)
                            {
                                cur_code_lists.Add("");
                                test.Text += "" + "\n";
                            }
                        }
                        else
                        {

                            test.Text += work[count] + "\n";
                            cur_code_lists.Add(work[count]);

                        }
                    }

                }
                sr.Close();
            }

            sr = new StreamReader(@"res\roma_num.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() + "";
                    string[] values = line.Split("\n");

                    List<string> lists = new List<string>();
                    lists.AddRange(values);
                    foreach (string liner in lists)
                    {
                        string[] work = liner.Split(",");



                        test.Text += work[count] + "\n";
                        cur_roma_lists.Add(work[count]);


                    }

                }
                sr.Close();
            }


            List<string> write_lists =replace_code(ref temp_code_list);

            song_label.Text = "";
            if (File_path == null) return;


            using (StreamWriter sw = new StreamWriter(File_path.Replace(".txt","_"+cur_key+"_.txt"), false,
                                Encoding.GetEncoding("utf-8")))
            {
                foreach (string item in write_lists)
                {

                    sw.WriteLine(item);
                    song_label.Text += item + "\n";

                }

                sw.Close();
            }

            


        }



        private void comp_message_Tick(object sender, EventArgs e)
        {
            test.Text = "";
            comp_message.Stop();
        }

        private void measure_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (active_btn != null)
            {
                active_btn[0].BackColor = Color.Black;
                active_btn[0].ForeColor = Color.LimeGreen;
                active_btn = null;
            }
        }
    }
}
