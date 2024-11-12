using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartPlayer : Player
{
    [SerializeField] private Vector2 direction;
    private void Start()
    {
        movementCompo.ShootRope(direction);
    }
    private void Update()
    {
        if (movementCompo.isRope)
        {
            GetCompo<GrappleGun>().Roping();
        }
    }
}
