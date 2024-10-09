using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemyType;

    private SpriteRenderer enemyColor;

    private float speed; //속도

    private float waitTime; //다시 움직이기 위해 기다릴 시간

    private void Awake()
    {
        enemyColor = GetComponent<SpriteRenderer>();
        Setting();
    }

    private void Setting() //적 기본 세팅하기
    {
        float size = enemyType.size;
        gameObject.transform.localScale = Vector3.one * size; //크기

        enemyColor.color = enemyType.color; //색

        waitTime = enemyType.waitTime; //다시 움직이기 위해 기다릴 시간


    }
}
