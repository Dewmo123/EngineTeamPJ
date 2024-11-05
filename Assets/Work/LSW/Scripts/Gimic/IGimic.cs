using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGimic
{
    public Action useGimicEvent { get; }
    public void UseGimic();
}
