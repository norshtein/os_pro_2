using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osproject
{
    public partial class Form1 : Form
    {
        private int memory_size = 640;
        private System.Collections.ArrayList freespace = new System.Collections.ArrayList();
        private System.Collections.ArrayList usedspace = new System.Collections.ArrayList();
        //private FileStream instrction = new FileStream("instrction.txt", FileMode.OpenOrCreate, FileAccess.Read);
        private StreamReader instruction = new StreamReader("instruction.txt");
        //private Algorithm method = Algorithm.BESTFIT;
        bool started = false;

        private void repaintPanel()
        {
            Graphics g = memory_panel.CreateGraphics();
            memory_panel.Show();
            for(int freeindex = 0;freeindex < freespace.Count;freeindex++)
            {
                SpaceNode nowNode = (SpaceNode)freespace[freeindex];

                g.FillRectangle((Brush)Brushes.Green,
                0, (int)((double)(nowNode.begin - 1) / memory_size * memory_panel.Height),
                memory_panel.Width, (int)((double)nowNode.capacity / memory_size * memory_panel.Height));

                g.DrawLine(new Pen(Color.Black, 2),
                    new Point(0, (int)((double)nowNode.end / memory_size * memory_panel.Height)),
                    new Point(memory_panel.Width, (int)((double)nowNode.end / memory_size * memory_panel.Height)));

                g.DrawString(String.Format("FREE MEMORY:{0} BYTE", nowNode.capacity),
                    Font, (Brush)Brushes.Black,
                    memory_panel.Width / 2 - 50,
                    (int)((double)(nowNode.begin - 1) / memory_size * memory_panel.Height) + (int)((double)nowNode.capacity / memory_size * memory_panel.Height) / 2 - 4);
            }

            for(int usedindex = 0;usedindex < usedspace.Count;usedindex++)
            {
                SpaceNode nowNode = (SpaceNode)usedspace[usedindex];

                g.FillRectangle((Brush)Brushes.Red,
                0, (int)((double)(nowNode.begin - 1) / memory_size * memory_panel.Height),
                memory_panel.Width, (int)((double)nowNode.capacity / memory_size * memory_panel.Height));

                g.DrawLine(new Pen(Color.Black, 2),
                    new Point(0, (int)((double)nowNode.end / memory_size * memory_panel.Height)),
                    new Point(memory_panel.Width, (int)((double)nowNode.end / memory_size * memory_panel.Height)));

                g.DrawString(String.Format("{0} USE MEMORY:{1} BYTE", nowNode.jobname, nowNode.capacity),
                    Font, (Brush)Brushes.Black,
                    memory_panel.Width / 2 - 60,
                    (int)((double)(nowNode.begin - 1) / memory_size * memory_panel.Height) + (int)((double)nowNode.capacity / memory_size * memory_panel.Height) / 2 - 4);
            }
                            
        }
        public Form1()
        {
            freespace.Add(new SpaceNode(1, memory_size, memory_size));
            InitializeComponent();
        }

        private void memory_panel_Paint(object sender, PaintEventArgs e)
        {
            repaintPanel();
        }

        private void algorithm_choose_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            string algorithm_string = algorithm_choose_box.Text;
            if (!algorithm_string.Equals("首次适应算法") && !algorithm_string.Equals("最佳适应算法"))
            {
                MessageBox.Show("请选择首次适应算法或最佳适应算法！");
                return;
            }
            string s = instruction.ReadLine();
            if(s == null)
            {
                MessageBox.Show("文件已读取完毕！");
                return;
            }
            Regex pattern = new Regex(@"^(?<jobname>\w+) (?<operation>alloc|free) (?<size>\d+)$");
            if(!pattern.IsMatch(s))
            {
                MessageBox.Show("无效指令，请检查文件！");
                return;
            }
            if (!started)
            {
                started = true;
            }

            Match match = pattern.Match(s);
            string jobname = match.Groups["jobname"].Value;
            string operation = match.Groups["operation"].Value;
            int size = Convert.ToInt32(match.Groups["size"].Value);
            

            if(operation.Equals("alloc"))
            {
                for(int i = 0;i < usedspace.Count;i++)
                {
                    if(((SpaceNode)usedspace[i]).jobname.Equals(jobname))
                    {
                        MessageBox.Show(String.Format("指令{0}非法！已有同名任务存在于内存中，将执行下一条指令",s));
                        instruction_displayer.Text += s + " [failed]" + '\n';
                        return;
                    }
                }
                Algorithm method = algorithm_string.Equals("首次适应算法") ? Algorithm.FIRSTFIT : Algorithm.BESTFIT;
                int index = -1;
                if(method == Algorithm.FIRSTFIT)
                {
                    int begin = memory_size + 10;
                    for(int i = 0;i < freespace.Count;i++)
                    {
                        if(((SpaceNode)freespace[i]).capacity >= size && ((SpaceNode)freespace[i]).begin < begin)
                        {
                            begin = ((SpaceNode)freespace[i]).begin;
                            index = i;
                        }
                    }
                }
                if(method == Algorithm.BESTFIT)
                {
                    int min_capacity = memory_size + 10;
                    for(int i = 0;i < freespace.Count;i++)
                    {
                        if(((SpaceNode)freespace[i]).capacity >= size && ((SpaceNode)freespace[i]).capacity < min_capacity)
                        {
                            min_capacity = ((SpaceNode)freespace[i]).capacity;
                            index = i;
                        }
                    }
                }
                if (index == -1)
                {
                    MessageBox.Show(String.Format("内存不足，指令{0}执行失败，将执行下一条指令",s));
                    instruction_displayer.Text += s + " [failed]" + '\n';
                    return;
                }

                usedspace.Add(new SpaceNode(((SpaceNode)freespace[index]).begin, ((SpaceNode)freespace[index]).begin + size - 1, size, jobname));
                ((SpaceNode)freespace[index]).capacity -= size;
                ((SpaceNode)freespace[index]).begin += size;
                if(((SpaceNode)freespace[index]).begin == ((SpaceNode)freespace[index]).end + 1)
                    freespace.RemoveAt(index);
                
                
            }

            if(operation.Equals("free"))
            {
                int usedindex = -1;
                for(int i = 0;i < usedspace.Count;i++)
                {
                    if(((SpaceNode)usedspace[i]).jobname.Equals(jobname))
                    {
                        usedindex = i;
                        break;
                    }
                }
                if(usedindex == -1)
                {
                    MessageBox.Show(String.Format("在内存中寻找不到{0}，指令{1}执行失败，将执行下一条指令", jobname,s));
                    instruction_displayer.Text += s + " [failed]" + '\n';
                    return;
                }
                if(((SpaceNode)usedspace[usedindex]).capacity == size)
                {
                    freespace.Add(new SpaceNode(((SpaceNode)usedspace[usedindex]).begin,
                    ((SpaceNode)usedspace[usedindex]).end, ((SpaceNode)usedspace[usedindex]).capacity));
                    usedspace.RemoveAt(usedindex);
                }
                else
                {
                    freespace.Add(new SpaceNode(((SpaceNode)usedspace[usedindex]).begin,
                        ((SpaceNode)usedspace[usedindex]).begin + size - 1, size));
                    ((SpaceNode)usedspace[usedindex]).begin += size;
                    ((SpaceNode)usedspace[usedindex]).capacity -= size;
                }
                freespace.Sort(new SortWithBeginTime());
                for(int i = 1;i < freespace.Count;i++)
                {
                    SpaceNode preNode = (SpaceNode)freespace[i - 1];
                    SpaceNode nowNode = (SpaceNode)freespace[i];
                    if(preNode.end + 1 == nowNode.begin)
                    {
                        preNode.end = nowNode.end;
                        preNode.capacity += nowNode.capacity;
                        freespace.RemoveAt(i);
                        i--;
                    }
                }
            }
            memory_panel.CreateGraphics().Clear(Color.FromArgb(255, 255, 255, 255));
            repaintPanel();
            instruction_displayer.Text += s + '\n';
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            if (started)
            {
                MessageBox.Show("程序已开始，无法重新指定内存大小！");
                return;
            }
            int memory = 0;
            try
            {
                memory = Convert.ToInt32(memory_text.Text);
            }
             catch(Exception exce)
            {
                MessageBox.Show("Invaild Input!");
                return;
            }
            
            memory_size = memory;
            freespace.Clear();
            usedspace.Clear();
            freespace.Add(new SpaceNode(1, memory_size, memory_size));
            memory_panel.CreateGraphics().Clear(Color.FromArgb(255,255,255,255));
            repaintPanel();
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            started = false;
            freespace.Clear();
            usedspace.Clear();
            freespace.Add(new SpaceNode(1, memory_size, memory_size));
            instruction_displayer.Text = "";
            instruction.Close();
            instruction = new StreamReader("instruction.txt");
            memory_panel.CreateGraphics().Clear(Color.FromArgb(255, 255, 255, 255));
            repaintPanel();
        }
    }
    public class SpaceNode
    {
        public int begin, end;
        public int capacity;
        public string jobname;
        public SpaceNode(int _begin,int _end,int _capacity)
        {
            begin = _begin;
            end = _end;
            capacity = _capacity;
        }
        public SpaceNode(int _begin, int _end, int _capacity, string _jobname)
        {
            begin = _begin;
            end = _end;
            capacity = _capacity;
            jobname = _jobname;
        }
    }
    public enum Algorithm
    {
        FIRSTFIT,
        BESTFIT
    }
    public class SortWithBeginTime : System.Collections.IComparer
    {
        public int Compare(object a,object b)
        {
            return ((SpaceNode)a).begin - ((SpaceNode)b).begin;
        }
    }
}
