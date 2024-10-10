using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemyType;
    private EnemyState State;

    private SpriteRenderer enemyColor;

    public bool target; //타겟 구별

    private float speed; //속도
    private float waitTime; //다시 움직이기 위해 기다릴 시간
    public Transform movePoint1;
    public Transform movePoint2; // 도착 / 출발 지점

    public EnemyType type; //종류

    private void Awake()
    {
        State = GetComponent<EnemyState>();
        enemyColor = GetComponent<SpriteRenderer>();
        Setting();
    }

    private void Update()
    {
        State.StateUpdate();
    }

    private void Setting() //적 기본 세팅하기
    {
        target = enemyType.target; //타겟 구별

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //크기

        enemyColor.color = enemyType.color; //색

        type = enemyType.type; //종류

        switch (type) //초기 상태 정하기 및 기타
        {
            case EnemyType.Look:
                State.stateType = EnemyStateType.Idle;
                break;
            case EnemyType.Move:
                State.stateType = EnemyStateType.Move;

                speed = enemyType.speed; //속도

                waitTime = enemyType.waitTime; //다시 움직이기 위해 기다릴 시간
                break;
            case EnemyType.Stop:
                State.stateType = EnemyStateType.Idle;
                break;
            default:
                break;
        }
    }
}
