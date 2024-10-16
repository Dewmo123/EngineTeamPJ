using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemyType;

    public SpriteRenderer enemySR;
    public Animator animCompo { get; private set; }
    protected EnemyStateMachine _stateMachine;


    public bool target; //타겟 구별

    public float speed; //속도
    public float waitTime; //다시 움직이기 위해 기다릴 시간
    public Transform movePoint1; // 도착 / 출발 지점
    public Transform movePoint2; //1이 오른쪽, 2가 왼쪽

    protected virtual void Awake()
    {
        enemySR = GetComponent<SpriteRenderer>();
        animCompo = GetComponent<Animator>();

        _stateMachine = new EnemyStateMachine();
        _stateMachine.AddState(EnemyStateType.Stop, new StopIdleEnemyState(_stateMachine,"Idle",this));
        _stateMachine.AddState(EnemyStateType.Look, new LookIdleEnemyState(_stateMachine,"Idle",this));
        _stateMachine.AddState(EnemyStateType.Move, new WalkEnemyState(_stateMachine,"Move",this));
        _stateMachine.AddState(EnemyStateType.MoveIdle, new WalkIdleEnemyState(_stateMachine,"Idle",this));
        _stateMachine.AddState(EnemyStateType.Die, new DieEnemyState(_stateMachine,"Die",this));
        _stateMachine.Init(EnemyStateType.Stop, this);
        Setting();
    }

    protected virtual void Update()
    {
        _stateMachine.currentState.UpdateState();
        if (Input.GetKeyDown(KeyCode.P)) //죽는 조건 알아서 바꾸기
        {
            DieEnemy();
        }
    }

    private void DieEnemy()
    {
        _stateMachine.ChangeState(EnemyStateType.Die);
    }

    public virtual void EnemyDie()
    {
        Destroy(gameObject); //바꿀 때 MoveEnemy의 EnemyDie도 같이
    }

    private void Setting() //적 기본 세팅하기
    {
        target = enemyType.target; //타겟 구별

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //크기

        enemySR.color = enemyType.color; //색

        Type();
    }

    protected virtual void Type() //타입 별
    {

    }
}
