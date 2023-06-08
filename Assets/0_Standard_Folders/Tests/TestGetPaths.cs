using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TestGetPaths : MonoBehaviour
{
    string[] applicationNames = new string[] { "Launcher", "Fase_Intermedia", "Gestures", "MT", "BBT", "Clothespin", "Fruits", "Supermarket" };

    List<string> exePaths = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
#if PLATFORM_STANDALONE_WIN && !UNITY_EDITOR

        //Recojo path del Launcher y lo divido
        string[] splittedPath = Application.dataPath.Split('/');

        //Me quedo con las partes del path que hacen la ruta donde están el resto de carpetas de ejecutables
        string[] rootSplittedPath = new string[splittedPath.Length - 2];
        Array.Copy(splittedPath, rootSplittedPath, splittedPath.Length - 2);

        //Creo el path de raiz
        string rootPath = string.Join("/", rootSplittedPath);

        //Recojo los directorios de todos los juegos
        string[] applicationPaths = Directory.GetDirectories(rootPath);

        //Voy a buscar el directorio por orden de juego, y buscar el archivo dentro de él que tiene un .exe
        //Guardo en una listalos paths de ejecutables
        for (int i = 0; i < applicationPaths.Length; i++)
        {
            string dir = Array.Find(applicationPaths, p => p.Contains(applicationNames[i]));

            if (dir != null)
            {
                string exeFile = Directory.GetFiles(dir).First(f => f.Contains(".exe"));

                exePaths.Add(dir + "/" + exeFile);
            }
            else
            {
                Debug.Log("No se ha encontrado un path. Se deja vacío ese hueco.");

                exePaths.Add("");
            }
        }

        //Por último sobreescribo el .txt de paths
        File.WriteAllLines(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths/ApplicationsPaths.txt", exePaths.ToArray());
#endif

    }

    // Update is called once per frame
    void Update()
    {

    }
}