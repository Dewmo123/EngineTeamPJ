using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCCTV : MonoBehaviour, IGimic
{
    [SerializeField] private CCTV _target;
    public event Action useGimic;
    public Action useGimicEvent => useGimic;
    int cnt = 0;

    public void UseGimic()
    {
        cnt += 1;
        if (cnt == 1)
            _target.rotate = true;
        else if (cnt == 2)
        {
            _target.rotate = false;
            cnt = 0;
        }
    }

}
