using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private Dictionary<string, MoveUIManager> _managers = new Dictionary<string, MoveUIManager>();
    private void Awake()
    {
        GetComponentsInChildren<MoveUIManager>().ToList().ForEach(item => _managers.Add(item.gameObject.name, item));
        _managers["StartSceneUI"].MoveTargetPos();
    }
    public void StartGame()
    {
        _managers["ChooseSceneUI"].MoveTargetPos(true);
        _managers["StartSceneUI"].MoveOriginPos(true);
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
