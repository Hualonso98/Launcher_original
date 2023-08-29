using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ChangeResolution : MonoBehaviour
{
    public static ChangeResolution instance;

    Coroutine changeResolution_coroutine;

    int width;
    int height;


    private void Awake()
    {
        string directory_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/";
        string resol_path = directory_path + "MaxScreenResolution.txt";

        if (Directory.Exists(directory_path))
        {
            if (!File.Exists(resol_path))
            {
                File.Create(resol_path).Dispose();
                File.WriteAllText(resol_path, 1280 + System.Environment.NewLine + 720);
                Screen.SetResolution(1280, 720, true);
            }
            else
            {
                int w = int.Parse(File.ReadAllLines(resol_path)[0]);
                int h = int.Parse(File.ReadAllLines(resol_path)[1]);

                Screen.SetResolution(w, h, true);
            }
        }

        //Puedo fijar una resolución de 1920*1080 directamente, que tiene ratio 16:9 y es la resolución más común ahora en pantallas
    }

    // Start is called before the first frame update
    void Start()
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

        //Con Alt + Enter también funciona por defecto
        changeResolution_coroutine = StartCoroutine(ChangeResolutionFunction());
    }

    IEnumerator ChangeResolutionFunction()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F11));

            Screen.fullScreen = !Screen.fullScreen;

            Debug.Log(Screen.fullScreen);


            /*   Invoke(nameof(*///RestartCoroutine()/*), .5f)*/; //Necesito para y reiniciar la corrutina porque entraba dos veces seguidas si no la paraba
            StartCoroutine(RestartCoroutine()); //Aquí dentro está el delay ya
            StopCoroutine(changeResolution_coroutine);


        }
    }

    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        changeResolution_coroutine = StartCoroutine(ChangeResolutionFunction());
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(Screen.width + "/" + Screen.height);
           // GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = Screen.width + "/" + Screen.height;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            float w = Screen.width;
            float h = Screen.height;
            float r = w / h;

            //Con esto puedo fijar una resolución de 16:9, pero maximizando la calidad de la imagen
            if ((r - 16f / 9f) > 0.001f)
            { //R > 16:9
                while ((r - 16f / 9f) > 0.001f)
                {
                    h++;
                    r = w / h;
                }
            }
            else
            { //R < 16:9
                while ((r - 16f / 9f) < 0.001f)
                {
                    h--;
                    r = w / h;
                }
            }

            string resol_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/RoboticsLab_UC3M/Develop/" + "MaxScreenResolution.txt";

            File.WriteAllText(resol_path, w + System.Environment.NewLine + h);

            //  GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = w + "/" + h;
            Screen.SetResolution((int)w, (int)h, true);
        }
        /* if (Input.GetKey(KeyCode.W))
         {
             if (Input.GetKeyDown(KeyCode.KeypadPlus))
             {
                 width++;
                 GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = width + "/" + height;
             }
             if (Input.GetKeyDown(KeyCode.KeypadMinus))
             {
                 width--;
                 GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = width + "/" + height;
             }
         }
         if (Input.GetKey(KeyCode.H))
         {
             if (Input.GetKeyDown(KeyCode.KeypadPlus))
             {
                 height++;
                 GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = width + "/" + height;
             }
             if (Input.GetKeyDown(KeyCode.KeypadMinus))
             {
                 height--;
                 GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = width + "/" + height;
             }
         }
         if (Input.GetKeyDown(KeyCode.T))
         {
             Screen.SetResolution(width, height, true);
             GameObject.Find("Resol").GetComponent<TMPro.TextMeshProUGUI>().text = width + "/" + height;
         }
        */
    }

}