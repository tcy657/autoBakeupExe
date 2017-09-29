﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO; //文件读写命名空间
using System.Text.RegularExpressions; //正则表达式
using System.Net.NetworkInformation; //ping测试站点是否可达
using Tamir.SharpSsh;    //第三方库 Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using System.Collections;
using System.Timers;
using Microsoft.Win32;  //操作注册表
using MySql.Data.MySqlClient; //C# 连接数据库MySql并执行查询命令
using System.Runtime.InteropServices; //从网管导入站点清单

namespace 自动备份
{
    
    public partial class Form1 : Form
    {
        string PC_Path = @"D:\fh_bakeup\";
        int flag_bk = 0;   //用于指示当前备份是否完成
        SetTimerVar frm = new SetTimerVar(); //调用窗体2
        string AutoOldTime = "19000101"; //设置“上一次备份值”为一个永远达不到的历史值，开始第一次备份
        //线程，不卡3
        //创建一个委托，是为访问TextBox控件服务的。
        public delegate void UpdateTxt(string log);
        //定义一个委托变量
        public UpdateTxt updateTxt;

        //修改TextBox值的方法。

        //此为在非创建线程中的调用方法，其实是使用TextBox的Invoke方法。
        //public void ThreadMethodTxt(int n)
        public void ThreadMethodTxt(string ip)
        {
            this.BeginInvoke(updateTxt, ip );
                //一秒 执行一次
                Thread.Sleep(1000);
        }

        //test1.1
        System.Timers.Timer t = new System.Timers.Timer(5000); //设置时间间隔为5秒


        //线程，不卡3
        //创建一个委托，是为访问TextBox控件服务的。
        public delegate void UpdateTxt_2(string log);
        //定义一个委托变量
        public UpdateTxt_2 updateTxt_2;

        //修改TextBox值的方法。

        //test1.3-事件。
        public void ThreadMethodTxt_2(string log)
        {
            int n = 5;
            for (int i = 0; i < n; i++)
            {
                this.BeginInvoke(updateTxt_2, log+","+i);
                //一秒 执行一次
                Thread.Sleep(1000);
            }
        }

        //保存“倒计时”/“定时一次”的比较值
        int SetHour = 0; //时钟
        int SetMin = 0;  //分钟
        int SetSec = 0;  //秒钟
        int BkMode = 0;  //定时方式
        DateTime DateTimeComp = new DateTime(2015, 05, 24, 02, 25,00);  //定时模式，比较专用
        string yyyyMMddHHmm = "0"; //用于记录备份开始后的时间值

        string fileNe; //导入站点文件名
        string fileListName="FileList.ini"; //文件清单文件名
        string NeListName = "IP.ini"; //站点清单文件名
        string backupResultListName = "BackupResult.log"; //备份结果文件名
        string CfgListName = "CFG.ini"; //配置清单文件名

        static string workPath = System.Windows.Forms.Application.StartupPath; //获取启动了应用程序的可执行文件的路径，“D：\fh_bk”形式，末尾不带“\”

        string[] NeTypeList =  {"Citrans 680",
                                                "Citrans R860",
                                                "Citrans R840",
                                                "Citrans R820",
                                                "Citrans R830",
                                                "Citrans R845",
                                                "Citrans R865",
                                                "Citrans R835E",
                                                "Citrans R830E",
                                                "Citrans 680v2",
                                                "Citrans R8000-10",
                                                "CiTRANS 690 U20",
                                                "Citrans R835Ev2",
                                                "CiTRANS 690 U10",
                                                "Citrans R8000-3",
                                                "Citrans R8000-5",
                                                "Citrans R810",
                                                "Citrans 680A",
                                                "CiTRANS 690 U30",
                                                "Citrans R820_V2",
                                                "CiTRANS R835Ev3" };//支持备份的站点

        string autoBackupLogPath = workPath + @"\autoBackup.log"; //自动备份助手的日志
        string failIpAddressPath = workPath + @"\failBackup.ini"; //记录备份失败的站点

        //读写ini文件, 2017.1.22
            /*传统的配置文件ini已有被xml文件逐步代替的趋势，但对于简单的配置，ini文件还是有用武之地的。
            ini文件其实就是一个文本文件，它有固定的格式，节Section的名字用[]括起来，然后换行说明key的值：
            [section]
            key=value
             */
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        #endregion
        #region 读Ini文件

        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region 写Ini文件
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        //end,读写ini文件
        
        //数字转换为IP
        public  string IntToIp(long ipInt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append((ipInt >> 24) & 0xFF).Append(".");  //转换成了1.15.198.4
                //sb.Append((ipInt >> 16) & 0xFF).Append(".");
                //sb.Append((ipInt >> 8) & 0xFF).Append(".");
                //sb.Append(ipInt & 0xFF);

                sb.Append(ipInt & 0xFF).Append(".");  //转换成了4.198.15.1
                sb.Append((ipInt >> 8) & 0xFF).Append(".");
                sb.Append((ipInt >> 16) & 0xFF).Append(".");
                sb.Append((ipInt >> 24) & 0xFF);
                return sb.ToString();
            }//try 
            catch (Exception objException)
            {
                output("数字转换为IP时发生意外：" + objException.Message);
                return "None";
            }
        }

        //删除文件夹
        /****************************************
        * 函数名称：DeleteFolder
        * 功能说明：递归删除文件夹目录及文件
        * 参    数：dir:文件夹路径
        * 调用示列：
        *           string dir = Server.MapPath("test/");  
        *           DotNet.Utilities.FileOperate.DeleteFolder(dir);       
       *****************************************/
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>  
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            try
            {
                if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
                {
                    foreach (string d in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                        
                        else
                            DeleteFolder(d); //递归删除子文件夹 
                    }
                    Directory.Delete(dir, true); //删除已空文件夹                 
                }
            }//try 
            catch (Exception objException)
            {
                //添加打印报错：非静态的字段、方法或属性“自动备份.Form1.output(string)”要求对象引用
                 ;
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        //F0.1 导出文件清单，保存到FileList.ini文件
        private void saveFileList()
        {
            try
            {
                //保存数据到文件
                FileStream fs = new FileStream(workPath + @"\" + fileListName, FileMode.Create, FileAccess.Write); //保存前，清空
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                sw.Flush();
                sw.BaseStream.Seek(0, SeekOrigin.Begin);

                //整理数据
                //遍历listview_ip中所有元素
                for (int i = 0; i < lstView_file.Items.Count; i++)
                {
                    sw.WriteLine(lstView_file.Items[i].Text);
                }
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
                //MessageBox.Show("成功生成指定文件！","提示");
            }//try 
            catch (Exception objException)
            {
                output("保存文件清单时发生意外：" + objException.Message);
            }
        }

        //F0.11 保存到文件，不带日期+时间。
        //补充，用于记录IP地址，不用检查文件大小。 2017.9.26
        private void save2File(string filePath, string log)
        {
            try
            {
                //保存数据到文件
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write); //追加
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码

                //整理数据
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
                //MessageBox.Show("成功生成指定文件！","提示");
            }//try 
            catch (Exception objException)
            {
                output("保存到文件(不带日期时间)时发生意外：" + objException.Message);
            }
        }

         //F0.11 保存到文件，带日期+时间
        private void save2FileTime(string filePath, string log)
        {
            try
            {
                //文件超过10M则删除
                if (File.Exists(filePath))
                {
                    System.IO.FileInfo MyFileInfo = new FileInfo(filePath);
                    int MyFileSize = (int)MyFileInfo.Length / (1024 * 1024);
                    if (MyFileSize > 10) //文件大于10M
                    {
                        File.Delete(filePath);
                    }
                }

                //保存数据到文件
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write); //追加
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码

                //整理数据
                sw.WriteLine(DateTime.Now.ToLocalTime().ToString() + "," +log);
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
                //MessageBox.Show("成功生成指定文件！","提示");
            }//try 
            catch (Exception objException)
            {
                //不处理。因为处理可能会导致死循环。若save2FileTime()出错，则save2FileTime()->output()->save2FileTime()
                //output("保存到文件(带时间)时发生意外：" + objException.Message);
                ;
            }
        }

