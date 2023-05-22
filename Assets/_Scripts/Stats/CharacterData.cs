using UnityEngine;

[CreateAssetMenu (fileName = "CharacterData", menuName = "Character Data", order = 2)]
public class CharacterData : ScriptableObject
{
    public BaseStat cookSpeed;
    public BaseStat tipping;
    public BaseStat costOfUpgrades;
    public BaseStat wowDiners;
}
