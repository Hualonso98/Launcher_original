using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
public class CallExe : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Bat"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Bat");
        }
    }

    private void Start()
    {
        foreach (Process pi in Process.GetProcesses())
        {
            if (pi.ProcessName.Contains("LeapControl"))
            {
                UnityEngine.Debug.Log("LeapControl");
                pi.Kill();
            }
        }

        Process p = new Process();
        //p.StartInfo.FileName = "C:\\Users\\hugoa\\OneDrive\\Escritorio\\C#\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\net6.0\\ConsoleApp1.exe";
        //  p.StartInfo.FileName = "C:\\Users\\hugoa\\OneDrive\\Escritorio\\a.bat";
        p.StartInfo.FileName = Application.dataPath + "/Bat/a.bat";
        p.StartInfo.Verb = "runas";
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();

     //   Invoke(nameof(Function), 1f);
    }
}
