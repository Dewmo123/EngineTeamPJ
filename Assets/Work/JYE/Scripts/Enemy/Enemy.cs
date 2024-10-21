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
    private bool boom; //���� ����
    private float radius = 1.5f; //���� ������
    [SerializeField] private LayerMask PlayerLayer; //�÷��̾� ���̾� (����)
    public bool playerDie = false; //�÷��̾� ��� (����) (true�� �� �÷��̾� ��� �ؾ���)

    public float speed; //�ӵ�
    public float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
    public Transform movePoint1; // ���� / ��� ����
    public Transform movePoint2; //1�� ������, 2�� ����

    public CCTV view;

    protected virtual void Awake()
    {
        enemySR = GetComponent<SpriteRenderer>();
        animCompo = GetComponent<Animator>();
        view = GetComponentInChildren<CCTV>();

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
        Boom();
    }

    private void DieEnemy()
    {
        _stateMachine.ChangeState(EnemyStateType.Die);
    }

    public virtual void EnemyDie()
    {
        Destroy(gameObject); //�ٲ� �� MoveEnemy�� EnemyDie�� ����
    }

    private IEnumerator Boom() //���� ��
    {
        if (boom) //������ �� �÷��̾ 1.5�� ������ ���� �����ȿ� �ִ���
        {
            animCompo.StopPlayback();
            yield return new WaitForSeconds(1.5f);
            Collider2D collider = Physics2D.OverlapCircle(gameObject.transform.position, radius,PlayerLayer);
            playerDie = collider != null;
            EnemyDie();
        }
        else
        {
            EnemyDie();
        }
    }
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }
    public void HandleSpriteFlip(Vector3 targetPosition)
    {
        bool isRight = IsFacingRight();
        if (targetPosition.x < transform.position.x && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180F, 0);
        }
        else if (targetPosition.x > transform.position.x && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
        }

    }
    private void OnDrawGizmos()
    {
        if (boom) //���� �����̶��
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, radius);
        }
    }

    private void Setting() //�� �⺻ �����ϱ�
    {
        target = enemyType.target; //Ÿ�� ����

        boom = enemyType.boom; //���� ����

        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //ũ��
        radius = radius * size; //���� ����

        enemySR.color = enemyType.color; //��

        Type();
    }

    protected virtual void Type() //Ÿ�� ��
    {

    }
}
