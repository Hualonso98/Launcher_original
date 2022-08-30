using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
public class SetProtocol : MonoBehaviour
{
    [Header("Panel seleccionar protocolo")]
    [Space]
    public TMP_Dropdown protocols_drop;
    public Button loadProtocol_button;
    public GameObject panel_select_protocol;
    public GameObject panel_add_protocol;
    public GameObject view_protocol;
    public GameObject table_view_protocol;
    public GameObject deleteProtocol;
    public GameObject deletePanel;
    public GameObject warningPanel;
    [Header("Panel crear protocolo")]
    [Space]
    public Button addExercise;
    public Button addStep;
    public TMP_InputField numReps;

    public TMP_Dropdown exerciseDrop;
    public TextMeshProUGUI listExer;
    public TextMeshProUGUI listReps;
    public TMP_InputField protocol_name;

    public GameObject add_protocol_button;

    List<string> exercises = new List<string>();
    List<string> reps = new List<string>();
    List<string> games = new List<string>();
    List<Dictionary<string, bool>> exerciseValues = new List<Dictionary<string, bool>>();


    Protocols protocols = new Protocols();
    Protocol patientProtocol = new Protocol();

    int patientId = -1;

    bool startConfigProtocols = false;

    List<string> exercises_names = new List<string>();

    private void Awake()
    {
        LoadProtocols();
        EditDropList();

        try { patientId = SaveInfoPatients_launch.Instance.SelectedPatient.ID1; } catch { patientId = 0; }


    }

    // Start is called before the first frame update
    void Start()
    {
        VerifyProtocolAlreadyExists();

        StartCoroutine(CorrutinaStart());
    }

