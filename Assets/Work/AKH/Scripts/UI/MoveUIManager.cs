using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct MoveUIElement
{
    public Vector2 targetPos;
    public float wait;
}
public class MoveUIManager : MonoBehaviour
{
    public List<MoveUIElement> MoveUIElements = new List<MoveUIElement>();
    private void Start()
    {

    }
}
