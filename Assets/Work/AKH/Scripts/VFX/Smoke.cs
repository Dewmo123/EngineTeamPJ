using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour, IPoolable
{
    private Pool _myPool;
    [SerializeField] private PoolTypeSO _myType;
    public PoolTypeSO PoolType => _myType;

    public GameObject GameObject => gameObject;
    public bool _endTriggerCalled;
    public void Play(Transform trm)
    {
        transform.position = trm.position;
    }
    public void ResetItem()
    {
    }
    private void Update()
    {
        if (_endTriggerCalled)
        {
            _endTriggerCalled = false;
            _myPool.Push(this);
        }
    }
    public void EndTriggerCalled()
    {
        _endTriggerCalled = true;
    }
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }
}
