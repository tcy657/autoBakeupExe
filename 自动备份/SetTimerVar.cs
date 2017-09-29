using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; //文件读写命名空间

namespace 自动备份
{
    public partial class SetTimerVar : Form
    {
        public  int[] cboAuto = new int[8+25+60];     //定义8+25+60=93个整型数元素数组，每个元素的初值为0
       
        //0-6,day; 7为每天 
        //8-31, Hour; 32为每时
        //33-92,Minute
        public string[] cboAutoText = {"Monday","Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday","EveryDay",
                                       "00","01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19","20","21","22","23","EveryHour",
                                       "00","01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19",
                                       "20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39",
                                       "40","41","42","43","44","45","46","47","48","49","50","51","52","53","54","55","56","57","58","59",      
                                     }; //定义8+25+60=93个字符串数组，每个元素的初值为复选框形式描述名

        //保存数据到文件
        string filename = "fhBK.ini";

        //获取启动了应用程序的可执行文件的路径，“D：\fh_bk”形式，末尾不带“\”
        string workPath = System.Windows.Forms.Application.StartupPath;
        
        public SetTimerVar()
        {
            InitializeComponent();
        }

        private void cboDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //窗体2-默认加载
        private void SetTimerVar_Load(object sender, EventArgs e)
        {
            try
            {
                //检查目录下fhBK.ini文件是否存在
                if (File.Exists(workPath + @"\" + filename))
                {
                    //存在
                    string[] stringlines = File.ReadAllLines(workPath + @"\" + filename, Encoding.Default);
                    string input = stringlines[0];

                    for (int Fi = 7; Fi < input.Length; Fi++)   //去掉“string=”
                    {
                        //将字符串“1”转化为数字1
                        int x = Convert.ToInt32(input[Fi]) - 48;

                        //char x = input[Fi];
                        //处理函数(x);   
                        //逐个取  
                        //是零还是壹 = 字符串.substring(i, 1);
                        if (1 == x)
                        {
                            int Fii = Fi - 7;
                            if (0 == Fii)
                            {
                                chbD1.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D1
                            if (1 == Fii)
                            {
                                chbD2.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D2
                            if (2 == Fii)
                            {
                                chbD3.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D3
                            if (3 == Fii)
                            {
                                chbD4.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D4
                            if (4 == Fii)
                            {
                                chbD5.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D5
                            if (5 == Fii)
                            {
                                chbD6.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D6
                            if (6 == Fii)
                            {
                                chbD7.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //D7
                            if (7 == Fii)
                            {
                                chbDx.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //Dx
                            //8-32
                            if (8 == Fii)
                            {
                                chbH00.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H00
                            if (9 == Fii)
                            {
                                chbH01.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H01
                            if (10 == Fii)
                            {
                                chbH02.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H02
                            if (11 == Fii)
                            {
                                chbH03.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H03
                            if (12 == Fii)
                            {
                                chbH04.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H04
                            if (13 == Fii)
                            {
                                chbH05.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H05
                            if (14 == Fii)
                            {
                                chbH06.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H06
                            if (15 == Fii)
                            {
                                chbH07.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H07
                            if (16 == Fii)
                            {
                                chbH08.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H08
                            if (17 == Fii)
                            {
                                chbH09.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H09
                            if (18 == Fii)
                            {
                                chbH10.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H10
                            if (19 == Fii)
                            {
                                chbH11.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H11
                            if (20 == Fii)
                            {
                                chbH12.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H12
                            if (21 == Fii)
                            {
                                chbH13.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H13
                            if (22 == Fii)
                            {
                                chbH14.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H14
                            if (23 == Fii)
                            {
                                chbH15.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H15
                            if (24 == Fii)
                            {
                                chbH16.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H16
                            if (25 == Fii)
                            {
                                chbH17.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H17
                            if (26 == Fii)
                            {
                                chbH18.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H18
                            if (27 == Fii)
                            {
                                chbH19.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H19
                            if (28 == Fii)
                            {
                                chbH20.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H20
                            if (29 == Fii)
                            {
                                chbH21.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H21
                            if (30 == Fii)
                            {
                                chbH22.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H22
                            if (31 == Fii)
                            {
                                chbH23.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //H23
                            if (32 == Fii)
                            {
                                chbHxx.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //Hxx
                            //33-92
                            if (33 == Fii)
                            {
                                chbM00.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M00
                            if (34 == Fii)
                            {
                                chbM01.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M01
                            if (35 == Fii)
                            {
                                chbM02.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M02
                            if (36 == Fii)
                            {
                                chbM03.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M03
                            if (37 == Fii)
                            {
                                chbM04.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M04
                            if (38 == Fii)
                            {
                                chbM05.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M05
                            if (39 == Fii)
                            {
                                chbM06.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M06
                            if (40 == Fii)
                            {
                                chbM07.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M07
                            if (41 == Fii)
                            {
                                chbM08.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M08
                            if (42 == Fii)
                            {
                                chbM09.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M09
                            if (43 == Fii)
                            {
                                chbM10.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M10
                            if (44 == Fii)
                            {
                                chbM11.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M11
                            if (45 == Fii)
                            {
                                chbM12.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M12
                            if (46 == Fii)
                            {
                                chbM13.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M13
                            if (47 == Fii)
                            {
                                chbM14.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M14
                            if (48 == Fii)
                            {
                                chbM15.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M15
                            if (49 == Fii)
                            {
                                chbM16.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M16
                            if (50 == Fii)
                            {
                                chbM17.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M17
                            if (51 == Fii)
                            {
                                chbM18.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M18
                            if (52 == Fii)
                            {
                                chbM19.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M19
                            if (53 == Fii)
                            {
                                chbM20.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M20
                            if (54 == Fii)
                            {
                                chbM21.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M21
                            if (55 == Fii)
                            {
                                chbM22.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M22
                            if (56 == Fii)
                            {
                                chbM23.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M23
                            if (57 == Fii)
                            {
                                chbM24.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M24
                            if (58 == Fii)
                            {
                                chbM25.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M25
                            if (59 == Fii)
                            {
                                chbM26.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M26
                            if (60 == Fii)
                            {
                                chbM27.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M27
                            if (61 == Fii)
                            {
                                chbM28.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M28
                            if (62 == Fii)
                            {
                                chbM29.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M29
                            if (63 == Fii)
                            {
                                chbM30.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M30
                            if (64 == Fii)
                            {
                                chbM31.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M31
                            if (65 == Fii)
                            {
                                chbM32.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M32
                            if (66 == Fii)
                            {
                                chbM33.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M33
                            if (67 == Fii)
                            {
                                chbM34.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M34
                            if (68 == Fii)
                            {
                                chbM35.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M35
                            if (69 == Fii)
                            {
                                chbM36.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M36
                            if (70 == Fii)
                            {
                                chbM37.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M37
                            if (71 == Fii)
                            {
                                chbM38.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M38
                            if (72 == Fii)
                            {
                                chbM39.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M39
                            if (73 == Fii)
                            {
                                chbM40.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M40
                            if (74 == Fii)
                            {
                                chbM41.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M41
                            if (75 == Fii)
                            {
                                chbM42.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M42
                            if (76 == Fii)
                            {
                                chbM43.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M43
                            if (77 == Fii)
                            {
                                chbM44.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M44
                            if (78 == Fii)
                            {
                                chbM45.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M45
                            if (79 == Fii)
                            {
                                chbM46.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M46
                            if (80 == Fii)
                            {
                                chbM47.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M47
                            if (81 == Fii)
                            {
                                chbM48.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M48
                            if (82 == Fii)
                            {
                                chbM49.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M49
                            if (83 == Fii)
                            {
                                chbM50.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M50
                            if (84 == Fii)
                            {
                                chbM51.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M51
                            if (85 == Fii)
                            {
                                chbM52.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M52
                            if (86 == Fii)
                            {
                                chbM53.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M53
                            if (87 == Fii)
                            {
                                chbM54.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M54
                            if (88 == Fii)
                            {
                                chbM55.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M55
                            if (89 == Fii)
                            {
                                chbM56.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M56
                            if (90 == Fii)
                            {
                                chbM57.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M57
                            if (91 == Fii)
                            {
                                chbM58.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M58
                            if (92 == Fii)
                            {
                                chbM59.Checked = true;
                                cboAuto[Fii] = 1;
                            }
                            //M59


                            // MessageBox.Show(x.ToString(), "提示");
                        }

                    }


                }//存在文件
                else
                {
                    //不存在文件
                    File.Create(workPath + @"\" + filename);//创建该文件
                }
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("加载默认配置发生意外：" + objException.Message);                
            }
        }

        public void gboAuto_Enter(object sender, EventArgs e)
        {
            
        }

        //按键-取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确定-按键
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cboInput();

                //1.4 输入合法性检查，定时分钟到
                string textSTR = "未选中！";             //记录三者选中提示
                bool flagAutoDay = false;   //星期是否选中
                bool flagAutoHour = false;  //时钟是否选中
                bool flagAutoMin = false;   //分钟是否选中

                int chkMin = 17;    //分钟是否间隔15分钟
                string Minu = "分钟："; //记录分钟值；Minu.Lenght =3;

                for (int Di = 0; Di <= 7; Di++) //星期
                {
                    if (1 == cboAuto[Di])  //星期被选中
                    {
                        flagAutoDay = true;
                    }
                }

                for (int Hi = 8; Hi <= 32; Hi++) // 时钟
                {
                    if (1 == cboAuto[Hi]) // 时钟被选中
                    {
                        flagAutoHour = true;
                    }
                }

                for (int Mi = 33; Mi <= 92; Mi++) //分钟
                {
                    if (1 == cboAuto[Mi])  //分钟被选中             
                    {
                        flagAutoMin = true;

                        //分钟是否间隔15分钟
                        chkMin += 15;
                        if (Mi < chkMin) Minu += cboAutoText[Mi] + "/";

                        //保存当前复选框的值
                        chkMin = Mi;
                    }
                }

                if ((true == flagAutoDay) && (true == flagAutoHour) && (true == flagAutoMin)) //检查是否三者都选中
                {
                    if (3 < Minu.Length) ///三者都选中，检查分钟间隔是否小于15
                    {
                        MessageBox.Show("警告：" + Minu + "左右间隔小于15分钟，可能导致下一次备份失效！" + "\r\n", "提示");
                    }

                }
                else //三者未选全，且分钟间隔可能不合法
                {
                     //检查日期是否选中
                     if ( false == flagAutoDay)
                     {
                         textSTR = "日期/" + textSTR;
                     }

                     //检查时钟是否选中
                     if (false == flagAutoHour)
                     {
                         textSTR = "时钟/" + textSTR;
                     }
                     
                     //检查分钟是否选中
                     if (false == flagAutoMin)
                     {
                         textSTR = "分钟/" + textSTR;
                     }

                     //检查分钟间隔是否小于15
                     if (3 < Minu.Length) //分钟间隔小于15
                     {
                         MessageBox.Show("警告：" + Minu + "左右间隔小于15分钟，可能导致下一次备份失效！" + "\r\n" + "错误：" + textSTR, "提示");
                     }
                     else//分钟间隔大于15
                     {
                         MessageBox.Show("错误：" + textSTR, "提示");
                     }

                    //返回
                    return;
                }

                saveFile();
                this.Close();
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("保存设置参数时发生意外：" + objException.Message, "提示");
            }
        }


        private void chbDx_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //获取设置信息
                if (chbDx.Checked == true)
                {
                    //chbD1.Checked = true;
                    //chbD2.Checked = true;
                    //chbD3.Checked = true;
                    //chbD4.Checked = true;
                    //chbD5.Checked = true;
                    //chbD6.Checked = true;
                    //chbD7.Checked = true;
                    //关闭checkbox
                    chbD1.Enabled = false;
                    chbD2.Enabled = false;
                    chbD3.Enabled = false;
                    chbD4.Enabled = false;
                    chbD5.Enabled = false;
                    chbD6.Enabled = false;
                    chbD7.Enabled = false;
                }
                else
                {
                    //chbD1.Checked = false;
                    //chbD2.Checked = false;
                    //chbD3.Checked = false;
                    //chbD4.Checked = false;
                    //chbD5.Checked = false;
                    //chbD6.Checked = false;
                    //chbD7.Checked = false;
                    //打开checkbox
                    chbD1.Enabled = true;
                    chbD2.Enabled = true;
                    chbD3.Enabled = true;
                    chbD4.Enabled = true;
                    chbD5.Enabled = true;
                    chbD6.Enabled = true;
                    chbD7.Enabled = true;

                }
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("星期全选发生意外：" + objException.Message, "提示");
            }

        }

        private void chbHxx_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbHxx.Checked == true)
                {
                    //chbH00.Checked = true;
                    //chbH01.Checked = true;
                    //chbH02.Checked = true;
                    //chbH03.Checked = true;
                    //chbH04.Checked = true;
                    //chbH05.Checked = true;
                    //chbH06.Checked = true;
                    //chbH07.Checked = true;
                    //chbH08.Checked = true;
                    //chbH09.Checked = true;
                    //chbH10.Checked = true;
                    //chbH11.Checked = true;
                    //chbH12.Checked = true;
                    //chbH13.Checked = true;
                    //chbH14.Checked = true;
                    //chbH15.Checked = true;
                    //chbH16.Checked = true;
                    //chbH17.Checked = true;
                    //chbH18.Checked = true;
                    //chbH19.Checked = true;
                    //chbH20.Checked = true;
                    //chbH21.Checked = true;
                    //chbH22.Checked = true;
                    //chbH23.Checked = true;
                    //chbHxx.Checked = true;
                    //禁用checkbox
                    chbH00.Enabled = false;
                    chbH01.Enabled = false;
                    chbH02.Enabled = false;
                    chbH03.Enabled = false;
                    chbH04.Enabled = false;
                    chbH05.Enabled = false;
                    chbH06.Enabled = false;
                    chbH07.Enabled = false;
                    chbH08.Enabled = false;
                    chbH09.Enabled = false;
                    chbH10.Enabled = false;
                    chbH11.Enabled = false;
                    chbH12.Enabled = false;
                    chbH13.Enabled = false;
                    chbH14.Enabled = false;
                    chbH15.Enabled = false;
                    chbH16.Enabled = false;
                    chbH17.Enabled = false;
                    chbH18.Enabled = false;
                    chbH19.Enabled = false;
                    chbH20.Enabled = false;
                    chbH21.Enabled = false;
                    chbH22.Enabled = false;
                    chbH23.Enabled = false;
                }
                else
                {
                    //chbH00.Checked = false;
                    //chbH01.Checked = false;
                    //chbH02.Checked = false;
                    //chbH03.Checked = false;
                    //chbH04.Checked = false;
                    //chbH05.Checked = false;
                    //chbH06.Checked = false;
                    //chbH07.Checked = false;
                    //chbH08.Checked = false;
                    //chbH09.Checked = false;
                    //chbH10.Checked = false;
                    //chbH11.Checked = false;
                    //chbH12.Checked = false;
                    //chbH13.Checked = false;
                    //chbH14.Checked = false;
                    //chbH15.Checked = false;
                    //chbH16.Checked = false;
                    //chbH17.Checked = false;
                    //chbH18.Checked = false;
                    //chbH19.Checked = false;
                    //chbH20.Checked = false;
                    //chbH21.Checked = false;
                    //chbH22.Checked = false;
                    //chbH23.Checked = false;
                    //chbHxx.Checked = false;
                    //打开checkbox
                    chbH00.Enabled = true;
                    chbH01.Enabled = true;
                    chbH02.Enabled = true;
                    chbH03.Enabled = true;
                    chbH04.Enabled = true;
                    chbH05.Enabled = true;
                    chbH06.Enabled = true;
                    chbH07.Enabled = true;
                    chbH08.Enabled = true;
                    chbH09.Enabled = true;
                    chbH10.Enabled = true;
                    chbH11.Enabled = true;
                    chbH12.Enabled = true;
                    chbH13.Enabled = true;
                    chbH14.Enabled = true;
                    chbH15.Enabled = true;
                    chbH16.Enabled = true;
                    chbH17.Enabled = true;
                    chbH18.Enabled = true;
                    chbH19.Enabled = true;
                    chbH20.Enabled = true;
                    chbH21.Enabled = true;
                    chbH22.Enabled = true;
                    chbH23.Enabled = true;
                }
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("时钟全选发生意外：" + objException.Message, "提示");
            }
        }

        //应用-按键
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //保存数据到文件
                cboInput();

                //1.4 输入合法性检查，定时分钟到
                int flagAuto = 0;  //星期/时钟/分钟是否选中
                int chkMin = 17;    //分钟是否间隔15分钟
                string Minu = "分钟："; //记录分钟值；Minu.Lenght =3;

                for (int Di = 0; Di <= 7; Di++) //星期
                {
                    if (1 == cboAuto[Di])  //星期被选中
                    {
                        flagAuto = 1;
                        break;
                    }
                }

                for (int Hi = 8; Hi <= 32; Hi++) // 时钟
                {
                    if (1 == cboAuto[Hi]) // 时钟被选中
                    {
                        if (1 == flagAuto) flagAuto = 2;
                        break;
                    }
                }

                for (int Mi = 33; Mi <= 92; Mi++) //分钟
                {
                    if (1 == cboAuto[Mi])  //分钟被选中             
                    {
                        //分钟是否间隔15分钟
                        chkMin += 15;
                        if (Mi >= chkMin)
                        {
                            if (2 == flagAuto) flagAuto = 3;
                        }
                        else
                        {
                            Minu += cboAutoText[Mi] + "/";
                            //MessageBox.Show(Minu.Length.ToString(), "提示");

                        }
                        chkMin = Mi;
                    }
                }

                if (3 != flagAuto) //检查是否三者都选中
                {
                    if (3 < Minu.Length) //分钟间隔小于15
                    {
                        MessageBox.Show("警告：" + Minu + "左右间隔小于15分钟，可能导致下一次备份失效！" + "\r\n" + "错误：日期/时钟/分钟必须都要选中！", "提示");
                    }
                    else
                        MessageBox.Show("错误：日期/时钟/分钟必须都要选中！", "提示");

                    //返回
                    return;
                }
                else //三者都选中，只是分钟不合法
                    if (3 < Minu.Length) //分钟间隔小于15
                    {
                        MessageBox.Show("警告：" + Minu + "左右间隔小于15分钟，可能导致下一次备份失效！" + "\r\n", "提示");

                        //不返回
                        //return;
                    }


                saveFile();
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("应用按键时发生意外：" + objException.Message, "提示");
            }
        }

        //以下为新增函数
        private void cboInput()
        {
            try
            {
                //1、处理已有输入信息
                //1.1 获取设置信息
                if (chbD1.Checked == true) cboAuto[0] = 1; else cboAuto[0] = 0;
                if (chbD2.Checked == true) cboAuto[1] = 1; else cboAuto[1] = 0;
                if (chbD3.Checked == true) cboAuto[2] = 1; else cboAuto[2] = 0;
                if (chbD4.Checked == true) cboAuto[3] = 1; else cboAuto[3] = 0;
                if (chbD5.Checked == true) cboAuto[4] = 1; else cboAuto[4] = 0;
                if (chbD6.Checked == true) cboAuto[5] = 1; else cboAuto[5] = 0;
                if (chbD7.Checked == true) cboAuto[6] = 1; else cboAuto[6] = 0;
                if (chbDx.Checked == true) cboAuto[7] = 1; else cboAuto[7] = 0;
                //MessageBox.Show("1=" + cboAuto[0] +","+"2=" + cboAuto[1] +","+"3=" + cboAuto[2] +","+"4=" + cboAuto[3] +","+"5=" + cboAuto[4] +","+"6=" + cboAuto[5] +","+"7=" + cboAuto[6] +","+"8=" + cboAuto[7] +",", "提示");


                //1.2 时钟选中
                if (chbH00.Checked == true) cboAuto[8] = 1; else cboAuto[8] = 0;
                if (chbH01.Checked == true) cboAuto[9] = 1; else cboAuto[9] = 0;
                if (chbH02.Checked == true) cboAuto[10] = 1; else cboAuto[10] = 0;
                if (chbH03.Checked == true) cboAuto[11] = 1; else cboAuto[11] = 0;
                if (chbH04.Checked == true) cboAuto[12] = 1; else cboAuto[12] = 0;
                if (chbH05.Checked == true) cboAuto[13] = 1; else cboAuto[13] = 0;
                if (chbH06.Checked == true) cboAuto[14] = 1; else cboAuto[14] = 0;
                if (chbH07.Checked == true) cboAuto[15] = 1; else cboAuto[15] = 0;
                if (chbH08.Checked == true) cboAuto[16] = 1; else cboAuto[16] = 0;
                if (chbH09.Checked == true) cboAuto[17] = 1; else cboAuto[17] = 0;
                if (chbH10.Checked == true) cboAuto[18] = 1; else cboAuto[18] = 0;
                if (chbH11.Checked == true) cboAuto[19] = 1; else cboAuto[19] = 0;
                if (chbH12.Checked == true) cboAuto[20] = 1; else cboAuto[20] = 0;
                if (chbH13.Checked == true) cboAuto[21] = 1; else cboAuto[21] = 0;
                if (chbH14.Checked == true) cboAuto[22] = 1; else cboAuto[22] = 0;
                if (chbH15.Checked == true) cboAuto[23] = 1; else cboAuto[23] = 0;
                if (chbH16.Checked == true) cboAuto[24] = 1; else cboAuto[24] = 0;
                if (chbH17.Checked == true) cboAuto[25] = 1; else cboAuto[25] = 0;
                if (chbH18.Checked == true) cboAuto[26] = 1; else cboAuto[26] = 0;
                if (chbH19.Checked == true) cboAuto[27] = 1; else cboAuto[27] = 0;
                if (chbH20.Checked == true) cboAuto[28] = 1; else cboAuto[28] = 0;
                if (chbH21.Checked == true) cboAuto[29] = 1; else cboAuto[29] = 0;
                if (chbH22.Checked == true) cboAuto[30] = 1; else cboAuto[30] = 0;
                if (chbH23.Checked == true) cboAuto[31] = 1; else cboAuto[31] = 0;
                if (chbHxx.Checked == true) cboAuto[32] = 1; else cboAuto[32] = 0;

                //1.3 分钟选中
                if (chbM00.Checked == true) cboAuto[33] = 1; else cboAuto[33] = 0;
                if (chbM01.Checked == true) cboAuto[34] = 1; else cboAuto[34] = 0;
                if (chbM02.Checked == true) cboAuto[35] = 1; else cboAuto[35] = 0;
                if (chbM03.Checked == true) cboAuto[36] = 1; else cboAuto[36] = 0;
                if (chbM04.Checked == true) cboAuto[37] = 1; else cboAuto[37] = 0;
                if (chbM05.Checked == true) cboAuto[38] = 1; else cboAuto[38] = 0;
                if (chbM06.Checked == true) cboAuto[39] = 1; else cboAuto[39] = 0;
                if (chbM07.Checked == true) cboAuto[40] = 1; else cboAuto[40] = 0;
                if (chbM08.Checked == true) cboAuto[41] = 1; else cboAuto[41] = 0;
                if (chbM09.Checked == true) cboAuto[42] = 1; else cboAuto[42] = 0;
                if (chbM10.Checked == true) cboAuto[43] = 1; else cboAuto[43] = 0;
                if (chbM11.Checked == true) cboAuto[44] = 1; else cboAuto[44] = 0;
                if (chbM12.Checked == true) cboAuto[45] = 1; else cboAuto[45] = 0;
                if (chbM13.Checked == true) cboAuto[46] = 1; else cboAuto[46] = 0;
                if (chbM14.Checked == true) cboAuto[47] = 1; else cboAuto[47] = 0;
                if (chbM15.Checked == true) cboAuto[48] = 1; else cboAuto[48] = 0;
                if (chbM16.Checked == true) cboAuto[49] = 1; else cboAuto[49] = 0;
                if (chbM17.Checked == true) cboAuto[50] = 1; else cboAuto[50] = 0;
                if (chbM18.Checked == true) cboAuto[51] = 1; else cboAuto[51] = 0;
                if (chbM19.Checked == true) cboAuto[52] = 1; else cboAuto[52] = 0;
                if (chbM20.Checked == true) cboAuto[53] = 1; else cboAuto[53] = 0;
                if (chbM21.Checked == true) cboAuto[54] = 1; else cboAuto[54] = 0;
                if (chbM22.Checked == true) cboAuto[55] = 1; else cboAuto[55] = 0;
                if (chbM23.Checked == true) cboAuto[56] = 1; else cboAuto[56] = 0;
                if (chbM24.Checked == true) cboAuto[57] = 1; else cboAuto[57] = 0;
                if (chbM25.Checked == true) cboAuto[58] = 1; else cboAuto[58] = 0;
                if (chbM26.Checked == true) cboAuto[59] = 1; else cboAuto[59] = 0;
                if (chbM27.Checked == true) cboAuto[60] = 1; else cboAuto[60] = 0;
                if (chbM28.Checked == true) cboAuto[61] = 1; else cboAuto[61] = 0;
                if (chbM29.Checked == true) cboAuto[62] = 1; else cboAuto[62] = 0;
                if (chbM30.Checked == true) cboAuto[63] = 1; else cboAuto[63] = 0;
                if (chbM31.Checked == true) cboAuto[64] = 1; else cboAuto[64] = 0;
                if (chbM32.Checked == true) cboAuto[65] = 1; else cboAuto[65] = 0;
                if (chbM33.Checked == true) cboAuto[66] = 1; else cboAuto[66] = 0;
                if (chbM34.Checked == true) cboAuto[67] = 1; else cboAuto[67] = 0;
                if (chbM35.Checked == true) cboAuto[68] = 1; else cboAuto[68] = 0;
                if (chbM36.Checked == true) cboAuto[69] = 1; else cboAuto[69] = 0;
                if (chbM37.Checked == true) cboAuto[70] = 1; else cboAuto[70] = 0;
                if (chbM38.Checked == true) cboAuto[71] = 1; else cboAuto[71] = 0;
                if (chbM39.Checked == true) cboAuto[72] = 1; else cboAuto[72] = 0;
                if (chbM40.Checked == true) cboAuto[73] = 1; else cboAuto[73] = 0;
                if (chbM41.Checked == true) cboAuto[74] = 1; else cboAuto[74] = 0;
                if (chbM42.Checked == true) cboAuto[75] = 1; else cboAuto[75] = 0;
                if (chbM43.Checked == true) cboAuto[76] = 1; else cboAuto[76] = 0;
                if (chbM44.Checked == true) cboAuto[77] = 1; else cboAuto[77] = 0;
                if (chbM45.Checked == true) cboAuto[78] = 1; else cboAuto[78] = 0;
                if (chbM46.Checked == true) cboAuto[79] = 1; else cboAuto[79] = 0;
                if (chbM47.Checked == true) cboAuto[80] = 1; else cboAuto[80] = 0;
                if (chbM48.Checked == true) cboAuto[81] = 1; else cboAuto[81] = 0;
                if (chbM49.Checked == true) cboAuto[82] = 1; else cboAuto[82] = 0;
                if (chbM50.Checked == true) cboAuto[83] = 1; else cboAuto[83] = 0;
                if (chbM51.Checked == true) cboAuto[84] = 1; else cboAuto[84] = 0;
                if (chbM52.Checked == true) cboAuto[85] = 1; else cboAuto[85] = 0;
                if (chbM53.Checked == true) cboAuto[86] = 1; else cboAuto[86] = 0;
                if (chbM54.Checked == true) cboAuto[87] = 1; else cboAuto[87] = 0;
                if (chbM55.Checked == true) cboAuto[88] = 1; else cboAuto[88] = 0;
                if (chbM56.Checked == true) cboAuto[89] = 1; else cboAuto[89] = 0;
                if (chbM57.Checked == true) cboAuto[90] = 1; else cboAuto[90] = 0;
                if (chbM58.Checked == true) cboAuto[91] = 1; else cboAuto[91] = 0;
                if (chbM59.Checked == true) cboAuto[92] = 1; else cboAuto[92] = 0;
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("处理输入信息时发生意外：" + objException.Message, "提示");
            }
       }



        private void saveFile()
        {
            try
            {
                //保存数据到文件
                string text = "string=";
                FileStream fs = new FileStream(workPath + @"\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                sw.Flush();
                sw.BaseStream.Seek(0, SeekOrigin.Begin);

                //获取控件值
                for (int i = 0; i < cboAuto.Length; i++)
                {
                    text += cboAuto[i];

                }
                sw.WriteLine(text);
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
                //MessageBox.Show("成功生成指定文件！","提示");
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("保存到文件时发生意外：" + objException.Message, "提示");
            }
        }

        public void gboAutoHour_Enter(object sender, EventArgs e)
        {

        }

        public void gboAutoMin_Enter(object sender, EventArgs e)
        {
            //定时时间到？
            //foreach (Control cl in gboAutoMin.Controls)
            //{
            //    CheckBox cb = cl as CheckBox;
            //    if (true == cb.Checked )
            //    {   // checkbox == true.
            //        //这10个复选框任意个数的选中情况有A1010中
            //        MessageBox.Show(cb.Text, "提示");
            //    }
            //    else
            //    {
            //        // checkbox != true.
            //    }
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {   
            try
            {
            //清除所有复选框
                 //不选，Day
                chbD1.Checked = false;
                chbD2.Checked = false;
                chbD3.Checked = false;
                chbD4.Checked = false;
                chbD5.Checked = false;
                chbD6.Checked = false;
                chbD7.Checked = false;
                chbDx.Checked = false;
                
                //不选，H
                chbH00.Checked = false;
                chbH01.Checked = false;
                chbH02.Checked = false;
                chbH03.Checked = false;
                chbH04.Checked = false;
                chbH05.Checked = false;
                chbH06.Checked = false;
                chbH07.Checked = false;
                chbH08.Checked = false;
                chbH09.Checked = false;
                chbH10.Checked = false;
                chbH11.Checked = false;
                chbH12.Checked = false;
                chbH13.Checked = false;
                chbH14.Checked = false;
                chbH15.Checked = false;
                chbH16.Checked = false;
                chbH17.Checked = false;
                chbH18.Checked = false;
                chbH19.Checked = false;
                chbH20.Checked = false;
                chbH21.Checked = false;
                chbH22.Checked = false;
                chbH23.Checked = false;
                chbHxx.Checked = false;
                
                //不选，Min
                 chbM00.Checked = false;
                 chbM01.Checked = false;
                 chbM02.Checked = false;
                 chbM03.Checked = false;
                 chbM04.Checked = false;
                 chbM05.Checked = false;
                 chbM06.Checked = false;
                 chbM07.Checked = false;
                 chbM08.Checked = false;
                 chbM09.Checked = false;
                 chbM10.Checked = false;
                 chbM11.Checked = false;
                 chbM12.Checked = false;
                 chbM13.Checked = false;
                 chbM14.Checked = false;
                 chbM15.Checked = false;
                 chbM16.Checked = false;
                 chbM17.Checked = false;
                 chbM18.Checked = false;
                 chbM19.Checked = false;
                 chbM20.Checked = false;
                 chbM21.Checked = false;
                 chbM22.Checked = false;
                 chbM23.Checked = false;
                 chbM24.Checked = false;
                 chbM25.Checked = false;
                 chbM26.Checked = false;
                 chbM27.Checked = false;
                 chbM28.Checked = false;
                 chbM29.Checked = false;
                 chbM30.Checked = false;
                 chbM31.Checked = false;
                 chbM32.Checked = false;
                 chbM33.Checked = false;
                 chbM34.Checked = false;
                 chbM35.Checked = false;
                 chbM36.Checked = false;
                 chbM37.Checked = false;
                 chbM38.Checked = false;
                 chbM39.Checked = false;
                 chbM40.Checked = false;
                 chbM41.Checked = false;
                 chbM42.Checked = false;
                 chbM43.Checked = false;
                 chbM44.Checked = false;
                 chbM45.Checked = false;
                 chbM46.Checked = false;
                 chbM47.Checked = false;
                 chbM48.Checked = false;
                 chbM49.Checked = false;
                 chbM50.Checked = false;
                 chbM51.Checked = false;
                 chbM52.Checked = false;
                 chbM53.Checked = false;
                 chbM54.Checked = false;
                 chbM55.Checked = false;
                 chbM56.Checked = false;
                 chbM57.Checked = false;
                 chbM58.Checked = false;
                 chbM59.Checked = false;
            }//try 
            catch (Exception objException)
            {
                MessageBox.Show("清除所有复选框时发生意外：" + objException.Message, "提示");
            }
        }

    }
}
