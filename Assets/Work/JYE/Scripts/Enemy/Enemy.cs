using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyType;

    protected SpriteRenderer enemyColor;
    public Animator animCompo { get; private set; }
    protected EnemyStateMachine _stateMachine;


    public bool target; //Ÿ�� ����

    protected float speed; //�ӵ�
    protected float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
    protected float nowTime; //���� ��ٸ� �ð� (0 �� �����ϸ� �ٽ� waiteTime)
    public Transform movePoint1;
    public Transform movePoint2; // ���� / ��� ����

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

    private void Setting() //�� �⺻ �����ϱ�
    {
        target = enemyType.target; //Ÿ�� ����

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //ũ��

        enemyColor.color = enemyType.color; //��

        Type();
    }

    protected virtual void Type() //Ÿ�� ��
    {

    }
}
