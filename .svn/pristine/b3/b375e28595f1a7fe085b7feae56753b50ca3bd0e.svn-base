using System;
using System.Collections.Generic;
// using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace GprinterTest
{
    public class LoadPOSDll
    {
        const string _DllVer = "1.4";
        /// <summary>
        /// 获取动态库版本号
        /// </summary>
        public string GetDllVer
        {
            get
            {
                return _DllVer;
            }
        }

      
        /// <summary>
        /// 设备打开后的句柄
        /// </summary>
        public IntPtr POS_IntPtr;

        /// <summary>
        /// 函数返回值
        /// </summary>
        public const int POS_SUCCESS = 1001;// 函数执行成功
        public const int POS_FAIL = 1002;// 函数执行失败
        public const int POS_ERROR_INVALID_HANDLE = 1101;// 端口或文件的句柄无效
        public const int POS_ERROR_INVALID_PARAMETER = 1102;// 参数无效
        public const int POS_ERROR_NOT_BITMAP = 1103;// 不是位图格式的文件
        public const int POS_ERROR_NOT_MONO_BITMAP = 1104;// 位图不是单色的
        public const int POS_ERROR_BEYONG_AREA = 1105;// 位图超出打印机可以
                                        // 处理的大小
        public const int POS_ERROR_INVALID_PATH = 1106;// 没有找到指定的文件
											            // 路径或名称
        public const int POS_COM_DTR_DSR = 0x00;// 流控制为DTR/DST
        public const int POS_COM_RTS_CTS = 0x01;// 流控制为RTS/CTS
        public const int POS_COM_XON_XOFF = 0x02;// 流控制为XON/OFF
        public const int POS_COM_NO_HANDSHAKE = 0x03;// 无握手
        public const int POS_COM_ONESTOPBIT=0x00;// 停止位为1 
        public const int POS_COM_ONE5STOPBITS=0x01;// 停止位为1．5  
        public const int POS_COM_TWOSTOPBITS=0x02;// 停止位为2 
        public const int POS_COM_NOPARITY=0x00;// 无校验 
        public const int POS_COM_ODDPARITY=0x01;// 奇校验 
        public const int POS_COM_EVENPARITY=0x02;// 偶校验 
        public const int POS_COM_MARKPARITY=0x03;// 标记校验 
        public const int POS_COM_SPACEPARITY=0x04;// 空格校验 
        public const int POS_OPEN_PARALLEL_PORT = 0x12;// 打开并口通讯端口
        public const int POS_OPEN_BYUSB_PORT = 0x13;//打开USB通讯端
        public const int POS_OPEN_PRINTNAME = 0X14;// 打开打印机驱动程序
        public const int POS_OPEN_NETPORT = 0X15;// 打开以太网打印机
        public const int POS_FONT_TYPE_STANDARD = 0x00;// 标准 ASCII
        public const int POS_FONT_TYPE_COMPRESSED = 0x01;//压缩 ASCII
        public const int POS_FONT_TYPE_UDC = 0x02;//  用户自定义字符
        public const int POS_FONT_TYPE_CHINESE = 0x03;// 标准 “宋体”
        public const int POS_FONT_STYLE_NORMAL = 0x00;// 正常
        public const int POS_FONT_STYLE_BOLD = 0x08;// 加粗
        public const int POS_FONT_STYLE_THIN_UNDERLINE = 0x80;// 1点粗的下划线
        public const int POS_FONT_STYLE_THICK_UNDERLINE = 0x100;// 2点粗的下划线
        public const int POS_FONT_STYLE_UPSIDEDOWN = 0x200;// 倒置（只在行首有效)
        public const int POS_FONT_STYLE_REVERSE = 0x400;// 反显（黑底白字）
        public const int POS_FONT_STYLE_SMOOTH = 0x800;// 平滑处理（用于放大时）
        public const int POS_FONT_STYLE_CLOCKWISE_90 = 0x1000;// 每个字符顺时针旋转 90 度
        public const int POS_PRINT_MODE_STANDARD = 0x00;// 标准模式（行模式）
        public const int POS_PRINT_MODE_PAGE = 0x01;// 页模式
        public const int POS_PRINT_MODE_BLACK_MARK_LABEL = 0x02;// 黑标记标签模式
        public const int POS_BARCODE_TYPE_UPC_A = 0x02;// UPC-A
        public const int POS_BARCODE_TYPE_UPC_E = 0x41;// UPC-C
        public const int POS_BARCODE_TYPE_JAN13 = 0x43;// JAN13(EAN13)
        public const int POS_BARCODE_TYPE_JAN8 = 0x44;// JAN8(EAN8)
        public const int POS_BARCODE_TYPE_CODE39 = 0x45 ;// CODE39
        public const int POS_BARCODE_TYPE_ITF = 0x46;// INTERLEAVED 2 OF 5
        public const int POS_BARCODE_TYPE_CODEBAR = 0x47;// ICODEBAR
        public const int POS_BARCODE_TYPE_CODE93 = 0x48;// INTERLEAVED 2 OF 5
        public const int POS_BARCODE_TYPE_CODE128 = 0x49;// CODE 128


//         public const int POS_FONT_TYPE_STANDARD = 0x00;//标准ASCII 
//         public const int POS_FONT_TYPE_COMPRESSED = 0x01;//压缩ASCII
        public const int POS_HRI_POSITION_NONE = 0x00;//不打印
        public const int POS_HRI_POSITION_ABOVE = 0x01;//只在条码上方打印
        public const int POS_HRI_POSITION_BELOW = 0x02;//只在条码下方打印
        public const int POS_HRI_POSITION_BOTH = 0x03;//条码上、下方都打印
        public const int POS_BITMAP_PRINT_NORMAL = 0x00;//正常
        public const int POS_BITMAP_PRINT_DOUBLE_WIDTH = 0x01;//倍宽
        public const int POS_BITMAP_PRINT_DOUBLE_HEIGHT = 0x02;//倍高
        public const int POS_BITMAP_PRINT_QUADRUPLE = 0x03;//倍宽且倍高
        public const int POS_CUT_MODE_FULL = 0x00;//全切
        public const int POS_CUT_MODE_PARTIAL = 0x01;//半切
        public const int POS_AREA_LEFT_TO_RIGHT = 0x0;//左上角
        public const int POS_AREA_BOTTOM_TO_TOP = 0x1;//左下角
        public const int POS_AREA_RIGHT_TO_LEFT = 0x2;//右下角
        public const int POS_AREA_TOP_TO_BOTTOM = 0x3;//右上角

        /// <summary>
        /// 打开POS机的端口 开始会话
        /// </summary>
        /// <param name="lpName">
        ///指向以 null 结尾的打印机名称或端口名称。
        ///当参数nParam的值为POS_COM_DTR_DSR、POS_COM_RTS_CTS、POS_COM_XON_XOFF或POS_COM_NO_HANDSHAKE 时， “COM1”，“COM2”，“COM3”，“COM4”等表示串口；
        ///当参数nParam的值为POS_OPEN_PARALLEL_PORT时，“LPT1”，“LPT2”等表示并口；
        ///当参数nParam的值为POS_OPEN_BYUSB_PORT时，“BYUSB-0”、“BYUSB-1”、“BYUSB-2”、“BYUSB-3”等表示USB端口。
        ///当参数nParam的值为POS_OPEN_PRINTNAME时，表示打开指定的打印机。
        ///当参数nParam的值为POS_OPEN_NETPORT时，表示打开指定的网络接口，如“192.168.10.251”表示网络接口IP地址</param>
        /// <param name="nComBaudrate">串口通信需要的波特率</param>
        /// <param name="nComDataBits">串口通信需要的数据位</param>
        /// <param name="nComStopBits">串口通信需要的停止位</param>
        /// <param name="nComParity">串口通信需要的是否要奇偶校验</param>
        /// <param name="nParam">指向以 null 结尾的打印机名称或端口名称。
        /// 参数nParam的值为POS_COM_DTR_DSR、POS_COM_RTS_CTS、POS_COM_XON_XOFF或POS_COM_NO_HANDSHAKE 时，
        /// “COM1”，“COM2”，“COM3”，“COM4”等表示串口；
        /// 当参数nParam的值为POS_OPEN_PARALLEL_PORT时，“LPT1”，“LPT2”等表示并口；
        /// 当参数nParam的值为POS_OPEN_BYUSB_PORT时，“BYUSB-0”、“BYUSB-1”、“BYUSB-2”、“BYUSB-3”等表示USB端口。
        /// 当参数nParam的值为POS_OPEN_PRINTNAME时，表示打开指定的打印机。</param>
        /// <returns>如果函数调用成功，返回一个已打开的端口句柄。如果函数调用失败，返回值为 INVALID_HANDLE_VALUE （-1）。</returns>        [DllImport("POSDLL.dll", CharSet = CharSet.Ansi)]
        [DllImport("POSDLL.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr POS_Open([MarshalAs(UnmanagedType.LPStr)]string lpName,
                                             uint nComBaudrate,
                                             uint nComDataBits,
                                             uint nComStopBits,
                                             uint nComParity,
                                             uint nParam);

        /// <summary>
        /// 关闭已经打开的并口或串口，USB端口，网络接口或打印机。
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_Close();

        /// <summary>
        /// 复位打印机，把打印缓冲区中的数据清除，字符和行高的设置被清除，打印模式被恢复到上电时的缺省模式。
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_Reset();

        /// <summary>
        /// 设置打印机的移动单位。
        /// </summary>
        /// <param name="nHorizontalMU">把水平方向上的移动单位设置为 25.4 / nHorizontalMU 毫米。可以为0到255。</param>
        /// <param name="nVerticalMU">把垂直方向上的移动单位设置为 25.4 / nVerticalMU 毫米。可以为0到255。</param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL POS_ERROR_INVALID_HANDLE POS_ERROR_INVALID_PARAMETER </returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetMotionUnit(uint nHorizontalMU, uint nVerticalMU);
        /// <summary>
        /// 选择国际字符集和代码页
        /// </summary>
        /// <param name="nCharSet">
        /// 指定国际字符集。不同的国际字符集对0x23到0x7E的ASCII码值对应的符号定义是不同的。
        /// 可以为以下列表中所列值之一。
        /// 0x00 U.S.A  0x01 France  0x02 Germany  0x03 U.K. 0x04 Denmark I 0x05 Sweden 
        /// 0x06 Italy 0x07 Spain I  0x08 Japan 0x09 Nonway 0x0A Denmark II 0x0B Spain II 
        /// 0x0C Latin America 0x0D Korea </param>
        /// <param name="nCodePage">
        /// 指定字符的代码页。不同的代码页对0x80到0xFF的ASCII码值对应的符号定义是不同的。
        /// 0x00 PC437 [U.S.A. Standard Europe 0x01 Reserved 0x02 PC850 [Multilingual] 
        /// 0x03 PC860 [Portuguese] 0x04 PC863 [Canadian-French] 0x05 PC865 [Nordic] 
        /// 0x12 PC852 0x13 PC858 
        /// </param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL POS_ERROR_INVALID_HANDLE POS_ERROR_INVALID_PARAMETER </returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetCharSetAndCodePage(uint nCharSet, uint nCodePage);

        /// <summary>
        /// POS字体样式
        /// </summary>
        /// 
//         public uint  POS_FONT_TYPE_STANDARD  = 0x00;// 标准 ASCII 
//         public uint  POS_FONT_TYPE_COMPRESSED = 0x01;// 压缩 ASCII  
//         public uint  POS_FONT_TYPE_UDC = 0x02;       // 用户自定义字符 
//         public uint  POS_FONT_TYPE_CHINESE = 0x03;   // 标准 “宋体” 
//         public uint  POS_FONT_STYLE_NORMAL =  0x00;   //  正常 
//         public uint  POS_FONT_STYLE_BOLD =  0x08;   //  加粗 
//         public uint  POS_FONT_STYLE_THIN_UNDERLINE =  0x80;   //  1点粗的下划线 
//         public uint  POS_FONT_STYLE_THICK_UNDERLINE =  0x100;   //  2点粗的下划线 
//         public uint  POS_FONT_STYLE_UPSIDEDOWN =  0x200;   //  倒置（只在行首有效） 
//         public uint  POS_FONT_STYLE_REVERSE =  0x400;   //  反显（黑底白字） 
//         public uint  POS_FONT_STYLE_SMOOTH =  0x800;   //  平滑处理（用于放大时） 
//         public uint POS_FONT_STYLE_CLOCKWISE_90 = 0x1000;   //  每个字符顺时针旋转 90 度

        /// <summary>
        /// 把将要打印的字符串数据发送到打印缓冲区中，并指定X 方向（水平）上的绝对起始点位置，
        /// 指定每个字符宽度和高度方向上的放大倍数、类型和风格。
        /// </summary>
        /// <param name="pszString">指向以 null 结尾的字符串缓冲区</param>
        /// <param name="nOrgx">指定 X 方向（水平）的起始点位置离左边界的点数。</param>
        /// <param name="nWidthTimes">指定字符的宽度方向上的放大倍数。可以为 1到 6。</param>
        /// <param name="nHeightTimes">指定字符高度方向上的放大倍数。可以为 1 到 6。</param>
        /// <param name="nFontType">指定字符的字体类型。</param>
        /// <param name="nFontStyle">指定字符的字体风格。</param>
        /// <returns></returns>
        
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_S_TextOut([MarshalAs(UnmanagedType.LPStr)]string pszString, 
                                                   uint nOrgx, uint nWidthTimes, uint nHeightTimes, 
                                                   uint nFontType, uint nFontStyle);

        /// <summary>
        /// 设置POS的打印模式 (只有两种 页模式和标准模式)
        /// </summary>
        /// <param name="nPrintMode">
        /// POS_PRINT_MODE_STANDARD 0x00 标准模式（行模式） 
        /// POS_PRINT_MODE_PAGE 0x01 页模式 
        /// POS_PRINT_MODE_BLACK_MARK_LABEL 0x02 黑标记标签模式 
        /// POS_PRINT_MODE_WHITE_MARK_LABEL 0x03 白标记标签模式 </param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetMode(uint nPrintMode);
        /// <summary>
        /// 设置字符的行高。
        /// </summary>
        /// <param name="nDistance">指定行高点数。可以为 0 到 255。每点的距离与打印头分辨率相关。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetLineSpacing(uint nDistance);
        /// <summary>
        /// 设置字符的右间距（相邻两个字符的间隙距离）。
        /// </summary>
        /// <param name="nDistance">指定右间距的点数。可以为 0 到 255。每点的距离与打印头分辨率相关。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetRightSpacing(int nDistance);

        /// <summary>
        /// 向前走纸。
        /// 1．如果在标准打印模式（行模式）下打印文本，则打印缓冲区中的数据，且打印位置自动移动到下一行的行首。
        /// 2．如果在标准打印模式（行模式）下打印位图，则在指定的位置打印位图，且打印位置自动移动到下一行的行首。
        /// 3．如果在页模式或标签模式下，则把需要打印的数据设置在指定的位置，同时把打印位置移动到下一个行首，
        /// 但是并不立即进纸并打印，而是一直到调用 POS_PL_Print 函数时才打印。
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_FeedLine();
        /// <summary>
        /// 打印头换n行 
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_FeedLines(uint nLines);
        
        /// <summary>
        /// 切纸
        /// </summary>
        /// <param name="nMode">模式编号 半切或是全切</param>
        /// <param name="nDistance">走位的距离</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_CutPaper(uint nMode, uint nDistance);

        /// <summary>
        /// 设置右边距
        /// </summary>
        /// <param name="nDistance">右边距</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_SetRightSpacing(uint nDistance);
        /// <summary>
        /// 预下载一幅位图到打印机的 RAM 中，同时指定此位图的 ID 号。
        /// </summary>
        /// <param name="pszPath">指向以 null 结尾的表示位图路径及其文件名的字符串。</param>
        /// <param name="nID">指定将要下载的位图的 ID 号。可以为 0 到 7。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PreDownloadBmpToRAM([MarshalAs(UnmanagedType.LPStr)]string pszPath, uint nID);
        /// <summary>
        /// 预下载一幅或若干幅位图到打印机的 Flash 中。
        /// </summary>
        /// <param name="pszPath">[in] 指向包含若干位图的文件路径及其名称的字符串数组。</param>
        /// <param name="nID">[in] 指定将要下载的位图幅数。可以为 1 到 255。</param>
        /// <returns>如果函数成功，则返回值为 POS_SUCCESS</returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PreDownloadBmpsToFlash(IntPtr[] pszPath, uint nCount);
        /// <summary>
        /// 下载并打印位图
        /// </summary>
        /// <param name="pszPath">指向以null 结尾的包含位图文件路径及其名称的字符串。</param>
        /// <param name="nOrgx">指定将要打印的位图和左边界的距离点数。可以为 0到 65535 点。</param>
        /// <param name="nMode">指定位图的打印模式。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_S_DownloadAndPrintBmp([MarshalAs(UnmanagedType.LPStr)]string pszPath, uint nOrgx, uint nMode);

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_S_PrintBmpInRAM(uint nID, uint nOrgx, uint nMode);

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_S_PrintBmpInFlash(uint nID, uint nOrgx, uint nMode);

        /// <summary>
        /// 通过串口返回当前打印机的状态。此函数是实时的。
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_RTQueryStatus(byte[] address);

        /// <summary>
        /// 通过串口查询打印机当前的状态。此函数是非实时的。
        /// </summary>
        /// <param name="pszStatus">
        /// 指向返回的状态数据的缓冲区，缓冲区大小为 1 个字节。
        /// 0，1 0/1 容纸器中有纸 / 纸将用尽 2，3 0/1 打印头处有纸 / 无纸 
        /// 4，5 0/1 钱箱连接器引脚 3 的电平为低 / 高（表示打开或关闭） 
        /// 6，7 0 保留（固定为0） 
        /// </param>
        /// <param name="nTimeouts">设置查询状态时大约的超时时间（毫秒）。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_QueryStatus(byte[] pszStatus, int nTimeouts);
        /// <summary>
        /// 通过网络接口查询返回当前打印机的状态。
        /// </summary>
        /// <param name="ipAddress">设备IP地址。如“192.168.10.251”。</param>
        /// <param name="pszStatus">
        /// 指向接收返回状态的缓冲区，缓冲区大小为 1 个字节。 
        /// 0 0/1 钱箱连接器引脚 3 的电平为低/高（表示打开或关闭） 
        /// 1 0/1 打印机联机/脱机 
        /// 2 0/1 上盖关闭/打开 
        /// 3 0/1 没有/正在由Feed键按下而进纸 
        /// 4 0/1 打印机没有/有出错 
        /// 5 0/1 切刀没有/有出错 
        /// 6 0/1 有纸/纸将尽（纸将尽传感器探测） 
        /// 7 0/1 有纸/纸用尽（纸传感器探测） 
        /// </param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern int POS_NETQueryStatus([MarshalAs(UnmanagedType.LPStr)]string ipAddress, out Byte pszStatus);

        
        /// <summary>
        /// 设置并打印条码。
        /// </summary>
        /// <param name="pszInfo">指向以 null 结尾的字符串。每个字符允许的范围和格式与具体条码类型有关。</param>
        /// <param name="nOrgx">指定将要打印的条码的水平起始点与左边界的距离点数。可以为 0 到65535。</param>
        /// <param name="nType">
        /// 指定条码的类型。可以为以下列表中所列值之一。
        /// POS_BARCODE_TYPE_UPC_A 0x41 UPC-A POS_BARCODE_TYPE_UPC_E 0x42 UPC-C 
        /// POS_BARCODE_TYPE_JAN13 0x43 JAN13(EAN13) POS_BARCODE_TYPE_JAN8 0x44 JAN8(EAN8) 
        /// POS_BARCODE_TYPE_CODE39 0x45 CODE39 POS_BARCODE_TYPE_ITF 0x46 INTERLEAVED 2 OF 5 
        /// POS_BARCODE_TYPE_CODEBAR 0x47 CODEBAR POS_BARCODE_TYPE_CODE93 0x48 25 
        /// POS_BARCODE_TYPE_CODE128 0x49 CODE 128 </param>
        /// <param name="nWidthX">
        /// 指定条码的基本元素宽度。
        /// 2 0．25mm 0．25mm 0．625mm 3 0．375mm 0．375mm 1．0mm 4 0．5mm 0．5mm 1．25mm 
        /// 5 0．625mm 0．625mm 1．625mm 6 0．75mm 0．75mm 1.875mm 
        /// </param>
        /// <param name="nheight">指定条码的高度点数。可以为 1 到 255 。默认值为162 点。</param>
        /// <param name="nHriFontType">
        /// 指定 HRI（Human Readable Interpretation）字符的字体类型。可以为以下列表中所列值之一。
        /// POS_FONT_TYPE_STANDARD 0x00 标准ASCII POS_FONT_TYPE_COMPRESSED 0x01 压缩ASCII 
        /// </param>
        /// <param name="nHriFontPosition">
        /// 指定HRI（Human Readable Interpretation）字符的位置。
        /// POS_HRI_POSITION_NONE  0x00 不打印 POS_HRI_POSITION_ABOVE 0x01 只在条码上方打印 
        /// POS_HRI_POSITION_BELOW 0x02 只在条码下方打印 POS_HRI_POSITION_BOTH  0x03 条码上、下方都打印 
        /// </param>
        /// <param name="nBytesOfInfo">指定由参数 pszInfoBuffer指向的字符串个数，即将要发送给打印机的字符总数。具体值与条码类型有关。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_S_SetBarcode([MarshalAs(UnmanagedType.LPStr)]string pszInfo,
                                                      uint nOrgx,uint nType,uint nWidthX,uint nheight,
                                                      uint nHriFontType,uint nHriFontPosition,uint nBytesOfInfo);


        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_SetArea(uint nOrgx , uint nOrgY , uint nWidth , uint nheight , uint nDirection );

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_TextOut([MarshalAs(UnmanagedType.LPStr)]string pszString,uint nOrgx,uint nOrgY, 
                                                   uint nWidthTimes, uint nHeightTimes, uint nFontType, uint nFontStyle);

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_PrintBmpInRAM(uint nID , uint nOrgx , uint nOrgY , uint nMode );

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_PrintBmpInFlash(uint nID, uint nOrgx, uint nOrgY, uint nMode);

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_SetBarcode([MarshalAs(UnmanagedType.LPStr)]string pszInfo, 
                                                       uint nOrgx , uint nOrgY , uint nType , uint nWidthX,uint nheight,
                                                       uint nHriFontType,uint nHriFontPosition,uint nBytesOfInfo);

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_Clear();

        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_PL_Print();
        /// <summary>
        /// 往钱箱引脚发送脉冲以打开钱箱。
        /// </summary>
        /// <param name="nID">指定钱箱的引脚。0x00 钱箱连接器引脚2 0x01 钱箱连接器引脚5 </param>
        /// <param name="nOnTimes">指定往钱箱发送的高电平脉冲保持时间，即 nOnTimes × 2 毫秒。可以为1 到 255。</param>
        /// <param name="nOffTimes">指定往钱箱发送的低电平脉冲保持时间，即 nOffTimes × 2 毫秒。可以为1 到 255。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_KickOutDrawer(uint nID, uint nOnTimes, uint nOffTimes);
        /// <summary>
        /// 新建一个打印作业。
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern bool POS_StartDoc();
        /// <summary>
        /// 结束一个打印作业。
        /// </summary>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern bool POS_EndDoc();
        /// <summary>
        /// 发送数据到端口或文件。通用端口打印可以使用此函数 一般不能设置字体大小样式等
        /// </summary>
        /// <param name="hPort">端口或文件句柄。可以通过POS_Open来获取</param>
        /// <param name="pszData">指向将要发送的数据缓冲区。</param>
        /// <param name="nBytesToWrite">指定将要发送的数据的字节数。</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_WriteFile(IntPtr hPort, byte[] pszData, uint nBytesToWrite);
        /// <summary>
        /// 从串口，或USB端口或文件读数据到指定的缓冲区。
        /// </summary>
        /// <param name="hPort">端口或文件句柄。可以通过POS_Open来获取</param>
        /// <param name="pszData">指向将要读取的数据缓冲区。</param>
        /// <param name="nBytesToRead">数据的字节数</param>
        /// <param name="nTimeouts">可能是读取数据的间隔时间</param>
        /// <returns></returns>
        [DllImport("POSDLL.dll", SetLastError = true)]
        public static extern IntPtr POS_ReadFile(IntPtr hPort, byte[] pszData, uint nBytesToRead, uint nTimeouts);

				public IntPtr DownloadBmpsToFlash(IntPtr[] pszPath, uint nCount)
				{
					  return POS_PreDownloadBmpsToFlash(pszPath,nCount);
				}
				public IntPtr PrintBmpInFlash(uint nID, uint nOrgx, uint nMode)
				{
					  return POS_S_PrintBmpInFlash(nID, nOrgx, nMode);
				}
        /// <summary>
        /// 打开打印设备的串口
        /// </summary>
        /// <param name="PrintSerialPort">串口对象(需要先初始化并测试参数都有效的情况下，传进来)</param>
        /// <returns>是否打开成功</returns>
        public bool OpenComPort(ref SerialPort PrintSerialPort)
        { 
            uint i_stopbits=0;
            if (PrintSerialPort.StopBits== StopBits.One)
                i_stopbits=POS_COM_ONESTOPBIT;
            if (PrintSerialPort.StopBits== StopBits.OnePointFive)
                i_stopbits=POS_COM_ONE5STOPBITS;
            if (PrintSerialPort.StopBits== StopBits.Two)
                i_stopbits=POS_COM_TWOSTOPBITS;

            uint i_nComParity=0;
            if (PrintSerialPort.Parity== Parity.None)
                i_nComParity=POS_COM_NOPARITY;
            if (PrintSerialPort.Parity== Parity.Even)
                i_nComParity=POS_COM_EVENPARITY;
            if (PrintSerialPort.Parity== Parity.Odd)
                i_nComParity=POS_COM_ODDPARITY;
            if (PrintSerialPort.Parity== Parity.Space)
                i_nComParity=POS_COM_SPACEPARITY;
            if (PrintSerialPort.Parity== Parity.Mark)
                i_nComParity=POS_COM_MARKPARITY;

            uint i_para=0;
            if (PrintSerialPort.Handshake== Handshake.None)
                i_para=POS_COM_NO_HANDSHAKE;
            if (PrintSerialPort.Handshake== Handshake.RequestToSend)
                i_para=POS_COM_DTR_DSR;
            if (PrintSerialPort.Handshake== Handshake.RequestToSendXOnXOff)
                i_para=POS_COM_RTS_CTS;
            if (PrintSerialPort.Handshake== Handshake.XOnXOff)
                i_para=POS_COM_XON_XOFF;

            POS_IntPtr = POS_Open(PrintSerialPort.PortName, 
                                 (uint)PrintSerialPort.BaudRate,
                                 (uint)PrintSerialPort.DataBits,
                                 i_stopbits, i_nComParity, i_para); 

            if ((int)POS_IntPtr != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 打开打印设备并口
        /// </summary>
        /// <param name="LPTPortName">并口名称</param>
        /// <returns>是否打开成功</returns>
        public bool OpenLPTPort(string LPTPortName)
        {
            POS_IntPtr = POS_Open(LPTPortName, 0, 0, 0, 0, POS_OPEN_PARALLEL_PORT);
            if ((int)POS_IntPtr != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 打开打印设备的网口
        /// </summary>
        /// <param name="IPAddress">设备的IP地址</param>
        /// <returns>是否打开成功</returns>
        public bool OpenNetPort(string IPAddress)
        {
            POS_IntPtr = POS_Open(IPAddress, 0, 0, 0, 0, POS_OPEN_NETPORT);
            if ((int)POS_IntPtr != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 打开打印设备的USB端口
        /// </summary>
        /// <param name="USBPortName">“BYUSB-0”、“BYUSB-1”、“BYUSB-2”、“BYUSB-3”</param>
        /// <returns>是否打开成功</returns>
        public bool OpenUSBPort(string USBPortName)
        {
            POS_IntPtr = POS_Open(USBPortName, 0, 0, 0, 0, POS_OPEN_BYUSB_PORT);
            if ((int)POS_IntPtr != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 使用windows打印驱动程序来驱动OPOS设备
        /// </summary>
        /// <param name="PrintName">打印驱动程序对应的打印机名称</param>
        /// <returns>是否打开成功</returns>
        public bool OpenPrinter(string PrintName)
        {
            POS_IntPtr = POS_Open(PrintName, 0, 0, 0, 0, POS_OPEN_PRINTNAME);
            if ((int)POS_IntPtr != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 关闭设备端口
        /// </summary>
        /// <returns>是否关闭成功</returns>
        public bool ClosePrinterPort()
        {
            IntPtr tmpIntPtr = POS_Close();
            return ((uint)tmpIntPtr == POS_SUCCESS);
        }
    }
}