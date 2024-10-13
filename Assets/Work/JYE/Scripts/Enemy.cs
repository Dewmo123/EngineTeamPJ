using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyType;
    private EnemyState State;

    private SpriteRenderer enemyColor;
    public Animator animCompo { get; private set; }
    private EnemyStateMachine _stateMachine;


    public bool target; //Ÿ�� ����

    private float speed; //�ӵ�
    private float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
    public Transform movePoint1;
    public Transform movePoint2; // ���� / ��� ����

    public EnemyType type; //����

    private void Awake()
    {
        State = GetComponent<EnemyState>();
        enemyColor = GetComponent<SpriteRenderer>();
        animCompo = GetComponent<Animator>();
        Setting();
        _stateMachine = new EnemyStateMachine();
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

        type = enemyType.type; //����

        switch (type) //�ʱ� ���� ���ϱ� �� ��Ÿ
        {
            case EnemyType.Look:
                _stateMachine.ChangeState(EnemyStateType.Look);
                break;
            case EnemyType.Move:
                _stateMachine.ChangeState(EnemyStateType.Move);

                speed = enemyType.speed; //�ӵ�

                waitTime = enemyType.waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
                break;
            case EnemyType.Stop:
                _stateMachine.ChangeState(EnemyStateType.Stop);
                break;
            default:
                break;
        }
    }
}