    IEnumerator CorrutinaStart()
    {
        yield return new WaitUntil(() => startConfigProtocols);

        exercises_names = new List<string> {"Pinza indice", "Pinza medio", "Pinza anular" , "Pinza menique", "Cierre puno","Apertura dedos", "Flexion muneca",
            "Extension muneca", "Desv. radial", "Desv. Cubital", "Pronacion", "Supinacion", "Alcance"};

        List<string> list = new List<string>(exercises_names);
        list.Insert(0, "Selecciona...");
        exerciseDrop.options.Clear();
        foreach (string option in list)
        {
            exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
        }

        Dictionary<string, bool> dictionary_gest_gest = new Dictionary<string, bool>();
        dictionary_gest_gest.Add("Pinza indice", true);
        dictionary_gest_gest.Add("Pinza medio", true);
        dictionary_gest_gest.Add("Pinza anular", true);
        dictionary_gest_gest.Add("Pinza menique", true);
        dictionary_gest_gest.Add("Cierre puno", true);
        dictionary_gest_gest.Add("Apertura dedos", true);
        dictionary_gest_gest.Add("Flexion muneca", true);
        dictionary_gest_gest.Add("Extension muneca", true);
        dictionary_gest_gest.Add("Desv. Radial", true);
        dictionary_gest_gest.Add("Desv. Cubital", true);
        dictionary_gest_gest.Add("Pronacion", false);
        dictionary_gest_gest.Add("Supinacion", false);
        dictionary_gest_gest.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_ark = new Dictionary<string, bool>();
        dictionary_gest_ark.Add("Pinza indice", true);
        dictionary_gest_ark.Add("Pinza medio", true);
        dictionary_gest_ark.Add("Pinza anular", true);
        dictionary_gest_ark.Add("Pinza menique", true);
        dictionary_gest_ark.Add("Cierre puno", true);
        dictionary_gest_ark.Add("Apertura dedos", true);
        dictionary_gest_ark.Add("Flexion muneca", true);
        dictionary_gest_ark.Add("Extension muneca", true);
        dictionary_gest_ark.Add("Desv. Radial", true);
        dictionary_gest_ark.Add("Desv. Cubital", true);
        dictionary_gest_ark.Add("Pronacion", true);
        dictionary_gest_ark.Add("Supinacion", true);
        dictionary_gest_ark.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_spa = new Dictionary<string, bool>();
        dictionary_gest_spa.Add("Pinza indice", true);
        dictionary_gest_spa.Add("Pinza medio", true);
        dictionary_gest_spa.Add("Pinza anular", true);
        dictionary_gest_spa.Add("Pinza menique", true);
        dictionary_gest_spa.Add("Cierre puno", true);
        dictionary_gest_spa.Add("Apertura dedos", true);
        dictionary_gest_spa.Add("Flexion muneca", true);
        dictionary_gest_spa.Add("Extension muneca", true);
        dictionary_gest_spa.Add("Desv. Radial", true);
        dictionary_gest_spa.Add("Desv. Cubital", true);
        dictionary_gest_spa.Add("Pronacion", true);
        dictionary_gest_spa.Add("Supinacion", true);
        dictionary_gest_spa.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_cook = new Dictionary<string, bool>();
        dictionary_gest_cook.Add("Pinza indice", true);
        dictionary_gest_cook.Add("Pinza medio", true);
        dictionary_gest_cook.Add("Pinza anular", true);
        dictionary_gest_cook.Add("Pinza menique", true);
        dictionary_gest_cook.Add("Cierre puno", true);
        dictionary_gest_cook.Add("Apertura dedos", true);
        dictionary_gest_cook.Add("Flexion muneca", true);
        dictionary_gest_cook.Add("Extension muneca", true);
        dictionary_gest_cook.Add("Desv. Radial", true);
        dictionary_gest_cook.Add("Desv. Cubital", true);
        dictionary_gest_cook.Add("Pronacion", false);
        dictionary_gest_cook.Add("Supinacion", false);
        dictionary_gest_cook.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_tres = new Dictionary<string, bool>();
        dictionary_gest_tres.Add("Pinza indice", true);
        dictionary_gest_tres.Add("Pinza medio", true);
        dictionary_gest_tres.Add("Pinza anular", true);
        dictionary_gest_tres.Add("Pinza menique", true);
        dictionary_gest_tres.Add("Cierre puno", true);
        dictionary_gest_tres.Add("Apertura dedos", true);
        dictionary_gest_tres.Add("Flexion muneca", true);
        dictionary_gest_tres.Add("Extension muneca", true);
        dictionary_gest_tres.Add("Desv. Radial", true);
        dictionary_gest_tres.Add("Desv. Cubital", true);
        dictionary_gest_tres.Add("Pronacion", false);
        dictionary_gest_tres.Add("Supinacion", false);
        dictionary_gest_tres.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_flota = new Dictionary<string, bool>();
        dictionary_gest_flota.Add("Pinza indice", true);
        dictionary_gest_flota.Add("Pinza medio", true);
        dictionary_gest_flota.Add("Pinza anular", true);
        dictionary_gest_flota.Add("Pinza menique", true);
        dictionary_gest_flota.Add("Cierre puno", true);
        dictionary_gest_flota.Add("Apertura dedos", true);
        dictionary_gest_flota.Add("Flexion muneca", true);
        dictionary_gest_flota.Add("Extension muneca", true);
        dictionary_gest_flota.Add("Desv. Radial", true);
        dictionary_gest_flota.Add("Desv. Cubital", true);
        dictionary_gest_flota.Add("Pronacion", false);
        dictionary_gest_flota.Add("Supinacion", false);
        dictionary_gest_flota.Add("Alcance", false);

        Dictionary<string, bool> dictionary_bbt = new Dictionary<string, bool>();
        dictionary_bbt.Add("Pinza indice", true);
        dictionary_bbt.Add("Pinza medio", true);
        dictionary_bbt.Add("Pinza anular", false);
        dictionary_bbt.Add("Pinza menique", false);
        dictionary_bbt.Add("Cierre puno", false);
        dictionary_bbt.Add("Apertura dedos", false);
        dictionary_bbt.Add("Flexion muneca", false);
        dictionary_bbt.Add("Extension muneca", false);
        dictionary_bbt.Add("Desv. Radial", false);
        dictionary_bbt.Add("Desv. Cubital", false);
        dictionary_bbt.Add("Pronacion", false);
        dictionary_bbt.Add("Supinacion", false);
        dictionary_bbt.Add("Alcance", true);

        Dictionary<string, bool> dictionary_cloth = new Dictionary<string, bool>();
        dictionary_cloth.Add("Pinza indice", true);
        dictionary_cloth.Add("Pinza medio", true);
        dictionary_cloth.Add("Pinza anular", false);
        dictionary_cloth.Add("Pinza menique", false);
        dictionary_cloth.Add("Cierre puno", false);
        dictionary_cloth.Add("Apertura dedos", false);
        dictionary_cloth.Add("Flexion muneca", false);
        dictionary_cloth.Add("Extension muneca", false);
        dictionary_cloth.Add("Desv. Radial", false);
        dictionary_cloth.Add("Desv. Cubital", false);
        dictionary_cloth.Add("Pronacion", false);
        dictionary_cloth.Add("Supinacion", false);
        dictionary_cloth.Add("Alcance", true);

        Dictionary<string, bool> dictionary_fruits_alcance = new Dictionary<string, bool>();
        dictionary_fruits_alcance.Add("Pinza indice", false);
        dictionary_fruits_alcance.Add("Pinza medio", false);
        dictionary_fruits_alcance.Add("Pinza anular", false);
        dictionary_fruits_alcance.Add("Pinza menique", false);
        dictionary_fruits_alcance.Add("Cierre puno", false);
        dictionary_fruits_alcance.Add("Apertura dedos", false);
        dictionary_fruits_alcance.Add("Flexion muneca", false);
        dictionary_fruits_alcance.Add("Extension muneca", false);
        dictionary_fruits_alcance.Add("Desv. Radial", false);
        dictionary_fruits_alcance.Add("Desv. Cubital", false);
        dictionary_fruits_alcance.Add("Pronacion", false);
        dictionary_fruits_alcance.Add("Supinacion", false);
        dictionary_fruits_alcance.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_secuencia = new Dictionary<string, bool>();
        dictionary_fruits_secuencia.Add("Pinza indice", false);
        dictionary_fruits_secuencia.Add("Pinza medio", false);
        dictionary_fruits_secuencia.Add("Pinza anular", false);
        dictionary_fruits_secuencia.Add("Pinza menique", false);
        dictionary_fruits_secuencia.Add("Cierre puno", false);
        dictionary_fruits_secuencia.Add("Apertura dedos", false);
        dictionary_fruits_secuencia.Add("Flexion muneca", false);
        dictionary_fruits_secuencia.Add("Extension muneca", false);
        dictionary_fruits_secuencia.Add("Desv. Radial", false);
        dictionary_fruits_secuencia.Add("Desv. Cubital", false);
        dictionary_fruits_secuencia.Add("Pronacion", false);
        dictionary_fruits_secuencia.Add("Supinacion", false);
        dictionary_fruits_secuencia.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_agarre = new Dictionary<string, bool>();
        dictionary_fruits_agarre.Add("Pinza indice", false);
        dictionary_fruits_agarre.Add("Pinza medio", false);
        dictionary_fruits_agarre.Add("Pinza anular", false);
        dictionary_fruits_agarre.Add("Pinza menique", false);
        dictionary_fruits_agarre.Add("Cierre puno", true);
        dictionary_fruits_agarre.Add("Apertura dedos", true);
        dictionary_fruits_agarre.Add("Flexion muneca", false);
        dictionary_fruits_agarre.Add("Extension muneca", false);
        dictionary_fruits_agarre.Add("Desv. Radial", false);
        dictionary_fruits_agarre.Add("Desv. Cubital", false);
        dictionary_fruits_agarre.Add("Pronacion", false);
        dictionary_fruits_agarre.Add("Supinacion", false);
        dictionary_fruits_agarre.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_volteo = new Dictionary<string, bool>();
        dictionary_fruits_volteo.Add("Pinza indice", false);
        dictionary_fruits_volteo.Add("Pinza medio", false);
        dictionary_fruits_volteo.Add("Pinza anular", false);
        dictionary_fruits_volteo.Add("Pinza menique", false);
        dictionary_fruits_volteo.Add("Cierre puno", false);
        dictionary_fruits_volteo.Add("Apertura dedos", false);
        dictionary_fruits_volteo.Add("Flexion muneca", false);
        dictionary_fruits_volteo.Add("Extension muneca", false);
        dictionary_fruits_volteo.Add("Desv. Radial", false);
        dictionary_fruits_volteo.Add("Desv. Cubital", false);
        dictionary_fruits_volteo.Add("Pronacion", true);
        dictionary_fruits_volteo.Add("Supinacion", true);
        dictionary_fruits_volteo.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_prension = new Dictionary<string, bool>();
        dictionary_fruits_prension.Add("Pinza indice", false);
        dictionary_fruits_prension.Add("Pinza medio", false);
        dictionary_fruits_prension.Add("Pinza anular", false);
        dictionary_fruits_prension.Add("Pinza menique", false);
        dictionary_fruits_prension.Add("Cierre puno", true);
        dictionary_fruits_prension.Add("Apertura dedos", true);
        dictionary_fruits_prension.Add("Flexion muneca", false);
        dictionary_fruits_prension.Add("Extension muneca", false);
        dictionary_fruits_prension.Add("Desv. Radial", false);
        dictionary_fruits_prension.Add("Desv. Cubital", false);
        dictionary_fruits_prension.Add("Pronacion", false);
        dictionary_fruits_prension.Add("Supinacion", false);
        dictionary_fruits_prension.Add("Alcance", true);

        exerciseValues.Add(dictionary_gest_gest);
        exerciseValues.Add(dictionary_gest_ark);
        exerciseValues.Add(dictionary_gest_spa);
        exerciseValues.Add(dictionary_gest_cook);
        exerciseValues.Add(dictionary_gest_tres);
        exerciseValues.Add(dictionary_gest_flota);
        exerciseValues.Add(dictionary_bbt);
        exerciseValues.Add(dictionary_cloth);
        exerciseValues.Add(dictionary_fruits_alcance);
        exerciseValues.Add(dictionary_fruits_secuencia);
        exerciseValues.Add(dictionary_fruits_agarre);
        exerciseValues.Add(dictionary_fruits_volteo);
        exerciseValues.Add(dictionary_fruits_prension);

        if (exerciseDrop.value == 0)
        {
            addExercise.interactable = false;
            addExercise.GetComponentInChildren<Image>().color = Color.grey;
        }

        if (numReps.text == "")
        {
            addStep.interactable = false;
        }
    }

