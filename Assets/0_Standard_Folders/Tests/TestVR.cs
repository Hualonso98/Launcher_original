using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class TestVR : MonoBehaviour
{/// <summary>
/// Script para probar si se pueden detectar las gafas después del play, o reconectarlas después de desconectarlas.
/// No ha funcionado nada. Los booleanos siguen a true aunque descnecte las gafas, excepto deviceActive
/// </summary>
    public bool deviceActive = false;

    public bool enabled_xr = false;
    public bool initialization = false;

    public bool xrsubsystem_1 = false;
    public bool xrsubsystem_2 = false;

    public bool devicesList = false;

    public XRLoader loader;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deviceActive = XRSettings.isDeviceActive;
        enabled_xr = XRSettings.enabled;
        initialization = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.isInitializationComplete;

        List<UnityEngine.XR.XRInputSubsystem> xRInputSubsystems = new List<UnityEngine.XR.XRInputSubsystem>();
        SubsystemManager.GetInstances<UnityEngine.XR.XRInputSubsystem>(xRInputSubsystems);

        loader = XRGeneralSettings.Instance.Manager.activeLoader;

        if (xRInputSubsystems.Count > 0)
        {
            xrsubsystem_1 = true;
            if (xRInputSubsystems[0].running)
            {
                xrsubsystem_2 = true;
            }
            else { xrsubsystem_2 = false; }
        }
        else { xrsubsystem_1 = false; }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Keypad 1");
            UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.SetDirty();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
            Debug.Log("Keypad 2");
        {
            UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.automaticLoading = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Keypad 3");

            UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.automaticRunning = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Debug.Log("Keypad 5");

            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Debug.Log("Keypad 6");
            XRGeneralSettings.Instance.Manager.activeLoader.Stop();
            XRGeneralSettings.Instance.Manager.activeLoader.Initialize();
            XRGeneralSettings.Instance.Manager.activeLoader.Start();
        }
        List<InputDevice> deviceList = new List<InputDevice>();
        InputDevices.GetDevices(deviceList);

        if(deviceList.Count > 0) { devicesList = true; Debug.Log(deviceList[0].name); } else { devicesList = false; }

        if(Input.GetKeyDown(KeyCode.R)) { UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); }
    }
}
