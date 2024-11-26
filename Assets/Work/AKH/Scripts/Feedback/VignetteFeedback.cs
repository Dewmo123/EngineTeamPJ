using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFeedback : Feedback
{
    [SerializeField] private Volume _volume;
    [SerializeField] private float _intensity;
    public override void PlayFeedback()
    {
        _volume.profile.TryGet(out Vignette vig);
        vig.intensity.value = _intensity;
    }

    public override void StopFeedback()
    {
    }

}
