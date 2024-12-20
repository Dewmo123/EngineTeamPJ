using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public bool target; //타겟 구별

    public bool boom; //자폭 적 구별

    public Color color; //색

    public float speed; //속도

    public float waitTime; //다시 움직이기 위해 기다릴 시간

    public EnemyStateType myType;

    //각도 추가할 것
}
