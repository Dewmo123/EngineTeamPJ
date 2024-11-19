using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnManager : MonoBehaviour
{
    [SerializeField]private Stage[] stageInfo;

    private void Start()
    {
        for (int i = 0; i < stageInfo.Length; i++)
            stageInfo[i]._onEnter += StageManager.Instance.EnterStage;
    }
    public void SetButton()
    {
        int cnt = StageManager.Instance.curStageCnt;
        for (int i = 0; i < cnt; i++)
            stageInfo[i]._isUnlock.Value = true;
        for (int i = cnt; i < stageInfo.Length; i++)
            stageInfo[i]._isUnlock.Value = false;
    }

}
