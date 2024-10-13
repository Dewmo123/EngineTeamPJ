using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemyType;
    private EnemyState State;

    private SpriteRenderer enemyColor;

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
        Setting();
    }

    private void Update()
    {
        State.StateUpdate();
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
                State.stateType = EnemyStateType.Idle;
                break;
            case EnemyType.Move:
                State.stateType = EnemyStateType.Move;

                speed = enemyType.speed; //�ӵ�

                waitTime = enemyType.waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�
                break;
            case EnemyType.Stop:
                State.stateType = EnemyStateType.Idle;
                break;
            default:
                break;
        }
    }
}