    #region Comprobar protocolo empezado

    /////////////////////////////  Comprobar si hay protocolo guardado  /////////////////////////////
    public void VerifyProtocolAlreadyExists()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";
        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientId + "/LastProtocol.dat";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Protocol aux = null;
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                try
                {
                    aux = (Protocol)bf.Deserialize(file);
                }
                catch (SerializationException)
                {
                    Debug.Log("Error de serialización en lectura de fichero persistente.");
                    return;
                }
            }

            if (aux.Name != "")
            {
                //Warning de que ya hay un protocol empezado
                OpenWarningPanel();
            }
            else
            {
                Debug.Log("No existe protocolo para el paciente");
                startConfigProtocols = true; //Si no hay protocolo empezado, activo el booleano para que empiece la config inicial (cargar protocolos, diccionarios etc)
            }
        }
        else
        {

            Debug.Log("No existe el archivo de protocolo para el paciente");
            startConfigProtocols = true; //No hay archivo, así que lo doy por bueno
        }
    }

    public void OpenWarningPanel()
    {
        warningPanel.SetActive(!warningPanel.activeSelf);

        if (warningPanel.activeSelf)
        {
            warningPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Hay un protocolo empezado con este paciente. ¿Desea retomarlo?";
        }
        else
        {
            startConfigProtocols = true; //Si hay protocolo empezado, pero decido no retomarlo, activo el booleano para que empiece la config inicial (cargar protocolos, diccionarios etc)
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Paneles, drops etc. (UI)

    ////////////////////////////////////  Controlar UI inicial  ////////////////////////////////////
    public void EditDropList()
    {
        List<string> dropList = new List<string>();

        dropList.Add("Protocolos..."); //El primer elemento de la lista es el mensaje de ayuda

        for (int i = 0; i < protocols.Protocols_list.Count; i++)
        {
            dropList.Add(protocols.Protocols_list[i].Name);
        }


        protocols_drop.ClearOptions();
        protocols_drop.AddOptions(dropList);
    }
    public void ChangeProtocolDrop(int value)
    {
        if (value != 0)
        {
            loadProtocol_button.interactable = true;
            view_protocol.SetActive(true);
            deleteProtocol.SetActive(true);
            loadProtocol_button.interactable = true;


            table_view_protocol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            table_view_protocol.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";

            for (int i = 0; i < protocols.Protocols_list[value - 1].Exercises.Count; i++)
            {
                table_view_protocol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text += protocols.Protocols_list[value - 1].Exercises[i] + System.Environment.NewLine;
                table_view_protocol.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text += protocols.Protocols_list[value - 1].Reps[i] + System.Environment.NewLine;
            }
        }
        else
        {
            loadProtocol_button.interactable = false;
            view_protocol.SetActive(false);
            deleteProtocol.SetActive(false);
            loadProtocol_button.interactable = false;
        }
    }

    public void OpenAddProtocolPanel(bool value)
    {
        panel_add_protocol.SetActive(value);
        panel_select_protocol.SetActive(!value);

        if (value)
        {
            exercises.Clear();
            reps.Clear();
            games.Clear();
            listExer.text = "";
            listReps.text = "";
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Gestion de protocolos

    ///////////////////////////  Seleccionar, eliminar y ver protocolos  ///////////////////////////
    public void SelectProtocol()
    {
        // protocols.Index_protocol_selected = protocols_drop.value - 1;
        // SaveProtocols();

        //Ya no tengo que guardar los protocolos para guardar el índice, porque ahora se guarda en el protocol del paciente

        if (protocols_drop.value > 0)
        {
            patientProtocol = protocols.Protocols_list[protocols_drop.value - 1];
            patientProtocol.Index_protocol_selected_in_protocols = protocols_drop.value - 1;

            SavePatientProtocol();

            CreateProtocolDataCsv();

            CallIntermediatePhase();
        }
    }

    public void OpenDeletePanel()
    {
        deletePanel.SetActive(!deletePanel.activeSelf);

        if (deletePanel.activeSelf)
        {
            deletePanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Confirme que desea eliminar el protocolo: \n\n" +
               "<b>" + protocols_drop.captionText.text + "</b>";

            for (int i = 0; i < FindObjectsOfType<ShowWhenButtonHighlighted>().Length; i++)
            {
                FindObjectsOfType<ShowWhenButtonHighlighted>()[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < FindObjectsOfType<ShowWhenButtonHighlighted>().Length; i++)
            {
                FindObjectsOfType<ShowWhenButtonHighlighted>()[i].enabled = true;
            }
        }
    }

    public void DeleteProtocol()
    {
        int index = protocols_drop.value - 1; //El -1 es porque el primer valor no corresponde a un protocolo

        protocols.Protocols_list.RemoveAt(index);

        SaveProtocols();

        EditDropList();

        protocols_drop.value = 0;
        loadProtocol_button.interactable = false;

        ChangeProtocolDrop(0);

        OpenDeletePanel();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Crear protocolos

    //////////////////////////////////  Proceso crear protocolos  //////////////////////////////////

    public void AddName(string name)
    {
        if (string.IsNullOrEmpty(name) == false && string.IsNullOrWhiteSpace(name) == false)
        {
            add_protocol_button.GetComponent<Button>().interactable = true;
        }
        else
        {
            add_protocol_button.GetComponent<Button>().interactable = false;
        }
    }

    public void AddExercise()
    {
        if (!numReps.IsInteractable())
        {
            exerciseDrop.interactable = false;
            numReps.interactable = true;
            if (numReps.text != "" && int.Parse(numReps.text) != 0) { addStep.interactable = true; }
            addExercise.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            exerciseDrop.interactable = true;
            numReps.interactable = false;
            addStep.interactable = false;
            addExercise.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ChangeAppDrop(int value)
    {
        if (value != 0)
        {
            addExercise.interactable = true;
            addExercise.GetComponentInChildren<Image>().color = Color.white;
        }
        else
        {
            addExercise.interactable = false;
            addExercise.GetComponentInChildren<Image>().color = Color.grey;
        }
    }

    public void ChangeRepsInput(string value)
    {
        if (value != "" && int.Parse(value) != 0)
        {
            addStep.interactable = true;
        }
        else
        {
            addStep.interactable = false;
        }
    }

    public void AddStepFunc()
    {
        exercises.Add(exerciseDrop.captionText.text);
        reps.Add(numReps.text);

        exerciseDrop.interactable = true;
        exerciseDrop.value = 0;
        addExercise.interactable = false;
        addExercise.transform.localScale = new Vector3(1, 1, 1);
        numReps.interactable = false;
        numReps.text = "";
        addStep.interactable = false;

        AddTexts(exercises[exercises.Count - 1], reps[reps.Count - 1]);

        bool value = false;
        exerciseValues[0].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Gesture SÍ"); games.Add("Gesture"); } else { Debug.Log("Gesture NO"); }
        exerciseValues[1].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Arkanoid SÍ"); games.Add("Arkanoid"); } else { Debug.Log("Arkanoid NO"); }
        exerciseValues[2].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Space SÍ"); games.Add("Space"); } else { Debug.Log("Space NO"); }
        exerciseValues[3].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Cooking SÍ"); games.Add("Cooking"); } else { Debug.Log("Cooking NO"); }
        exerciseValues[4].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Tres SÍ"); games.Add("Tres"); } else { Debug.Log("Tres NO"); }
        exerciseValues[5].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Flota SÍ"); games.Add("Flota"); } else { Debug.Log("Flota NO"); }
        exerciseValues[6].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("BBT SÍ"); games.Add("BBT"); } else { Debug.Log("BBT NO"); }
        exerciseValues[7].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("CLOTHESPIN SÍ"); games.Add("Clothespin"); } else { Debug.Log("CLOTHESPIN NO"); }
        exerciseValues[8].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Alcance SÍ"); games.Add("Alcance"); } else { Debug.Log("Alcance NO"); }
        exerciseValues[9].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Secuencia SÍ"); games.Add("Secuencia"); } else { Debug.Log("Secuencia NO"); }
        exerciseValues[10].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Agarre SÍ"); games.Add("Agarre"); } else { Debug.Log("Agarre NO"); }
        exerciseValues[11].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Volteo SÍ"); games.Add("Volteo"); } else { Debug.Log("Volteo NO"); }
        exerciseValues[12].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("Prension SÍ"); games.Add("Prension"); } else { Debug.Log("Prension NO"); }

    }

    void AddTexts(string ex, string rep)
    {
        listExer.text += ex + System.Environment.NewLine;
        listReps.text += rep + System.Environment.NewLine;
    }

    public void AddProtocol()
    {
        Protocol new_protocol = new Protocol();

        new_protocol.Name = protocol_name.text;
        new_protocol.Exercises = exercises;
        new_protocol.Reps = reps;
        new_protocol.Games = games.Distinct().ToList(); //Con esto quito duplicados

        protocols.Protocols_list.Add(new_protocol);

        SaveProtocols();
        EditDropList();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Cargar y guardar protocolos

    ////////////////////////////////////////////// IO de protocolos generales //////////////////////////////////////////////
    public void SaveProtocols()
    {
        BinaryFormatter bf = new BinaryFormatter();

        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/Protocols.dat";

        FileStream file = File.Create(path);//" / pruebaSettings.dat");

        bf.Serialize(file, protocols);

        file.Close();
    }

    public void LoadProtocols()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/Protocols.dat";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Protocols protocols_loaded = new Protocols();

            using (FileStream file = File.Open(path, FileMode.Open))
            {
                try
                {
                    protocols_loaded = (Protocols)bf.Deserialize(file);
                }
                catch (SerializationException)
                {
                    Debug.Log("Error de serialización en lectura de fichero persistente.");
                    return;
                }
            }

            protocols = protocols_loaded;
        }
        else
        {
            SaveProtocols();
            protocols = new Protocols();
        }
    }

    //  Protocolo vacío del paciente para luego guardar ahí el protocolo cargado, índices etc //
    public void SavePatientProtocol()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";
        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientId + "/LastProtocol.dat";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);//" / pruebaSettings.dat");

        bf.Serialize(file, patientProtocol);

        file.Close();
    }

    public void CreateProtocolDataCsv()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";
        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientId + "/Protocol_Data/";
        string filename = System.DateTime.Now.ToString("dd-MM-yyyy__HH-mm") + "__" + patientProtocol.Name + ".csv";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path += filename;

        /* try
         {*/

        var f1 = File.CreateText(path);

        string data = "Ficha de protocolo" + System.Environment.NewLine;
        data += System.Environment.NewLine;

        data += "Nombre;" + patientProtocol.Name + System.Environment.NewLine;
        data += System.Environment.NewLine;

        data += "Fecha de inicio de protocolo;" + System.DateTime.Now.ToString("g") + System.Environment.NewLine;
        data += "Ultima sesion;" + System.DateTime.Now.ToString("g") + System.Environment.NewLine;

        data += System.Environment.NewLine;

        //  data += "Ejercicios" + System.Environment.NewLine;
        data += "Ejercicios;";
        for (int i = 0; i < patientProtocol.Exercises.Count; i++)
        {
            // data += ";" + patientProtocol.Exercises[i];
            data += patientProtocol.Exercises[i] + ";";
        }
        data += System.Environment.NewLine;
        //  data += "Repeticiones objetivo" + System.Environment.NewLine;
        data += "Repeticiones objetivo;";
        for (int i = 0; i < patientProtocol.Reps.Count; i++)
        {
            // data += ";" + patientProtocol.Reps[i];
            data += patientProtocol.Reps[i] + ";";
        }
        data += System.Environment.NewLine;
        //  data += "Repeticiones realizadas" + System.Environment.NewLine;
        data += "Repeticiones realizadas;";
        for (int i = 0; i < patientProtocol.Exercises.Count; i++)
        {
            // data += ";" + 0;
            data += 0 + ";";
        }

        data += System.Environment.NewLine;
        data += System.Environment.NewLine;

        data += "Historico de repeticiones por sesion" + System.Environment.NewLine;
        data += System.Environment.NewLine;
        data += "Ejercicios;Subjuegos" + System.Environment.NewLine;
        data += System.Environment.NewLine;
        data += System.Environment.NewLine;
        for (int i = 0; i < patientProtocol.Exercises.Count; i++)
        {
            data += patientProtocol.Exercises[i] + ";" + System.Environment.NewLine;
        }

        //VER EXCEL DEL MODELO DE CSV

        f1.Write(data);

        f1.Close();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Llamar a fase intermedia

    public void CallIntermediatePhase()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths";
        string[] data = File.ReadAllLines(path + "/ApplicationsPaths.txt");

        Application.OpenURL(data[1]); //La fase intermedia para cuando modifique el orden de los paths
    }
    #endregion
}

[System.Serializable]
public class Protocol
{
    int index_protocol_selected_in_protocols = -1;

    string name = "";
    List<string> exercises = new List<string>();
    List<string> reps = new List<string>();
    List<string> games = new List<string>();
    int index_subgame_selected = -1;
    int index_game_selected = -1;
    List<string> games_already_Selected = new List<string>();

    public string Name { get => name; set => name = value; }
    public List<string> Exercises { get => exercises; set => exercises = value; }
    public List<string> Reps { get => reps; set => reps = value; }
    public List<string> Games { get => games; set => games = value; }
    public int Index_subgame_selected { get => index_subgame_selected; set => index_subgame_selected = value; }
    public int Index_protocol_selected_in_protocols { get => index_protocol_selected_in_protocols; set => index_protocol_selected_in_protocols = value; }
    public int Index_game_selected { get => index_game_selected; set => index_game_selected = value; }
}

[System.Serializable]
public class Protocols
{
    [SerializeField] List<Protocol> protocols_list = new List<Protocol>();

    public List<Protocol> Protocols_list { get => protocols_list; set => protocols_list = value; }
}