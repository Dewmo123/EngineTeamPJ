using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyType;

    protected SpriteRenderer enemyColor;
    public Animator animCompo { get; private set; }
    protected EnemyStateMachine _stateMachine;


    public bool target; //타겟 구별

    protected float speed; //속도
    protected float waitTime; //다시 움직이기 위해 기다릴 시간
    protected float nowTime; //현재 기다린 시간 (0 에 도달하면 다시 waiteTime)
    public Transform movePoint1;
    public Transform movePoint2; // 도착 / 출발 지점

    private void Awake()
    {
        enemyColor = GetComponent<SpriteRenderer>();
        animCompo = GetComponent<Animator>();

        _stateMachine = new EnemyStateMachine();
        _stateMachine.AddState(EnemyStateType.Stop, new StopIdleEnemyState(_stateMachine,"Idle",this));
        _stateMachine.AddState(EnemyStateType.Look, new LookIdleEnemyState(_stateMachine,"Idle",this));
        _stateMachine.AddState(EnemyStateType.Move, new WalkEnemyState(_stateMachine,"Move",this));
        _stateMachine.AddState(EnemyStateType.Die, new DieEnemyState(_stateMachine,"Die",this));
        Setting();
    }

    private void Update()
    {
        _stateMachine.currentState.UpdateState();
    }

    private void Setting() //적 기본 세팅하기
    {
        target = enemyType.target; //타겟 구별

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //크기

        enemyColor.color = enemyType.color; //색

        Type();
    }

    protected virtual void Type() //타입 별
    {

    }
}
