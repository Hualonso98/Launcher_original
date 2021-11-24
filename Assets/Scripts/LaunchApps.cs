using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LaunchApps : MonoBehaviour
{
    public static LaunchApps instance;

    string[] paths = new string[4] { "", "", "", "" };
    string path = "";

    string pathSelected = "";

    int appSelected = -1;

    public string[] Paths { get => paths; set => paths = value; }
    public string PathSelected { get => pathSelected; set => pathSelected = value; }
    public int AppSelected { get => appSelected; set => appSelected = value; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this; //Aunque no haga un DontDestroyOnLoad puedo acceder a los campos de este script desde otra escena a través del instance

        path = Application.dataPath + "/Paths";
        
        ReadPaths();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadPaths()
    {
        string data = File.ReadAllText(path + "/ApplicationsPaths.txt");

        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = data.Split(';')[i]; Debug.Log(paths[i]);
        }
    }

    public void OpenGame(int number)
    {
        appSelected = number;
        pathSelected = paths[number];
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
