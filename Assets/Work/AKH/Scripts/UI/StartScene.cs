using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public void StartGame()
    {
        using (StreamWriter sw = new StreamWriter(File.Open("asd.txt", FileMode.OpenOrCreate)))
        {
            sw.Write("asd");
        }
    }
    public void Continue()
    {
        string s;
        using (StreamReader sr = new StreamReader(File.Open("asd.txt", FileMode.Open)))
        {
            s = sr.ReadLine();
        }
    }
}
