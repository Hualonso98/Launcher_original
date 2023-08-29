using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PruebaWrite : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Save1();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Save2();
        }
    }

    public void Save1()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Fichero.txt";

        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            int i = 0;
            // string newLine;
            while ((line = reader.ReadLine()) != null)
            {
                if (i == 3)
                {
                    line = 1234 + "";
                }

                if (i == 4)
                {
                    line = 5678 + "";
                }

                i++;
                lines.Add(line);
            }
            reader.Close();
        }

        using (StreamWriter writer = new StreamWriter(path, false))
        {
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
            writer.Close();
        }
    }

    public void Save2()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Fichero.txt";

        string[] arrLine = File.ReadAllLines(path);

        arrLine[15] = 1234 + "";

        arrLine[16] = 5678 + "";

        File.WriteAllLines(path, arrLine);
    }
}
