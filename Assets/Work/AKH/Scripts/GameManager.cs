using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion
    [field: SerializeField] public PoolManagerSO poolManager { get; private set; }
    public Dictionary<string, PoolingItemSO> poolItemDic { get; private set; }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        poolItemDic = new Dictionary<string, PoolingItemSO>();
    }
    private void Start()
    {
        InitPoolItemDic();
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void InitPoolItemDic()
    {
        poolManager.poolingItemList.ForEach((item) => {
            poolItemDic.Add(item.prefab.name, item);
            Debug.Log(item.prefab.name);
        });
    }
}
