using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Inicio : MonoBehaviour
{
    public GameObject canvas_error;
    string path = "";

    public void StartApplication()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void Start()
    {
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths";
     //   path = Application.dataPath + "/Paths";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/ApplicationsPaths.txt"))
        {
            File.Create(path + "/ApplicationsPaths.txt").Dispose();

            string data = "0;0;0;0;0;";
            File.WriteAllText(path + "/ApplicationsPaths.txt", data);
        }

        if (File.ReadAllText(path + "/ApplicationsPaths.txt").Contains("0;"))
        {
            canvas_error.SetActive(true);
            Invoke(nameof(CloseLauncher), 2f);
        }


    }
    public void CloseLauncher()
    {
        Application.Quit();
    }
}
