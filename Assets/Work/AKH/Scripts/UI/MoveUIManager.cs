using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct MoveUIElement
{
    public MoveUI text;
    public Vector2 originPos;
    public Vector2 targetPos;
    public float wait;
}
public class MoveUIManager : MonoBehaviour
{
    public List<MoveUIElement> StartUIElements = new List<MoveUIElement>();
    private void Start()
    {
        MoveTargetPos();
    }
    public void MoveTargetPos()
    {
        StartUIElements.ToList().ForEach((item) => item.text.Move(item.targetPos, new WaitForSeconds(item.wait)));
    }
    public void MoveOriginPos()
    {
        StartUIElements.ToList().ForEach((item) => item.text.Move(item.originPos, new WaitForSeconds(item.wait)));
    }
}
