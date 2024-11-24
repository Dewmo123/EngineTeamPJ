using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diamond : MonoBehaviour
{
    public UnityEvent Complete;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Complete?.Invoke();
        Destroy(gameObject);
    }
}
