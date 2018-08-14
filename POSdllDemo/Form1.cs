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
using GprinterDEMO;
using System.Net.Security;


namespace POSdllDemo
{
    public partial class Form1 : Form
    {
        
        private libUsbContorl.UsbOperation NewUsb=new libUsbContorl.UsbOperation();
        public Form1()
        {
            InitializeComponent();
        }

   
        private void button3_Click(object sender, EventArgs e)
        {
        	NewUsb.FindUSBPrinter();
        	for(int i=0;i<NewUsb.USBPortCount;i++)
        	{
        		if(NewUsb.LinkUSB(i))
        		{

                    SendData2USB("CLS \r\n");//清除影响资料
        			SendData2USB("SIZE 40 mm,30 mm\r\n");//标签尺寸
        			SendData2USB("GAP 2 mm,0 mm\r\n");//间距为1
        			SendData2USB("DENSITY 7\r\n");//打印浓度
                    SendData2USB("REFERENCE 0,0\r\n");
                    SendData2USB("DIRECTION 1\r\n");//打印方向
        			SendData2USB("TEXT 300,210,\"1\",270,1,1,\"18-09-01 14:08:09\"\r\n");
                    SendData2USB("TEXT 20,230,\"TSS24.BF2\",270,1,1,\"杭州大话西游科技有限公司\"\r\n");
                    SendData2USB("TEXT 45,230,\"TSS24.BF2\",270,1,1,\"服务器 DELL T440D\"\r\n");
        			
        			//StreamReader strReadFile=new StreamReader(@"./10.bmp");
        			//byte[] byteReadData=new byte[strReadFile.BaseStream.Length];
        			//strReadFile.BaseStream.Read(byteReadData,0,byteReadData.Length);
        			//strReadFile.Close();
        			//SendData2USB("DOWNLOAD\"10.bmp\",4096,");
        			//SendData2USB(byteReadData);//bmp数据
        			//SendData2USB("PUTBMP 14,110,\"10.bmp\"\r\n");

                    sendBmpToUsb(82, 20, "http://vrimmer.cn/id/034923DFSDFSDF2344df");//10.bmp


        			//SendData2USB("PRINT 1\r\n");///输出打印180813


        			NewUsb.CloseUSBPort();
        		}
        	}
        }
        private void SendData2USB1(byte[] str)
        {
        	NewUsb.SendData2USB(str,str.Length);
        }
        private void SendData2USB(string str)
        {
        	byte[] by_SendData=System.Text.Encoding.GetEncoding(54936).GetBytes(str);
        	SendData2USB1(by_SendData);
        }
      
        
        /// <summary>
        /// 重写metset
        /// </summary>
        /// <param name="buf">设置的数组</param>
        /// <param name="val">设置的数据</param>
        /// <param name="size">数据长度</param>
        /// <returns>void</returns>     
        public void memset(byte[] buf, byte val, int size)
        {
            int i;
            for (i = 0; i < size; i++)
                buf[i] = val;
        }

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        //字符转二维码
        public   Bitmap    QRCodeBimapForString    ( string nr)
        {
            string enCodeString = nr;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 7;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            return qrCodeEncoder.Encode(enCodeString, Encoding.GetEncoding("GB2312"));//
        }

        public void sendBmpToUsb(long x ,long y,string strQRPath)
        {
            Image img = QRCodeBimapForString(strQRPath);

            pictureBox1.Image = img; ;

            string path1 = Path.Combine(System.Windows.Forms.Application.StartupPath,"15.bmp");

            img.Save(path1);


            StreamReader srReadFile = new StreamReader(path1);

            byte[] b_bmpdata = StreamToBytes(srReadFile.BaseStream);//获取读取文件的byte[]数据

            srReadFile.Close();

            byte bmpBitCount = b_bmpdata[0x1c]; //获取位图 位深度
            //if (b_bmpdata[0] != 'B' || b_bmpdata[1] != 'M')
            //{
           //     MessageBox.Show("文件不支持");
                //return;
           // }

            uint byteLeght = (uint)b_bmpdata.Length;
            if (byteLeght > 1024000)
            {
                MessageBox.Show("所选文件过大");
                return;
            }

            byte[] SendBmpData;
            uint sendWidth = 0;     //实际发送的宽
            uint sendHeight = 0;    //实际发送的高
            byte[] setHBit = { 0x80, 0x40, 0x30, 0x10, 0x08, 0x04, 0x02, 0x01 };    //算法辅助 置1
            byte[] clsLBit = { 0x7F, 0xBF, 0xDF, 0xEF, 0xF7, 0xFB, 0xFD, 0xFE };    //算法辅助 置0

            {
                Stream str1 = new MemoryStream();
                Image getimage = Image.FromFile(path1);

                sendWidth = (uint)getimage.Width;      //实际发送的宽
                sendHeight = (uint)getimage.Height;    //实际发送的高

                if (getimage.Height % 8 != 0)
                    sendHeight = sendHeight + 8 - sendHeight % 8;
                if (getimage.Width % 8 != 0)
                    sendWidth = sendWidth + 8 - sendWidth % 8;

                Bitmap getbmp = new Bitmap(getimage);
                //                     Bitmap BmpCopy = new Bitmap(getimage.Width, getimage.Height, PixelFormat.Format32bppArgb);

                SendBmpData = new byte[sendWidth * sendHeight / 8];
                memset(SendBmpData, 0xff, (int)(sendWidth * sendHeight / 8));//0XFF为全白

                #region 求灰度平均值
                Double redSum = 0, geedSum = 0, blueSum = 0;
                Double total = sendWidth * sendHeight;
                byte[] huiduData = new byte[sendWidth * sendHeight / 8];
                for (int i = 0; i < getimage.Width; i++)
                {
                    int ta = 0, tr = 0, tg = 0, tb = 0;
                    for (int j = 0; j < getimage.Height; j++)
                    {
                        Color getcolor = getbmp.GetPixel(i, j);//取每个点颜色
                        ta = getcolor.A;
                        tr = getcolor.R;
                        tg = getcolor.G;
                        tb = getcolor.B;
                        redSum += ta;
                        geedSum += tg;
                        blueSum += tb;
                    }
                }
                int meanr = (int)(redSum / total);
                int meang = (int)(geedSum / total);
                int meanb = (int)(blueSum / total);
                #endregion 求灰度平均值

                #region 抖动效果

                #endregion 抖动效果

                for (int j = 0; j < getimage.Height; j++)
                {
                    for (int i = 0; i < getimage.Width; i++)
                    {
                        Color getcolor = getbmp.GetPixel(i, j);//取每个点颜色
                        if ((getcolor.R * 0.299) + (getcolor.G * 0.587) + (getcolor.B * 0.114) < ((meanr * 0.299) + (meang * 0.587) + (meanb * 0.114)))//颜色转灰度(可调 0-255)
                            SendBmpData[j * sendWidth / 8 + i / 8] &= clsLBit[i % 8];
                    }
                }
            }

            SendData2USB("BITMAP "+ x + ","  +  y  + "," + (sendWidth / 8).ToString() + "," + sendHeight.ToString() + ",0,");
            SendData2USB1(SendBmpData);
        }


