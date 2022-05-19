using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
public class TestProcess : MonoBehaviour
{
    public static TestProcess Instance;

    Process ultraleap_process = new Process();
    Process leapProcess = new Process();

    int hwnd = 0;
    // Start is called before the first frame update

    public const int SC_MINIMIZE = 6;

    // [DllImport("user32.dll")]
    // [return: MarshalAs(UnmanagedType.Bool)]
    // private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);




    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

    // [DllImport("user32.dll")]
    //  [return: MarshalAs(UnmanagedType.Bool)]
    //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private void Awake()
    {
        Instance = this;

        foreach (Process p in Process.GetProcesses())
        {
            if (p.ProcessName.Contains("Leap"))
            {
                p.Kill();
            }
           /* if (p.ProcessName.Contains("Tracking"))
            {
                p.Kill();
            }*/
        }
    }

    void Start()
    {
        ultraleap_process.StartInfo.FileName = "C:\\Program Files\\Ultraleap\\TrackingControlPanel\\bin\\TrackingControlPanel.exe";
        ultraleap_process.StartInfo.CreateNoWindow = true; // <- imp. line
        ultraleap_process.StartInfo.UseShellExecute = true;
        ultraleap_process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        ultraleap_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        ultraleap_process.Start();



        //ESTO ERA LA PRUEBA DE MINIMZADO, PERO NO FUNCIONA

        /*  Process p = new Process();
          p.StartInfo.FileName = "C:\\Program Files\\Leap Motion\\Core Services\\LeapControlPanel.exe";
          p.StartInfo.Verb = "runas";

          p.StartInfo.UseShellExecute = true;
          p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
          //p.StartInfo.UseShellExecute = true;
          p.StartInfo.RedirectStandardInput = false;
          p.StartInfo.UseShellExecute = true;
          // p.StartInfo.RedirectStandardOutput = true;
          // p.StartInfo.RedirectStandardError = true;
          // Set command to run in a hidden window
          p.StartInfo.CreateNoWindow = true;
          p.Start();
          p.WaitForExit();

          foreach (Process p1 in Process.GetProcesses())
          {
              UnityEngine.Debug.Log(p1.ProcessName);
               if (p1.ProcessName.Contains("eap"))
               {
                   UnityEngine.Debug.Log("Enterrr");
                   //hwnd = p1.MainWindowHandle.ToInt32();
                   //  ShowWindow(p1.MainWindowHandle, SC_MINIMIZE);

                   ShowWindowAsync(p1.MainWindowHandle, 6);
               }
          }*/
    }

    //PARA EL MINIMIZADO

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private void OnApplicationQuit()
    {
       // KillProcessByName("Leap");
    }

    bool KillProcessByName(string name)
    {
        foreach (Process p1 in Process.GetProcesses())
        {
            if (p1.ProcessName.Contains(name))
            {
                p1.Kill();
                return true;
            }
        }

        return false;
    }

    public void SetUltraleap()
    {
        if (KillProcessByName("Leap")) { UnityEngine.Debug.Log("Leap found"); }
        ultraleap_process.StartInfo.FileName = "C:\\Program Files\\Ultraleap\\TrackingControlPanel\\bin\\TrackingControlPanel.exe";

        ultraleap_process.Start();
    }

    public void SetLeapControl()
    {
        if (KillProcessByName("Tracking")) { UnityEngine.Debug.Log("Ultraleap found"); }

        leapProcess.StartInfo.FileName = "C:\\Program Files\\Leap Motion\\Core Services\\LeapControlPanel.exe";
        leapProcess.Start();
    }
}
