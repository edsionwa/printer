using GprinterTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using System.Security.Cryptography;
using System.Collections;
using System.Net;
using PrinterServer;
using System.Net.Security;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Management;
//using System.Linq;
//using System.Threading.Tasks;

namespace POSdllDemo
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 加密秘钥
        /// </summary>
        public static string SetServerIP = "http://print.mgo.kim:8078";
        public static string MD5_ATTACH = "immersion";
        public static string GetPrinterTask = SetServerIP + "/printer/getPrinterTask";
        public static string SetTaskSuccess = SetServerIP +  "/printer/setTaskSuccess";

        private libUsbContorl.UsbOperation NewUsb=new libUsbContorl.UsbOperation();
        
        /// <summary>
        /// api动态时间，ms
        /// </summary>
        public static int ApiRequestTime = 100;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 字节透传到USB口
        /// </summary>
        /// <param name="str"></param>
        private void SendData2USB1(byte[] str)
        {
        	NewUsb.SendData2USB(str,str.Length);
        }
        
        /// <summary>
        /// 发送数据到usb口
        /// </summary>
        /// <param name="str"></param>
        private void SendData2USB(string str)
        {
        	byte[] by_SendData=System.Text.Encoding.GetEncoding(54936).GetBytes(str);
        	SendData2USB1(by_SendData);
        }
      
          
           
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <returns></returns>
        public static string EncryptWithMD5(string source)
        {
            #region 加密算法
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            return strbul.ToString();
            #endregion 加密算法
        }



        /// <summary>
        /// 从服务器获取打印任务
        /// </summary>
        /// <returns></returns>
        public string Getdata(int printId)
        {
            string machineCode = GetHardwareID() ; 
            string secretKey= GetHardwarePassword(printId);
            string sign = EncryptWithMD5(machineCode + secretKey + MD5_ATTACH);
            string parameterss = "{\"machineCode\": \"" + machineCode + "\", \"secretKey\": \"" + secretKey + "\",\"sign\": \"" + sign + "\"}";
            string trt = HttpPost(GetPrinterTask, parameterss);
            return trt;

        }


        /// <summary>
        /// 服务器打印任务汇报成功
        /// </summary>
        /// <returns></returns>
        public string PrintTaskSuccess(string taskId,int printId)
        {
            string machineCode = GetHardwareID();
            string secretKey = GetHardwarePassword(printId);
            string sign = EncryptWithMD5(machineCode + secretKey + taskId + MD5_ATTACH);
            string parameterss = "{\"machineCode\": \"" + machineCode + "\", \"secretKey\": \"" + secretKey + "\",\"taskId\": \"" + taskId + "\",\"sign\": \"" + sign + "\"}";
            string trt = HttpPost(SetTaskSuccess, parameterss);
            return trt;
        }


        /// <summary>
        /// 接口调用
        /// </summary>
        /// <param name="Url">地址</param>
        /// <param name="postDataStr">参数</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json";
                // request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

                Stream myRequestStream = request.GetRequestStream();
                //如果为gb2312，参数中有汉字时会发生错误： 
                //远程服务器返回错误: (400) 错误的请求。      
                StreamWriter myStreamWriter =
                    new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));

                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                result = retString;
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                
                //LogHelper.Error("", ex);
            }
            return result;
        }


       

        /// <summary>
        /// 打印标签，透传
        /// </summary>
        /// <param name="PrintData"></param>
        /// <returns></returns>
         private Boolean NetLableDataToUsbPrint(string PrintData,int printId)
         {
             Boolean st = false;
             NewUsb.FindUSBPrinter();

             if (NewUsb.USBPortCount < 1)
             {
                 this.toolStripStatusLabel1.Text = "没找到标签打印机，请确认连接或电源开启！";
             }
             else
             {
                 if (NewUsb.LinkUSB(printId-1))
                 {
                     //var iii = NewUsb.mCurrentDevicePath[printId - 1];
                     SendData2USB(PrintData);

                     NewUsb.CloseUSBPort();

                     this.toolStripStatusLabel1.Text = "打印成功！";

                     st= true ;//打印成功
                 }
             }
             return st;
         }

        
        /// <summary>
        /// 创建子结构体
        /// </summary>
      public class Data
      {
          public string machineCode { get; set; }
          public string printerMessage { get; set; }
          public string printerType { get; set; }
          public string secretKey { get; set; }
          public string taskId { get; set; }
      }
        
        /// <summary>
        /// 创建主结构体
        /// </summary>
      public class RootObject
      {
          public string code { get; set; }
          public Data data { get; set; }
          public string msg { get; set; }
      }

     
        /// <summary>
        /// 查看打印机秘钥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      private void toolStripStatusLabel3_Click(object sender, EventArgs e)
      {   int t=printList();
          string temp="设备号=" + GetHardwareID() + "\r\n";
          for (int i=0;i<t;i++){
              temp=temp+"设备"+(i+1).ToString()+"秘钥="+GetHardwarePassword(i+1) + "\r\n" ;
          }
          temp=temp+"本程序主要服务于标签打印机，目前免费开放接口使用。具体联系杭州映墨科技有限公司0571-28285226";
          MessageBox.Show( temp);
      }

      private void Form1_Load(object sender, EventArgs e)
      {
        this.toolStripStatusLabel2.Text =GetHardwareID();
        From1min();
      }

      private void From1min()
      {
          //这个区域不包括任务栏的
          Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
          //这个区域包括任务栏，就是屏幕显示的物理范围
          //Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);

          int width1 = ScreenArea.Width; //屏幕宽度 
          int height1 = ScreenArea.Height; //屏幕高度

          this.Width = 400;
          this.Height = 200;

          this.Location = new System.Drawing.Point(width1 - this.Width, height1 - this.Height);  //指定窗体显示在右下角
          richTextBox01.SelectionStart = 0;
      }

      private void From1max()
      {
          //这个区域不包括任务栏的
          Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
          //这个区域包括任务栏，就是屏幕显示的物理范围
          //Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);

          int width1 = ScreenArea.Width; //屏幕宽度 
          int height1 = ScreenArea.Height; //屏幕高度

          this.Width = 600;
          this.Height = height1;

          this.Location = new System.Drawing.Point(width1 - this.Width, height1 - this.Height);  //指定窗体显示在右下角

      }


      /// <summary>
      /// 设备号
      /// </summary>
      /// <returns></returns>
      public static string GetHardwareID()
      {
          string MACString = GetMACString();
          string SizeOfDisk = GetSizeOfDisk();
          string SizeOfMemery = GetSizeOfMemery();
          string CpuID = GetCpuID();
          string DiskID = GetDiskID();

          string md5s = EncryptWithMD5(MACString + SizeOfDisk + SizeOfMemery + CpuID + DiskID +MD5_ATTACH);
          string  sbid=uint32String(md5s);

          md5s = EncryptWithMD5(MD5_ATTACH+sbid);
          string sbmy = uint32String(md5s);
          return sbid;
      }



      /// <summary>
      /// 设备秘钥
      /// </summary>
      /// <returns></returns>
      public static string GetHardwarePassword(int printCount)
      {
          string md5s;
          string sbmy;
          if (printCount <= 1) { 
              md5s = EncryptWithMD5(MD5_ATTACH + GetHardwareID());
              sbmy = uint32String(md5s);
          }
          else
          {
              md5s = EncryptWithMD5(MD5_ATTACH + GetHardwareID() + printCount.ToString());
              sbmy = uint32String(md5s);
          }
          return sbmy;
      }


      /// <summary>
      /// 16进制数字化
      /// </summary>
      /// <returns></returns>
      public static string uint32String(string zf )
      {
          uint i3 = Convert.ToUInt32(zf.Substring(0, 8), 16);
          uint i2 = Convert.ToUInt32(zf.Substring(8, 8), 16);
          uint i1 = Convert.ToUInt32(zf.Substring(16, 8), 16);
          uint i0 = Convert.ToUInt32(zf.Substring(24, 8), 16);
          uint ii = i0 + i1 + i2 + i3;
          return ii.ToString();
      }

      /// <summary>
      /// 获取本机的MAC地址
      /// </summary>
      /// <returns></returns>
      public static string GetMACString()
      {
          ManagementClass mAdapter = new ManagementClass("Win32_NetworkAdapterConfiguration");
          ManagementObjectCollection mo = mAdapter.GetInstances();
          foreach (ManagementBaseObject m in mo)
          {
              if ((bool)m["IpEnabled"] == true)
              {
                  return m["MacAddress"].ToString();
              }
          }
          mo.Dispose();
          return null;
      }

     

        /// <summary>
       /// 获取硬盘的大小
       /// </summary>
       /// <returns></returns>
       public static string GetSizeOfDisk()
       {
           ManagementClass mc = new ManagementClass("Win32_DiskDrive");
           ManagementObjectCollection moj = mc.GetInstances();
           foreach (ManagementObject m in moj)
           {
               return m.Properties["Size"].Value.ToString();
           }
           return "-1";
       }
    


        /// <summary>
        /// 获取内存的大小
        /// </summary>
        /// <returns></returns>
       public static string GetSizeOfMemery()
       {
           ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
           ManagementObjectCollection moc = mc.GetInstances();

           double sizeAll = 0.0;
           foreach (ManagementObject m in moc)
           {
               if (m.Properties["TotalVisibleMemorySize"].Value != null)
               {
                   sizeAll += Convert.ToDouble(m.Properties["TotalVisibleMemorySize"].Value.ToString());
               }
           }
           mc = null;
           moc.Dispose();

           return sizeAll.ToString();
       }


       /// <summary>
       /// 获取CPU序列号代码
       /// </summary>
       /// <returns></returns>
       public static string GetCpuID()
       {
           try
           {
               //获取CPU序列号代码 
               string cpuInfo = "";//cpu序列号 
               ManagementClass mc = new ManagementClass("Win32_Processor");
               ManagementObjectCollection moc = mc.GetInstances();
               foreach (ManagementObject mo in moc)
               {
                   cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
               }
               moc = null;
               mc = null;
               return cpuInfo;
           }
           catch
           {
               return "unknow";
           }
           finally
           {
           }

       }

       /// <summary>
       ///  获取硬盘ID 
       /// </summary>
       /// <returns></returns>
       public static string GetDiskID()
       {
           try
           {
              
               String HDid = "";
               ManagementClass mc = new ManagementClass("Win32_DiskDrive");
               ManagementObjectCollection moc = mc.GetInstances();
               foreach (ManagementObject mo in moc)
               {
                   HDid = (string)mo.Properties["SerialNumber"].Value;
               }
               moc = null;
               mc = null;
               return HDid;
           }
           catch
           {
               return "unknow";
           }
           finally
           {
           }

       }


        /// <summary>
        /// 定时轮询打印任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void timer1_Tick(object sender, EventArgs e)
       {
           timer1.Enabled = false;
           int t=printList();
           for (int i = 0; i < t; i++) {

           //获取接口数据
           string jsonData = Getdata(i+1);
#region
           //Json.NET反序列化
           RootObject descJsonStu = JsonConvert.DeserializeObject<RootObject>(jsonData);//反序列化
           if (descJsonStu != null)
           {
               this.toolStripStatusLabel1.Text =descJsonStu.code+ descJsonStu.msg;//接收到代码

               if (descJsonStu.data != null && descJsonStu.data.machineCode == GetHardwareID() && descJsonStu.data.secretKey == GetHardwarePassword(i + 1))
               {
                   From1max();
                   string temp = DateTime.Now.ToString();
                   temp = temp + "\r\n设备端口=" + (i+1).ToString();
                   temp = temp + "\r\n解析结果=" + descJsonStu.msg;
                   temp = temp + "；\r\n解析代码=" + descJsonStu.code;
                   temp = temp + "；\r\n任务ID=" + descJsonStu.data.taskId;
                   temp = temp + "；\r\n打印信息:\r\n" + descJsonStu.data.printerMessage;

                   this.richTextBox01.Text = temp+"\r\n"+"\r\n"+"\r\n"+ this.richTextBox01.Text;

                   if (this.richTextBox01.Text.Length>10000)
                   {
                       this.richTextBox01.Text = this.richTextBox01.Text.Substring(0, 10000);
                   }

                   PrintDone(descJsonStu.data,i+1);
                   ApiRequestTime = 100;
               }
               else
               {
                   ApiRequestTime =Convert.ToInt32(ApiRequestTime*1.1);//延时再启动
                   if (ApiRequestTime>5000)
                   {
                       ApiRequestTime=5000;
                   }
                   this.toolStripStatusLabel1.Text ="延时"+ ApiRequestTime.ToString()+"毫秒";
               }

           }

#endregion

           }
           if (ApiRequestTime > 4000){ From1min(); }
           timer1.Interval = ApiRequestTime;
           timer1.Enabled =true;
       }


        /// <summary>
        /// 遍历下设备列表，有插拔就即时更新
        /// </summary>
       private int printList()
       {
           NewUsb.FindUSBPrinter();
           int st = NewUsb.USBPortCount;
           toolStripStatusLabel6.Text = "打印机已连接：" + st + "台";
           return st;//打印失败
       }

        /// <summary>
       /// 打印任务
       /// </summary>
        private void PrintDone(Data data1,int printId)
       {
            //先打印
           if (NetLableDataToUsbPrint(data1.printerMessage, printId))
           {
               PrintTaskSuccess(data1.taskId, printId);//打印成功删除打印消息
           }
       }


        /// <summary>
        /// 窗口尺寸改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            richTextBox01.Width = this.Width - 40;
            richTextBox01.Height = this.Height - 80;
        }

      
    }
}
