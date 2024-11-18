using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; set; }
    [SerializeField] private Stage[] stageInfo;
    public int _curStageCnt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < stageInfo.Length; i++)
            stageInfo[i]._onEnter += EnterStage;
    }

    public void SaveStageData(int stageCnt)
    {
        using (StreamWriter sw = new StreamWriter(File.Open("asd.txt", FileMode.OpenOrCreate)))
        {
            sw.Write(stageCnt);
        }
    }
    public bool LoadStageData()
    {
        try
        {
            using (StreamReader sr = new StreamReader(File.Open("asd.txt", FileMode.Open)))
            {
                int cnt = int.Parse(sr.ReadLine());
                for (int i = 0; i < cnt; i++)
                    stageInfo[i]._isUnlock.Value = true;
                for (int i = cnt; i < stageInfo.Length; i++)
                    stageInfo[i]._isUnlock.Value = false;
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    public void CompleteStage()      //클리어하면 호출할 것!!
    {
        if (_curStageCnt == 9) return;
        SaveStageData(++_curStageCnt);
    }

    private void EnterStage(int sta)
    {
        SceneManager.LoadScene(sta);
        for (int i = 0; i < stageInfo.Length; i++)
            stageInfo[i].ClearAllListener();
    }
}
