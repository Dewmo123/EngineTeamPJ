using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemyType;

    private SpriteRenderer enemyColor;

    private float speed; //�ӵ�

    private float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�

    private void Awake()
    {
        enemyColor = GetComponent<SpriteRenderer>();
        Setting();
    }

    private void Setting() //�� �⺻ �����ϱ�
    {
        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //ũ��

        enemyColor.color = enemyType.color; //��

        waitTime = enemyType.waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�


    }
}
