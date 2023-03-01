using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Inicio : MonoBehaviour
{
    public GameObject canvas_error;
    public GameObject yes_button;
    public GameObject no_button;

    string path = "";
    int app_selected = 0;

    public void StartApplication()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    private void Start()
    {
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths";
        // path = Application.dataPath + "/Paths";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/ApplicationsPaths.txt"))
        {
            File.Create(path + "/ApplicationsPaths.txt").Dispose();

            //TAG: Modificar para añadir juego al Launcher
            string data = "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine +
                "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;" +
                System.Environment.NewLine + "0;";

            File.WriteAllText(path + "/ApplicationsPaths.txt", data);
        }

        if (File.ReadAllText(path + "/ApplicationsPaths.txt").Contains("0;"))
        {
            canvas_error.SetActive(true);
            Invoke(nameof(CloseLauncher), 2f);
        }

        string patientSelectedPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt";

        if (File.Exists(patientSelectedPath))
        {
            //Commpruebo la cantidad de líneas del array, para ver si es una versión antigua y añadir el campo del protocolo
            string[] linesRead = File.ReadAllLines(patientSelectedPath);
            if (linesRead.Length < 4) { linesRead = linesRead.Append("False").ToArray(); }
            File.WriteAllLines(patientSelectedPath, linesRead);


            if (((File.ReadAllLines(patientSelectedPath))[0] != "--")
                && (bool.Parse(File.ReadAllLines(patientSelectedPath)[3]) == false))
            {
                string[] lines = File.ReadAllLines(patientSelectedPath);
                int ID = int.Parse(lines[0]);
                app_selected = int.Parse(lines[1]);

                string name_app = "";
                switch (app_selected)
                {
                    case 1:
                        name_app = "Gestures";
                        break;
                    case 2:
                        name_app = "MT";
                        break;
                    case 3:
                        name_app = "BBT";
                        break;
                    case 4:
                        name_app = "Clothespin";
                        break;
                    case 5:
                        name_app = "Fruits";
                        break;
                    case 6:
                        name_app = "Supermarket";
                        break;
                        //TAG: Modificar para añadir juego al Launcher
                }
                canvas_error.SetActive(true);
                canvas_error.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 40;
                canvas_error.GetComponentInChildren<TMPro.TextMeshProUGUI>().transform.localPosition += new Vector3(0, 40);
                canvas_error.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Hay una sesión abierta en\n" + name_app + "\ncon el paciente\n" + ID + "\n¿Desea continuarla?";
                yes_button.SetActive(true);
                no_button.SetActive(true);
            }
            else
            {
                if ((File.ReadAllLines(patientSelectedPath))[2] == "1")
                {
                    //Es que quiero ir a la escena de Apps directamente
                    UnityEngine.SceneManagement.SceneManager.LoadScene("LauncherMenu");
                }

                if ((File.ReadAllLines(patientSelectedPath))[2] == "2")
                {
                    //Es que quiero ir a escena pacientes directamente
                    string[] lines = File.ReadAllLines(patientSelectedPath);

                    app_selected = int.Parse(lines[1]);
                    //Guardo la APP y el Path
                    SavingData_launch.appSelected = app_selected;
                    SavingData_launch.pathSelected = File.ReadAllLines(path + "/ApplicationsPaths.txt")[app_selected + 1]; //Como ahora el path de índice 1 es la fase intermedia, le sumo 1 a la appSelected
                    //Voy a escena pacientes directamente
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Patient Menu");
                }
            }
        }
        else
        {
            File.Create(patientSelectedPath).Dispose();
            File.WriteAllText(patientSelectedPath, "--" + System.Environment.NewLine + "0" + System.Environment.NewLine + "0" +
                System.Environment.NewLine + SavingData_launch.protocolMode);
            //La primera línea es el ID del paciente
            //La segunda línea es la aplicación donde estaba
            //La tercera línea es la escena a la que quiero que vaya al abrirse (por defecto 0 (inicio), 1 (Apps) o puede ser la 2 (pacientes)
            //La cuarta línea es la que indica si he seleccionado protocolo

            Debug.Log("No existe");
        }
    }

    public void ContinueSession()
    {
        string path_selected = File.ReadAllLines(path + "/ApplicationsPaths.txt")[app_selected + 1]; //Como ahora el path de índice 1 es la fase intermedia, le sumo 1 a la appSelected

        //Pongo a True el state de que vengo del Launcher
        ReadSaveLauncherState.instance.SaveState(true);

        Application.OpenURL(path_selected);
        CloseLauncher();
    }

    public void ExitSession()
    {
        //Sobreescribo la info: el patient selected a "--" y la app a 0
        File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt",
            "--" + System.Environment.NewLine + "0" + System.Environment.NewLine + "0" + System.Environment.NewLine + SavingData_launch.protocolMode);
        StartApplication();
    }

    public void CloseLauncher()
    {
        Application.Quit();
    }
}