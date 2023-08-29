/**************************************************************************************
* Copyright (c) 2023. Universidad Carlos III de Madrid. Todos los derechos reservados *
*                                                                                     *
* Authors: Hugo Alonso Cámara, Edwin Daniel Oña, Carlos Balaguer, and Alberto Jardón  *
*                                                                                     *
* Descripción del proyecto:                                                           *
* [Desarrollo de Serious Games orientados a la rehabilitación de miembro superior     *
*  mediante el uso de dispositivos de tracking de manos y antebrazo como Ultraleap,   *
*  complementado con la inmersión en Realidad Virtual con Oculus]                     *
*                                                                                     *
*                                                                                     *
* Descripción del juego:                                                              *
* [Lanzador que centraliza la gestión de pacientes/usuarios y se encarga de           *
*  la apertura del lanzamiento del resto de juegos]                                   *
*                                                                                     *
***************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.IO.MemoryMappedFiles;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class SetStandardFolders : MonoBehaviour
{
#if UNITY_EDITOR
    List<string> folders = new List<string>();
    public TextAsset readme; //Solo lo creo para attachearlo desde Editor y que se añada como dependencia para exportar el package.
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
        folders.Add("Prefabs/FromScenes");
        folders.Add("Prefabs/UI");
        folders.Add("Scenes");
        folders.Add("Scripts");
        folders.Add("Scripts/Protocol");
        folders.Add("Scripts/Data");
        folders.Add("Scripts/Data/GameData");
        folders.Add("Scripts/Data/Patients");
        folders.Add("Scripts/Data/Static");
        folders.Add("Scripts/FromScenes");
        folders.Add("Scripts/FromPrefabs");
        folders.Add("Scripts/Hands");
        folders.Add("Scripts/Utilities");
        folders.Add("Scripts/Utilities/UI");
        folders.Add("Scripts/Utilities/UI/Logos");
        folders.Add("Scripts/Utilities/UI/Texts-Labels");
        folders.Add("Sounds");
        folders.Add("Sounds/EventsSounds");
        folders.Add("Sounds/Themes");
        folders.Add("Sprites");
        folders.Add("Sprites/Figures");
        folders.Add("Sprites/Figures/FromScenes");
        folders.Add("Sprites/Figures/FromPrefabs");
        folders.Add("Sprites/Backgrounds");
        folders.Add("Sprites/Menu_UI");
        folders.Add("Sprites/Menu_UI/Figures");
        folders.Add("Sprites/Menu_UI/StandardButtons");
        folders.Add("Sprites/Menu_UI/Logos");
        folders.Add("Sprites/Menu_UI/Panels");
        folders.Add("Sprites/Captures");
        folders.Add("Sprites/Resources");
        folders.Add("Multimedia");
        folders.Add("Multimedia/Videos");
        folders.Add("Multimedia/Gifs");
        folders.Add("Tests");

        CreateFolders();
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
                    append_parent = "/" + folder.Substring(0, folder.LastIndexOf('/')); //Previo al último "/"
                    folder_name = folder.Substring(folder.LastIndexOf('/') + 1, (folder.Length - 1 - folder.LastIndexOf('/'))); //Desde el último "/" al final
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