        //F0.2 导出站点清单，保存到NeList.ini文件
        private void saveNeListName()
        {
            try
            {
                //保存数据到文件
                FileStream fs = new FileStream(workPath + @"\" + NeListName, FileMode.Create, FileAccess.Write); //保存前，清空
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                sw.Flush();
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                //整理数据
                //遍历listview_ip中所有元素
                for (int i = 0; i <lstMyListView.Items.Count; i++)
                {
                    sw.WriteLine(lstMyListView.Items[i].Text);
                }
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
                //MessageBox.Show("成功生成指定文件！","提示");
            }//try 
            catch (Exception objException)
            {
                output("保存文件清单时发生意外：" + objException.Message);
            }
        }

        //F0.3 保存配置到CFG.ini文件
        private void saveCfgListName(string Item, string Cfg)
        {
            try
            {
                    string  VarMode ="none";  //记录备份模式
                    string  VarStart ="none"; //记录是否开机启动
                    string  VarBkPath ="none"; //记录备份保存路径
                    string  VarCompTime = "none"; //记录工作时间点
                    
                    string[] sArray = {"none"};//记录配置值

                    //正则表达式匹配格式
                    string regformat = @"\=$";
                    Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
           
           //1、保存前先读取 
           //CfgListName文件存在，且不为空
            if (true == File.Exists(workPath + @"\" + CfgListName))
            {
                //存在，判断是否为空
                FileInfo fInfo = new FileInfo(workPath + @"\" + CfgListName);
                if (Convert.ToInt32(fInfo.Length.ToString()) >= 3)
                {
                    //存在,读取文件的所有内容
                    string[] stringlines = File.ReadAllLines(workPath + @"\" + CfgListName, Encoding.Default);

                    //去除文本中的空行
                    stringlines = Array.FindAll(stringlines, line => !string.IsNullOrEmpty(line));

                    for (int fi = 0; fi < stringlines.Length; fi++) //逐行处理
                    {
                        //读取配置
                        int flag= 0; //记录配置项是否存在
                        int flagOk = 0; //记录是否存在符号“=”，防止不存在导致数组跑飞


                        flagOk = stringlines[fi].ToString().IndexOf("=");  //检查是否存在“=”符号

                        if ((0 > flagOk) || (0 == flagOk) || (true == regex.IsMatch(stringlines[fi])))
                        {
                            continue; //读取下一行
                        }

                        flag = stringlines[fi].ToString().IndexOf("boAuto");  //1、模式选择 cboAuto 
                        if( 0 < flag)  //存在，则读取
                        {
                            sArray = stringlines[fi].Split('=');
                            if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                            {
                                VarMode = sArray[1];
                            }
                        }

                        flag = stringlines[fi].ToString().IndexOf("heckBox1"); //2、开机启动(true) checkBox1 
                        if( 0 < flag)  //存在，则读取
                        {
                         sArray=stringlines[fi].Split('=');
                         if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                         {
                             VarStart = sArray[1];
                         }
                        }

                        flag = stringlines[fi].ToString().IndexOf("tnBkPath");//3、备份路径选择 btnBkPath
                        if( 0 < flag)  //存在，则读取
                        {
                         sArray=stringlines[fi].Split('=');
                         if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                         {
                             VarBkPath = sArray[1];
                         }
                        }


                        flag = stringlines[fi].ToString().IndexOf("ompTime"); //4、定时时间,CompTime, 例year:mon:day:hour:min:sec
                        if( 0 < flag)  //存在，则读取
                        {                        
                          //以下判断不是很严格，条件苛刻时，可使用正则表达式来完成。
                            sArray = stringlines[fi].Split('=',':');  //存在“=”和“：”
                          if (7 == sArray.Length)
                          {
                              sArray = stringlines[fi].Split('=');  //存在两段值
                              if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                              {
                                  VarCompTime = sArray[1];
                              }
                          }
                        }
                                           
                    }//for 逐行处理
                }//不为空
                else  //配置文件存在但为空
                {
                    output("配置文件可能被损坏!");
                }
            }//配置文件存在
              
              //2、保存数据到文件
                FileStream fs = new FileStream(workPath + @"\" + CfgListName, FileMode.Create, FileAccess.Write); //创建
                //fs.SetLength(0); //清空文件
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                sw.Flush();
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                //整理数据
                    if (( "none" != VarMode) && (Item != "cboAuto"))  //记录备份模式，不存在或值有更新则不记录，下同
                    {
                     sw.WriteLine("cboAuto=" + VarMode);
                    }

                    if (("none" != VarStart) && (Item != "checkBox1")) //记录是否开机启动
                    {
                        sw.WriteLine("checkBox1=" + VarStart);
                    }

                    if (("none" != VarBkPath) && (Item != "btnBkPath"))  //记录备份保存路径
                    {
                        sw.WriteLine("btnBkPath=" + VarBkPath);
                    }

   
                    if (("none" != VarCompTime) && (Item != "CompTime"))  //记录工作时间点
                    {
                        sw.WriteLine("CompTime=" + VarCompTime);
                    }

                     switch(Item){       //更新项有效
                         case "cboAuto"  :
                         case "checkBox1"  :
                         case "btnBkPath"  :
                         case "CompTime"  :
                             sw.WriteLine(Item + "=" + Cfg);
                           break;

                         default : /* 可选的 */
                            
                            break; 
                     }
                                
                sw.Flush();
                sw.Close();
                sw.Dispose(); //确保文件对象被完全释放
            }//try 
            catch (Exception objException)
            {
                output("保存配置时发生意外：" + objException.Message);
            }
        }

        //F0.4 加载CFG.ini配置文件，读取上一次的配置项
        private void loadCfgListName()
        {
            try
            {
                string compTimeYear = "none"; //记录工作时间点
                string compTimeMon = "none";
                string compTimeDay = "none";
                string compTimeHour = "none";
                string compTimeMin = "none";
                string compTimeSec = "none";

                //正则表达式匹配格式
                string regformat = @"\=$";
                Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

                //1、保存前先读取 
                //CfgListName文件存在，且不为空
                if (true == File.Exists(workPath + @"\" + CfgListName))
                {
                    //存在，判断是否为空
                    FileInfo fInfo = new FileInfo(workPath + @"\" + CfgListName);
                    if (Convert.ToInt32(fInfo.Length.ToString()) >= 3)
                    {
                        //存在,读取文件的所有内容
                        string[] stringlines = File.ReadAllLines(workPath + @"\" + CfgListName, Encoding.Default);

                        //去除文本中的空行
                        stringlines = Array.FindAll(stringlines, line => !string.IsNullOrEmpty(line));

                        for (int fi = 0; fi < stringlines.Length; fi++) //逐行处理
                        {
                            //读取配置
                            int flag = 0; //记录配置项是否存在
                            string[] sArray;//记录配置值
                            int flagOk = 0; //记录是否存在符号“=”，防止不存在导致数组跑飞                                           

                            

                            flagOk = stringlines[fi].ToString().IndexOf("=");  //检查是否存在“=”符号

                            if ((0 > flagOk) || (0 == flagOk) || (true == regex.IsMatch(stringlines[fi])))
                            {
                               continue; //读取下一行
                            }

                            flag = stringlines[fi].ToString().IndexOf("boAuto");  //1、模式选择 cboAuto 
                            if (0 < flag)    //存在，则读取
                            {   
                                sArray = stringlines[fi].Split('=');
                                if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                                {
                                    if (2 == int.Parse(sArray[1]))  //多次定时模式？
                                    {
                                        DialogResult dr = MessageBox.Show("继续执行上次的工作模式-定时多次？" + "\n(是：进入定时多次的参数确认；否：恢复定时一次)", "上一次的定时模式", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                                        if (dr == DialogResult.Yes)
                                        {
                                            cboAuto.SelectedIndex = int.Parse(sArray[1]);
                                        }
                                        else    //默认恢复成定时一次
                                        {
                                            cboAuto.SelectedIndex = 1;//定时1次
                                            saveCfgListName("cboAuto", "1"); //记录工作模式配置到文件
                                            output("<提醒>工作方式有变化：定时多次 -> 定时一次。");
                                        }
                                    }
                                    else if (0 == int.Parse(sArray[1]))   //倒计时
                                    {
                                        cboAuto.SelectedIndex = 0;//倒计时
                                    }
                                    else    //定时一次
                                    {
                                        cboAuto.SelectedIndex = 1;//定时1次
                                    }
                                }
                            }

                            
                            flag = stringlines[fi].ToString().IndexOf("heckBox1"); //2、开机启动(true) checkBox1
                            if (0 < flag)    //存在，则读取
                            {
                                sArray = stringlines[fi].Split('=');
                                if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                                {
                                    checkBox1.Checked = ("true" == sArray[1] ? true : false);
                                }
                            }
                            
                            flag = stringlines[fi].ToString().IndexOf("tnBkPath");//3、备份路径选择 btnBkPath
                            if (0 < flag)    //存在，则读取
                            {
                                sArray = stringlines[fi].Split('=');
                                if (2 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                                {
                                    lbl_path.Text = sArray[1];
                                    PC_Path = sArray[1] + @"\";
                                }
                            }

                            flag = stringlines[fi].ToString().IndexOf("ompTime"); //4、定时时间,CompTime, 例year=mon=day=hour=min=sec
                            if (0 < flag)    //存在，则读取
                            {
                                sArray = stringlines[fi].Split('=', ':');  //多分隔符取字符串
                                if (7 == sArray.Length)   //格式正确的数据才会处理，异常则丢弃
                                {
                                    compTimeYear = sArray[1];
                                    compTimeMon = sArray[2];
                                    compTimeDay = sArray[3];
                                    compTimeHour = sArray[4];
                                    compTimeMin = sArray[5];
                                    compTimeSec = sArray[6];

                                    DateTimeComp = new DateTime(int.Parse(compTimeYear), int.Parse(compTimeMon), int.Parse(compTimeDay), int.Parse(compTimeHour), int.Parse(compTimeMin), int.Parse(compTimeSec));  //定时模式，比较专用
                                }

                                
                            }

                        }//for 逐行处理
                    }//不为空
                    else  //配置文件存在但为空
                    {
                        output("配置文件可能被损坏!");
                    }

                    //是否启动备份，上一次配置是否有效。
                    int n = 0;
                    n = DateTime.Now.CompareTo(DateTimeComp);
                    if (2 == cboAuto.SelectedIndex)
                    {
                        //执行完毕，启动“定时”模式
                        output("恢复上一次配置完成，启动工作模式-定时多次。");
                        btn_auto.PerformClick();
                    }
                    else if (n < 0)   //定时时间还未到               
                    {
                        //执行完毕，启动“定时”模式
                        output("恢复上一次配置完成，启动工作模式-定时一次或倒计时。");
                        btn_auto.PerformClick();
                    }
                    else
                    {
                        output("恢复上一次配置完成，不启动定时任务。");
                    }

                }//配置文件存在
            }//try 
            catch (Exception objException)
            {
                output("加载配置时发生意外：" + objException.Message);
            }
        }        
        //F0,sftp代码实现
        public class SFTPHelper
        {
            private Session m_session;
            private Channel m_channel;
            private ChannelSftp m_sftp;

            //host:sftp地址   user：用户名   pwd：密码        
            public SFTPHelper(string host, string user, string pwd)
            {
                    string[] arr = host.Split(':');
                    string ip = arr[0];
                    int port = 22;
                    if (arr.Length > 1) port = Int32.Parse(arr[1]);

                    JSch jsch = new JSch();
                    m_session = jsch.getSession(user, ip, port);
                    MyUserInfo ui = new MyUserInfo();
                    ui.setPassword(pwd);
                    m_session.setUserInfo(ui);
            }

            //SFTP连接状态        
            public bool Connected { get { return m_session.isConnected(); } }

            //连接SFTP        
            public bool Connect()
            {
                try
                {
                    if (!Connected)
                    {
                        m_session.connect();
                        m_channel = m_session.openChannel("sftp");
                        m_channel.connect();
                        m_sftp = (ChannelSftp)m_channel;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            //断开SFTP        
            public void Disconnect()
            {
                if (Connected)
                {
                    m_channel.disconnect();
                    m_session.disconnect();
                }
            }

            //SFTP存放文件        
            public bool Put(string localPath, string remotePath)
            {
                try
                {
                    Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(localPath);
                    Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(remotePath);
                    m_sftp.put(src, dst);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            //SFTP获取文件        
            public bool Get(string remotePath, string localPath)
            {
                try
                {
                    Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(remotePath);
                    Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(localPath);
                    m_sftp.get(src, dst);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            //删除SFTP文件
            public bool Delete(string remoteFile)
            {
                try
                {
                    m_sftp.rm(remoteFile);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            //获取SFTP文件列表        
            public System.Collections.ArrayList GetFileList(string remotePath, string fileType)
            {
                try
                {
                    Tamir.SharpSsh.java.util.Vector vvv = m_sftp.ls(remotePath);
                    ArrayList objList = new ArrayList();
                    foreach (Tamir.SharpSsh.jsch.ChannelSftp.LsEntry qqq in vvv)
                    {
                        string sss = qqq.getFilename();
                        if (sss.Length > (fileType.Length + 1) && fileType == sss.Substring(sss.Length - fileType.Length))
                        { objList.Add(sss); }
                        else { continue; }
                    }

                    return objList;
                }
                catch
                {
                    return null;
                }
            }


            //登录验证信息        
            public class MyUserInfo : Tamir.SharpSsh.jsch.UserInfo
            {
                String passwd;
                public String getPassword() { return passwd; }
                public void setPassword(String passwd) { this.passwd = passwd; }

                public String getPassphrase() { return null; }
                public bool promptPassphrase(String message) { return true; }

                public bool promptPassword(String message) { return true; }
                public bool promptYesNo(String message) { return true; }
                public void showMessage(String message) { }
            }
        }
        //sftp，End



        //F1 进行时钟比较
        private void HourComp()
        {
            try
            {
                if (1 == frm.cboAuto[32]) //每时？
                { MinutComp(); }
                else  //非“每时”，00点-23点
                {
                    for (int Hi = 8; Hi <= 31; Hi++)
                    {
                        if (1 == frm.cboAuto[Hi])
                            if (0 == DateTime.Now.Hour.CompareTo(Convert.ToInt32(frm.cboAutoText[Hi])))
                            { MinutComp(); }  //时钟相等，则执行                          
                    }
                }
            }//try
            catch (Exception objException)
            {
                output("时钟比较发生意外：" + objException.Message);
            }
        }

        //F2 进行分钟比较
        private void MinutComp()
        {
            try
            {
                for (int Mi = 33; Mi <= 92; Mi++)
                {
                    if (1 == frm.cboAuto[Mi])  //分钟被选中
                    {


                        if (0 == DateTime.Now.Minute.CompareTo(Convert.ToInt32(frm.cboAutoText[Mi])))
                        {
                            //分钟相等，则执行
                            //重复备份，则退出
                            if (0 == AutoOldTime.CompareTo(DateTime.Now.ToString("yyyyMMddHHmm")))
                            {
                                //output("重复备份，本次取消：" + DateTime.Now.ToString("yyyyMMddHHmm"));
                                return;
                            }
                            else
                            {
                                //记录本次备份值
                                AutoOldTime = DateTime.Now.ToString("yyyyMMddHHmm");
                                // output("本次备份时间：" + DateTime.Now.ToString("yyyyMMddHHmm"));
                            }

                            //以“日期+时钟+分钟”为名，创建目录
                            //"yyyyMMddHHmm" -- 201506291823
                            //"hh" -- 12小时制，"HH" --24小时制

                            //存储备份路径值
                            string PC_PathBin = PC_Path;
                            //PC_Path += DateTime.Now.ToString("yyyyMMddHHmm");
                            yyyyMMddHHmm = DateTime.Now.ToString("yyyyMMddHHmm");
                            PC_Path += yyyyMMddHHmm;
                            //MessageBox.Show("目录名： " + PC_Path,"提示");


                            //开始备份
                            bakeup();


                            //恢复备份路径值
                            PC_Path = PC_PathBin;
                        }//if 分钟相等，则执行
                    }//if 分钟被选中

                } //for 每分钟
            }//try
            catch (Exception objException)
            {
                output("分钟比较发生意外：" + objException.Message);
            }
        }

     
      //F3 进行sftp版的备份操作
        private void bakeup()
        {
            //备份属于关键字段，加以保护
            try
            {
                //初始化进度条pgb
                prgBarBakeup.Value = 0;
                prgBarBakeup.Minimum = 0; //最小值
                prgBarBakeup.Maximum = lstMyListView.Items.Count;

                //判断站点、文件列表是否为空
                if (lstMyListView.Items.Count < 1) //列表无数据
                {
                    MessageBox.Show("站点列表为空！！ ", "提示");
                    output("Error： 站点列表为空！！ ");
                    return;
                }
                else if (lstView_file.Items.Count < 1) //列表无数据
                {
                    MessageBox.Show("文件列表为空！！ ", "提示");
                    output("Error： 文件列表为空！！ ");
                    return;
                }
                else
                {
                    save2FileTime(autoBackupLogPath, "单站备份开始 ");  //记录到日志
                }

                save2FileTime(autoBackupLogPath, "备份前，清空备份失败站点文件failIpAddress.ini");//记录到日志
                if (File.Exists(failIpAddressPath)) //清空IP.ini文件
                {
                    File.Delete(failIpAddressPath);
                }

                save2FileTime(autoBackupLogPath, "备份开始，遍历listview_ip中所有元素");//记录到日志
                for (int i = 0; i < lstMyListView.Items.Count; i++)
                {
                    string ip = lstMyListView.Items[i].Text;
                    output("单站备份开始： " + ip); //提示备份开始


                    //站点是否可达
                    //构造Ping实例
                    Ping ping = new Ping();

                    //Ping选项设置，用于控制如何传输数据包
                    PingOptions poptions = new PingOptions();
                    poptions.DontFragment = true;

                    //测试数据
                    string data = "Fiberhome IPran";
                    Byte[] buffer = Encoding.ASCII.GetBytes(data);

                    //设置超时时间,ms
                    int timeout = 5000;

                    //调用同步send方法发送数据，将返回结果保存至PingReply实例
                    //此处如果直接ping IP的话，先引用命名空间using System.Net;
                    //然后代码改为：PingReply pingreply = ping.Send(IPadress.Parse("192.168.1.1"),timeout,buffer,poptions);

                    PingReply pingreply = ping.Send(ip, timeout, buffer, poptions);

                    if (pingreply.Status == IPStatus.Success)
                    {
                        //命名第1/2次备份目录
                        string SPath = PC_Path + @"\" + ip;
                        string DPath = PC_Path + @"\" + ip + "_TMP";

                        output("网络通畅: " + ip);

                        //备份次数，不相同则备份3次。
                        int backupTime = 0;


                        while (backupTime < 3) //最多备份3次
                        {
                            //sftp实现
                            SFTPHelper sftp = new SFTPHelper(ip, "root", "root");
                            // if (true == sftp.Connect())  //sftp连接成功
                            if ((true == sftp.Connect()) || (true == sftp.Connect()) || (true == sftp.Connect())) //解决界面卡死问题后，再进行多次连接
                            {
                                //output("sftp连接成功！");
                                save2FileTime(autoBackupLogPath, "sftp连接成功！");  //记录到日志

                                //如果不存在就创建file文件夹
                                if (Directory.Exists(SPath) == false)//不存在,就创建第1次备份NE文件夹
                                {
                                    Directory.CreateDirectory(SPath);
                                }

                                if (Directory.Exists(DPath) == false)//不存在,就创建第2次备份NE_TMP文件夹
                                {
                                    Directory.CreateDirectory(DPath);
                                }

                                //遍历listview_file中所有元素
                                for (int j = 0; j < lstView_file.Items.Count; j++)
                                {
                                    if (true == sftp.Get(lstView_file.Items[j].Text, SPath + @"\"))  //第一次获取
                                    {
                                        //output("第一次获取文件成功！");
                                        save2FileTime(autoBackupLogPath, "第一次获取文件成功！" + lstView_file.Items[j].Text);  //记录到日志
                                        ;
                                    }
                                    else
                                    {
                                        // output("第一次获取文件失败！");
                                        save2FileTime(autoBackupLogPath, "第一次获取文件失败！" + lstView_file.Items[j].Text);  //记录到日志
                                    }

                                    if (true == sftp.Get(lstView_file.Items[j].Text, DPath + @"\"))  //第二次获取
                                    {
                                        //output("第二次获取文件成功！");
                                        save2FileTime(autoBackupLogPath, "第二次获取文件成功！" + lstView_file.Items[j].Text);  //记录到日志
                                    }
                                    else
                                    {
                                        //output("第二次获取文件失败！");
                                        save2FileTime(autoBackupLogPath, "第二次获取文件失败！" + lstView_file.Items[j].Text);  //记录到日志
                                        Thread.Sleep(3000); //延时3s代码，给系统释放文件留取时间
                                    }
                                } //for 文件列表

                                sftp.Disconnect();
                                //output("sftp断开连接！");
                                save2FileTime(autoBackupLogPath, "sftp断开连接！");  //记录到日志

                                //若单个文件备份失败，则继续备份。失败的原因一般是tmp目录中文件被占用，导致无法被删除。
                                //650和690故障现象：文件第一次备份成功，第二次备份失败。
                                try
                                {
                                    //文件夹检查
                                    if (true != CompareDir(SPath, DPath)) //两次备份不相同，再次备份
                                    {
                                        //do.1, 保留最新的备份，删除原备份。且删除TMP文件夹前，先删除文件
                                        if (Directory.Exists(SPath))
                                        {
                                            foreach (string deleteFile in Directory.GetFileSystemEntries(SPath))
                                            {
                                                if (File.Exists(deleteFile))
                                                    File.Delete(deleteFile);
                                            }
                                            //output("删除原备份文件夹");
                                            save2FileTime(autoBackupLogPath, "删除原备份文件夹");  //记录到日志
                                            Directory.Delete(SPath);    //删除原备份文件夹
                                        }

                                        //do.2, 重命名IP_TMP为IP文件夹，以进行下一次比较。
                                        Directory.Move(DPath, SPath);
                                        //测试比较次数用
                                        save2FileTime(autoBackupLogPath, " 重命名IP_TMP为IP文件夹");  //记录到日志

                                        //do.3, 创建一个目录。
                                        if (Directory.Exists(DPath) == false)//不存在,就创建NE_TMP文件夹
                                        {
                                            Directory.CreateDirectory(DPath);
                                            save2FileTime(autoBackupLogPath, " 重新建立IP_TMP文件夹");  //记录到日志

                                        }

                                        //do.4, 备份计数统计。
                                        backupTime += 1;
                                    }
                                    else
                                    {
                                        //do.1 删除TMP文件夹
                                        if (Directory.Exists(DPath)) //删除TMP文件夹前，先删除文件
                                        {
                                            foreach (string deleteFile in Directory.GetFileSystemEntries(DPath))
                                            {
                                                if (File.Exists(deleteFile))
                                                    File.Delete(deleteFile);
                                            }
                                            Directory.Delete(DPath);    //删除TMP文件夹
                                        }

                                    } //for if 文件夹检查
                                }
                                catch (Exception objException)
                                {
                                    output("备份单个时发生意外：" + objException.Message);
                                    try
                                    {
                                        save2File(failIpAddressPath, ip); //备份失败的站点保存到文件
                                        save2FileTime(autoBackupLogPath, "备份失败的站点保存到文件：" +ip);//记录到日志
                                        sftp.Disconnect(); //强制断开
                                    }
                                    catch
                                    {
                                        save2FileTime(autoBackupLogPath, "sftp强制断开发生意外，可能之前已经断开！");  //记录到日志
                                    }
                                }

                            } //if sftp连接成功
                            else
                            {
                                sftp.Disconnect(); //强制断开
                            }

                            //测试同步执行 
                            output("备份工具执行中");

                            //MessageBox.Show("You are the best! ","提示")

                            //do.4, 备份计数统计。
                            backupTime = 3;

                        } //for while 最多备份3次


                    }     //for 站点是否ping可达
                    else
                    {
                        output("网络不通: " + ip);
                    }



                    //一个站点获取完毕，统计进度; 
                    output("单站备份结束： " + ip);
                    //定时观察
                    // dbgBK(ip, PC_Path, i);

                    if (prgBarBakeup.Value < prgBarBakeup.Maximum)
                    {
                        prgBarBakeup.Value++;
                        Application.DoEvents(); //作用就是立刻执行用户事件，刷新界面，此处无效。
                        output("备份进行中[" + prgBarBakeup.Value.ToString() + "/" + prgBarBakeup.Maximum + "]...");  //在output中显示当前备份状态                       
                        lblPrgBar.Text = "[" + prgBarBakeup.Value.ToString() + "/" + prgBarBakeup.Maximum + "]"; //在进度条中显示当前备份状态
                        Application.DoEvents(); //作用就是立刻执行用户事件，刷新界面。

                        if (prgBarBakeup.Value >= prgBarBakeup.Maximum)
                        {
                            output("备份完成[" + prgBarBakeup.Value.ToString() + "/" + prgBarBakeup.Maximum + "]");  //在output中显示当前备份状态                       
                            lblPrgBar.Text = "备份完成"; //在进度条中显示当前备份状态
                            Application.DoEvents(); //作用就是立刻执行用户事件，刷新界面。
                        }

                    }

                } //for ip

                //删除tmp目录和备份不成功的站点，作为备份单个站点时失败的补充措施。            
                try
                {
                    string strIP;
                    string bakeupPathBin = PC_Path + @"\";  //备份存放路径
                    if (true == Directory.Exists(bakeupPathBin))
                    {
                        output("删除tmp目录和备份不成功的站点！"); //输出到界面，同时记录到日志autoBackupLogPath

                        foreach (string deleteFolder in Directory.GetFileSystemEntries(bakeupPathBin))
                        {
                            try //一个文件夹删除失败，不影响其他的
                            {
                                if (Directory.Exists(deleteFolder) && true == deleteFolder.Contains("_TMP")) //IP_TMP文件夹及IP文件夹，删除 
                                {
                                    save2FileTime(autoBackupLogPath, "删除TMP文件夹:" + deleteFolder);  //记录到日志
                                    DeleteFolder(deleteFolder); //删除TMP文件夹
                                    strIP = deleteFolder.Substring(0, deleteFolder.Length - 4); //获取IP
                                    if (Directory.Exists(strIP)) //IP文件夹存在，删除 
                                    {
                                        save2FileTime(autoBackupLogPath, "删除IP文件夹:" + strIP);  //记录到日志
                                        DeleteFolder(strIP); //删除IP文件夹
                                    }
                                }//删除TMP文件夹
                            }//try
                            catch (Exception objException)
                            {
                                save2FileTime(autoBackupLogPath, "注意：删除备份不成功的站点时发生意外," +deleteFolder + ":"+ objException.Message);  //记录到日志
                            }

                        }//遍历备份目录
                    }//备份目录存在
                }//try
                catch (Exception objException)
                {
                    output("注意：删除tmp目录和备份不成功的站点时发生意外，备份目录下带tmp文件夹的站点备份不完整：" + objException.Message);//输出到界面，同时记录到日志
                }

                
                //统计站点备份情况
                int fileCount = 0;      //文件数目
                int directoryCount = 0; //文件夹/站点数目
                string bakeupPath = PC_Path + @"\";  //备份存放路径
                //如果嵌套文件夹很多，可以开子线程去统计

                //如果所有站点都不能ssh登录，则一个站点文件夹都没有，总文件夹不创建
                if (true == Directory.Exists(bakeupPath))
                {
                    directoryCount = System.IO.Directory.GetDirectories(bakeupPath).Length; //获取文件夹数目

                    //显示文件夹（站点）、文件清单
                    //待调
                    //保存备份结果到文件
                    FileStream fs = new FileStream(workPath + @"\" + backupResultListName, FileMode.Create, FileAccess.Write); //保存前，清空
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    sw.Flush();
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);

                    //fileCount = System.IO.Directory.GetFiles(bakeupPath).Length; //获取文件总数
                    foreach (var folder in System.IO.Directory.GetDirectories(bakeupPath))   //获取子文件夹下文件总数
                    {
                        fileCount += System.IO.Directory.GetFiles(folder).Length;  //获取文件总数

                        //整理数据
                        //遍历子文件夹下所有元素

                        sw.WriteLine(folder);//写入IP，即子文件夹名
                        DirectoryInfo dires = new DirectoryInfo(folder);
                        FileInfo[] files = dires.GetFiles();

                        foreach (FileInfo f in files)
                        {
                            sw.WriteLine("文件： " + f.Name + ", " + f.Length.ToString() + " 字节");
                        }
                    }


                    //记录总文件夹数和文件数目
                    sw.WriteLine("备份统计" + yyyyMMddHHmm + "：站点数目—" + directoryCount + "/" + "文件数目—" + fileCount);

                    //关闭备份结果文件
                    sw.Flush();
                    sw.Close();
                    sw.Dispose(); //确保文件对象被完全释放
                    //MessageBox.Show("成功生成指定文件！","提示");

                    //待调

                }
                else
                {    //保存备份结果到文件
                    FileStream fs = new FileStream(workPath + @"\" + backupResultListName, FileMode.Create, FileAccess.Write); //保存前，清空
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    sw.Flush();
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    
                    //记录总文件夹数和文件数目
                    sw.WriteLine("备份统计" + yyyyMMddHHmm + "：站点数目—" + directoryCount + "/" + "文件数目—" + fileCount);

                    //关闭备份结果文件
                    sw.Flush();
                    sw.Close();
                    sw.Dispose(); //确保文件对象被完全释放
                    //MessageBox.Show("成功生成指定文件！","提示");
                }
                
                output("备份统计" + yyyyMMddHHmm + "：站点数目—" + directoryCount + "/" + "文件数目—" + fileCount);
                output("备份明细请参考：" + workPath + @"\" + backupResultListName); //提示详细统计文件的存放路径
                output("备份失败站点明细请参考：" + workPath + @"\" + failIpAddressPath); //提示备份失败站点文件的存放路径
                
            }//try
            catch (Exception objException)
            {
                output("备份时发生意外：" + objException.Message);
            }
        }

        //F4，比较两个文件是否完全相等
        public bool CompareFile(string f_1, string f_2)
        {
            //文件比较功能防错
            try
            {
                //dbg
                //output(f_1 + " 比较 " + f_2);

                //检查目录下文件是否存在
                if (!File.Exists(f_1) || !File.Exists(f_2))
                {
                    //dbg
                    //output("文件不存在");               
                    return false;
                }

                //计算第一个文件的哈希值
                var hash = System.Security.Cryptography.HashAlgorithm.Create();
                var stream_1 = new System.IO.FileStream(f_1, System.IO.FileMode.Open);
                byte[] hashByte_1 = hash.ComputeHash(stream_1);
                stream_1.Close();
                stream_1.Dispose(); //确保文件对象被完全释放
                //计算第二个文件的哈希值
                var stream_2 = new System.IO.FileStream(f_2, System.IO.FileMode.Open);
                byte[] hashByte_2 = hash.ComputeHash(stream_2);
                stream_2.Close();
                stream_2.Dispose(); //确保文件对象被完全释放

                //比较两个哈希值
                if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                {
                    return true;
                }
                else
                {
                    //output("hash值不相等");  
                    return false;
                }
            }//try 
            catch (Exception objException)
            {
                output("文件比较时发生意外：" + objException.Message);
                return false;
            }

        }

        //F5，文件夹检查
        public bool CompareDir(string d_1, string d_2)
        {
            //文件夹比较功能防错
            try
            {
                //确保路径存在
                if (!Directory.Exists(d_1) || !Directory.Exists(d_1))
                {
                    //output("两文件夹不存在，返错!");
                    return false;
                }

                string[] SFiles = Directory.GetFiles(d_1);
                string[] DFiles = Directory.GetFiles(d_2);


                if (SFiles.Length != DFiles.Length)  //两文件夹下文件数目不等，返错
                {
                    //output("两文件夹下文件数目不等，返错!");
                    return false;
                }


                foreach (string sfilename in SFiles)
                {
                    string SFileName = sfilename;
                    string DFileName = d_2 + "\\" + Path.GetFileName(sfilename);

                    if (File.Exists(DFileName)) //如果目的目录中已经存在同名文件，则比较其MD5值
                    {
                        if (true != CompareFile(SFileName, DFileName)) //MD5值不等，则返错                   
                        {
                            //output("遍历文件夹，不相等：" + DFileName);
                            return false;
                        }
                    }
                    else
                    {
                        //output("遍历文件夹，TMP文件夹中文件不存在：" + DFileName);
                        return false;
                    }

                } //foreach

                //output("遍历文件夹，相等");
                return true; //文件夹相同
            }//try
            catch (Exception objException)
            {
                output("文件夹比较时发生意外：" + objException.Message);
                return false;
            }
        }
    

            //以下为控件操作

        //选中后，站点显示在文本框中
        private void lstMyListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add a column with width 20 and left alignment.
            if (lstMyListView.SelectedItems.Count > 0)
            {
                txtIP.Text = lstMyListView.SelectedItems[0].Text;
            }

            lblDev.Text = "站点情况： " + lstMyListView.Items.Count + "站点/" + lstMyListView.SelectedItems.Count + "选中";  //显示文件个数

        }

        //文件修改
        private void btn_file_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstView_file.SelectedItems.Count == 1)
                {
                    lstView_file.SelectedItems[0].Text = txtFile.Text;

                    txtFile.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框
                    lblFile.Text = "文件情况： " + lstView_file.Items.Count + "文件";  //显示文件个数
                }
                else
                {
                    MessageBox.Show("请先选择一个修改项！", "提示");
                }

                //保存文件清单到FileList.ini
                saveFileList();
            }//try
            catch (Exception objException)
            {
                output("文件修改时发生意外：" + objException.Message);
            }

        }

        //增加站点IP
        private void btn_ip_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag_exist = false;   //默认不重复，添加

                if (txtIP.Text != "")
                {
                    //不为空
                    //增加条件，检查对应站点是否存在
                    if (null == lstMyListView.FindItemWithText(txtIP.Text)) //BUG："abc"会被FindItemWithText认为在"abcd"中存在
                    {

                        lstMyListView.Items.Add(txtIP.Text);

                        //统计站点清单
                        lblNE.Text = "导入站点情况：" + lstMyListView.Items.Count.ToString() + "站点";  //显示站点个数
                        lblDev.Text = "站点情况： " + lstMyListView.Items.Count + "站点";  //显示站点个数

                        txtIP.Text = ""; //清空文本框
                        txtIP.Focus();
                    }
                    else  //重复文件再次确认
                    {
                        for (int ni = 0; ni < lstMyListView.Items.Count; ni++) //搜寻是否存在重复文件名
                        {
                            flag_exist = false;   //默认不重复，添加
                            if (txtIP.Text == lstMyListView.Items[ni].Text)
                            {
                                MessageBox.Show(txtIP.Text + " 文件已存在！", "提示");
                                flag_exist = true;   //发现重复，则置位
                                break;
                            }
                        }

                        if (false == flag_exist) //重复项不存在
                        {
                            lstMyListView.Items.Add(txtIP.Text);

                            //统计站点清单
                            lblNE.Text = "导入站点情况：" + lstMyListView.Items.Count.ToString() + "站点";  //显示站点个数
                            lblDev.Text = "站点情况： " + lstMyListView.Items.Count + "站点";  //显示站点个数

                            txtIP.Text = ""; //清空文本框
                            txtIP.Focus();
                            flag_exist = true;
                        }
                    }


                }
                else
                {
                    MessageBox.Show("增加的站点名称一栏为空！", "提示");
                }

                //保存站点清单到NeList.ini
                saveNeListName();

            }//try
            catch (Exception objException)
            {
                output("站点增加时发生意外：" + objException.Message);
            }
         
        }

        //删除站点IP
        private void btn_rm_Click(object sender, EventArgs e)
        {
            try
            {
                //删除前确认，只有是/否两种选择
                DialogResult dr = MessageBox.Show("继续删除操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (dr == DialogResult.No)
                {
                    txtIP.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框
                    return; //退出，不再删除
                }

                //确认后，继续删除
                if (lstMyListView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.lstMyListView.SelectedItems)
                    {
                        this.lstMyListView.Items.Remove(item);

                        //统计站点清单
                        lblNE.Text = "导入站点情况：" + lstMyListView.Items.Count.ToString() + "站点";  //显示站点个数
                        lblDev.Text = "站点情况： " + lstView_file.Items.Count.ToString() + "站点";  //显示站点个数
                    }
                }
                else
                {
                    MessageBox.Show("未选中站点项！", "提示");
                }

                txtIP.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框

                //保存文件清单到NeList.ini
                saveNeListName();

            }//try
            catch (Exception objException)
            {
                output("文件删除时发生意外：" + objException.Message);
            }
        }

        //修改站点IP
        private void btn_change_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstMyListView.SelectedItems.Count == 1)
                {
                    lstMyListView.SelectedItems[0].Text = txtIP.Text;

                    txtIP.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框

                    //统计站点清单
                    lblNE.Text = "导入站点情况：" + lstMyListView.Items.Count.ToString() + "站点";  //显示站点个数
                    lblDev.Text = "站点情况： " + lstMyListView.Items.Count + "站点";  //显示站点个数
                }
                else
                {
                    MessageBox.Show("请先选择一个修改项！", "提示");
                }

                //保存文件清单到NeList.ini
                saveNeListName();
            }//try
            catch (Exception objException)
            {
                output("站点修改时发生意外：" + objException.Message);
            }
        }

        //按键-增加文件
        private void btn_file_add_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag_exist = false;   //默认不重复，添加

                if (txtFile.Text != "")
                {
                    //不为空
                    //增加条件，检查对应文件是否存在
                    if (null == lstView_file.FindItemWithText(txtFile.Text)) //BUG："abc"会被FindItemWithText认为在"abcd"中存在
                    {

                        lstView_file.Items.Add(txtFile.Text);
                        lblFile.Text = "文件情况： " + lstView_file.Items.Count + "文件";  //显示文件个数

                        txtFile.Text = ""; //清空文本框
                        txtFile.Focus();
                    }
                    else  //重复文件再次确认
                    {
                        for (int ni = 0; ni < lstView_file.Items.Count; ni++) //搜寻是否存在重复文件名
                        {
                            flag_exist = false;   //默认不重复，添加
                            if (txtFile.Text == lstView_file.Items[ni].Text)
                            {
                                MessageBox.Show(txtFile.Text + " 文件已存在！", "提示");
                                flag_exist = true;   //发现重复，则置位
                                break;
                            }
                        }

                        if (false == flag_exist) //重复项不存在
                        {
                            lstView_file.Items.Add(txtFile.Text);
                            lblFile.Text = "文件情况： " + lstView_file.Items.Count + "文件";  //显示文件个数

                            txtFile.Text = ""; //清空文本框
                            txtFile.Focus();
                            flag_exist = true;
                        }
                    }


                }
                else
                {
                    MessageBox.Show("增加的文件名称一栏为空！","提示");
                }

                //保存文件清单到FileList.ini
                saveFileList();

            }//try
            catch (Exception objException)
            {
                output("文件增加时发生意外：" + objException.Message);
            }
 
        }

        private void lstView_file_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (1 ==lstView_file.SelectedItems.Count)
                txtFile.Text = lstView_file.SelectedItems[0].Text;
            lblFile.Text = "文件情况： " + lstView_file.Items.Count + "文件/" + lstView_file.SelectedItems.Count + "选中";  //显示文件个数
        }

        //按键-原退出用，测试用
        private void button1_Click_2(object sender, EventArgs e)
        {
            string deleteFolder="1.2.3.4_TMP";
            string strIP = deleteFolder.Substring(0, deleteFolder.Length - 4); //获取IP
            MessageBox.Show(strIP);
        }

        //test1.3-事件1
        private void Timer_TimesUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            //到达指定时间5秒触发该事件输出 Hello World!!!!
           // System.Diagnostics.Debug.WriteLine("Hello World!!!!");
            output("Hello World!!!!");
        }
        
   
        //按键 -- 文件删除
        private void btn_file_rm_Click(object sender, EventArgs e)
        {
            try
            {
                //删除前确认，只有是/否两种选择
                DialogResult dr = MessageBox.Show("继续删除操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (dr == DialogResult.No)
                {
                    txtFile.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框
                    return ; //退出，不再删除
                }

                //确认后，继续删除
                if (lstView_file.SelectedItems.Count > 0)
                {                   
                    foreach (ListViewItem item in this.lstView_file.SelectedItems)
                    {
                        this.lstView_file.Items.Remove(item);
                        lblFile.Text = "文件情况： " + lstView_file.Items.Count.ToString() + "文件";  //显示文件个数
                    }
                }
                else
                {
                    MessageBox.Show("未选中文件项！", "提示");
                }

                txtFile.Text = ""; //选中项目后，会在文件框中显示，此处需清空文本框

                //保存文件清单到FileList.ini
                saveFileList();

            }//try
            catch (Exception objException)
            {
                output("文件删除时发生意外：" + objException.Message);
            }
        }


        //日志输出函数
        private void output(string log)
        {
            try
            {
                //如果日志超过100行，则自动清空；
                if (txtLog.GetLineFromCharIndex(txtLog.Text.Length) > 100)
                {
                    //清空显示框
                    txtLog.Text = "";
                } //if 日志超过100行

                //添加日志
                txtLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss ") + log + "\r\n");
                save2FileTime(autoBackupLogPath, log);  //日志记录到文件
            }//try
            catch (Exception objException)
            {
                //output("日志输出时发生意外：" + objException.Message);
                ; //不处理。因为处理可能会导致死循环。若save2FileTime()出错，则save2FileTime()->output()->save2FileTime()
            }
        }
        
        //按键-开始
        private void btn_auto_Click(object sender, EventArgs e)
        {
            try
            {
                //do.1, 初始化进度条pgb
                prgBarBakeup.Value = 0;

                //do.2, 判断站点、文件列表是否为空
                if (lstMyListView.Items.Count < 1) //列表无数据
                {
                    MessageBox.Show("站点列表为空！ ", "提示");
                    output("Error： 站点列表为空！ ");
                    return;
                }
                else if (lstView_file.Items.Count < 1) //列表无数据
                {
                    MessageBox.Show("文件列表为空！", "提示");
                    output("Error： 文件列表为空！");
                    return;
                }
                else
                {
                    //output("开始备份站点！"); //提示备份开始
                    ;
                }


                //do.3, 保存“倒计时”/“定时一次”/“定时多次”的比较值
                switch (cboAuto.SelectedIndex)
                {

                    case 0:    //倒计时

                        BkMode = 1;

                        // 获取设置的时间
                        SetSec = Convert.ToInt32(cboNowSec.SelectedItem.ToString()); //秒钟
                        SetMin = Convert.ToInt32(cboNowMin.SelectedItem.ToString());  //分钟
                        SetHour = Convert.ToInt32(cboNowHour.SelectedItem.ToString());//时钟

                        //计算到当前时间
                        DateTimeComp = DateTime.Now;
                        DateTimeComp = DateTimeComp.AddHours(SetHour);  //增加时钟
                        DateTimeComp = DateTimeComp.AddMinutes(SetMin); //增加分钟
                        DateTimeComp = DateTimeComp.AddSeconds(SetSec); //增加秒钟

                        //显示定时模式
                        lblCtrl.Text = "当前状态: 倒计时";
                        output("当前状态: 倒计时");

                        saveCfgListName("CompTime", DateTimeComp.ToString("yyyy:MM:dd:HH:mm:ss")); //记录工作时间到文件
                        saveCfgListName("cboAuto", "0"); //记录工作模式配置到文件
                        break;

                    case 1:   //定时一次
                        BkMode = 2;

                        SetSec = Convert.ToInt32(cboSec.SelectedItem.ToString()); //秒钟
                        SetMin = Convert.ToInt32(cboMin.SelectedItem.ToString());  //分钟
                        SetHour = Convert.ToInt32(cboHour.SelectedItem.ToString());//时钟


                        //DateTime d1 = new DateTime(2004, 1, 1, 15, 36, 05);
                        DateTimeComp = new DateTime(dtp1time.Value.Year, dtp1time.Value.Month, dtp1time.Value.Day, SetHour, SetMin, SetSec);
                        // MessageBox.Show(DateTimeComp.ToString(),"提示");

                        //显示定时模式
                        lblCtrl.Text = "当前状态: 定时一次";
                        output("当前状态: 定时一次");

                        saveCfgListName("CompTime", DateTimeComp.ToString("yyyy:MM:dd:HH:mm:ss")); //记录工作时间到文件
                        saveCfgListName("cboAuto", "1"); //记录工作模式配置到文件
                        break;

                    case 2:   //定时多次
                        BkMode = 3;

                        //开启定时多次备份；

                        //显示定时模式
                        lblCtrl.Text = "当前状态: 定时多次";
                        output("当前状态: 定时多次");

                        saveCfgListName("CompTime", DateTimeComp.ToString("yyyy:MM:dd:HH:mm:ss")); //记录工作时间到文件
                        saveCfgListName("cboAuto", "2"); //记录工作模式配置到文件
                        break;

                    //增加default操作

                    default:
                        BkMode = 1; //默认为“定时一次”

                        //显示定时模式
                        lblCtrl.Text = "当前状态: 定时一次";
                        output("当前状态: 定时一次");

                        saveCfgListName("CompTime", DateTimeComp.ToString("yyyy:MM:dd:HH:mm:ss")); //记录工作时间到文件
                        saveCfgListName("cboAuto", "1"); //记录工作模式配置到文件
                        break;
                }

                //启动备份
                timerAuto.Enabled = true;    //启动备份   
            }//try
            catch (Exception objException)
            {
                output("启动备份时发生意外：" + objException.Message);
            }

        }

        //默认加载
        private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
            //0、欢迎界面
            output("欢迎使用Fiberhome数通自动备份工具，祝您愉快!");

            string workPath = System.Windows.Forms.Application.StartupPath; //获取启动了应用程序的可执行文件的路径，“D：\fh_bk”形式，末尾不带“\”
               
           //1、默认备份的四个文件 
           //FileList.ini文件存在，且不为空
            if (true == File.Exists(workPath + @"\" + fileListName))
            {
                //存在，判断是否为空
                FileInfo fInfo = new FileInfo(workPath + @"\" + fileListName);
                if (Convert.ToInt32(fInfo.Length.ToString()) >= 10)
                {
                    //存在,读取文件的所有内容
                    string[] stringlines = File.ReadAllLines(workPath + @"\" + fileListName, Encoding.Default);
                    bool flag_exist = false;  //默认不重复，添加

                    //去除文本中的空行
                    stringlines = Array.FindAll(stringlines, line => !string.IsNullOrEmpty(line));

                    //清空lstView_file
                    lstView_file.Items.Clear();
                    output("从FileList.ini加载备份文件清单");

                    for (int fi = 0; fi < stringlines.Length; fi++) //逐行处理
                    {
                        //增加条件，检查对应文件是否重复
                        if (null == lstView_file.FindItemWithText(stringlines[fi].Trim())) //BUG："abc"会被FindItemWithText认为在"abcd"中存在
                        {

                            lstView_file.Items.Add(stringlines[fi].Trim());
                            //output("增加文件： " + stringlines[fi].Trim());
                            
                        }
                        else  //重复站点再次确认
                        {
                            for (int ni = 0; ni < lstView_file.Items.Count; ni++)
                            {
                                flag_exist = false;   //默认不重复，添加
                                if (stringlines[fi].Trim() == lstView_file.Items[ni].Text)
                                {
                                    //output("站点非法(重复)： " + stringlines[fi].Trim());
                                    flag_exist = true;   //发现重复，则置位
                                    break;
                                }
                            }

                            if (false == flag_exist) //重复项不存在
                            {
                                lstView_file.Items.Add(stringlines[fi].Trim());
                                //output("增加文件： " + stringlines[fi].Trim());
                                flag_exist = true;
                            }
                        }
                    }//for 逐行处理
                }//不为空
                else  //FileList.ini为空
                {
                    //配置文件不存在或存在但为空
                    output("R800默认备份ZEBOS.CFG、mac.conf、nmagent_config等3个文件，R8000默认备份ZEBOS.CFG文件!");
                    lstView_file.Items.Add("/home/scu/ZEBOS.CFG");
                    lstView_file.Items.Add("/home/scu/mac.conf");
                    lstView_file.Items.Add("/home/scu/nmagent_config");
                    lstView_file.Items.Add("/mnt/cfdisk2/ZEBOS.CFG");

                    //保存文件清单
                    saveFileList();
                }
            }//FileList.ini存在
            else  //FileList.ini不存在
            {
                //配置文件不存在或存在但为空
                output("R800默认备份ZEBOS.CFG、mac.conf、nmagent_config等3个文件，R8000默认备份ZEBOS.CFG文件!");
                lstView_file.Items.Add("/home/scu/ZEBOS.CFG");
                lstView_file.Items.Add("/home/scu/mac.conf");
                lstView_file.Items.Add("/home/scu/nmagent_config");
                lstView_file.Items.Add("/mnt/cfdisk2/ZEBOS.CFG");

                //保存文件清单
                saveFileList();
            }
            
            //统计文件清单
            lblFile.Text = "文件情况： " + lstView_file.Items.Count.ToString() + "文件";  //显示文件个数

            //1.1默认加载备份站点
            //NeList.ini文件存在，且不为空
            if (true == File.Exists(workPath + @"\" + NeListName))
            {
                //存在，判断是否为空
                FileInfo fInfo = new FileInfo(workPath + @"\" + NeListName);
                if (Convert.ToInt32(fInfo.Length.ToString()) >= 10)
                {
                    readNeFromIP_ini(workPath + @"\" + NeListName, lstMyListView); //从ip.ini加载站点清单
                }//不为空
                else  //NeList.ini为空
                {
                    //配置文件存在但内容过于短小
                    output("站点清单文件NeList.ini内容异常!");
                }
            }//NeList.ini存在

            //统计站点清单-“站点修改”-Tab页
            lblDev.Text = "站点情况： " + lstMyListView.Items.Count.ToString() + "站点";  //显示站点个数
                
            //2、检查依赖的脚本是否存在。检查目录下Tamir.SharpSSH.dll文件是否存在
            output("备份工具完整性检查 -- 开始");
            if (false == File.Exists(workPath + @"\" + "Tamir.SharpSSH.dll"))
            {
                //Tamir.SharpSSH.dll不存在
                MessageBox.Show("Tamir.SharpSSH.dll文件不存在，请确认目录：" + workPath + @"\", "提示");  
            }
            else
            {
                output("备份工具完整性检查 -- 通过");
            }

            //3、默认组合框选中为1次
            cboAuto.SelectedIndex = 1;//将combox的默认选项设为定时一次。


            //4、组合框
            //4.1 定时一次
            for (int k = 0; k < 24; k++)
            {
                cboHour.Items.Add(k);
            }
            //默认值 
            cboHour.SelectedIndex = 0;//将combox的默认选项设为00

            for (int k = 0; k < 60; k++)
            {
                cboMin.Items.Add(k);
            }
            //默认值 
            cboMin.SelectedIndex = 0;//将combox的默认选项设为00

            for (int k = 0; k < 60; k++)
            {
                cboSec.Items.Add(k);
            }
            //将日期等各值调为当前时间 
            //dtp1time日历默认显示为当前日期
            cboHour.Text = DateTime.Now.Hour.ToString(); //时钟

            cboMin.Text = DateTime.Now.Minute.ToString(); //分钟

            cboSec.Text = DateTime.Now.Second.ToString(); //秒钟

            //4.2 倒计时
            for (int k = 0; k < 24; k++) //设置时钟
            {
                cboNowHour.Items.Add(k);
            }

            for (int k = 0; k < 60; k++) //设置分钟
            {
                cboNowMin.Items.Add(k);
            }

            for (int k = 3; k < 60; k++) //设置秒钟，考虑到windows为非实时操作系统，小于3秒钟可能备份不成功
            {
                cboNowSec.Items.Add(k);
            }

            //默认值 
            cboNowHour.SelectedIndex = 0;//将时钟的combox的默认选项设为00
            cboNowMin.SelectedIndex = 0;//将分钟的combox的默认选项设为00
            cboNowSec.SelectedIndex = 56;//将秒钟的combox的默认选项设为59,此处第56项即是数值"59"

            //将日期等各值调为当前时间 
            cboNowHour.Text = "0"; //时钟
            cboNowMin.Text = "0"; //分钟
            cboNowSec.Text = "59"; //秒钟


             //5、默认显示，定时一次
            gboNow.Visible = false;
            gboAuto1Time.Visible = true;
            gboAutoXtimes.Visible = false;

            //6、增加版本提示信息，待补充

            //7、默认开机启动, 2016-4-1
            //checkBox1.Checked = true;  //注释掉，改在CFG.ini文件中默认开机自启动。


            //8、加载上一次配置,2016-4-1
            loadCfgListName();


            //实例化委托，不卡2
            //updateTxt_2 = new UpdateTxt_2(output);


            //步骤5，自动更新编译时间，指示版本号
            string ver = "ver" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); //获取程序集的版本号, V1.0
            string timeComp = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString();    //获取程序集的最后编译时间，日期+时间
            //string timeComp = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToShortDateString();  //编译日期, 
            toolTip1.SetToolTip(lblVersion, ver + ", SVNxx, by cytao@fiberhome.com" + ", " + timeComp);              //ver 1.0.2, SVN41, by cytao@fiberhome.com, 2016.4
            
            }//try
            catch (Exception objException)
            {
                output("加载初始配置时发生意外：" + objException.Message);
            }

        }

        //最小化隐藏，2015-10-30
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.notifyIcon1.Visible = true;
            }

        }

        //备份定时器，循环处理备份事件
        private void timerAuto_Tick(object sender, EventArgs e)
        {
            try
            {
                //调用执行事件
                if (flag_bk == 0)
                {
                    flag_bk = 1;  //锁定定时备份。当前备份完成，才触发下一次备份


                    //备份方式选择
                    switch (BkMode)
                    {

                        case 1:    //倒计时

                            //output("当前状态：倒计时, " + "执行时间-" + DateTimeComp.ToString() + "，当前时间-" + DateTime.Now.ToString());
                            output("当前状态：倒计时, " + "执行时间-" + DateTimeComp.ToString());
                           

                            if (DateTime.Now.ToString("yyyyMMdd") == DateTimeComp.ToString("yyyyMMdd"))
                                if (DateTime.Now.ToString("HHmmss") == DateTimeComp.ToString("HHmmss"))
                                {
                                    // if ( 0 == DateTime.Now.CompareTo(DateTimeComp))
                                    //MessageBox.Show(DateTime.Now.ToString() + "-1Time-" + DateTimeComp.ToString(),"提示");

                                    //存储备份路径值
                                    string PC_PathBin = PC_Path;
                                    //PC_Path += DateTime.Now.ToString("yyyyMMddHHmm");
                                    yyyyMMddHHmm = DateTime.Now.ToString("yyyyMMddHHmm");
                                    PC_Path += yyyyMMddHHmm;
                                    //MessageBox.Show("目录名： " + PC_Path,"提示");

                                    //时间到，执行备份操作
                                    bakeup();
                                    //bakeup_thread();

                                    //恢复备份路径值
                                    PC_Path = PC_PathBin;

                                    //执行完毕，取消“倒计时”模式
                                    btnAutoStop.PerformClick();
                                }


                            break;

                        case 2:   //定时一次
                            //output("timer: 定时一次, " + DateTime.Now.ToString() + "-1Time-" + DateTimeComp.ToString());
                            //output(DateTime.Now.CompareTo(DateTimeComp).ToString());

                            if (DateTime.Now.ToString("yyyyMMdd") == DateTimeComp.ToString("yyyyMMdd"))
                                if (DateTime.Now.ToString("HHmmss") == DateTimeComp.ToString("HHmmss"))
                                {
                                    //存储备份路径值
                                    string PC_PathBin = PC_Path;
                                    //PC_Path += DateTime.Now.ToString("yyyyMMddHHmm");
                                    yyyyMMddHHmm = DateTime.Now.ToString("yyyyMMddHHmm");
                                    PC_Path += yyyyMMddHHmm;
                                    //MessageBox.Show("目录名： " + PC_Path,"提示");

                                    //时间到，执行备份操作
                                    bakeup();

                                    //恢复备份路径值
                                    PC_Path = PC_PathBin;

                                    //执行完毕，取消“定时一次”模式
                                    btnAutoStop.PerformClick();
                                }



                            break;

                        case 3:   //定时多次
                            //output("timer: 定时多次");
                            //0-6,day; 7为每天 
                            //8-31, Hour; 32为每时
                            //33-92,Minute
                            if (1 == frm.cboAuto[7]) //每天？
                            { HourComp(); } //时钟相等，则执行   
                            else  //非“每天”，周1-周7
                            {
                                for (int Di = 0; Di <= 6; Di++)
                                {
                                    if (1 == frm.cboAuto[Di])  //复选框被选中
                                        if (DateTime.Now.DayOfWeek.ToString() == frm.cboAutoText[Di])  //时钟相等，则执行 
                                        { HourComp(); }
                                }
                            }

                            break;

                        default:
                            output("工具进入空闲状态！"); //执行到此处为异常情况
                            break;

                    } //switch



                    flag_bk = 0;  //解除定时备份限制
                }  //if (flag_bk == 0)

            }//try
            catch (Exception objException)
            {
                output("备份定时器发生意外：" + objException.Message);
            }
        }

        //按键-取消，备份控制台
        private void btnAutoStop_Click(object sender, EventArgs e)
        {
            try
            {
                timerAuto.Enabled = false;
                BkMode = 0; //恢复定时器工作模式为0.

                //显示定时模式
                lblCtrl.Text = "当前状态: 备份已取消";
                output("当前状态: 备份已取消");

                //test1.4-停止
                t.Stop();
                System.Diagnostics.Debug.WriteLine("未到指定时间5秒提前终结！！！");
            }//try
            catch (Exception objException)
            {
                output("定时方式取消时发生意外：" + objException.Message);
            }
        }


        //定时方式选择框
        private void cboAuto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                switch (cboAuto.SelectedIndex)
                {
                    case 0:    //倒计时

                        gboNow.Visible = true;
                        gboAuto1Time.Visible = false;
                        gboAutoXtimes.Visible = false;
                        groupBox1.Visible = true;  //站点导入

                        break;

                    case 1:   //定时一次

                        gboNow.Visible = false;
                        gboAuto1Time.Visible = true;
                        gboAutoXtimes.Visible = false;

                        break;

                    case 2:   //定时多次

                        gboNow.Visible = false;
                        gboAuto1Time.Visible = false;
                        gboAutoXtimes.Visible = true;

                        frm.ShowDialog(); //只能处理当前窗口
                        break;

                    default:
                        cboAuto.SelectedIndex = 1; //默认为“定时一次”
                        break;

                }
            }//try
            catch (Exception objException)
            {
                output("定时方式选择时发生意外：" + objException.Message);
            }
          
        }

        //按键--设置定时多次参数
        private void btnManyTimes_Click(object sender, EventArgs e)
        {
            try
            {
                //显示窗体2，设置定时参数
                //frm.Show();    //能设置任意窗口
                frm.ShowDialog(); //必须设置当前窗口 
            }//try
            catch (Exception objException)
            {
                output("弹出定时多次参数窗体时发生意外：" + objException.Message);
            }
        }

        //选择备份文件存放路径
        private void btnBkPath_Click(object sender, EventArgs e)
        {
            try
            {
                //显示folderBrowserDialog1控件
                fbdPath.Description = "备份到...";
                if (fbdPath.ShowDialog() == DialogResult.OK)
                {

                    if (fbdPath.SelectedPath.IndexOf(" ") < 0) //路径非空
                    {
                        lbl_path.Text = fbdPath.SelectedPath;
                        PC_Path = fbdPath.SelectedPath + @"\";

                        output("您选择的文件目录为: " + lbl_path.Text);
                        saveCfgListName("btnBkPath", PC_Path); //记录配置到文件
                        //output("文件保存路径" + PC_Path);
                    }
                    else
                    {
                        MessageBox.Show("错误！您选择的文件目录中带有空格，请重新选择: " + fbdPath.SelectedPath, "提示");
                        output("错误：您选择的文件目录中带有空格: " + fbdPath.SelectedPath);
                    }

                }
            }//try
            catch (Exception objException)
            {
                output("选择备份路径时发生意外：" + objException.Message);
            }
            
        }

        //将日期等各值调为当前时间
        private void btnNow_Click(object sender, EventArgs e)
        {
            try
            {              
                //dtp1time日历默认显示为当前日期
                dtp1time.Value = DateTime.Now;

                //cboHour.Text = Convert.ToInt32(DateTime.Now.Hour.ToString()); //秒钟;//将combox的默认选项设为00
                cboHour.Text = DateTime.Now.Hour.ToString(); //时钟

                //cboMin.SelectedIndex = 0;//分钟
                cboMin.Text = DateTime.Now.Minute.ToString();

                //cboSec.SelectedIndex = 0;//秒钟
                cboSec.Text = DateTime.Now.Second.ToString();
            }//try
            catch (Exception objException)
            {
                output("设置为当前时间发生意外：" + objException.Message);
            }
        }

        //按键-导入站点
        private void btnInput_Click(object sender, EventArgs e)
        {
            //站点导入功能防错
            try
            {
                string workPath = System.Windows.Forms.Application.StartupPath; //获取启动了应用程序的可执行文件的路径，“D：\fh_bk”形式，末尾不带“\”
                //output("当前工作目录" + workPath);
                

                if (false == checkBoxOtnm.Checked)
                {
                    openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                    openFileDialog1.Title = "选择导入的站点文件";
                    openFileDialog1.Filter = "All files(*.*)|*.*|ini files (*.ini)|*.ini"; //设备控件打开的文件类型
                    openFileDialog1.FilterIndex = 2; //默认打开*.ini
                    openFileDialog1.RestoreDirectory = true; //记忆之前打开的目录
                    openFileDialog1.FileName = "IP.ini";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        fileNe = openFileDialog1.FileName;
                        output("选择的文件为： " + fileNe);
                        readNeFromIP_ini(fileNe, lstMyListView); //从ip.ini加载站点清单
                    } //if选择了文件
                }
                else  //从网管导入
                {
                    //步骤1， 要获取的MySql参数
                    string DATABASE_SERVER; //数据库IP
                    string DATABASE_NAME;  //数据库名称
                    string DATABASE_PORT; //端口
                    string DATABASE_USER; //帐号-用户名
                    string DATABASE_PWD; //帐号-密码

                    string Section ="BROWSER";
                    string NoText ="None";
                    string iniFilePath =@"D:\OTNM\ui\ini\otnm.ini";  //默认路径，后面再加上文件选择操作。
                    
                    string Key = "DATABASE_SERVER";
                    DATABASE_SERVER = ReadIniData(Section, Key, NoText, iniFilePath);  //1，获取数据库IP
                    Key = "DATABASE_NAME";
                    DATABASE_NAME = ReadIniData(Section, Key, NoText, iniFilePath);  //2，获取数据库名称
                    Key = "DATABASE_PORT";
                    DATABASE_PORT = ReadIniData(Section, Key, NoText, iniFilePath);  //3，获取端口
                    Key = "DATABASE_USER";
                    DATABASE_USER = ReadIniData(Section, Key, NoText, iniFilePath);  //4，获取用户名
                    Key = "DATABASE_PWD";
                    DATABASE_PWD = ReadIniData(Section, Key, NoText, iniFilePath);  //5，获取密码

                    save2FileTime(autoBackupLogPath, DATABASE_SERVER + ", " + DATABASE_NAME + ", " + DATABASE_PORT + ", " + DATABASE_USER + ", " + DATABASE_PWD);//记录到日志
                    //检查5个数据库参数是否获取成功
                    if ((NoText == DATABASE_SERVER) || (NoText == DATABASE_NAME) || (NoText == DATABASE_PORT) || (NoText == DATABASE_USER) || (NoText == DATABASE_PWD)) 
                    {
                        output("获取的数据库参数不全，从网管导入配置失败。请重试或手动导入站点");  //输出到界面，同时记录到文件。
                        return;
                    }

                    string M_str_sqlcon = "server=" + DATABASE_SERVER + ";User Id=" + DATABASE_USER + ";password=" + DATABASE_PWD + ";Database=" + DATABASE_NAME; //建立连接代码
                    MySqlConnection sqlCon = new MySqlConnection(M_str_sqlcon);

                    //设置查询命令1-获取网元类型表，区分是否为IPRAN 800/680/690/8000设备
                    MySqlCommand cmdNeType = new MySqlCommand("select netype,ename from `_netype`", sqlCon);

                    //设置查询命令2-获取网元IP
                    MySqlCommand cmd = new MySqlCommand("select neid,netype,neip1,nename from ne ", sqlCon);

                    //查询结果读取器
                    MySqlDataReader reader = null;

                    Dictionary<string, string> neType = new Dictionary<string, string>();    //定义字典
                    string sqlResult="查询结果：\n";
 
                    try
                    {
                        //打开连接
                        sqlCon.Open();
                        //执行查询，并将结果返回给读取器

                        reader = cmdNeType.ExecuteReader(); //查询1，读取网元类型，保存到字典                        
                        save2FileTime(autoBackupLogPath, "网元类型获取如下：");  //记录到日志
                        while (reader.Read())
                        {
                            neType.Add(reader[0].ToString(), reader[1].ToString()); //添加元素到字典，（netype, ename）
                            save2FileTime(autoBackupLogPath, "网元类型,网元名称：" + reader[0].ToString() + "," + reader[1].ToString());  //记录到日志
                        }
                    } //try
                    catch (Exception objException)
                    {
                        output("从网管查询网元类型时发生意外：" + objException.Message);
                    }
                    finally
                    {
                        reader.Close();
                        sqlCon.Close();
                    }

                    try
                    {
                        //打开连接
                        sqlCon.Open();
                        //执行查询，并将结果返回给读取器

                        reader = cmd.ExecuteReader();  //查询2，读取网元IP
                        long IpStr;
                        string IPaddress;
                        string fileName = workPath +@"\IP.ini"; //保存网管中获取的IP地址.
                        if (File.Exists(fileName)) //清空IP.ini文件
                        {
                            File.Delete(fileName);
                        }
                        save2FileTime(autoBackupLogPath, "查询结果：");//记录到日志
                        while (reader.Read())
                        {
                            IpStr = Convert.ToInt64(reader[2].ToString()); 
                            IPaddress = IntToIp(IpStr); //数字转换为IP,“10.1.1.1”形式
                            sqlResult = "NeID=" + reader[0].ToString() + " ,NeType=" + reader[1].ToString() + " ,NeIp=" + IPaddress + " ,NeName=" + reader[3].ToString() ;
                            save2FileTime(autoBackupLogPath, sqlResult);//记录到日志
                            //遍历value
                            foreach (string neTypeValue in NeTypeList)
                            {
                                if (neTypeValue == neType[reader[1].ToString()])
                                {
                                    save2File(fileName, IPaddress); //保存到文件
                                    break;
                                }
                            }
                        }
                        //MessageBox.Show(sqlResult);
                        readNeFromIP_ini(fileName, lstMyListView); //从ip.ini加载站点清单
                    } //try
                    catch (Exception objException)
                    {
                        output("从网管导入站点时发生意外：" + objException.Message);
                    }
                    finally
                    {
                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }//try
            catch (Exception objException)
            {
                output("站点导入时发生意外：" + objException.Message);
            }

        } //导入站点

        //从IP.ini文件加载站点列表
        private bool readNeFromIP_ini(string ipFilePath, ListView listIpListView)
        {
            try
            {
            bool flag_exist = true;  //标志是否存在重复项，false -无， true -有。
            
            //保存到listview
            if (File.Exists(ipFilePath)) //文件存在
            {
                //读取文件的所有内容
                string[] stringlines = File.ReadAllLines(ipFilePath, Encoding.Default);

                //去除文本中的空行
                stringlines = Array.FindAll(stringlines, line => !string.IsNullOrEmpty(line));

                //清空lstMyListView
                listIpListView.Items.Clear();

                for (int fi = 0; fi < stringlines.Length; fi++) //逐行处理
                {
                    if (true == IsIPAddress(stringlines[fi].Trim()))  //Trim()去掉行首和行尾空格
                    {
                        //增加条件，检查对应网元是否存在
                        if (null == listIpListView.FindItemWithText(stringlines[fi].Trim())) //BUG："abc"会被FindItemWithText认为在"abcd"中存在
                        {

                            listIpListView.Items.Add(stringlines[fi].Trim());
                            output("增加站点： " + stringlines[fi].Trim());
                        }
                        else  //重复站点再次确认
                        {
                            for (int ni = 0; ni < listIpListView.Items.Count; ni++)
                            {
                                flag_exist = false;   //默认不重复，添加
                                if (stringlines[fi].Trim() == listIpListView.Items[ni].Text)
                                {
                                    output("站点非法(重复)： " + stringlines[fi].Trim());
                                    flag_exist = true;   //发现重复，则置位
                                    break;
                                }
                            }

                            if (false == flag_exist) //重复项不存在
                            {
                                listIpListView.Items.Add(stringlines[fi].Trim());
                                output("增加站点： " + stringlines[fi].Trim());
                                flag_exist = true;
                            }
                        }

                    }
                    else
                    {
                        output("站点非法（非x.x.x.x形式或中间有空格）： " + stringlines[fi]);
                    }
                }//for 逐行处理

                //提示保存结果
                if (listIpListView.Items.Count.ToString() == stringlines.Length.ToString())
                {
                    //MessageBox.Show("提醒：站点全部导入！" + "\r\n" +
                    //               "有效站点数量/总站点数据： " + listIpListView.Items.Count.ToString() + "/" + stringlines.Length + "\r\n","提示");

                    //统计站点情况
                    lblNE.Text = "导入站点数/总站点数：" + listIpListView.Items.Count.ToString() + "/" + stringlines.Length;
                    lblDev.Text = "站点情况： " + listIpListView.Items.Count + "站点";  //显示站点个数
                    output("文件中全部站点导入成功：" + listIpListView.Items.Count.ToString());
                }
                else
                {
                    MessageBox.Show("提醒：文件中存在不合法站点！" + "\r\n" +
                                   "有效站点数量/总站点数据： " + listIpListView.Items.Count.ToString() + "/" + stringlines.Length, "提示");

                    //统计站点
                    lblNE.Text = "导入站点数/总站点数：" + listIpListView.Items.Count.ToString() + "/" + stringlines.Length;
                    lblDev.Text = "站点情况： " + listIpListView.Items.Count + "站点";  //显示站点个数
                    output("文件中存在不合法站点！有效站点数量/总站点数据： " + listIpListView.Items.Count.ToString() + "/" + stringlines.Length);
                }

            } //文件存在
            else
            {
                output("文件不存在，不加载站点清单。" + ipFilePath);
            }
            return true;
            }//try
            catch (Exception objException)
            {
                output("加载站点列表时发生意外：" + objException.Message);
                return false;
            }
            finally
            {
                //保存导入的文件，2015.12.31
                saveNeListName();
            }

        }

        //IP地址合法性检查
        public bool IsIPAddress(string ip)
        {            
            try
            {
                if (string.IsNullOrEmpty(ip) || ip.Length < 7 || ip.Length > 15) return false;

                string regformat = @"^((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))$";

                Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

                return regex.IsMatch(ip);
            }//try
            catch (Exception objException)
            {
                output("检查IP地址合法性时发生意外：" + objException.Message);
                return false;
            }

       }

        //按键-导出站点
        private void btnOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string workPath = System.Windows.Forms.Application.StartupPath; //获取启动了应用程序的可执行文件的路径，“D：\fh_bk”形式，末尾不带“\”
                saveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                saveFileDialog1.Title = "导出站点到文件";
                saveFileDialog1.Filter = "All files(*.*)|*.*|ini files (*.ini)|*.ini"; //设备控件保存的文件类型
                saveFileDialog1.FilterIndex = 2; //默认打开*.ini            
                saveFileDialog1.RestoreDirectory = true; //记忆之前打开的目录
                saveFileDialog1.FileName = "IP.ini";

                //saveFileDialog控件特有
                saveFileDialog1.OverwritePrompt = true; //覆盖提示
                saveFileDialog1.CreatePrompt = true; //指定文件不存在，创建前提醒用户

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileNe = saveFileDialog1.FileName;
                    output("选择的文件为： " + fileNe);

                    //保存数据到文件
                    //FileStream fs = new FileStream(fileNe, FileMode.OpenOrCreate, FileAccess.Write);
                    FileStream fs = new FileStream(fileNe, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    sw.Flush();
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);  //跳到文件开头处

                    //整理数据
                    //遍历listview_ip中所有元素
                    for (int i = 0; i < lstMyListView.Items.Count; i++)
                    {
                        sw.WriteLine(lstMyListView.Items[i].Text);
                    }

                    //提示保存结果
                    //MessageBox.Show("保存的站点数量为： " + lstMyListView.Items.Count.ToString(),"提示");
                    lblNE.Text = "导出站点数：" + lstMyListView.Items.Count.ToString();

                    //将数据写到文件
                    sw.Flush();
                    sw.Close();
                    sw.Dispose(); //确保文件对象被完全释放

                } //if选择了文件
            }//try
            catch (Exception objException)
            {
                output("导出站点时发生意外：" + objException.Message);
            }                 
        }

        //增加通知栏托盘，防止误关闭，1-“退出”菜单
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "退出确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        //增加通知栏托盘，防止误关闭，2-“显示”菜单
        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }


        //增加通知栏托盘，防止误关闭，3-“隐藏”菜单
        private void hideMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
        //增加通知栏托盘，防止误关闭,点击“关闭”按键时，隐藏窗体
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
                    //MessageBox.Show("确定要退出程序?");
                    e.Cancel = true;           
                    this.Visible = false;
        }

        //增加通知栏托盘
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {

        }

        //开机自动启动
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //设置开机自启动  
            {
                //MessageBox.Show("设置开机自启动，需要修改注册表", "提示");
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("JcShutdown", path);
                rk2.Close();
                rk.Close();

                saveCfgListName("checkBox1", "true"); //记录配置到文件，开机启动
            }
            else //取消开机自启动  
            {
                //MessageBox.Show("取消开机自启动，需要修改注册表", "提示");
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("JcShutdown", false);
                rk2.Close();
                rk.Close();

                saveCfgListName("checkBox1", "false"); //记录配置到文件，开机不启动
            }  
        }//开机自启动

        //最小化隐藏
        private void Form1_Deactivate(object sender, EventArgs e)
        {
             if (this.WindowState == FormWindowState.Minimized)
                 this.Visible = false;
        }

    }
}
