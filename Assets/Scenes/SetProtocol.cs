using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetProtocol : MonoBehaviour
{
    public Button addApp;
    public Button addStep;
    public TMP_InputField numRpes;
    public TMP_Dropdown appDrop;
    public TMP_Dropdown exerciseDrop;
    public TextMeshProUGUI listApp;
    public TextMeshProUGUI listExer;
    public TextMeshProUGUI listReps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddApp()
    {
        exerciseDrop.gameObject.SetActive(true);
        numRpes.gameObject.SetActive(true);
        addStep.gameObject.SetActive(true);
    }

    public void ChangeAppDrop(int value)
    {
        if (value != 0)
        {
            addApp.interactable = true;
            addApp.GetComponentInChildren<Image>().color = Color.black;
        }
        else
        {
            addApp.interactable = false;
            addApp.GetComponentInChildren<Image>().color = Color.grey;
        }
    }
}
