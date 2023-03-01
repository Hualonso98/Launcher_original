using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class SetStandardFolders : MonoBehaviour
{
#if UNITY_EDITOR
    List<string> folders = new List<string>() { };
    public TextAsset readme; //Solo lo creo para attachearlo desde Editor y que se a�ada como dependencia para exportar el package.
    private void Awake()
    {
        folders.Add("Animations");
        folders.Add("Animations/FromScenes");
        folders.Add("Fonts");
        folders.Add("Materials");
        folders.Add("Models");
        folders.Add("Models/Environment_Models");
        folders.Add("Models/Hands");
        folders.Add("Prefabs");
        folders.Add("Prefabs/Hands");
        folders.Add("Scenes");
        folders.Add("Scripts");
        folders.Add("Scripts/Data");
        folders.Add("Scripts/Data/GameData");
        folders.Add("Scripts/Data/Patients");
        folders.Add("Scripts/Data/Static");
        folders.Add("Scripts/FromScenes");
        folders.Add("Scripts/Hands");
        folders.Add("Scripts/Utilities");
        folders.Add("Scripts/Utilities/UI");
        folders.Add("Sounds");
        folders.Add("Sounds/EventsSounds");
        folders.Add("Sounds/Themes");
        folders.Add("Sprites");
        folders.Add("Sprites/Menu_UI");
        folders.Add("Sprites/Resources");
        folders.Add("Sprites/StandardButtons");
        folders.Add("Tests");

        CreateFolders();

        //string a = "Scripts/Data/Static";
        //string b = a.Substring(0, a.LastIndexOf('/'));
        //string c = a.Substring(a.LastIndexOf('/') + 1, (a.Length - 1 - a.LastIndexOf('/')));

        //Debug.Log("A: " + a);
        //Debug.Log("B: " + b);
        //Debug.Log("C: " + c);
        //Debug.Log("B + C: " + b + "/" + c);
    }

    [MenuItem("Assets/Create Asset Folders")]
    public void CreateFolders()
    {
        if (!AssetDatabase.IsValidFolder("Assets/0_Standard_Folders")) { AssetDatabase.CreateFolder("Assets", "0_Standard_Folders"); }

        foreach (var folder in folders)
        {
            string path = "Assets/0_Standard_Folders/" + folder;

            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.Log("New path: " + path);
                string append_parent;
                string folder_name;

                if (folder.Contains("/"))
                {
                    append_parent = "/" + folder.Substring(0, folder.LastIndexOf('/')); //Previo al �ltimo "/"
                    folder_name = folder.Substring(folder.LastIndexOf('/') + 1, (folder.Length - 1 - folder.LastIndexOf('/'))); //Desde el �ltimo "/" al final
                }
                else
                {
                    append_parent = "";
                    folder_name = folder;
                }

                AssetDatabase.CreateFolder("Assets/0_Standard_Folders" + append_parent, folder_name);
            }
        }
        AssetDatabase.Refresh();
    }
#endif
}