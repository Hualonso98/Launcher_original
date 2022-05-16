using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SaveInfoPatients_launch: MonoBehaviour
{
    ////////////////OBJETOS DE LA INTERFAZ////////////////

    //////////////////////////////////////////////////////


    ////////////////VARIABLES////////////////
    private static SaveInfoPatients_launch instance = null;
    private bool allowedPatient = false; //variable que indica que hay un paciente seleccionado
    private int index = -1; //Para indicar la posición del paciente  

    private Patient selectedPatient = new Patient(); //Cada vez que se inicia el juego, el paciente se renueva


    private Patients pacientes = new Patients();

    private PatientsNamesID patientsNamesId = new PatientsNamesID();
    /////////////////////////////////////////


    ////////////////GETTERS Y SETTERS////////////////
    public static SaveInfoPatients_launch Instance { get => instance; set => instance = value; }
    public bool AllowedPatient { get => allowedPatient; set => allowedPatient = value; }
    public int Index { get => index; set => index = value; }
    public Patient SelectedPatient { get => selectedPatient; set => selectedPatient = value; }
    public Patients Pacientes { get => pacientes; set => pacientes = value; }

    public PatientsNamesID PatientsNamesId { get => patientsNamesId; set => patientsNamesId = value; }



    /////////////////////////////////////////////////

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Lo tengo que inicializar aquí porque si lo hago en la declaración no se modifica el valor y lo deja en 0.
        index = -2; //Lo pongo en -2, porque desde PatientMenu, en el ChangeDrop, comparo el valor del drop con el index + 1, y si el drop es 0 (ningun paciente), el index = -1  + 1, es 0 y coincide

        patientsNamesId.LoadCsvPatientsName();


        if (!Directory.Exists(Application.dataPath + "/Paths"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Paths");
        }

        if (!File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/PatientSelected.txt"))
        {
            File.Create(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/PatientSelected.txt").Dispose();
        }
        else
        {
            File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop" + "/PatientSelected.txt", 
                "--" + System.Environment.NewLine + SavingData_launch.appSelected + System.Environment.NewLine + "0");
        }
    }


    //////////////////////////////////////////////////////////////////////////////
    ///

    public void ResetPatient()
    {
        selectedPatient = new Patient();
        index = -2;
    }

    public string[] FromListToArray(List<string> list)
    {
        string[] array = new string[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            array[i] = list[i];
        }
        return array;
    }

    public List<string> FromArrayToList(string[] array)
    {
        List<string> list = new List<string>();

        for (int i = 0; i < array.Length; i++)
        {
            list.Add(array[i]);
        }
        return list;
    }

    public string FromStringArrayToOnly(string[] array)
    {
        string text = null;

        for (int i = 0; i < array.Length; i++)
        {
            text += array[i] + ";"; //funcionaría, pero el problema son los saltos de línea
        }

        return text;
    }

}

[System.Serializable]
public class Patient
{
    [SerializeField] private string name;
    [SerializeField] private string surname1;
    [SerializeField] private string surname2;
    [SerializeField] private string pathology;
    [SerializeField] private int ID;
    [SerializeField] private string affectedHand;

    [SerializeField] private bool leftHandUsed;
    [SerializeField] private int numHandModelType = 0; //0: reales   1: black   2: capsule   3: guante   4: skeleton   5: robot

    [SerializeField] private int lastSession_Gestures = 0;
    [SerializeField] private int lastSession_MT = 0;
    [SerializeField] private int lastSession_BBT = 0;
    [SerializeField] private int lastSession_Clothespin = 0;
    [SerializeField] private int lastSession_Fruits = 0;

    //Las opciones de juego llevan unos valores por defecto cada vez que creo un paciente
    /*  [SerializeField] private bool modeGesture = true;
      [SerializeField] private bool modeTimeGesture = true;
      [SerializeField] private bool progressive = false;
      [SerializeField] private int numTheme = 0;
      [SerializeField] private float timeGeneration = 0.75f;
      [SerializeField] private float timeFalling = 0.75f;
      [SerializeField] private float linePosition = -150f;

      [SerializeField] private bool length = true;
      [SerializeField] private bool arkMode = true;
      [SerializeField] private float speedPongValue = 0.75f;
      [SerializeField] private bool speedBall = true;

      [SerializeField] private int lives = 3;
      [SerializeField] private float timeAttacks = 0.75f;
      [SerializeField] private float speedShots = 0.75f;
      [SerializeField] private float speedCraft = 0.75f;
      [SerializeField] private bool heatTracking = false;

      [SerializeField] private bool difficultMode = false;
      [SerializeField] private int timeHolding = 3;

      [SerializeField] private int ammoSelected = 13;
      [SerializeField] private int flotaHolding = 3;


      [SerializeField] private int[] gest_Gestures = new int[5] { 0, 1, 2, 3, 4 }; //Gesto1, Gesto2, Gesto3, Gesto4, Gesto5
      [SerializeField] private int[] ark_Gestures = new int[2] { 0, 1 }; //Gesto1, Gesto2
      [SerializeField] private int[] spa_Gestures = new int[3] { 0, 1, 2 };
      [SerializeField] private int[] tres_Gestures = new int[5] { 0, 1, 2, 3, 4 };
      [SerializeField] private int[] flota_Gestures = new int[5] { 0, 1, 2, 3, 4 };


      [SerializeField] private float tolerance_pinch = 0.01f;
      [SerializeField] private float tolerance_exten = 0.005f;
      [SerializeField] private float tolerance_desv_vert = 5;
      [SerializeField] private float tolerance_desv_hor = 5;

      [SerializeField] private float last_left_indexPinchCalibValue = SavingData.default_minIndexPinchDistance;
      [SerializeField] private float last_left_middlePinchCalibValue = SavingData.default_minMiddlePinchDistance;
      [SerializeField] private float last_left_ringPinchCalibValue = SavingData.default_minRingPinchDistance;
      [SerializeField] private float last_left_pinkyPinchCalibValue = SavingData.default_minPinkyPinchDistance;

      [SerializeField] private float last_left_thumbExtensionCalibValue = SavingData.default_maxThumbExtension;
      [SerializeField] private float last_left_indexExtensionCalibValue = SavingData.default_maxIndexExtension;
      [SerializeField] private float last_left_middleExtensionCalibValue = SavingData.default_maxMiddleExtension;
      [SerializeField] private float last_left_ringExtensionCalibValue = SavingData.default_maxRingExtension;
      [SerializeField] private float last_left_pinkyExtensionCalibValue = SavingData.default_maxPinkyExtension;

      [SerializeField] private float last_left_flexCalibValue = SavingData.default_maxVerticalAngleDown;
      [SerializeField] private float last_left_extenCalibValue = SavingData.default_maxVerticalAngleUp;
      [SerializeField] private float last_left_desvRadCalibValue = SavingData.default_maxHorizontalAngleRight;
      [SerializeField] private float last_left_desvCubitCalibValue = SavingData.default_maxHorizontalAngleLeft;

      [SerializeField] private float last_left_pron = SavingData.default_pron;
      [SerializeField] private float last_left_sup = SavingData.default_left_sup;

      [SerializeField] private float last_right_indexPinchCalibValue = SavingData.default_minIndexPinchDistance;
      [SerializeField] private float last_right_middlePinchCalibValue = SavingData.default_minMiddlePinchDistance;
      [SerializeField] private float last_right_ringPinchCalibValue = SavingData.default_minRingPinchDistance;
      [SerializeField] private float last_right_pinkyPinchCalibValue = SavingData.default_minPinkyPinchDistance;

      [SerializeField] private float last_right_thumbExtensionCalibValue = SavingData.default_maxThumbExtension;
      [SerializeField] private float last_right_indexExtensionCalibValue = SavingData.default_maxIndexExtension;
      [SerializeField] private float last_right_middleExtensionCalibValue = SavingData.default_maxMiddleExtension;
      [SerializeField] private float last_right_ringExtensionCalibValue = SavingData.default_maxRingExtension;
      [SerializeField] private float last_right_pinkyExtensionCalibValue = SavingData.default_maxRingExtension;

      [SerializeField] private float last_right_flexCalibValue = SavingData.default_maxVerticalAngleDown;
      [SerializeField] private float last_right_extenCalibValue = SavingData.default_maxVerticalAngleUp;
      [SerializeField] private float last_right_desvRadCalibValue = SavingData.default_maxHorizontalAngleLeft;
      [SerializeField] private float last_right_desvCubitCalibValue = SavingData.default_maxHorizontalAngleRight;

      [SerializeField] private float last_right_pron = SavingData.default_pron;
      [SerializeField] private float last_right_sup = SavingData.default_right_sup;
      */

    public Patient() { } //Constructor base
    public Patient(string name, string surname1, string surname2, string pathology, int ID, string affectedHand)
    {
        this.name = name;
        this.surname1 = surname1;
        this.surname2 = surname2;
        this.pathology = pathology;
        this.ID = ID;
        this.affectedHand = affectedHand.ToUpper(); //Lo guardo en mayúsculas
    }

    public Patient(string name, string surname1, string surname2) //constructor para cuando vaya a cargar un paciente solo por el nombre del drop
    {
        this.name = name;
        this.surname1 = surname1;
        this.surname2 = surname2;
    }
    public Patient(int ID) //constructor para cuando vaya a cargar un paciente por el id del drop
    {
        this.ID = ID;
    }
    public Patient(string name, string surname1, string surname2, int ID)
    {
        this.name = name;
        this.name = name;
        this.surname1 = surname1;
        this.surname2 = surname2;
        this.ID = ID;
    }

    ////////////////GETTERS Y SETTERS////////////////
    public string Name { get => name; set => name = value; }
    public string Surname1 { get => surname1; set => surname1 = value; }
    public string Surname2 { get => surname2; set => surname2 = value; }
    public int ID1 { get => ID; set => ID = value; }
    public string AffectedHand { get => affectedHand; set => affectedHand = value; }

    public int LastSession_Gestures { get => lastSession_Gestures; set => lastSession_Gestures = value; }
    public int LastSession_MT { get => lastSession_MT; set => lastSession_MT = value; }
    public int LastSession_BBT { get => lastSession_BBT; set => lastSession_BBT = value; }
    public int LastSession_Clothespin { get => lastSession_Clothespin; set => lastSession_Clothespin = value; }
    public int LastSession_Fruits { get => lastSession_Fruits; set => lastSession_Fruits = value; }

    /////////////////////////////////////////////////
    ///

    public void SaveCsvPatient() //Pido el juego como parámetro, para evitar hacer los appends de todos los juegos cada vez que la llame
    {
        string patientFolder = name + " " + surname1 + " " + surname2;
        patientFolder = ID.ToString(); //AHORA LLAMARÉ AL PACIENTE POR EL ID

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";

        ///////////////////////////Guardar TODOS///////////////////////////
        if (!Directory.Exists(desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder))
        {
            Directory.CreateDirectory(desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder);
        }

        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder + "/" + patientFolder + ".csv";
      
        if (!File.Exists(path)) //si no existe el archivo, es que es la primera vez
        {
            var ff = File.CreateText(path);

            string data = "Last session Gestures;Last session MT;Last session BBT;Last session Clothespin;Last session Fruits" + System.Environment.NewLine;

            data += lastSession_Gestures + ";" + lastSession_MT + ";" + lastSession_BBT + ";" + lastSession_Clothespin + ";" + lastSession_Fruits + System.Environment.NewLine;

            data += System.Environment.NewLine;

            data += "ID;Pathology;Mano amputada" + System.Environment.NewLine;

            data += ID + ";" + pathology + ";" + affectedHand + System.Environment.NewLine;

            data += System.Environment.NewLine;

            data += "Modelo de mano" + System.Environment.NewLine;

            data += numHandModelType + System.Environment.NewLine;

            ff.Write(data);
            ff.Close();
        }
        else
        {
            //Esto es para modificar la Last Session nada más.

            string[] arrLine = File.ReadAllLines(path);

            arrLine[1] = lastSession_Gestures + ";" + lastSession_MT + ";" + lastSession_BBT + ";" + lastSession_Clothespin + ";" + lastSession_Fruits;

            File.WriteAllLines(path, arrLine);
        }
    }

    public void LoadCsvPatient()
    {
        string patientFolder = name + " " + surname1 + " " + surname2;
        patientFolder = ID.ToString(); //NUEVO PATH ES EL ID

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";

        ///////////////////////////Guardar TODOS///////////////////////////
        if (!Directory.Exists(desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder))
        {
            Directory.CreateDirectory(desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder);
            return; //Si no existe el directorio vuelvo, porque no va a haber ningún fichero que cargar
        }

        string path = desktopPath + "/Patients_Data/Exported Data/Patients/" + patientFolder + "/" + patientFolder + ".csv";

        if (!File.Exists(path))
        {
            return;
        }

        //var ff = File.ReadAllText(path);
        //StreamReader strReader = new StreamReader(path);
        //bool endOfFile = false;
        int line = 0;
        string[] lines = File.ReadAllLines(path);

        while (line < lines.Length)
        {
            //Línea 0: cabecera last session
            //Línea 1: last session
            //Línea 2: salto (¿lo detectará como endOfFile?)
            //Línea 3: cabecera de los datos
            //Línea 4: los datos
            

            string targetLine = lines[line];
            string[] targetLineSplitted = targetLine.Split(';');

            switch (line)
            {
                case 1:
                    lastSession_Gestures = int.Parse(targetLineSplitted[0]);
                    lastSession_MT = int.Parse(targetLineSplitted[1]);
                    lastSession_BBT = int.Parse(targetLineSplitted[2]);
                    lastSession_Clothespin = int.Parse(targetLineSplitted[3]);
                    lastSession_Fruits = int.Parse(targetLineSplitted[4]);
                    break;

                case 4: //la línea de los datos de paciente, AHORA SOLO EL ID Y PATHOLOGY
                    /* name = targetLineSplitted[0];
                     surname1 = targetLineSplitted[1];
                     surname2 = targetLineSplitted[2];*/
                    ID = int.Parse(targetLineSplitted[0]);
                    pathology = targetLineSplitted[1];
                    affectedHand = targetLineSplitted[2];
                    break;
            }

            if (line == 4) { break; }

            line++;
        }
        //strReader.Close();
    }

    public void SetSavingDataValues()
    {//Método para guardar en el fichero estático SavingData, todos los valores del paciente al cargar un paciente

        //El si usé mano izquierda o derecha
        //SavingData.affectedHand = affectedHand;
    }
}

////////////////////// NO SE USA CON EL LEAP, ERA PARA LOS JSON NO EL CSV ///////////////////////
[System.Serializable]
public class Patients
{
    [SerializeField] private List<Patient> patientsList = new List<Patient>();

    ////////////////GETTERS Y SETTERS////////////////
    public List<Patient> PatientsList { get => patientsList; set => patientsList = value; }
    /////////////////////////////////////////////////
}
/////////////////////////////////////////////////////////////////////////////////////////////////
[System.Serializable]
public class PatientsNamesID
{
    [SerializeField] private List<int> patientsIdList = new List<int>();
    [SerializeField] private List<string> patientsNamesList = new List<string>();
    [SerializeField] private List<Patient> patients = new List<Patient>();
    ////////////////GETTERS Y SETTERS////////////////
    public List<int> PatientsIdList { get => patientsIdList; set => patientsIdList = value; }
    public List<string> PatientsNamesList { get => patientsNamesList; set => patientsNamesList = value; }
    public List<Patient> Patients { get => patients; set => patients = value; }

    /////////////////////////////////////////////////

    public void SaveCsvPatientsName()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";

        ///////////////////////////Guardar TODOS///////////////////////////
        if (!Directory.Exists(desktopPath + "/Patients_Data/Exported Data/Patients List"))
        {
            Directory.CreateDirectory(desktopPath + "/Patients_Data/Exported Data/Patients List");
        }

        string path = desktopPath + "/Patients_Data/Exported Data/Patients List/" + "PatientsList.csv";

        var ff = File.CreateText(path);
        //ff.Dispose();
        string data = "ID;Nombre completo;Mano amputada afectada" + System.Environment.NewLine;

        for (int i = 0; i < patients.Count; i++)
        {
            //data += patientsIdList[i] + ";" + patientsNamesList[i] + System.Environment.NewLine;
            data += patients[i].ID1 + ";" + patients[i].Name + " " + patients[i].Surname1 + " " + patients[i].Surname2 + ";" + patients[i].AffectedHand + System.Environment.NewLine;
        }

        ff.Write(data);
        ff.Close();
    }
    public void LoadCsvPatientsName()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M";

        ///////////////////////////Guardar TODOS///////////////////////////
        if (!Directory.Exists(desktopPath + "/Patients_Data/Exported Data/Patients List"))
        {
            Directory.CreateDirectory(desktopPath + "/Patients_Data/Exported Data/Patients List");

            return; //Si no existe el directorio vuelvo, porque no va a haber ningún fichero que cargar
        }

        string path = desktopPath + "/Patients_Data/Exported Data/Patients List/" + "PatientsList.csv";

        if (!File.Exists(path))
        {
            return;
        }

        var ff = File.ReadAllText(path);
        StreamReader strReader = new StreamReader(path);
        bool endOfFile = false;
        int line = 0;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            string[] dataStringSplitted = data_String.Split(';');

            //Línea 0: cabecera de ID y "Nombre completo"
            //Línea 1 ... n: nombres de los pacientes

            if (line > 0) //Empiezo a sumar en la segunda fila (de índice 1)
            {
                //patientsIdList.Add(int.Parse(dataStringSplitted[0]));
                //patientsNamesList.Add(dataStringSplitted[1]); //Añado directamente la línea porque no hay puntos y coma

                int id = int.Parse(dataStringSplitted[0]);
                string name = dataStringSplitted[1].Split(' ')[0]; //la segunda columna la separo también en espacios
                string surname1 = dataStringSplitted[1].Split(' ')[1];
                string surname2 = dataStringSplitted[1].Split(' ')[2];
                Patient newPatient = new Patient(name, surname1, surname2, id);
                newPatient.AffectedHand = dataStringSplitted[2]; //Recoge la mano afectada
                patients.Add(newPatient);
            }

            line++;
        }
        strReader.Close();
    }
}