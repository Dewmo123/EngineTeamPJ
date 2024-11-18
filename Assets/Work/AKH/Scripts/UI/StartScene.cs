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
        _managers["ErrorUI"].MoveOriginPos(true);
        _managers["ChooseSceneUI"].MoveTargetPos(true);
        _managers["StartSceneUI"].MoveOriginPos(true);
        StageManager.Instance.SaveStageData(1);
        StageManager.Instance.LoadStageData();
    }
    public void Continue()
    {
        if (!StageManager.Instance.LoadStageData())
            _managers["ErrorUI"].MoveTargetPos();
        else
        {
            _managers["ChooseSceneUI"].MoveTargetPos(true);
            _managers["StartSceneUI"].MoveOriginPos(true);
        }
    }
}
