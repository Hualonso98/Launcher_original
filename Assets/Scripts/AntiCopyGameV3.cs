using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AntiCopyGameV3 : MonoBehaviour
{

    private long licenseTime = 12 * 7 * 24 * 60 * 60; // In Seconds, 12 semanas, 3 meses
    public GameObject canvas_timeoff;

    string numberKey = "acp_SGs_Number0000000001";
    string dateKey = "acp_SGs_Date0000000001";
    string lastDateKey = "lastDate";  //Para evitar que puedan atrasar el reloj a antes de la última vez que se abrió el Launcher

    public GameObject canvasReset;
    public TMPro.TMP_InputField passInput;

    // Use this for initialization
    void Start()
    {
        int val = PlayerPrefs.GetInt(numberKey);

        Debug.Log("acpNumber key al empezar: " + val);

        if (val == 0)
        {
            Debug.Log("Primera vez que se entra en el juego");
            var firstDate = System.DateTimeOffset.Now.ToUniversalTime().ToString();
            PlayerPrefs.SetString(dateKey, firstDate); //Inicializo con first
            PlayerPrefs.SetString(lastDateKey, firstDate); //Inicializo con first
            PlayerPrefs.SetInt(numberKey, 1);
        }
        else if (val == 1)
        {
            Debug.Log("No es la primera vez que se entra");

            //System.DateTime.Now.ToUniversalTime().ToString();
            /*
             VERSIÓN PREVIA: timespan.Substract no funciona con DateTimes, sino con timespan. Entonces el diff estaba mal
             DateTime first;

             System.DateTime.TryParse(PlayerPrefs.GetString(dateKey, ""), out first);
             DateTime now = System.DateTime.Now.ToUniversalTime();
             TimeSpan diff = now.Subtract(first);
             */

            DateTimeOffset first;
            DateTimeOffset.TryParse(PlayerPrefs.GetString(dateKey, ""), out first); //Recupero la fecha de inicio

            DateTimeOffset lastDate;
            DateTimeOffset.TryParse(PlayerPrefs.GetString(lastDateKey, ""), out lastDate); //Recupero la última fecha guardada

            DateTimeOffset now = DateTimeOffset.Now.ToUniversalTime();
           // Debug.Log(first.ToString("G") + " ... " + first.AddSeconds(double.Parse(licenseTime.ToString())).ToString("G"));

            long diff = now.ToUnixTimeSeconds() - first.ToUnixTimeSeconds(); //Saber si se ha atrasado el reloj antes de la fecha de inicio
            long diff_2 = now.ToUnixTimeSeconds() - lastDate.ToUnixTimeSeconds(); //Saber si se ha atrasado el reloj  antes de la última fecha

            if (diff > licenseTime)
            {
                Debug.Log("Tiempo de licencia excedido");
                canvas_timeoff.SetActive(true);
            }
            else if (diff < 0 || diff_2 < 0)
            {
                if (diff < 0)
                {
                    Debug.Log("Tiempo modificado: First > Now");
                }

                if (diff_2 < 0)
                {
                    Debug.Log("Tiempo modificado: LastDate > Now");
                }
               
                canvas_timeoff.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Se ha alterado la fecha del sistema.\n\nPor favor, ajústela o contacte con el soporte.";

                canvas_timeoff.SetActive(true);
               // Invoke(nameof(QuitApp), 2f);
            }
            else
            {
                PlayerPrefs.SetString(lastDateKey, now.ToUniversalTime().ToString()); //Actualizo la LastDate al Now

                Debug.Log("Quedan: " + (licenseTime - diff) + " segundos de licencia");
            }
        }
    }
    public void OpenResetPanel()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            canvasReset.SetActive(true);
        }
    }
    public void ResetDate()
    {
        string inputPassCode = passInput.text;

        passInput.text = "";

        if (inputPassCode == "Ecocin&7.")
        {
            PlayerPrefs.SetInt(numberKey, 0);
            var firstDate = System.DateTimeOffset.Now.ToUniversalTime().ToString();
            PlayerPrefs.SetString(dateKey, firstDate); //Inicializo con first
            PlayerPrefs.SetString(lastDateKey, firstDate); //Inicializo con first
            PlayerPrefs.SetInt(numberKey, 1);

            canvasReset.SetActive(false);
            canvas_timeoff.SetActive(false);
        }
    }

    public void QuitApp()
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}