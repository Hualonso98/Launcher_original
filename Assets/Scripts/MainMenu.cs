using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPatientMenu()
    {
        SavingData_launch.protocolMode = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void CallLaunchAppsScene()
    {
        SavingData_launch.protocolMode = false; //Aunque por defecto sea false, por si vuelvo desde el protocolo al menú con los botones
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
