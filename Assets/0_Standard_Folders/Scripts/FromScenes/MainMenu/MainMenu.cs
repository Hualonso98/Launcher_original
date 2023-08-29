using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Transform buttons;
    // Start is called before the first frame update
    void Start()
    {
       DisableProtocolIfNotAllowed();
    }
    public void DisableProtocolIfNotAllowed()
    {
        buttons.GetChild(1).gameObject.SetActive(SavingData_launch.gamesAllowed[1]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPatientMenu()
    {
        SavingData_launch.protocolMode = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Patient Menu");
    }
    public void CallLaunchAppsScene()
    {
        SavingData_launch.protocolMode = false; //Aunque por defecto sea false, por si vuelvo desde el protocolo al menú con los botones
        UnityEngine.SceneManagement.SceneManager.LoadScene("LauncherMenu");
    }
}
