using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public bool target; //Ÿ�� ����

    public Color color; //��

    public float speed; //�ӵ�

    public float size; //ũ��

    public float waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�

    //���� �߰��� ��
}
