using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct MoveUIElement
{
    public MoveUI text;
    public Vector2 targetPos;
    public float wait;
}
public class MoveUIManager : MonoBehaviour
{
    public List<MoveUIElement> MoveUIElements = new List<MoveUIElement>();
    private void Start()
    {
        MoveUIElements.ToList().ForEach((item) => item.text.Move(item.targetPos, new WaitForSeconds(item.wait)));
    }
}
