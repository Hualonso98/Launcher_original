using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class ReadSaveLauncherState : MonoBehaviour
{
    bool isEditor = false;

    public static ReadSaveLauncherState instance;
    string path = "";
    bool isLauncher = false;
    bool isIntermediatePhaseProtocol = false;

    bool allowedSaveState = true;

    public bool AllowedSaveState { get => allowedSaveState; set => allowedSaveState = value; }

    private void Awake()
    {

#if UNITY_EDITOR
        isEditor = true;
#endif

        //Lo hago DontDestroyOnLoad para que se llame al OnApplicationQuit desde cualquier escena que estoy.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        path = Application.persistentDataPath.Replace(Application.productName, "Launcher");
        path += "/ls.dat"; //launcherstate

        //Guardo un bool de si estoy o no en el Launcher, o en la fase intermedia del protocolo
        isLauncher = Application.productName.Contains("Launcher");

        if (isLauncher)
        {
            allowedSaveState = false;
            return;
        }
        else
        {
            //Tanto si es protocolo como si no, me aseguro de primeras que se va a sobreescribir
            //el valor del launcherState al cerrar, poniéndose a false.
            //Si estoy en protocolo y no quiero ponerlo a false, lo evitaré si lanzo una app. Si el protocolo se ha terminado, al cerrar
            //el .exe, se guardará el false.
            allowedSaveState = true;

            isIntermediatePhaseProtocol = Application.productName.Contains("Fase_Intermedia");

            //Si estoy en fase intermedia, voy a comprobar si vengo de Launcher o no.
            //Esto es porque si estoy en protocolo, el launcherState guardado puede ser false (si vengo de un subjuego)

            if (isIntermediatePhaseProtocol)
            {
                Debug.Log("Intermediate_Phase");
                //En fase intermedia no voy a leer el LauncherState, dado que puede ser true o false, dependiendo de dónde venga
                string pathPatientSelected = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
                "/RoboticsLab_UC3M/Develop/PatientSelected.txt";

                string[] lines = File.ReadAllLines(pathPatientSelected);
                bool protocolActived = false;

                if (bool.TryParse(lines[lines.Length - 1], out protocolActived))
                {
                    if (!protocolActived)
                    { //El protocolo no ha sido lanzado desde el launcher. Aviso

                        SetWarning();
                        return;
                    }
                    else
                    {
                        //Si estoy en protocolo activado, sobreescribo a true el state
                        SaveState(true);
                        return;
                    }
                }
                else
                {
                    SetWarning();
                    return;
                }
            }
        }

        //Leo el estado aunque sea el propio Launcher
        //Si me devuelve false en el launcher no hago nada, si me devuelve false en otro juego, entonces abro un panel y cierro el .exe

        //Solo leo si estoy en un subjuego
        if (!ReadState())
        {
            SetWarning();
        }
    }

    public void SetWarning()
    {
        Time.timeScale = 0;

        Debug.Log("warning");

        //Abro panel de aviso
        transform.GetChild(0).gameObject.SetActive(true);

        //Según esté en editor o ejecutable, abro un mesnaje u otro
        if (isEditor) { transform.GetChild(0).Find("Aviso_editor").gameObject.SetActive(true); } else { transform.GetChild(0).Find("Aviso_exe").gameObject.SetActive(true); }

        //Cierro App con delay
        StartCoroutine(CloseApp());
    }

    public void SaveState(bool launcherState)
    {
        //El archivo lo crearé y pondré a true en el Launcher, al lanzar los juegos o el protocolo
        if (!System.IO.File.Exists(path)) { File.Create(path).Dispose(); }

        File.WriteAllText(path, XOREncryptDecrypt(launcherState.ToString()));
    }

    bool ReadState()
    {
        if (File.Exists(path))
        {
            bool state = false;
            bool success = bool.TryParse(XOREncryptDecrypt(System.IO.File.ReadAllText(path)), out state);

            Debug.Log("Success: " + success + " | State: " + state);
            //Uso el Success para comprobar que el archivo existe y no se ha modificado
            //Si el TryParse falla es que está corrupto. Y en ese caso devolverá false siempre
            return state && success;
        }
        else
        { //No existe el archivo, así que no dejo entrar
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public string XOREncryptDecrypt(string inputData)
    {
        StringBuilder outSB = new StringBuilder(inputData.Length);
        for (int i = 0; i < inputData.Length; i++)
        {
            //Here 1234 is key for Encrypt/Decrypt, You can use any int number
            char ch = (char)((inputData[i] ^ 1234));
            outSB.Append(ch);
        }
        return outSB.ToString();
    }

    private void OnApplicationQuit()
    {
        if (allowedSaveState)
        {
            Debug.Log("EXIT. STATE A FALSE");
            SaveState(false); //Al cerrar el juego individual, pongo el estado del Launcher a false para que no se pueda entrar a otro juego
        }
    }
    public void ProtocolIsFinished()
    {
        StartCoroutine(CloseApp());
    }

    IEnumerator CloseApp()
    {
        yield return new WaitForSecondsRealtime(3);
        if (isEditor)
        { //En el editor solo quiero que desaparezca el panel, no apagar.

            //EditorApplication.ExitPlaymode();
            Time.timeScale = 1;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            //Si no existe el archivo, aun así llamo a esta función, dado que lo crea si no existe.
            Application.Quit();

        }
    }
}