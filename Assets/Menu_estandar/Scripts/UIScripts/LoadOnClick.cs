using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadOnClick : MonoBehaviour
{
    public GameObject warning_panel;

    int option_Selected = -1;

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

   /* public void OpenPause()
    {
        pause = !pause;
        if (pause)
        {
            PauseMenu.Instance.Pause();
        }
        else
        {
            PauseMenu.Instance.UnPause();
        }
    }*/

    public void OpenWarningPanel(string text) //0: reload scene, 1: return menu, 2: change user, 3: exit game
    {
        Time.timeScale = 0;

       // option_Selected = button; Debug.Log(option_Selected);
        warning_panel.SetActive(true);

        //En vez de indicar aquí el texto lo puedo personalizar desde la llamada, así puedo generalizar el uso de este script
        warning_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;

        /*
        switch (button)
        {
            case 0:
                warning_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "¿Desea volver a cargar la escena?";
                break;
            case 1:
                warning_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "¿Desea volver al menú de selección?";
                break;
            case 2:
                warning_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "¿Desea cerrar la sesión y volver al inicio?";
                break;

            case 3:
                warning_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "¿Desea cerrar la aplicación?";
                break;
        }*/
    }

    public void SelectOption(int button)
    {
        option_Selected = button;
    }

    public void CloseWarningPanel()
    {
        option_Selected = -1;
        warning_panel.SetActive(false);

        Time.timeScale = 1;
    }

    public void ChargeOption()
    {
        Time.timeScale = 1;
        Debug.Log(option_Selected);
        switch (option_Selected)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 1:Debug.Log(SceneManager.GetActiveScene().buildIndex - 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
            case 2:
                ReturnToLauncher();
                break;
            case 3:
                ExitGame();
                break;
        }
    }
    public void LoadActualScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ReturnToLauncher()
    { //También vale para cambiar de paciente
        string launcher_path = File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths/ApplicationsPaths.txt")[0];
        Application.OpenURL(launcher_path);
        Application.Quit();
    }



    /*
    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    */
}
