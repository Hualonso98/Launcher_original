using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CallExe1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Process p = new Process();
        //p.StartInfo.FileName = "C:\\Users\\hugoa\\OneDrive\\Escritorio\\C#\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\net6.0\\ConsoleApp1.exe";
        // p.StartInfo.FileName = "C:\\Users\\hugoa\\OneDrive\\Escritorio\\b.bat";
        p.StartInfo.FileName = Application.dataPath + "/Bat/b.bat";
        p.StartInfo.Verb = "runas";
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Function()
    {
       

    }
}
