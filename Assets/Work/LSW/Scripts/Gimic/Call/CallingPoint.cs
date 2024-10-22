using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingPoint : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;
    [SerializeField] protected GameObject _beCalledPoint;

    public GameObject[] _enemies;
    [SerializeField] private LayerMask _enemyLayer;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _callSound;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Call += GetComponentInChildren<BeCalledPoint>().Call;
    }

    private void Awake()
    {
        _audioSource = _beCalledPoint.GetComponent<AudioSource>();
    }

    private void Update()
    {
        for(int i = 0; i < _enemies.Length; i++)
        {
            //if(_enemies[i].GetComponentsInChildren<>)       //±¸Çö ¾ÈµÊ
            //{
                //_enemies.RemoveAll(enemy => );      //±¸Çö ¾ÈµÊ
            //}
        }
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Call -= GetComponentInChildren<BeCalledPoint>().Call;
    }
}
