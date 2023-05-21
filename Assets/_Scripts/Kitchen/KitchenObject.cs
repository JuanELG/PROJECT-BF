using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectData _kitchenObjectData;

    public void Interact()
    {
        Debug.Log($"Interacting with kitchen object: {_kitchenObjectData.kitchenObjectName}");
    }
}
