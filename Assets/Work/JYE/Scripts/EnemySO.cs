using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public bool target; //타겟 구별

    public EnemyType type; //적 종류

    public Color color; //색

    public float speed; //속도

    public float size; //크기

    public float waitTime; //다시 움직이기 위해 기다릴 시간

    public Transform movePoint1;
    public Transform movePoint2; // 도착 / 출발 지점

    //각도 추가할 것
}

public enum EnemyType
{
    Stop/*가만히 있는 적*/,Move/*앞뒤 돌아다니는 적*/,Look/*플레이어 바라보는 적*/
}
