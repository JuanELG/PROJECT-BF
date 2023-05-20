using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Kitchen Object Data", order = 0)]
public class KitchenObjectData : ScriptableObject
{
    public List<GameObject> levelsVisualPrefabList;
}
