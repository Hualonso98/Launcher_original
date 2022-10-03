using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System;
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

    public Button undoButton;

    public TextMeshProUGUI message;

    //Listas de nombres de ejercicios para el protocolo
    [SerializeField] List<string> exercises = new List<string>();
    [SerializeField] List<string> exercises_display = new List<string>();

    [SerializeField] List<string> reps = new List<string>();
    [SerializeField] List<string> games = new List<string>();
    List<Dictionary<string, bool>> exerciseValues = new List<Dictionary<string, bool>>();


    Protocols protocols = new Protocols();
    Protocol patientProtocol = new Protocol();

    int patientId = -1;

    bool startConfigProtocols = false;

    [SerializeField] List<int> indexDropsSelected = new List<int>();

    //Listas de nombres de ejercicios completas
    [SerializeField] List<string> exercises_names = new List<string>();
    [SerializeField] List<string> exercises_names_for_drop = new List<string>();
    [SerializeField] List<string> exercises_names_to_display = new List<string>();

    List<string> gameNames = new List<string>();

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
            "Extension muneca", "Desv. Radial", "Desv. Cubital", "Pronacion", "Supinacion", "Alcance"};

        exercises_names_to_display = new List<string> {"Pinza índice", "Pinza medio", "Pinza anular" , "Pinza meñique", "Cierre puño","Apertura dedos", "Flexión muñeca",
            "Extensión muñeca", "Desv. Radial", "Desv. Cubital", "Pronación", "Supinación", "Alcance"};

        exercises_names_for_drop = new List<string>(exercises_names_to_display); //Hago una copia no referenciada

        gameNames = new List<string> { "Gesture", "Arkanoid", "Space", "Cooking", "Tres", "Flota", "BBT", "Clothespin", "Alcance", "Secuencia", "Agarre", "Volteo", "Prension" };

        List<string> list = new List<string>(exercises_names_to_display); //Hago la copia de los nombres para displya (con tildes, y 'ñ')
        list.Insert(0, "Selecciona...");
        exerciseDrop.options.Clear();
        foreach (string option in list)
        {
            exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
        }

        Dictionary<string, bool> dictionary_gest_gest = new Dictionary<string, bool>();
        dictionary_gest_gest.Add("Pinza índice", true);
        dictionary_gest_gest.Add("Pinza medio", true);
        dictionary_gest_gest.Add("Pinza anular", true);
        dictionary_gest_gest.Add("Pinza meñique", true);
        dictionary_gest_gest.Add("Cierre puño", true);
        dictionary_gest_gest.Add("Apertura dedos", true);
        dictionary_gest_gest.Add("Flexión muñeca", true);
        dictionary_gest_gest.Add("Extensión muñeca", true);
        dictionary_gest_gest.Add("Desv. Radial", true);
        dictionary_gest_gest.Add("Desv. Cubital", true);
        dictionary_gest_gest.Add("Pronación", false);
        dictionary_gest_gest.Add("Supinación", false);
        dictionary_gest_gest.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_ark = new Dictionary<string, bool>();
        dictionary_gest_ark.Add("Pinza índice", true);
        dictionary_gest_ark.Add("Pinza medio", true);
        dictionary_gest_ark.Add("Pinza anular", true);
        dictionary_gest_ark.Add("Pinza meñique", true);
        dictionary_gest_ark.Add("Cierre puño", true);
        dictionary_gest_ark.Add("Apertura dedos", true);
        dictionary_gest_ark.Add("Flexión muñeca", true);
        dictionary_gest_ark.Add("Extensión muñeca", true);
        dictionary_gest_ark.Add("Desv. Radial", true);
        dictionary_gest_ark.Add("Desv. Cubital", true);
        dictionary_gest_ark.Add("Pronación", true);
        dictionary_gest_ark.Add("Supinación", true);
        dictionary_gest_ark.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_spa = new Dictionary<string, bool>();
        dictionary_gest_spa.Add("Pinza índice", true);
        dictionary_gest_spa.Add("Pinza medio", true);
        dictionary_gest_spa.Add("Pinza anular", true);
        dictionary_gest_spa.Add("Pinza meñique", true);
        dictionary_gest_spa.Add("Cierre puño", true);
        dictionary_gest_spa.Add("Apertura dedos", true);
        dictionary_gest_spa.Add("Flexión muñeca", true);
        dictionary_gest_spa.Add("Extensión muñeca", true);
        dictionary_gest_spa.Add("Desv. Radial", true);
        dictionary_gest_spa.Add("Desv. Cubital", true);
        dictionary_gest_spa.Add("Pronación", true);
        dictionary_gest_spa.Add("Supinación", true);
        dictionary_gest_spa.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_cook = new Dictionary<string, bool>();
        dictionary_gest_cook.Add("Pinza índice", true);
        dictionary_gest_cook.Add("Pinza medio", true);
        dictionary_gest_cook.Add("Pinza anular", true);
        dictionary_gest_cook.Add("Pinza meñique", true);
        dictionary_gest_cook.Add("Cierre puño", true);
        dictionary_gest_cook.Add("Apertura dedos", true);
        dictionary_gest_cook.Add("Flexión muñeca", true);
        dictionary_gest_cook.Add("Extensión muñeca", true);
        dictionary_gest_cook.Add("Desv. Radial", true);
        dictionary_gest_cook.Add("Desv. Cubital", true);
        dictionary_gest_cook.Add("Pronación", false);
        dictionary_gest_cook.Add("Supinación", false);
        dictionary_gest_cook.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_tres = new Dictionary<string, bool>();
        dictionary_gest_tres.Add("Pinza índice", true);
        dictionary_gest_tres.Add("Pinza medio", true);
        dictionary_gest_tres.Add("Pinza anular", true);
        dictionary_gest_tres.Add("Pinza meñique", true);
        dictionary_gest_tres.Add("Cierre puño", true);
        dictionary_gest_tres.Add("Apertura dedos", true);
        dictionary_gest_tres.Add("Flexión muñeca", true);
        dictionary_gest_tres.Add("Extensión muñeca", true);
        dictionary_gest_tres.Add("Desv. Radial", true);
        dictionary_gest_tres.Add("Desv. Cubital", true);
        dictionary_gest_tres.Add("Pronación", false);
        dictionary_gest_tres.Add("Supinación", false);
        dictionary_gest_tres.Add("Alcance", false);
        Dictionary<string, bool> dictionary_gest_flota = new Dictionary<string, bool>();
        dictionary_gest_flota.Add("Pinza índice", true);
        dictionary_gest_flota.Add("Pinza medio", true);
        dictionary_gest_flota.Add("Pinza anular", true);
        dictionary_gest_flota.Add("Pinza meñique", true);
        dictionary_gest_flota.Add("Cierre puño", true);
        dictionary_gest_flota.Add("Apertura dedos", true);
        dictionary_gest_flota.Add("Flexión muñeca", true);
        dictionary_gest_flota.Add("Extensión muñeca", true);
        dictionary_gest_flota.Add("Desv. Radial", true);
        dictionary_gest_flota.Add("Desv. Cubital", true);
        dictionary_gest_flota.Add("Pronación", false);
        dictionary_gest_flota.Add("Supinación", false);
        dictionary_gest_flota.Add("Alcance", false);

        Dictionary<string, bool> dictionary_bbt = new Dictionary<string, bool>();
        dictionary_bbt.Add("Pinza índice", true);
        dictionary_bbt.Add("Pinza medio", true);
        dictionary_bbt.Add("Pinza anular", false);
        dictionary_bbt.Add("Pinza meñique", false);
        dictionary_bbt.Add("Cierre puño", false);
        dictionary_bbt.Add("Apertura dedos", false);
        dictionary_bbt.Add("Flexión muñeca", false);
        dictionary_bbt.Add("Extensión muñeca", false);
        dictionary_bbt.Add("Desv. Radial", false);
        dictionary_bbt.Add("Desv. Cubital", false);
        dictionary_bbt.Add("Pronación", false);
        dictionary_bbt.Add("Supinación", false);
        dictionary_bbt.Add("Alcance", true);

        Dictionary<string, bool> dictionary_cloth = new Dictionary<string, bool>();
        dictionary_cloth.Add("Pinza índice", true);
        dictionary_cloth.Add("Pinza medio", true);
        dictionary_cloth.Add("Pinza anular", false);
        dictionary_cloth.Add("Pinza meñique", false);
        dictionary_cloth.Add("Cierre puño", false);
        dictionary_cloth.Add("Apertura dedos", false);
        dictionary_cloth.Add("Flexión muñeca", false);
        dictionary_cloth.Add("Extensión muñeca", false);
        dictionary_cloth.Add("Desv. Radial", false);
        dictionary_cloth.Add("Desv. Cubital", false);
        dictionary_cloth.Add("Pronación", false);
        dictionary_cloth.Add("Supinación", false);
        dictionary_cloth.Add("Alcance", true);

        Dictionary<string, bool> dictionary_fruits_alcance = new Dictionary<string, bool>();
        dictionary_fruits_alcance.Add("Pinza índice", false);
        dictionary_fruits_alcance.Add("Pinza medio", false);
        dictionary_fruits_alcance.Add("Pinza anular", false);
        dictionary_fruits_alcance.Add("Pinza meñique", false);
        dictionary_fruits_alcance.Add("Cierre puño", false);
        dictionary_fruits_alcance.Add("Apertura dedos", false);
        dictionary_fruits_alcance.Add("Flexión muñeca", false);
        dictionary_fruits_alcance.Add("Extensión muñeca", false);
        dictionary_fruits_alcance.Add("Desv. Radial", false);
        dictionary_fruits_alcance.Add("Desv. Cubital", false);
        dictionary_fruits_alcance.Add("Pronación", false);
        dictionary_fruits_alcance.Add("Supinación", false);
        dictionary_fruits_alcance.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_secuencia = new Dictionary<string, bool>();
        dictionary_fruits_secuencia.Add("Pinza índice", false);
        dictionary_fruits_secuencia.Add("Pinza medio", false);
        dictionary_fruits_secuencia.Add("Pinza anular", false);
        dictionary_fruits_secuencia.Add("Pinza meñique", false);
        dictionary_fruits_secuencia.Add("Cierre puño", false);
        dictionary_fruits_secuencia.Add("Apertura dedos", false);
        dictionary_fruits_secuencia.Add("Flexión muñeca", false);
        dictionary_fruits_secuencia.Add("Extensión muñeca", false);
        dictionary_fruits_secuencia.Add("Desv. Radial", false);
        dictionary_fruits_secuencia.Add("Desv. Cubital", false);
        dictionary_fruits_secuencia.Add("Pronación", false);
        dictionary_fruits_secuencia.Add("Supinación", false);
        dictionary_fruits_secuencia.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_agarre = new Dictionary<string, bool>();
        dictionary_fruits_agarre.Add("Pinza índice", false);
        dictionary_fruits_agarre.Add("Pinza medio", false);
        dictionary_fruits_agarre.Add("Pinza anular", false);
        dictionary_fruits_agarre.Add("Pinza meñique", false);
        dictionary_fruits_agarre.Add("Cierre puño", true);
        dictionary_fruits_agarre.Add("Apertura dedos", true);
        dictionary_fruits_agarre.Add("Flexión muñeca", false);
        dictionary_fruits_agarre.Add("Extensión muñeca", false);
        dictionary_fruits_agarre.Add("Desv. Radial", false);
        dictionary_fruits_agarre.Add("Desv. Cubital", false);
        dictionary_fruits_agarre.Add("Pronación", false);
        dictionary_fruits_agarre.Add("Supinación", false);
        dictionary_fruits_agarre.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_volteo = new Dictionary<string, bool>();
        dictionary_fruits_volteo.Add("Pinza índice", false);
        dictionary_fruits_volteo.Add("Pinza medio", false);
        dictionary_fruits_volteo.Add("Pinza anular", false);
        dictionary_fruits_volteo.Add("Pinza meñique", false);
        dictionary_fruits_volteo.Add("Cierre puño", false);
        dictionary_fruits_volteo.Add("Apertura dedos", false);
        dictionary_fruits_volteo.Add("Flexión muñeca", false);
        dictionary_fruits_volteo.Add("Extensión muñeca", false);
        dictionary_fruits_volteo.Add("Desv. Radial", false);
        dictionary_fruits_volteo.Add("Desv. Cubital", false);
        dictionary_fruits_volteo.Add("Pronación", true);
        dictionary_fruits_volteo.Add("Supinación", true);
        dictionary_fruits_volteo.Add("Alcance", true);
        Dictionary<string, bool> dictionary_fruits_prension = new Dictionary<string, bool>();
        dictionary_fruits_prension.Add("Pinza índice", false);
        dictionary_fruits_prension.Add("Pinza medio", false);
        dictionary_fruits_prension.Add("Pinza anular", false);
        dictionary_fruits_prension.Add("Pinza meñique", false);
        dictionary_fruits_prension.Add("Cierre puño", true);
        dictionary_fruits_prension.Add("Apertura dedos", true);
        dictionary_fruits_prension.Add("Flexión muñeca", false);
        dictionary_fruits_prension.Add("Extensión muñeca", false);
        dictionary_fruits_prension.Add("Desv. Radial", false);
        dictionary_fruits_prension.Add("Desv. Cubital", false);
        dictionary_fruits_prension.Add("Pronación", false);
        dictionary_fruits_prension.Add("Supinación", false);
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
                //string exercise_from_exercises_to_display = exercises_names_to_display[exercises_names.IndexOf(protocols.Protocols_list[value - 1].Exercises[i])]; //Busco el nombre del ejercicio en la lista con tildes y 'ñ'
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

    public void OpenAddProtocolPanel()
    {
        panel_add_protocol.SetActive(!panel_add_protocol.activeSelf);
        panel_select_protocol.SetActive(!panel_select_protocol.activeSelf);

        // panel_add_protocol.SetActive(value);
        // panel_select_protocol.SetActive(!value);

        if (panel_add_protocol.activeSelf)
        {
            exercises.Clear();
            reps.Clear();
            games.Clear();
            listExer.text = "";
            listReps.text = "";
            protocol_name.text = "";

            undoButton.interactable = false;

            exercises_names_for_drop = new List<string>(exercises_names_to_display); //Rehago la copia por si he creado un protocolo y voy a crear otro
            indexDropsSelected.Clear(); //Reseteo la lista de índices seleccionados

            //Reseteo el drop de ejercicios
            List<string> list = new List<string>(exercises_names_to_display);
            list.Insert(0, "Selecciona...");
            exerciseDrop.options.Clear();
            foreach (string option in list)
            {
                exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
            }

            message.gameObject.SetActive(false);
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
            for (int i = 0; i < Resources.FindObjectsOfTypeAll<ShowWhenButtonHighlighted>().Length; i++) //Uso el TypeAll porque al estar inactivos, con el Type no los encontraba
            {
                Resources.FindObjectsOfTypeAll<ShowWhenButtonHighlighted>()[i].enabled = true;
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

            numReps.Select();
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
        undoButton.interactable = true;

        // exercises.Add(exerciseDrop.captionText.text);

        //Busco en la lista de nombres sin tildes, el índice que saco de buscar el ejercicio con tildes en la lista con tildes
       // exercises.Add(exercises_names[exercises_names_to_display.IndexOf(exerciseDrop.captionText.text)]);
       // exercises_display.Add(exerciseDrop.captionText.text); //Recojo los nombres con tildes, para las listas de display

        //****Prueba para guardar textos con tildes y ñ
        exercises.Add(exerciseDrop.captionText.text);
        
        
        
        reps.Add(numReps.text);





        exerciseDrop.interactable = true;

        indexDropsSelected.Add(exerciseDrop.value); //Lo guardo por si uso el Deshacer

        ///Para quitar el ejercicio ya seleccionado del drop///
        exercises_names_for_drop.RemoveAt(exerciseDrop.value - 1);

        //Quito este ejercicio
        List<string> list = new List<string>(exercises_names_for_drop);
        list.Insert(0, "Selecciona...");
        exerciseDrop.options.Clear();
        foreach (string option in list)
        {
            exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
        }
        ///////////////////////////////////////////////////////

        exerciseDrop.value = 0;
        addExercise.interactable = false;
        addExercise.transform.localScale = new Vector3(1, 1, 1);
        numReps.interactable = false;
        numReps.text = "";
        addStep.interactable = false;
                
        AddTexts(exercises[exercises.Count - 1], reps[reps.Count - 1], true);


     //   AddTexts(exercises_display[exercises.Count - 1], reps[reps.Count - 1], true);

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

    void AddTexts(string ex, string rep, bool add)
    {
        if (add)
        {
            listExer.text += ex + "\n";
            listReps.text += rep + "\n";
        }
        else
        {
            string[] arrayExerc = listExer.text.Split('\n');

            List<string> listArrayExerc = arrayExerc.ToList();
            listArrayExerc.RemoveAt(arrayExerc.Length - 2); //Es -2 porque el espacio en blanco lo detecta como uno
            arrayExerc = listArrayExerc.ToArray();

            listExer.text = String.Join("\n", arrayExerc);

            string[] arrayRep = listReps.text.Split('\n');
            List<string> listArrayRep = arrayRep.ToList();
            listArrayRep.RemoveAt(arrayRep.Length - 2);
            arrayRep = listArrayRep.ToArray();

            listReps.text = String.Join("\n", arrayRep);
        }
    }

    public void AddProtocol()
    {
        if (exercises.Count != 0)
        {
            if (FindProtocolByName(protocol_name.text) == false)
            {
                Protocol new_protocol = new Protocol();

                new_protocol.Name = protocol_name.text;
                new_protocol.Exercises = exercises;
                new_protocol.Reps = reps;


                List<string> gamesNoDuplicates = games.Distinct().ToList(); //Con esto quito duplicados

                List<string> gamesOrdered = gamesNoDuplicates.OrderBy(g => gameNames.IndexOf(g)).ToList(); //Ordeno la lista de juegos según el índice global

                new_protocol.Games = gamesOrdered;


                protocols.Protocols_list.Add(new_protocol);

                protocols.Protocols_list.Sort(SortProtocolByName); //La ordeno según el nombre

                message.color = new Color(5f / 255f, 210f / 255f, 0f, 255f / 255f);
                message.text = "Protocolo añadido correctamente.";
                message.gameObject.SetActive(true);


                SaveProtocols();
                EditDropList();

                Invoke(nameof(OpenAddProtocolPanel), 0.5f);
            }
            else
            {
                message.color = new Color(255f / 255f, 111f / 255f, 0f, 255f / 255f);
                message.text = "Ya existe un protocolo con ese nombre.";
                message.gameObject.SetActive(true);
            }
        }
        else
        {
            message.color = new Color(255f / 255f, 111f / 255f, 0f, 255f / 255f);
            message.text = "Debe añadir algún ejercicio al protocolo.";
            message.gameObject.SetActive(true);
        }
    }

    //Para ordenar la lista de protocolos cada vez que añado uno nuevo
    int SortProtocolByName(Protocol p1, Protocol p2)
    {
        return p1.Name.CompareTo(p2.Name);
    }

    //Para ver si existe un protocolo con ese nombre
    public bool FindProtocolByName(string name)
    {
        bool isFound = false;
        Protocol protocol_founded = null;
        protocol_founded = protocols.Protocols_list.Find(p => p.Name == name);
        if (protocol_founded != null)
        {
            isFound = true;
        }

        return isFound;
    }

    public void UndoStep()
    {
        //Hago el proceso contrario al AddStep

        //1º Borro del panel de ejercicios y reps, los últimos valores
         AddTexts(exercises[exercises.Count - 1], reps[reps.Count - 1], false);
       // AddTexts(exercises_display[exercises.Count - 1], reps[reps.Count - 1], false);

        //2º Borro de la lista de Games los juegos que he añadido
        bool value = false;
        exerciseValues[0].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Gesture"); }
        exerciseValues[1].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Arkanoid"); }
        exerciseValues[2].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Space"); }
        exerciseValues[3].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Cooking"); }
        exerciseValues[4].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Tres"); }
        exerciseValues[5].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Flota"); }
        exerciseValues[6].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("BBT"); }
        exerciseValues[7].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Clothespin"); }
        exerciseValues[8].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Alcance"); }
        exerciseValues[9].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Secuencia"); }
        exerciseValues[10].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Agarre"); }
        exerciseValues[11].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Volteo"); }
        exerciseValues[12].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { games.Remove("Prension"); }

        //3º Recojo el último índice del drop usado
        int indexExercise = indexDropsSelected[indexDropsSelected.Count - 1] - 1;

        //Añado de nuevo el ejercicio a la lista, usando el último índice como referencia
        exercises_names_for_drop.Insert(indexExercise, exercises[exercises.Count - 1]);
      //  exercises_names_for_drop.Insert(indexExercise, exercises_display[exercises_display.Count - 1]);

        List<string> list = new List<string>(exercises_names_for_drop);
        list.Insert(0, "Selecciona...");
        exerciseDrop.options.Clear();
        foreach (string option in list)
        {
            exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
        }

        exerciseDrop.interactable = false;

        exerciseDrop.value = indexDropsSelected[indexDropsSelected.Count - 1];
        exerciseDrop.captionText.text = list[exerciseDrop.value]; //Necesario porque el texto del drop no se actualiza al deshacer cuando deshago más de 1 vez
        addExercise.interactable = true;
        addExercise.transform.localScale = new Vector3(-1, 1, 1);
        numReps.interactable = true;
        numReps.text = reps[reps.Count - 1];
        addStep.interactable = true;

        //Borro el último index guardado
        indexDropsSelected.RemoveAt(indexDropsSelected.Count - 1);

        //Borro el ejercicio y las repeticiones de la lista
        exercises.RemoveAt(exercises.Count - 1);
        exercises_display.RemoveAt(exercises_display.Count - 1);
        reps.RemoveAt(reps.Count - 1);


        if (exercises.Count == 0) { undoButton.interactable = false; }
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


       File.WriteAllText(path.Replace(".dat",".json"), JsonUtility.ToJson(patientProtocol));
    }

    public void CreateProtocolDataCsv()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";
        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientId + "/Protocol_Data/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        int indexCSV = Directory.GetFiles(path).Length; //Este índice irá delante del nombre de los archivos para ordenarlos

        string filename = indexCSV + "__" + System.DateTime.Now.ToString("dd-MM-yyyy__HH-mm") + "__" + patientProtocol.Name + ".csv";



        path += filename;

        /* try
         {*/

       // var f1 = File.CreateText(path);

        string data = "Ficha de protocolo" + System.Environment.NewLine;
        data += System.Environment.NewLine;

        data += "Nombre;" + patientProtocol.Name + System.Environment.NewLine;
        data += System.Environment.NewLine;

        data += "Fecha de inicio de protocolo;" + System.DateTime.Now.ToString("g") + System.Environment.NewLine;
        data += "Última sesión;" + System.DateTime.Now.ToString("g") + System.Environment.NewLine;

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

        data += "Histórico de repeticiones por sesión" + System.Environment.NewLine;
        data += System.Environment.NewLine;
        data += "Ejercicios;Subjuegos" + System.Environment.NewLine;
        data += System.Environment.NewLine;
        data += System.Environment.NewLine;
        for (int i = 0; i < patientProtocol.Exercises.Count; i++)
        {
            data += patientProtocol.Exercises[i] + ";" + System.Environment.NewLine;
        }

        //VER EXCEL DEL MODELO DE CSV

        //f1.Write(data);

        //****Prueba para guardar textos con tildes y ñ
        File.WriteAllText(path, data, System.Text.Encoding.UTF8);

       // f1.Close();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region Llamar a fase intermedia

    public void CallIntermediatePhase()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Paths";
        string[] data = File.ReadAllLines(path + "/ApplicationsPaths.txt");

        Application.OpenURL(data[1]); //La fase intermedia para cuando modifique el orden de los paths

        Invoke(nameof(Finish), .5f);
    }

    void Finish()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        //  UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion
}

[System.Serializable]
public class Protocol
{
   [SerializeField] int index_protocol_selected_in_protocols = -1;
 
   [SerializeField] string name = "";
   [SerializeField] List<string> exercises = new List<string>();
   [SerializeField] List<string> reps = new List<string>();
   [SerializeField] List<string> games = new List<string>();
   [SerializeField] int index_subgame_selected = -1;
   [SerializeField] int index_game_selected = -1;
   [SerializeField] List<string> games_already_Selected = new List<string>();

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