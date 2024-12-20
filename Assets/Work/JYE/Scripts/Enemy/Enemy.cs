using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemyType;

    public SpriteRenderer enemySR;
    public Animator animCompo { get; private set; }
    protected EnemyStateMachine _stateMachine;


    public bool target; //타겟 구별
    private bool boom; //자폭 구별
    private float radius = 1.5f; //자폭 반지름
    [SerializeField] private LayerMask PlayerLayer; //플레이어 레이어 (자폭)
    public float moveDuraion;
    public bool playerDie = false; //플레이어 사망 (자폭) (true일 때 플레이어 사망 해야함)

    public float speed; //속도
    public float waitTime; //다시 움직이기 위해 기다릴 시간
    public Transform movePoint1; // 도착 / 출발 지점
    public Transform movePoint2; //1이 오른쪽, 2가 왼쪽
    public Vector3 originPos;

    public event Action onEnemyDead;

    public CCTV view;

    protected virtual void Awake()
    {
        try
        {
            enemySR = GetComponent<SpriteRenderer>();
            animCompo = GetComponent<Animator>();
            view = GetComponentInChildren<CCTV>();
        }
        catch
        {
            Debug.LogWarning("Component does not exist");
        }

        _stateMachine = new EnemyStateMachine();
        _stateMachine.AddState(EnemyStateType.Stop, new StopIdleEnemyState(_stateMachine, "Idle", this));
        _stateMachine.AddState(EnemyStateType.Look, new LookIdleEnemyState(_stateMachine, "Idle", this));
        _stateMachine.AddState(EnemyStateType.Move, new WalkEnemyState(_stateMachine, "Move", this));
        _stateMachine.AddState(EnemyStateType.MoveIdle, new WalkIdleEnemyState(_stateMachine, "Idle", this));
        _stateMachine.AddState(EnemyStateType.Die, new DieEnemyState(_stateMachine, "Die", this));
        _stateMachine.AddState(EnemyStateType.Hit, new HitEnemyState(_stateMachine, "Hit", this));
        _stateMachine.AddState(EnemyStateType.GoToPoint, new GoPointEnemyState(_stateMachine, "Move", this));
        _stateMachine.Init(EnemyStateType.Stop, this);
        Setting();
    }
    protected virtual void Update()
    {
        _stateMachine.currentState.UpdateState();
    }
    public void SetDeadState()
    {
        _stateMachine.ChangeState(EnemyStateType.Die);
    }
    public void SetDead()
    {
        onEnemyDead?.Invoke();
        Destroy(gameObject);
    }
    public void EndTriggerCalled()
    {
        _stateMachine.currentState.AnimationEndTrigger();
    }
    public void WalkSound()
    {
        var poolManager = GameManager.Instance.poolManager;
        var sp = poolManager.Pop(GameManager.Instance.poolItemDic["SoundPlayer"].poolType) as SoundPlayer;
        sp.PlaySound(GameManager.Instance._soundDic["Walk"]);
    }
    public void Hit()
    {
        _stateMachine.ChangeState(EnemyStateType.Hit);
    }
    public virtual void EnemyDie()
    {
        Destroy(gameObject); //바꿀 때 MoveEnemy의 EnemyDie도 같이
    }
    public void GoToPoint(Transform point)
    {
        originPos = transform.position;
        movePoint2 = point;
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        _stateMachine.ChangeState(EnemyStateType.GoToPoint);
        yield return new WaitForSeconds(moveDuraion * 1.5f);
        _stateMachine.ChangeState(EnemyStateType.GoToPoint);
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
        if (boom) //자폭 가능이라면
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, radius);
        }
    }

    private void Setting() //적 기본 세팅하기
    {
        target = enemyType.target; //타겟 구별

        boom = enemyType.boom; //자폭 구별

        enemySR.color = enemyType.color; //색

        Type();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GamePlayer player;
        if(collision.gameObject.TryGetComponent(out player))
        {
            player.SetDeadState();
        }
    }
    protected virtual void Type() //타입 별
    {

    }
}
