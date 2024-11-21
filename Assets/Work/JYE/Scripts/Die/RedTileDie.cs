using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTileDie : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<GamePlayer>().SetDeadState();
        }
    }
}
