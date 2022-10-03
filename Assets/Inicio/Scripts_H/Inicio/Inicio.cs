using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

            string data = "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine +
                "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;" + System.Environment.NewLine + "0;";
            File.WriteAllText(path + "/ApplicationsPaths.txt", data);
        }

        if (File.ReadAllText(path + "/ApplicationsPaths.txt").Contains("0;"))
        {
            canvas_error.SetActive(true);
            Invoke(nameof(CloseLauncher), 2f);
        }

        if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt"))
        {
            if (((File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt"))[0] != "--")
                && (bool.Parse(File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt")[3]) == false))
            {
                string[] lines = File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt");
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
                if ((File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt"))[2] == "1")
                {
                    //Es que quiero ir a la escena de Apps directamente
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                }

                if ((File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt"))[2] == "2")
                {
                    //Es que quiero ir a escena pacientes directamente
                    string[] lines = File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt");

                    app_selected = int.Parse(lines[1]);
                    //Guardo la APP y el Path
                    SavingData_launch.appSelected = app_selected;
                    SavingData_launch.pathSelected = File.ReadAllLines(path + "/ApplicationsPaths.txt")[app_selected + 1]; //Como ahora el path de índice 1 es la fase intermedia, le sumo 1 a la appSelected
                    //Voy a escena pacientes directamente
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                }
            }
        }
        else
        {
            string path_patient = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "PatientSelected.txt";
            File.Create(path_patient).Dispose();
            File.WriteAllText(path_patient, "--" + System.Environment.NewLine + "0" + System.Environment.NewLine + "0" +
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