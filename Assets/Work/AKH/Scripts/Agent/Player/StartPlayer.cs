using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartPlayer : Player
{
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _removeTime;
    private void Start()
    {
        movementCompo.ShootRope(_direction);
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(_removeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (movementCompo.isRope)
            GetCompo<GrappleGun>().Roping();
    }
    public void AddForce()
    {
        rbCompo.AddForce(rbCompo.velocity*2, ForceMode2D.Impulse);
    }
}
