using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeCalledPoint : MonoBehaviour
{
    public float gatherSpeed;
    [SerializeField] private GameObject _callingPoint;
    private CallingPoint _callingPointScr;
    private bool notCall = true;

    private void Awake()
    {
        _callingPointScr = GetComponentInChildren<CallingPoint>();
    }

    public void Call()
    {
        //base._audioSource.Play(_callSound);
        notCall = false;
        if(!notCall)
            StartCoroutine(enemyGather());
    }

    private IEnumerator enemyGather()
    {
        foreach(GameObject enemy in _callingPointScr._enemies)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, gatherSpeed);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject enemy in _callingPointScr._enemies)
        {
            if(collision.gameObject == enemy)
            {
                notCall = false;
            }
        }
        //base._audioSource.Pause(_callSound);
    }
}
