using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public bool target; //Ÿ�� ����

    public EnemyType type; //�� ����

    public Color color; //��

    public float speed; //�ӵ�

    public float size; //ũ��

    public float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�

    public Transform movePoint1;
    public Transform movePoint2; // ���� / ��� ����

    //���� �߰��� ��
}

public enum EnemyType
{
    Stop/*������ �ִ� ��*/,Move/*�յ� ���ƴٴϴ� ��*/,Look/*�÷��̾� �ٶ󺸴� ��*/
}
