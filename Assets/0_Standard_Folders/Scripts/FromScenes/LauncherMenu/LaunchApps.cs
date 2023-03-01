﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LaunchApps : MonoBehaviour
{
    public static LaunchApps instance;

    public GameObject panel_reminder;

    //TAG: Modificar para añadir juego al Launcher
    //string[] paths = new string[8] { "", "", "", "", "", "", "","" }; //0: Launcher, 1: Intermedio, 2: Gestures, 3: MT, 4: BBT, 5: Clothespin, 6: Fruits
    List<string> paths = new List<string>();

    string path = "";

    string pathSelected = "";

    int appSelected = -1;

    Coroutine wait_enter_coroutine;

    //public string[] Paths { get => paths; set => paths = value; }
    public List<string> Paths1 { get => paths; set => paths = value; }

    public string PathSelected { get => pathSelected; set => pathSelected = value; }
    public int AppSelected { get => appSelected; set => appSelected = value; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this; //Aunque no haga un DontDestroyOnLoad puedo acceder a los campos de este script desde otra escena a través del instance

        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths";
        // path = Application.dataPath + "/Paths";
        ReadPaths();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadPaths()
    {
        string[] data = File.ReadAllLines(path + "/ApplicationsPaths.txt");


        for (int i = 0; i < data.Length; i++)
        {
            paths.Add(data[i]); Debug.Log(paths[i]);
        }
    }

    public void SelectGame(int number)
    { //EL 0 ES EL LAUNCHER

        //****** El 1 ahora es la fase intermedia ********
        //Por eso le sumo 1 al number, para coger el path correcto, pero dejando el resto de usos de la appselected como estaba antes

        appSelected = number; SavingData_launch.appSelected = appSelected;
        pathSelected = paths[number + 1]; SavingData_launch.pathSelected = pathSelected;

        switch (number)
        {
            case 1:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Conecte las gafas VR ahora si se van a utilizar.";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(2).gameObject.SetActive(true);
                //   wait_enter_coroutine = StartCoroutine(WaitInput(0));
                //  Calls.instance.CallLeap();
                break;

            case 2:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Recuerde tener conectadas las gafas VR";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(0).gameObject.SetActive(true);

                //   wait_enter_coroutine = StartCoroutine(WaitInput(2));
                //  Calls.instance.CallUltraleap();
                break;

            case 3:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Conecte las gafas VR ahora si se van a utilizar.";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(2).gameObject.SetActive(true);

                //   wait_enter_coroutine = StartCoroutine(WaitInput(2));
                //  Calls.instance.CallUltraleap();
                break;

            case 4:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Recuerde tener conectadas las gafas VR";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(0).gameObject.SetActive(true);

                //   wait_enter_coroutine = StartCoroutine(WaitInput(1));
                //   Calls.instance.CallUltraleap();
                break;
            case 5:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Conecte las gafas VR ahora si se van a utilizar.";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(2).gameObject.SetActive(true);

                //  Calls.instance.CallUltraleap();
                break;
            case 6:
                panel_reminder.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "Recuerde desconectar las gafas.";

                panel_reminder.transform.Find("Panel_aviso/Icons").GetChild(1).gameObject.SetActive(true);

                //  Calls.instance.CallUltraleap();
                break;
                //TAG: Modificar para añadir juego al Launcher
        }

        wait_enter_coroutine = StartCoroutine(WaitInput());

        panel_reminder.SetActive(true);

        panel_reminder.transform.Find("Panel_aviso/Continue").GetComponent<Button>().Select();
    }

    IEnumerator WaitInput()
    { //vr: 0 → no vr, 1 → sí vr, 2 → ambas
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)));

        LoadPatientMenu();

        StopCoroutine(wait_enter_coroutine);


        //Código para comprobar si está o no conectadas las VR, pero solo puedo comprobar si se han desconectado, no puedo comprobar si se han conectado (no aparecen reflejadas)
        /* List<UnityEngine.XR.XRDisplaySubsystem> xRDisplaySubsystems = new List<UnityEngine.XR.XRDisplaySubsystem>();
         SubsystemManager.GetInstances<UnityEngine.XR.XRDisplaySubsystem>(xRDisplaySubsystems);

         if (xRDisplaySubsystems.Count > 0)
         {
             if (xRDisplaySubsystems[0].running)
             {
                 if (vr == 0)
                 {
                     panel_reminder.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Aún no se han desconectado las gafas VR\n\nPresione Enter cuando esté correcto";
                     StopCoroutine(wait_enter_coroutine);
                     wait_enter_coroutine = StartCoroutine(WaitInput(0));
                 }
             }
             else
             { //Si había VR conectadas desde el inicio pero ahora están desconectadas

             }
         }
         else
         { //Si no hay VR conectadas desde el inicio
             if (vr == 0) 
             { 

             }
         }
        */
    }

    public void LoadPatientMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Patient Menu");
    }

    bool VerifyXR()
    {
        return UnityEngine.XR.XRSettings.isDeviceActive;
    }
}