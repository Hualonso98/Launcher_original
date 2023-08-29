using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AntiCopyGameV2 : MonoBehaviour {

    private string saveName = "noc";
    //Note: 1 tick = 10.000.000 segundos
    //private long period =2 * 24 * 60 * 60 * 10000000L; // In ticks: the first value is the numbers of day
    private long period = 1 * 60 * 10000000L; // In ticks: the first value is the numbers of minutes
    private long licenseTime = 20 * 60 * 60; // In seconds

    /*
    // Use this for initialization
    void Start ()
    {
        //PlayerPrefs.SetInt("acpNumber", 0); 

        int val = PlayerPrefs.GetInt("acpNumber");

        Debug.Log("acpNumber key al empezar: " + val);

        if (val == 0)
        {
            Debug.Log("Primera vez que se entra en el juego");
            var firstDate = System.DateTime.Now.ToUniversalTime().ToString();
            PlayerPrefs.SetString("acpDate", firstDate);
            PlayerPrefs.SetInt("acpNumber", 1);
        }
        else if (val == 1)
        {
            Debug.Log("No es la primera vez que se entra");

            //System.DateTime.Now.ToUniversalTime().ToString();
            DateTime first;

            System.DateTime.TryParse(PlayerPrefs.GetString("acpDate", ""), out first);
            DateTime now = System.DateTime.Now.ToUniversalTime();
            TimeSpan diff = now.Subtract(first);
            if (diff.Seconds > licenseTime)
            {
                Debug.Log("Tiempo de licencia excedido");
                Application.Quit();
            }
            else if (diff.Seconds < 0)
            {
                Debug.Log("Tiempo modificado");
                Application.Quit();
            }
            else
            {
                Debug.Log("Quedan: " + (licenseTime - diff.Seconds) + " segundos de licencia");
            }
        }

        //SaveFirst();
        //Load();
    }
    */


    void Start()
    {
        //PlayerPrefs.SetInt("acpNumber", 0);

        int val = PlayerPrefs.GetInt("acpNumber");

        Debug.Log("acpNumber key al empezar: " + val);

        if (val == 0)
        {
            BinaryFormatter bf = new BinaryFormatter();

            string directory = Application.persistentDataPath + "/";

            Debug.Log("Primera vez que se entra en el juego");

            long timeStamp = System.DateTime.Now.Ticks + period;

            Debug.Log(directory + saveName + ".dat");

            FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + ".dat");

            //Save data to file
            bf.Serialize(file, timeStamp);
            Debug.Log("Guardado en la ruta: " + Application.persistentDataPath);
            file.Close();

            PlayerPrefs.SetInt("acpNumber", 1);
        }
        else if (val == 1)
        {
            Debug.Log("No es la primera vez que se entra");

            Load();
        }

        //SaveFirst();
        //Load();
    }


    /*
    public void SaveFirst()
    {
        BinaryFormatter bf = new BinaryFormatter();

        string directory = Application.persistentDataPath + "/";

        if (File.Exists(directory + saveName + ".dat"))
        {
            Debug.Log("El fichero ya existe");
        }
        else if (!File.Exists(directory + saveName + ".dat") || PlayerPrefs.GetInt("acp") == 0)
        {
            long timeStamp = System.DateTime.Now.Ticks + period;      

            Debug.Log(directory + saveName + ".dat");

            FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + ".dat");

            //Save data to file
            bf.Serialize(file, timeStamp);
            Debug.Log("Guardado en la ruta: " + Application.persistentDataPath);
            file.Close();
        }
    }
    */
    public void Load()
    {
        string directory = Application.persistentDataPath + "/";

        Debug.Log("directory + saveName + .dat: " + directory + saveName + ".dat");

        if (File.Exists(directory + saveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open((directory + saveName + ".dat"), FileMode.Open);

            long LoadedData = (long)bf.Deserialize(file);
            file.Close();

            // Later  (after re-reading timestamp)...
            if (System.DateTime.Now.Ticks >= (LoadedData))
            {
                Debug.Log("Tiempo de prueba superado");

                Application.Quit();
            }
            else if (System.DateTime.Now.Ticks < (LoadedData - period))
            {
                Debug.Log("Se ha modificado el tiempo");
                Application.Quit();
            }
            else
            {
                Debug.Log("Aun tiene tiempo para jugar, le quedan: " + ((LoadedData - System.DateTime.Now.Ticks) / (10000000)) + " segundos");
            }
        }
        else
        {
            Debug.Log("El fichero no existe");
            Application.Quit();
        }
    }

}
