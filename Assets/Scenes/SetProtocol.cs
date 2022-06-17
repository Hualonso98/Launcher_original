using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetProtocol : MonoBehaviour
{
    public Button addExercise;
    public Button addStep;
    public TMP_InputField numReps;

    public TMP_Dropdown exerciseDrop;
    public TextMeshProUGUI listExer;
    public TextMeshProUGUI listReps;

    List<string> exercises = new List<string>();
    List<string> reps = new List<string>();
    List<Dictionary<string, bool>> exerciseValues = new List<Dictionary<string, bool>>();
    // Start is called before the first frame update
    void Start()
    {
        List<string> list = new List<string> { "Selecciona...", "Pinza índice", "Pinza medio", "Pinza anular" , "Pinza meñique", "Apertura dedos","Cierre puño", "Flexión muñeca",
            "Extensión muñeca", "Desv. radial", "Desv. Cubital", "Pronosupinación", "Alcance"};


        exerciseDrop.options.Clear();
        foreach (string option in list)
        {
            exerciseDrop.options.Add(new TMP_Dropdown.OptionData(option));
        }

        Dictionary<string, bool> dictionary_gest = new Dictionary<string, bool>();
        dictionary_gest.Add("Pinza índice", true);
        dictionary_gest.Add("Pinza medio", true);
        dictionary_gest.Add("Pinza anular", true);
        dictionary_gest.Add("Pinza meñique", true);
        dictionary_gest.Add("Apertura dedos", true);
        dictionary_gest.Add("Cierre puño", true);
        dictionary_gest.Add("Flexión muñeca", true);
        dictionary_gest.Add("Extensión muñeca", true);
        dictionary_gest.Add("Desv. Radial", true);
        dictionary_gest.Add("Desv. Cubital", true);
        dictionary_gest.Add("Pronosupinación", true);
        dictionary_gest.Add("Alcance", false);

        Dictionary<string, bool> dictionary_bbt = new Dictionary<string, bool>();
        dictionary_bbt.Add("Pinza índice", true);
        dictionary_bbt.Add("Pinza medio", true);
        dictionary_bbt.Add("Pinza anular", false);
        dictionary_bbt.Add("Pinza meñique", false);
        dictionary_bbt.Add("Apertura dedos", false);
        dictionary_bbt.Add("Cierre puño", false);
        dictionary_bbt.Add("Flexión muñeca", false);
        dictionary_bbt.Add("Extensión muñeca", false);
        dictionary_bbt.Add("Desv. Radial", false);
        dictionary_bbt.Add("Desv. Cubital", false);
        dictionary_bbt.Add("Pronosupinación", false);
        dictionary_bbt.Add("Alcance", false);

        Dictionary<string, bool> dictionary_cloth = new Dictionary<string, bool>();
        dictionary_cloth.Add("Pinza índice", true);
        dictionary_cloth.Add("Pinza medio", true);
        dictionary_cloth.Add("Pinza anular", false);
        dictionary_cloth.Add("Pinza meñique", false);
        dictionary_cloth.Add("Apertura dedos", false);
        dictionary_cloth.Add("Cierre puño", false);
        dictionary_cloth.Add("Flexión muñeca", false);
        dictionary_cloth.Add("Extensión muñeca", false);
        dictionary_cloth.Add("Desv. Radial", false);
        dictionary_cloth.Add("Desv. Cubital", false);
        dictionary_cloth.Add("Pronosupinación", false);
        dictionary_cloth.Add("Alcance", true);

        Dictionary<string, bool> dictionary_fruits = new Dictionary<string, bool>();
        dictionary_fruits.Add("Pinza índice", false);
        dictionary_fruits.Add("Pinza medio", false);
        dictionary_fruits.Add("Pinza anular", false);
        dictionary_fruits.Add("Pinza meñique", false);
        dictionary_fruits.Add("Apertura dedos", true);
        dictionary_fruits.Add("Cierre puño", true);
        dictionary_fruits.Add("Flexión muñeca", false);
        dictionary_fruits.Add("Extensión muñeca", false);
        dictionary_fruits.Add("Desv. Radial", false);
        dictionary_fruits.Add("Desv. Cubital", false);
        dictionary_fruits.Add("Pronosupinación", true);
        dictionary_fruits.Add("Alcance", true);

        exerciseValues.Add(dictionary_gest);
        exerciseValues.Add(dictionary_bbt);
        exerciseValues.Add(dictionary_cloth);
        exerciseValues.Add(dictionary_fruits);

        if (exerciseDrop.value == 0)
        {
            addExercise.interactable = false;
            addExercise.GetComponentInChildren<Image>().color = Color.grey;
        }
        Debug.Log(numReps.text);
        if (numReps.text == "")
        {
            addStep.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

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
        if (value == true) { Debug.Log("Gestures SÍ"); } else { Debug.Log("Gestures NO"); }
        exerciseValues[1].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("BBT SÍ"); } else { Debug.Log("BBT NO"); }
        exerciseValues[2].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("CLOTHESPIN SÍ"); } else { Debug.Log("CLOTHESPIN NO"); }
        exerciseValues[3].TryGetValue(exercises[exercises.Count - 1], out value);
        if (value == true) { Debug.Log("FRUITS SÍ"); } else { Debug.Log("FRUITS NO"); }
    }

    void AddTexts(string ex, string rep)
    {
        listExer.text += System.Environment.NewLine + ex;
        listReps.text += System.Environment.NewLine + rep;
    }
}
