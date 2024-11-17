using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager Instance { get; set; }
    [SerializeField] private GameObject[] stageInfo;

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
        {
            stageInfo[i].GetComponent<Stage>()._onEnter += EnterStage;
        }
    }

    public void SaveStageData()
    {
        for (int i = 0; i < stageInfo.Length; i++)
        {
            PlayerPrefs.SetInt($"Stage_Unlock{i}", stageInfo[i].GetComponent<Stage>()._isUnlock.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadStageData()
    {
        for (int i = 0; i < stageInfo.Length; i++)
        {
            stageInfo[i].GetComponent<Stage>()._isUnlock.Value = PlayerPrefs.GetInt($"Stage_Unlock{i}") == 1;
        }
    }

    public void CompleteStage(int stageNumber)      //Ŭ�����ϸ� ȣ���� ��!!
    {
        PlayerPrefs.SetInt($"Stage_Unlock{stageNumber++}", 1);
        SaveStageData();
        LoadStageData();
    }

    private void EnterStage(int sta)
    {
        SceneManager.LoadScene(sta);
        for (int i = 0; i < stageInfo.Length; i++)
            stageInfo[i].GetComponent<Stage>().ClearAllListener();
    }
}
