using System;
using UnityEngine;

[Serializable]
public class BaseStat
{
    [SerializeField] public int baseStatValue;
    [SerializeField] public AnimationCurve baseStatModifier;
}
