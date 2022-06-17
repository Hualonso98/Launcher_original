using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class Calls : MonoBehaviour
{
    public static Calls instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

        }

        if (Input.GetKeyDown(KeyCode.R))
        { //Esto funciona perfecto para sucesivos cambios
            BatA();
            Invoke(nameof(BatB), 1f);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = Application.dataPath + "/Bat/a.bat";
            p1.StartInfo.Verb = "runas";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = Application.dataPath + "/Bat/b.bat";
            p1.StartInfo.Verb = "runas";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = Application.dataPath + "/Bat/c.bat";
            p1.StartInfo.Verb = "runas";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
        }
    }

    private void BatA()
    {
        Process p = new Process();
        p.StartInfo.FileName = Application.dataPath + "/Bat/a.bat";
        p.StartInfo.Verb = "runas";
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();
    }

    private void BatB()
    {
        Process p = new Process();
        p.StartInfo.FileName = Application.dataPath + "/Bat/b.bat";
        p.StartInfo.Verb = "runas";
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();

        Invoke(nameof(CallLeap2), 1f);
    }

    private void BatC()
    {
        Process p = new Process();
        p.StartInfo.FileName = Application.dataPath + "/Bat/c.bat";
        p.StartInfo.Verb = "runas";
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();
    }

    public void CallLeap()
    {
        foreach (Process p in Process.GetProcesses())
        {
            if (p.ProcessName.Contains("LeapControl"))
            {
                UnityEngine.Debug.Log("Leap found");
                p.Kill();
            }
        }

        //Para la primera ejecución, después de reiniciar el Ordenador va perfecto.
        //Para los sucesivos cambios va bien, pero se acaba abriendo el panel de control de Leap en vez de estar oculto
        BatA();

        Invoke(nameof(BatB), 2f);
       // BatB();
    }

    void CallLeap2()
    {
        Process p1 = new Process();
        p1.StartInfo.FileName = "C:\\Program Files\\Leap Motion\\Core Services\\LeapControlPanel.exe";
       
          p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
          p1.StartInfo.CreateNoWindow = true;
        p1.Start();
    }

    public void CallUltraleap()
    {
        //Esto funciona perfecto para sucesivos cambios
        BatA();

       Invoke(nameof(BatC),1);
    }
}
