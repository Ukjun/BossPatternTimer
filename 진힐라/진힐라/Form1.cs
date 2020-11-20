using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 진힐라
{
    public partial class Form1 : Form
    {
        //시작 시간
        static int count = 1634;

        //첫 패턴 주기 시간
        static int pattern = 150;

        //첫 낫베기 시간 = 27: 14
        static int first_pattern = 1484;

        //주기 변경 클릭 횟수
        static int checkCount = 0;

        static int after_pattern = 0;

        //패턴까지 남은 시간 초기변수
        int time_left = count - first_pattern;
        
        //시간으로 변환하는 함수
        DateTime datetime;

        //값 비교할 변수
        int compareInt = first_pattern;

        Boolean check = false;


        public Form1()
        {
            InitializeComponent();
            datetime = new DateTime();
            label1.Text = datetime.AddSeconds(count).ToString("mm:ss");
            //비교할 변수 처음 패턴 시간으로 지정
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            button1.Visible = false;
            label2.Visible = true;
            label6.Visible = true;
            label2.Text = datetime.AddSeconds(first_pattern).ToString("mm:ss");
            label6.Text = datetime.AddSeconds(pattern).ToString("mm분:ss초");
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = datetime.AddSeconds(count).ToString("mm:ss");

            //패턴 시간 도달했을 시1
            if (count == compareInt+1) {
                after_pattern = compareInt;
            }
            label6.Text = datetime.AddSeconds(time_left).ToString("mm분:ss초");
            count--;
        }

        //시간이 지났을때
        public void patternChange()
        {
            
            int temp = count;
            temp -= patternCount();
            label2.Text = datetime.AddSeconds(temp).ToString("mm:ss");
            time_left = count - temp;
            compareInt = temp;
            check = true;
            timer2.Start();
        }

        public void changePattern()
        {
            if(!check)
            {   
                compareInt += patternCount();
            }
            checkCount++;
            int temp = compareInt;
            temp -= patternCount();
            label2.Text = datetime.AddSeconds(temp).ToString("mm:ss");
            time_left = count - temp;
            check = false;
            timer2.Start();
        }

        public void showTime()
        {
            label2.Text = datetime.AddSeconds(after_pattern - patternCount()).ToString("mm:ss");
        }

        public int patternCount()
        {
            switch (checkCount)
            {
                case 1:
                    pattern = 125; 
                    return pattern;
                case 2:
                    pattern = 100;
                    return pattern;
                default:
                    pattern = 150;
                    return pattern;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show(temp.ToString());
            if(time_left <= 30)
            {
                label6.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Black;
            }
            
            if (time_left <= 1)
            {
                //patternChange();
                time_left = patternCount();
            }
            time_left--;
            
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                
                case Keys.F5:
                    {   
                        patternChange();
                        break;
                    }
                case Keys.F6:
                    {
                        changePattern();
                        break;
                    }
            }
        }
        public void nextPattern()
        {
            check = true;
            patternChange();
        }
    }
}
