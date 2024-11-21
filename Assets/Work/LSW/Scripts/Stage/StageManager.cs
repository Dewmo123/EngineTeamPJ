using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; set; }
    public int curStageCnt = 0;
    public int unlockStageCnt = 0;
    private int _targetCnt = 0;
    private int _finCnt = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (!LoadStageData()) Debug.LogWarning("Cant Save Load Data");
        SetTargetEnemy();
    }

    private void AddDeadCount()
    {
        Debug.Log("addFin");
        _finCnt++;
        if (_targetCnt == _finCnt)
        {
            Debug.Log("Complete");
            CompleteStage();
        }
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
                unlockStageCnt = cnt;
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
            return false;
        }
    }



    public void CompleteStage()      //클리어하면 호출할 것!!
    {
        if (++curStageCnt > unlockStageCnt)
            unlockStageCnt = curStageCnt;
        if (curStageCnt == 11) return;
        SaveStageData(unlockStageCnt);
        EnterStage(curStageCnt);
    }

    public void EnterStage(int sta)
    {
        SceneManager.LoadScene(sta);
    }

    private void SetTargetEnemy()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("TargetEnemy");
        _targetCnt = targets.Length;
        targets.ToList().ForEach(item =>
        {
            item.GetComponent<Enemy>().onEnemyDead += AddDeadCount;
        });
    }
}
