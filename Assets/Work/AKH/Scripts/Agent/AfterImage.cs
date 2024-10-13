using DG.Tweening;
using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour, IPoolable
{
    private SpriteRenderer _spriteRenderer;
    private Pool _myPool;
    [SerializeField] private PoolTypeSO _myType;
    public PoolTypeSO PoolType => _myType;

    public GameObject GameObject => gameObject;

    public void ResetItem()
    {
        _spriteRenderer.color = new Color(1, 1, 1);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetAfterImage(Sprite spirte, bool isFlip, Vector3 position, float fadeTime)
    {
        transform.position = position;
        _spriteRenderer.sprite = spirte;
        _spriteRenderer.flipX = isFlip;
        _spriteRenderer.DOFade(0f, fadeTime).OnComplete(() =>
        {
            _myPool.Push(this);
        });
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }
}
