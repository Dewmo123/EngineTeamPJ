using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeCalledPoint : MonoBehaviour
{
    private bool Calling = false;
    public UnityEvent callEvent;

    private void Update()
    {
        if (Calling)
        {
            callEvent?.Invoke();
        }
    }
}
