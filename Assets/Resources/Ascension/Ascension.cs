using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Ascension
{
    public AscensionInfoStat ascensionInfoStat { get; private set; }

    public Ascension(AscensionInfoStat AscensionInfoStat)
    {
        ascensionInfoStat = AscensionInfoStat;
    }

    public float GetStats(AnimationCurve AnimationCurve, int level)
    {
        Keyframe endKeyFrame = AnimationCurve[AnimationCurve.length - 1];
        float endValue = endKeyFrame.value;
        float firstValue = AnimationCurve[0].value;

        return ((endValue - firstValue) / endKeyFrame.time) * level + firstValue;
    }

}
