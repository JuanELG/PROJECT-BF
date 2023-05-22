using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    [Header("Animation curve for experience")]
    public AnimationCurve experienceCurve;

    public int maxLevel;
    public int maxRequiredExperience;
    
    public int GetRequiredExperience(int level)
    {
        int requiredExperience = Mathf.RoundToInt(experienceCurve.Evaluate(Mathf.InverseLerp(0, maxLevel, level)) * maxRequiredExperience);
        return requiredExperience;
    }
}