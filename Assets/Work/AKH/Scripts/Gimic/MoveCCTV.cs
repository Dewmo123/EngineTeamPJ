using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCCTV : MonoBehaviour
{
    [SerializeField] private CCTV target;
    private void OnTriggerStay2D(Collider2D collision)
    {
        target.rotate = Input.GetKey(KeyCode.F);
    }
}
