using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;


public class PatientMenu : MonoBehaviour
{
    private static PatientMenu instance;
    public TMP_Dropdown patientsDrop;
    public TMP_InputField patientNameInput;
    public TMP_InputField patientSurname1Input;
    public TMP_InputField patientSurname2Input;
    public TMP_InputField patientPathologyInput;
    public TMP_InputField patientIdInput;
    public Toggle leftHandAffected;
    public Toggle missLimb_toggle;
    public GameObject messageAddedPatient;

    public GameObject messageSelectedPatient;
    public TextMeshProUGUI sessionText;

    public Button startButton;
    public Button deleteButton;
    public GameObject addPatientPanel;
    public GameObject deletePatientPanel;
    public GameObject messageDeletePatient;

    public GameObject startGamePanel;
    public GameObject exitGamePanel;

    public bool protocolMode = false;

    public static PatientMenu Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SaveInfoPatients_launch.Instance.ResetPatient(); //Reseteo el paciente que está seleccionado y el index de paciente

        EditDropdown();


        protocolMode = SavingData_launch.protocolMode;

        if (protocolMode == false) //Si es modo protocolo solo me interesa cargar el paciente. Sin textos ni imágenes
        {
            LoadAppImage();
        }
        else
        {
            GameObject.Find("APP seleccionada").transform.parent.gameObject.SetActive(false);
        }
    }

    public void LoadAppImage()
    {
        string nameApp = "";
        switch (SavingData_launch.appSelected)
        {
            case 1: nameApp = "Gestures"; break;
            case 2: nameApp = "MT"; break;
            case 3: nameApp = "BBT"; break;
            case 4: nameApp = "Clothespin"; break;
            case 5: nameApp = "Fruits"; break;
            case 6: nameApp = "Supermarket"; break;
                //TAG: Modificar para añadir juego al Launcher
        }
        Debug.Log(SavingData_launch.appSelected + "  " + nameApp);

        GameObject.Find("APP seleccionada").GetComponent<Image>().sprite = Resources.Load<Sprite>(nameApp);
    }


    // Update is called once per frame
    void Update()
    {
        if (addPatientPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (EventSystem.current.currentSelectedGameObject == patientIdInput.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Añadir"));
                }

                if (EventSystem.current.currentSelectedGameObject == patientPathologyInput.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(patientIdInput.gameObject);
                }

                if (EventSystem.current.currentSelectedGameObject == patientSurname2Input.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(patientPathologyInput.gameObject);
                }

                if (EventSystem.current.currentSelectedGameObject == patientSurname1Input.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(patientSurname2Input.gameObject);
                }

                if (EventSystem.current.currentSelectedGameObject == patientNameInput.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(patientSurname1Input.gameObject);
                }
            }
        }
    }

    public void ChangeDropValue() //función a la que llamo cuando cambio el drop, así evito meterlo en el update
    {
        if (protocolMode == false)
        {
            //Si es modo protocolo no quiero que parezcan los avisos de últimas sesiones

            if (patientsDrop.value != SaveInfoPatients_launch.Instance.Index + 1)
            {
                deleteButton.interactable = false; //Evito borrar un paciente sin seleccionarlo
                startButton.interactable = false;
                sessionText.text = "Última sesión: ";
            }
            else
            {
                deleteButton.interactable = true;
                startButton.interactable = true;

                switch (SavingData_launch.appSelected)
                {//Actualizo la sesión
                    case 1:
                        sessionText.text = "Última sesión Gestures: " + SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Gestures;
                        break;

                    case 2:
                        sessionText.text = "Última sesión MT: " + SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_MT;
                        break;

                    case 3:
                        sessionText.text = "Última sesión BBT: " + SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_BBT;
                        break;

                    case 4:
                        sessionText.text = "Última sesión Clothespin: " + SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Clothespin;
                        break;
                    case 5:
                        sessionText.text = "Última sesión Fruits: " + SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Fruits;
                        break;
                        ///
                        ///TO DO: Meter registro de sesiones del Supermarket
                        ///

                        //TAG: Modificar para añadir juego al Launcher
                }
            }
        }
    }

    public void EditDropdown()
    {
        List<string> dropList = new List<string>();

        dropList.Add("Pacientes..."); //El primer elemento de la lista es el mensaje de ayuda
        //AHORA ES CON EL ID
        /*for (int i = 0; i < patientsNames.PatientsNamesList.Count; i++)
        {
            dropList.Add(patientsNames.PatientsNamesList[i]);
        }*/

        for (int i = 0; i < SaveInfoPatients_launch.Instance.PatientsNamesId.Patients.Count; i++)
        {
            dropList.Add(SaveInfoPatients_launch.Instance.PatientsNamesId.Patients[i].ID1.ToString());
        }

        patientsDrop.ClearOptions();
        patientsDrop.AddOptions(dropList);
    }

    public void OpenAddPatientPanel()
    {
        if (!addPatientPanel.activeSelf)
        {
            patientNameInput.text = "";
            patientSurname1Input.text = "";
            patientSurname2Input.text = "";
            patientPathologyInput.text = "";
            patientIdInput.text = "";
            missLimb_toggle.isOn = false;
        }

        addPatientPanel.SetActive(!addPatientPanel.activeSelf);
    }

    public void AddPatient()
    {
        if (patientNameInput.text != "" && patientSurname1Input.text != "" && patientSurname2Input.text != "" && patientPathologyInput.text != "" && patientIdInput.text != "")
        {
            string fullName = patientNameInput.text + " " + patientSurname1Input.text + " " + patientSurname2Input.text;
            string auxFullname = "";
            bool existsName = false;
            bool existsId = false;
            int i = 0;

            while (existsName == false && existsId == false && i < SaveInfoPatients_launch.Instance.PatientsNamesId.Patients.Count)
            {
                auxFullname = SaveInfoPatients_launch.Instance.PatientsNamesId.Patients[i].Name + " " + SaveInfoPatients_launch.Instance.PatientsNamesId.Patients[i].Surname1 + " " + SaveInfoPatients_launch.Instance.PatientsNamesId.Patients[i].Surname2;
                if (auxFullname == fullName)
                {
                    existsName = true;
                }

                if (SaveInfoPatients_launch.Instance.PatientsNamesId.Patients[i].ID1 == int.Parse(patientIdInput.text))
                {
                    existsId = true;
                }

                i++;
            }

            if (existsName == false && existsId == false)
            {
                messageAddedPatient.SetActive(true);
                messageAddedPatient.GetComponent<TextMeshProUGUI>().text = "Paciente añadido correctamente.";
                messageAddedPatient.GetComponent<TextMeshProUGUI>().color = new Color(5f / 255f, 166f / 255f, 0f, 255f / 255f);


                Patient newPatient = new Patient(patientNameInput.text, patientSurname1Input.text, patientSurname2Input.text, patientPathologyInput.text, int.Parse(patientIdInput.text), (missLimb_toggle.isOn) ? (((leftHandAffected.isOn) ? "IZQUIERDA" : "DERECHA")) : "-");

                newPatient.SaveCsvPatient(); //Guardo el nuevo paciente, le paso null porque no voy a hacer append de ningún juego

                SaveInfoPatients_launch.Instance.PatientsNamesId.Patients.Add(newPatient);

                //SaveInfoPatients.Instance.PatientsNamesId.PatientsNamesList.Add(fullName);
                //SaveInfoPatients.Instance.PatientsNamesId.PatientsIdList.Add(int.Parse(patientIdInput.text));
                //patientsNames.PatientsNamesList.Sort(SortPatientByName);

                SaveInfoPatients_launch.Instance.PatientsNamesId.Patients.Sort(SortPatientsByID); //AHORA SE ORDENA POR EL ID
                SaveInfoPatients_launch.Instance.PatientsNamesId.SaveCsvPatientsName(); //Guardo la nueva lista completa del nombre de pacientes

                EditDropdown(); //Edito el dropdown de pacientes (son el ID) 

                //Para que si había un paciente seleccionado, como tras el EditDropdown se resetea el drop, vuelva a poner cuál es el paciente
                if (SaveInfoPatients_launch.Instance.Index != -1)
                {
                    patientsDrop.value = SaveInfoPatients_launch.Instance.Index + 1;
                }

                Invoke(nameof(OpenAddPatientPanel), 1.3f);
            }
            else
            {
                if (existsId == true)
                {
                    messageAddedPatient.SetActive(true);
                    messageAddedPatient.GetComponent<TextMeshProUGUI>().text = "Este ID ya está asignado.";
                    messageAddedPatient.GetComponent<TextMeshProUGUI>().color = new Color(202f / 255f, 111f / 255f, 0f, 255f / 255f);
                }
                if (existsName == true)
                {
                    messageAddedPatient.SetActive(true);
                    messageAddedPatient.GetComponent<TextMeshProUGUI>().text = "Este paciente ya existe.";
                    messageAddedPatient.GetComponent<TextMeshProUGUI>().color = new Color(202f / 255f, 111f / 255f, 0f, 255f / 255f);
                }
            }
        }
        else
        {
            messageAddedPatient.SetActive(true);
            messageAddedPatient.GetComponent<TextMeshProUGUI>().text = "Por favor, rellene todos los campos.";
            messageAddedPatient.GetComponent<TextMeshProUGUI>().color = new Color(202f / 255f, 111f / 255f, 0f, 255f / 255f);
        }
    }

    public void SelectPatient()
    {
        if (patientsDrop.value != 0)
        {
            /*string fullname = patientsDrop.captionText.text;
            string[] identity = fullname.Split(' ');
            string name = identity[0];
            string surname1 = identity[1];
            string surname2 = identity[2];*/

            int id = int.Parse(patientsDrop.captionText.text);

            Patient selectedPatient = new Patient(id);

            selectedPatient.LoadCsvPatient(); //Se cargan todos los datos correctamente, pero si hago un debug.log en la función Load, no lo saca

            SaveInfoPatients_launch.Instance.SelectedPatient = selectedPatient;
            SaveInfoPatients_launch.Instance.Index = patientsDrop.value - 1; //El -1 es por la cabecera de Pacientes...
            startButton.interactable = true;
            deleteButton.interactable = true;

            //  SaveInfoPatients_launch.Instance.SelectedPatient.SetSavingDataValues(); //Con esto guardo los valores cargados en el fichero SavingData

            if (protocolMode == false) //Si es modo protocolo no me interesa que aparezcan estos textos de aviso
            {
                sessionText.gameObject.SetActive(true);

                switch (SavingData_launch.appSelected)
                {//Actualizo la sesión
                    case 1:
                        sessionText.text = "Última sesión Gestures:  " + selectedPatient.LastSession_Gestures;
                        break;

                    case 2:
                        sessionText.text = "Última sesión MT:  " + selectedPatient.LastSession_MT;
                        break;

                    case 3:
                        sessionText.text = "Última sesión BBT:  " + selectedPatient.LastSession_BBT;
                        break;

                    case 4:
                        sessionText.text = "Última sesión Clothespin:  " + selectedPatient.LastSession_Clothespin;
                        break;
                    case 5:
                        sessionText.text = "Última sesión Fruits:  " + selectedPatient.LastSession_Fruits;
                        break;
                        ///
                        ///TO DO: Meter registro de sesiones del Supermarket
                        ///

                        //TAG: Modificar para añadir juego al Launcher
                }
            }

            messageSelectedPatient.SetActive(true);
            messageSelectedPatient.GetComponent<TextMeshProUGUI>().text = "Paciente seleccionado correctamente.";
            messageSelectedPatient.GetComponent<TextMeshProUGUI>().color = new Color(5f / 255f, 166f / 255f, 0f, 255f / 255f);
        }
        else
        {
            messageSelectedPatient.SetActive(true);
            messageSelectedPatient.GetComponent<TextMeshProUGUI>().text = "Por favor, seleccione un paciente.";
            messageSelectedPatient.GetComponent<TextMeshProUGUI>().color = new Color(202f / 255f, 111f / 255f, 0f, 255f / 255f);
        }
    }

    public void OpenDeletePatientPanel()
    {
        if (!deletePatientPanel.activeSelf)
        {
            messageDeletePatient.GetComponent<TextMeshProUGUI>().text = "¿Desea eliminar al paciente \n\n" + patientsDrop.captionText.text + " ?";
        }
        deletePatientPanel.SetActive(!deletePatientPanel.activeSelf);
    }

    public void DeleteSelectedPatient()
    {
        SaveInfoPatients_launch.Instance.PatientsNamesId.Patients.RemoveAt(SaveInfoPatients_launch.Instance.Index);
        SaveInfoPatients_launch.Instance.PatientsNamesId.SaveCsvPatientsName(); //Actualizo la lista de nombres
        EditDropdown();
        patientsDrop.value = 0;

        SaveInfoPatients_launch.Instance.ResetPatient();
        deleteButton.interactable = false;
        startButton.interactable = false;

        sessionText.text = "Última sesión: ";
        sessionText.gameObject.SetActive(false);

        messageSelectedPatient.SetActive(true);
        messageSelectedPatient.GetComponent<TextMeshProUGUI>().text = "Paciente eliminado correctamente.";
        messageSelectedPatient.GetComponent<TextMeshProUGUI>().color = new Color(5f / 255f, 166f / 255f, 0f, 255f / 255f);

        OpenDeletePatientPanel();
    }


    public void OpenStartGamePanel()
    {
        if (!startGamePanel.activeSelf)
        {
            if (protocolMode == false) //Si es modo protocolo, cambio el mensaje del panel de empezar
            {
                startGamePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Si comienza se inciará una nueva sesión con el paciente:\n" +
            patientsDrop.captionText.text + "\n\n¿Desea continuar?";
            }
            else
            {
                startGamePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Si continúa se pasará a seleccionar un <b><u>protocolo </b></u> para el paciente:\n" +
            patientsDrop.captionText.text + "\n\n¿Desea continuar?";
            }
        }
        else { }

        startGamePanel.SetActive(!startGamePanel.activeSelf);

        //Para seleccionar directamente el botón de SÍ
        if (startGamePanel.activeSelf) { startGamePanel.transform.Find("Sí").GetComponent<Button>().Select(); }

    }
    public void StartGame()
    {
        StartCoroutine(CoroutineForStartGame());
    }

    IEnumerator CoroutineForStartGame()
    {
        yield return new WaitForSeconds(0.1f);

        //Guardo en el text el paciente que selecciono antes de lanzar la APP
        File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/PatientSelected.txt",
            SaveInfoPatients_launch.Instance.SelectedPatient.ID1.ToString() + System.Environment.NewLine + SavingData_launch.appSelected + 
            System.Environment.NewLine + "0" + System.Environment.NewLine + SavingData_launch.protocolMode);

        if (protocolMode == false) //Si es modo protocolo, no quiero actualizar sesiones, ni actualizar csv, ni abrir aplicaciones todavía
        {
            switch (SavingData_launch.appSelected)
            {//Actualizo la sesión
                case 1:
                    //Seteo el tipo de Tracking que quiero
                    //     TestProcess.Instance.SetLeapControl();

                    SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Gestures++;
                    break;

                case 2:
                    SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_MT++;
                    break;

                case 3:
                    SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_BBT++;
                    break;

                case 4:
                    SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Clothespin++;
                    break;
                case 5:
                    SaveInfoPatients_launch.Instance.SelectedPatient.LastSession_Fruits++;
                    break;
                    ///
                    ///TO DO: Meter registro de sesiones del Supermarket
                    ///

                    //TAG: Modificar para añadir juego al Launcher
            }

            #region Aparecer el panel de Conectar Leap (desuso)
            /*  startGamePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Desconecte y vuelva conectar el Leap, por favor";
              startGamePanel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 58;
              startGamePanel.GetComponentInChildren<TextMeshProUGUI>().transform.position += new Vector3(0, -90f, 0);
              startGamePanel.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 220f / 255f, 1, 1);
              startGamePanel.transform.Find("Sí").gameObject.SetActive(false);
              startGamePanel.transform.Find("No").gameObject.SetActive(false);

              yield return new WaitForSeconds(3f);*/
            #endregion

            SaveInfoPatients_launch.Instance.SelectedPatient.SaveCsvPatient(); //Para actualizar la session

            //Pongo a True el state de que vengo del Launcher
            ReadSaveLauncherState.instance.SaveState(true);

            //Lanzco la APP
            Application.OpenURL(SavingData_launch.pathSelected);

            Invoke(nameof(ExitGame), 1f);
        }
        else
        { //Si es modo protocolo abro la escena
            SceneManager.LoadScene("Protocolo");
        }

        /*TCPTestClient.instance.StartConnection();

        yield return new WaitUntil(() => TCPTestClient.instance.connected); //Cliente activo y conectado

        TCPTestClient.instance.SendMessageToApps(SavingData_launch.firstDate,SavingData_launch.lastDate);

        yield return new WaitUntil(() => TCPTestClient.instance.confirmationRecived);*/

        //Cierro el Launcher con retardo para que se cierre una vez abierta la APP


    }

    public void OpenExitPanel()
    {
        exitGamePanel.SetActive(!exitGamePanel.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    /*private static int SortPatientByName(string p1, string p2)
    {
        string p1Name = p1.Split(' ')[0];
        string p1Surname1 = p1.Split(' ')[1];
        string p1Surname2 = p1.Split(' ')[2];

        string p2Name = p2.Split(' ')[0];
        string p2Surname1 = p2.Split(' ')[1];
        string p2Surname2 = p2.Split(' ')[2];

        int pos = p1Name.CompareTo(p2Name); //pos == -1, el primero va antes    pos == 0, la posición es la misma    pos == 1, el primero va después

        if (pos == 0)
        {
            pos = p1Surname1.CompareTo(p2Surname1);

            if (pos == 0)
            {
                pos = p1Surname2.CompareTo(p2Surname2);
            }
        }

        return pos;
    }

    private static int SortPatientByID(string id1, string id2)
    {
        int pos = id1.CompareTo(id2); //pos == -1, el primero va antes    pos == 0, la posición es la misma    pos == 1, el primero va después
        
        return pos;
    }*/

    private static int SortPatientsByID(Patient p1, Patient p2)
    {
        int pos = p1.ID1.CompareTo(p2.ID1);

        return pos;
    }
}