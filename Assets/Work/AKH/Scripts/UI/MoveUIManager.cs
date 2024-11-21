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
    public virtual void MoveTargetPos(bool noTime = false)
    {
        StartUIElements.ToList().ForEach((item) => item.text.Move(item.targetPos, noTime ? null : new WaitForSeconds(item.wait)));
    }
    public virtual void MoveOriginPos(bool noTime = false)
    {
        StartUIElements.ToList().ForEach((item) => item.text.Move(item.originPos, noTime ? null : new WaitForSeconds(item.wait)));
    }
}
