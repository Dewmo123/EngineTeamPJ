using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonGimmick : MonoBehaviour
{
    [SerializeField] private GameObject _doorObj;
    [SerializeField] private Collider2D _targetObj;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _distance;
    private NotifyValue<Collider2D> _curObj = new NotifyValue<Collider2D>();

    private Animator buttonAnimator;
    private void Awake()
    {
        buttonAnimator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _curObj.OnValueChanged += HandleColliderChanged;
    }

    private void HandleColliderChanged(Collider2D prev, Collider2D next)
    {
        if (next == null)
        {
            buttonAnimator.SetBool("Click", false);
            _doorObj.SetActive(true);
        }
        else
        {
            buttonAnimator.SetBool("Click", true);
            _doorObj.SetActive(false);
        }
    }

    private void Update()
    {
        _curObj.Value = Physics2D.OverlapCircle(transform.position, _distance, _targetLayer);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _distance);
    }
#endif
}
