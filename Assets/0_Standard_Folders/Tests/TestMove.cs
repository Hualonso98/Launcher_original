using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEditor;

public class TestMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        TestClass test = new TestClass(true);

        BinaryFormatter binary = new BinaryFormatter();
        Directory.CreateDirectory(Application.dataPath + "/zzzzz");
        FileStream file = File.Create(Application.dataPath + "/zzzzz/test.txt");

        binary.Serialize(file, test);

        file.Close();
    }
    public void SaveJson()
    {
        TestClass test = new TestClass(true);

    }

    public void Load()
    {
        TestClass test = new TestClass(true);

        BinaryFormatter binary = new BinaryFormatter();

        FileStream file = File.Open(Application.dataPath + "/zzzzz/test.txt", FileMode.Open);

        test = (TestClass)binary.Deserialize(file);


    }
}

[Serializable]
public class TestClass
{
    public bool attribute_1;
    public bool attribute_2;

    public TestClass(bool attribute_1)
    {
        this.attribute_1 = attribute_1;
    }
}
