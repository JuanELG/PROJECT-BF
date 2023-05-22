using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int _level = 1;
    private int _experience = 0;
    private int _requiredExperience = 0;
    
    [SerializeField] private LevelConfig _levelConfig;

    private void Start()
    {
        CalculateRequieredExperience();
    }

    private void CalculateRequieredExperience()
    {
        _requiredExperience = _levelConfig.GetRequiredExperience(_level);
    }
    
    private void IncreaseExperience(int value)
    {
        if(_level >= _levelConfig.maxLevel) return;
        _experience += value;
        if (_experience < _requiredExperience) return;
        while (_experience >= _requiredExperience)
        {
            _experience -= _requiredExperience;
            LevelUp();
        }
    }
    
    private void LevelUp()
    {
        _level++;
        CalculateRequieredExperience();
    }
}