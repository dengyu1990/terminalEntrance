using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace underExtract
{
    class Program
    {
        public static PerformanceCounter cpu;
        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);
        //以下异常退出保护引用
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        /// <summary>
        /// 禁用关闭按钮
        /// </summary>
        static void closebtn()
        {
            IntPtr windowHandle = FindWindow(null, "Icefish Under Extract");
            IntPtr hideConsole = FindWindow("ConsoleWindowClass", "Icefish Under Extract");
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
            //ShowWindow(hideConsole, 0); //隐藏当前主窗体
        }

        public static Socket TerminalSocket { get; set; }
        static void Main(string[] args)
        {
            Console.Title = "Under Extract";            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseConsole);
            Console.WriteLine("------------Icefish Agent------------");
            Console.WriteLine("集群分类：应用");
            Console.WriteLine("当前机器名称: {0}  处理器个数:{1}", Environment.MachineName, Environment.ProcessorCount);
            Console.WriteLine("操作系统版本:" + Environment.OSVersion);
            Console.WriteLine("运行域帐号: {0}\\{1}  收集启动时间:{2}\n\n采集引擎已启动", Environment.UserDomainName, Environment.UserName, DateTime.Now.ToString());
            //临时关闭
            cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            MEMORY_INFO MemInfo;
            MemInfo = new MEMORY_INFO();

            //客户端连接服务器端,创建Socket对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TerminalSocket = socket;           
            //连接服务端即可
            try
            {
                socket.Connect(IPAddress.Parse("10.30.3.12"), 3331);  //应用
                Console.WriteLine("性能通道建立,数据传输中...\n （按Ctrl+C退出）");
            }
            catch
            {
                Console.WriteLine("性能数据传输通道验证失败! 〒▽〒 \n请检查主控服务端是否已经开启？\n按Ctrl+C退出...");
                Console.ReadKey();
                return;
            }
            closebtn(); //验证通道后再隐藏哈
            while (true)
            {
                GlobalMemoryStatus(ref MemInfo);
                string percentage = Convert.ToInt32(cpu.NextValue()).ToString();
                string str_data = string.Format(":{0}:{1}", percentage, MemInfo.dwMemoryLoad.ToString());
                if (TerminalSocket.Connected)
                {
                    byte[] data = Encoding.Default.GetBytes(str_data);
                    try
                    {
                        TerminalSocket.Send(data, 0, data.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("远程服务端口异常关闭:" + ex.ToString());
                        Console.ReadKey();                                      
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        private static void StopConnect()
        {
            try
            {
                if (TerminalSocket.Connected)
                {
                    TerminalSocket.Shutdown(SocketShutdown.Both);
                    TerminalSocket.Close(100); //如果100秒还没关闭就强制关闭
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>  
        /// 关闭时的事件  
        /// </summary>  
        /// <param name="sender">对象</param>  
        /// <param name="e">参数</param>  
        protected static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            StopConnect();
            Console.WriteLine("已经停止传输");
            Environment.Exit(0);
            //return;
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct CPU_INFO
    {
        public uint dwOemId;
        public uint dwPageSize;
        public uint lpMinimumApplicationAddress;
        public uint lpMaximumApplicationAddress;
        public uint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public uint dwProcessorLevel;
        public uint dwProcessorRevision;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public uint dwTotalPhys;
        public uint dwAvailPhys;
        public uint dwTotalPageFile;
        public uint dwAvailPageFile;
        public uint dwTotalVirtual;
        public uint dwAvailVirtual;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME_INFO
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
}
