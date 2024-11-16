using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_UseGimic : MonoBehaviour,IPlayerComponent
{
    private Player _player;
    public LayerMask _gimicLayer;
    [SerializeField] private float _length;

    private void HandleInteraction()
    {
        Collider2D gimic = Physics2D.OverlapCircle(transform.position, _length, _gimicLayer);
        Debug.Log(gimic);
        if(gimic)
            gimic.GetComponent<MainGimicScript>().UseGimic();
    }
    public void Initialize(Player agent)
    {
        _player = agent;
        _player.GetCompo<InputReader>().GimicEvent += HandleInteraction;
    }
    private void OnDestroy()
    {
        _player.GetCompo<InputReader>().GimicEvent -= HandleInteraction;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _length);
    }

#endif
}
