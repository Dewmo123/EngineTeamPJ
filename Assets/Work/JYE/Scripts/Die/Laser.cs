using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
    }
}