       // MD5加密
        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            return strbul.ToString();
        }


        //API接口测试
         public void APITest(string filename)
        {
            string url = "http://poll.kuaidi100.com/poll/query.do";
            Encoding encoding = Encoding.GetEncoding("utf-8");

            //参数
            String strCOM = "tiantian";
            String strNUM = "668972007276";
            String customer = "AB3088B4555C12BA92D9D70402FA607E";
            String key = "mzZruOYB1485";
            string resu = "\"resultv2\":0";

            String param = "{\"com\":\""+ strCOM +"\",\"num\":\"" + strNUM + "\",\"from\":\"\",\"to\":\"\","+resu+"}";
           
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            string OutString = EncryptWithMD5(param + key + customer);
            String sign = OutString.ToUpper();

            //string parameters =


            IDictionary parameters = new Dictionary<string, string>();
            parameters.Add("param", param);
            parameters.Add("customer", customer);
            parameters.Add("sign", sign);

            string parameters1 =url+ "?customer=" + customer + "&sign=" + sign +"&param=" + param;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(parameters1); //构建http request
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //发出请求并获得Response
            Stream  resStream = response.GetResponseStream();

            string content = "";
            using (StreamReader sr = new StreamReader(resStream))
            {
                content = sr.ReadToEnd();
                MessageBox.Show(content);
                
            }

        }

      private void button7_Click(object sender, EventArgs e)
        {
            NewUsb.FindUSBPrinter();
            APITest("9");

            for (int i = 0; i < NewUsb.USBPortCount; i++)
            {
                if (NewUsb.LinkUSB(i))
                {

                    SendData2USB("CLS \r\n");//清除影响资料
                    SendData2USB("SIZE 40 mm,30 mm\r\n");//标签尺寸
                    SendData2USB("GAP 2 mm,0 mm\r\n");//间距为1
                    SendData2USB("DENSITY 7\r\n");//打印浓度
                    SendData2USB("REFERENCE 0,0\r\n");
                    SendData2USB("DIRECTION 1\r\n");//打印方向
                    SendData2USB("TEXT 20,30,\"TSS24.BF2\",0,1,1,\"杭州大话西游科技有限公司\"\r\n");
                    SendData2USB("TEXT 20,50,\"TSS24.BF2\",0,1,1,\"服务器 DELL T440D\"\r\n");

                    SendData2USB("BARCODE 20,110,\"128\",48,1,0,2,4,\"1258664563645\"\r\n");

                    System.DateTime  Nowtimer1=new System.DateTime();
                    Nowtimer1=System.DateTime.Now;

                    
                    SendData2USB("TEXT 20,210,\"1\",0,1,1,\""+Nowtimer1.ToString() +"\"\r\n");

                    //SendData2USB("TEXT 20,210,\"1\",0,1,1,\"18-09-01 14:08:09\"\r\n");

                    //StreamReader strReadFile=new StreamReader(@"./10.bmp");
                    //byte[] byteReadData=new byte[strReadFile.BaseStream.Length];
                    //strReadFile.BaseStream.Read(byteReadData,0,byteReadData.Length);
                    //strReadFile.Close();
                    //SendData2USB("DOWNLOAD\"10.bmp\",4096,");
                    //SendData2USB(byteReadData);//bmp数据
                    //SendData2USB("PUTBMP 14,110,\"10.bmp\"\r\n");

                   // sendBmpToUsb(82, 20, "http://vrimmer.cn/id/034923DFSDFSDF2344df");//10.bmp


                   // SendData2USB("PRINT 1\r\n");   //取消打印


                    NewUsb.CloseUSBPort();
                }
            }
        }

       
    }
}
