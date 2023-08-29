using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AntiCopyGame : MonoBehaviour {


    string directory = "D:/3-Proyectos/Jorge/Cueva_impresión/Reparacion V2/";  
    string fileName = "ACCG";
    //string directory = Application.persistentDataPath + "/";

    void Start ()
    {
        //Debug.Log("El fichero: " + directory + fileName + ".dat");

        if (!File.Exists(directory + fileName + ".dat"))
        {
            Debug.Log("El fichero de seguridad no existe");
            Application.Quit();
        }
        else
        {
            Debug.Log("El fichero de seguridad existe");
        }
    }
}
