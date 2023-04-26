using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

public class SetHospitalLogo : MonoBehaviour
{
    bool activeLogo = true;
    bool isAsepeyo = true;
    GameObject asepeyoLogo;
    GameObject mutuaLogo;

    private void Awake()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/Logo/logo.dat";
        //TRUE: Asepeyo FALSE: Mutua
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            File.WriteAllText(path, "true");

            //Voy con los valores por defecto
        }
        else
        {
            activeLogo = bool.TryParse(File.ReadAllText(path), out isAsepeyo);
        }

        ChangeImageSprite();
    }
    void ChangeImageSprite()
    {
        asepeyoLogo = transform.Find("Logo ASEPEYO").gameObject;
        mutuaLogo = transform.Find("Logo MUTUA").gameObject;

        if (activeLogo)
        {
            if (isAsepeyo) { asepeyoLogo.SetActive(true); } else { mutuaLogo.SetActive(true); }
        }
        else
        {
            asepeyoLogo.SetActive(false);
            mutuaLogo.SetActive(false);
        }
    }
}