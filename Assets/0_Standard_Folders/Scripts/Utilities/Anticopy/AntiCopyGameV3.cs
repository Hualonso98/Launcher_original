﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using System.Net;
using System.Net.Sockets;
using System.Text;

[DefaultExecutionOrder(200)]
public class AntiCopyGameV3 : MonoBehaviour
{
    private long licenseTime = 12 * 7 * 24 * 60 * 60; // In Seconds, 12 semanas, 3 meses
    public GameObject canvas_timeoff;

    string numberKey = "acp_SGs_Number0000000001";
    string dateKey = "acp_SGs_Date0000000001";
    string lastDateKey = "lastDate";  //Para evitar que puedan atrasar el reloj a antes de la última vez que se abrió el Launcher

    public GameObject canvasReset;
    public TMPro.TMP_InputField passInput;

    string licensePass = "";

    // Use this for initialization
    void Start()
    {
        int val = PlayerPrefs.GetInt(numberKey);

        Debug.Log("acpNumber key al empezar: " + val);

        StartCoroutine(CheckDate(val));
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

        if (inputPassCode == licensePass)
        {
            RenewLicense();
        }
    }

    public void RenewLicense()
    {
        PlayerPrefs.SetInt(numberKey, 0);
        var firstDate = System.DateTimeOffset.Now.ToUniversalTime().ToString();
        PlayerPrefs.SetString(dateKey, firstDate); //Inicializo con first
        PlayerPrefs.SetString(lastDateKey, firstDate); //Inicializo con first
        PlayerPrefs.SetInt(numberKey, 1);

        canvasReset.SetActive(false);
        canvas_timeoff.SetActive(false);
    }

    public void QuitApp()
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    IEnumerator CheckDate(int val)
    {
        yield return new WaitForSeconds(.1f); //Espera necesaria para que de tiempo a conectar el RemoteConfig

        if (val == 0)
        {
            Debug.Log("Primera vez que se entra en el juego");

            RenewLicense();
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

            long diff = now.ToUnixTimeSeconds() - first.ToUnixTimeSeconds(); //Saber si se ha atrasado el reloj antes de la fecha de inicio
            long diff_2 = now.ToUnixTimeSeconds() - lastDate.ToUnixTimeSeconds(); //Saber si se ha atrasado el reloj  antes de la última fecha

            if (diff > licenseTime)
            {
                Debug.Log("Tiempo de licencia excedido");

                canvas_timeoff.SetActive(true);

                //Si hay conexión y la key de renovar licencia es correcta
                if (CheckRemoteKeys.instance.OkConnection &&
                    CheckRemoteKeys.instance.RenewLicense)
                {
                    //Renovar
                    RenewLicense();
                }
                else
                {//No conexión, o conexión pero sin el key correcto

                    //Fijo contraseña especial según la fecha y un factor 3 (de uc3m)
                    licensePass = (int.Parse(DateTime.Now.ToString("ddMMyyyy")) * 3).ToString();
                }
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

                canvas_timeoff.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Se ha alterado la fecha del sistema.\n\nPor favor, ajústela correctamente y contacte con el soporte.";

                canvas_timeoff.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetString(lastDateKey, now.ToUniversalTime().ToString()); //Actualizo la LastDate al Now

                Debug.Log("Quedan: " + (licenseTime - diff) + " segundos de licencia");
            }
        }
    }
}