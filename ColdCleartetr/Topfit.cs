
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace top_bot
{
    class Topfit
    {
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public static IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName("tetris"));
        //根据进程名获取PID
        public static int GetPidByProcessName(string processName)
        {
            //processName = "tetris";
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            //Process[] arrayProcess = Process.GetProcesses ();
            foreach (Process p in arrayProcess)
            {
                //Console.WriteLine(p.ProcessName);
                if (p.ProcessName == processName)
                    return p.Id;
            }
            return 0;
        }


        public static int[] Get_MuliNext(int nextcnt)
        {
            nextcnt = Math.Min(Math.Max(1, nextcnt), 10);
            byte[] buffer = new byte[4];
            byte[] buffer1 = new byte[4];
            int data;
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr byteAddress1 = Marshal.UnsafeAddrOfPinnedArrayElement(buffer1, 0);
            ReadProcessMemory(hProcess, (IntPtr)(0x400000 + 0x00192164), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x8), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x8), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x3c), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x10), byteAddress, 4, IntPtr.Zero);
            int[] Nexttab = new int[nextcnt];
            for (int i = 0; i < nextcnt; ++i)
            {
                ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + i * 4), byteAddress1, 4, IntPtr.Zero);
                Nexttab[i] = Marshal.ReadInt32(byteAddress1);
            }
            return Nexttab;
        }

        public static bool[,] Get_MuliMap()
        {
            byte[] buffer = new byte[4];
            byte[] buffer1 = new byte[4];
            int data;
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr byteAddress1 = Marshal.UnsafeAddrOfPinnedArrayElement(buffer1, 0);
            ReadProcessMemory(hProcess, (IntPtr)(0x400000 + 0x00191D54), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x30), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x114), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x4), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x1c), byteAddress, 4, IntPtr.Zero);
            //ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x1e84), byteAddress, 4, IntPtr.Zero);
            bool[,] field = new bool[40, 10];
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x1e84 + j * 20 - i * 200), byteAddress1, 4, IntPtr.Zero);
                    field[i, j] = Marshal.ReadInt32(byteAddress1) == 0;
                }
            }
            return field;
        }

        public static int[] Get_Mulistat()
        {
            byte[] buffer = new byte[4];
            byte[] buffer1 = new byte[4];

            byte[] playid = new byte[4];
            byte[] b2b = new byte[1];
            byte[] ren = new byte[4];
            byte[] rbs = new byte[4];
            byte[] Base = new byte[4];

            int data;
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr byteAddress1 = Marshal.UnsafeAddrOfPinnedArrayElement(buffer1, 0);

            IntPtr playidAddress = Marshal.UnsafeAddrOfPinnedArrayElement(playid, 0);
            IntPtr b2bAddress = Marshal.UnsafeAddrOfPinnedArrayElement(b2b, 0);
            IntPtr renAddress = Marshal.UnsafeAddrOfPinnedArrayElement(ren, 0);
            IntPtr rbsAddress = Marshal.UnsafeAddrOfPinnedArrayElement(rbs, 0);
            IntPtr BaseAddress = Marshal.UnsafeAddrOfPinnedArrayElement(Base, 0);

            ReadProcessMemory(hProcess, (IntPtr)(0x400000 + 0x192530), playidAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(0x400000 + 0x192234), BaseAddress, 4, IntPtr.Zero);
            IntPtr[] addr = new IntPtr[7];
            IntPtr[] player = new IntPtr[7];
            for (int i = 0; i < 7; ++i)
            {
                addr[i] = Marshal.UnsafeAddrOfPinnedArrayElement(new byte[4], 0);
                player[i] = Marshal.UnsafeAddrOfPinnedArrayElement(new byte[4], 0);
            }
            addr[0] = BaseAddress;

            for (int i = 0; i < 7; ++i)
            {
                if (Marshal.ReadInt32(addr[i]) == 0)
                {
                    continue;
                }
                for (int j = 0; j < 3; ++j)
                {
                    byte[] temp = new byte[4];
                    IntPtr tempAddress = Marshal.UnsafeAddrOfPinnedArrayElement(temp, 0);
                    ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(addr[i]) + j * 4), tempAddress, 4, IntPtr.Zero);
                    for (int k = 0; k < 7; ++k)
                    {
                        if (Marshal.ReadInt32(addr[k]) == Marshal.ReadInt32(tempAddress))
                        {
                            break;
                        }
                        if (Marshal.ReadInt32(addr[k]) == 0)
                        {
                            addr[k] = tempAddress;
                            break;
                        }
                    }

                }
            }
            for (int i =1; i < 7; ++i)
            {
                ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(addr[i]) + 0x10), player[i], 4, IntPtr.Zero);
                if (Marshal.ReadInt32(player[i])  != 0) { 
                    byte[] seat = new byte[4];
                    IntPtr seatAddress = Marshal.UnsafeAddrOfPinnedArrayElement(seat, 0);
                    ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(player[i]) + 0x4c), seatAddress, 4, IntPtr.Zero);

                    if (Marshal.ReadInt32(seatAddress) == Marshal.ReadInt32(playidAddress))
                    {
                        IntPtr res = Marshal.UnsafeAddrOfPinnedArrayElement(new byte[4], 0);
                        ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(player[i]) + 0x64), res, 4, IntPtr.Zero);
                        ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(res) + 0x2ec0), renAddress, 1, IntPtr.Zero);
                        ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(res) + 0x2406), b2bAddress, 1, IntPtr.Zero);
                        ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(res) + 0x249c), rbsAddress, 1, IntPtr.Zero);
                        return new int[] { Marshal.ReadInt32(renAddress), Marshal.ReadByte(b2bAddress), Marshal.ReadInt32(rbsAddress) };
                    }

                }
            }
            return new int[] { 0,0,0};
        }

        //    //读取内存中的值
        //    public static int ReadMemoryValue(int baseAddress, string processName)
        //    {
        //        try
        //        {
        //            byte[] buffer = new byte[4];
        //            //获取缓冲区地址
        //            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
        //            //打开一个已存在的进程对象  0x1F0FFF 最高权限
        //            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
        //            //将制定内存中的值读入缓冲区
        //            ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
        //            //关闭操作
        //            CloseHandle(hProcess);
        //            //从非托管内存中读取一个 32 位带符号整数。
        //            return Marshal.ReadInt32(byteAddress);
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }

        //    //将值写入指定内存地址中
        //    public static void WriteMemoryValue(int baseAddress, string processName, int value)
        //    {
        //        try
        //        {
        //            //打开一个已存在的进程对象  0x1F0FFF 最高权限
        //            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
        //            //从指定内存中写入字节集数据
        //            WriteProcessMemory(hProcess, (IntPtr)baseAddress, new int[] { value }, 4, IntPtr.Zero);
        //            //关闭操作
        //            CloseHandle(hProcess);
        //        }
        //        catch { }
        //    }

    }
}
