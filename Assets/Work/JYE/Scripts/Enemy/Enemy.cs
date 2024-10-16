using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemyType;

    public SpriteRenderer enemySR;
    public Animator animCompo { get; private set; }
    protected EnemyStateMachine _stateMachine;


    public bool target; //Ÿ�� ����

    public float speed; //�ӵ�
    public float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
    public Transform movePoint1; // ���� / ��� ����
    public Transform movePoint2; //1�� ������, 2�� ����

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
        if (Input.GetKeyDown(KeyCode.P)) //�״� ���� �˾Ƽ� �ٲٱ�
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
        Destroy(gameObject); //�ٲ� �� MoveEnemy�� EnemyDie�� ����
    }

    private void Setting() //�� �⺻ �����ϱ�
    {
        target = enemyType.target; //Ÿ�� ����

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //ũ��

        enemySR.color = enemyType.color; //��

        Type();
    }

    protected virtual void Type() //Ÿ�� ��
    {

    }
}
