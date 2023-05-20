using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get 
        {
            if (instance == null) 
            {
                instance = FindObjectOfType<T>(true);
                if (instance == null) 
                {
                    Debug.LogError($"Missing Singleton class of type: {typeof(T).Name}, creating instance");
                    GameObject newGameObject = new GameObject();
                    instance = newGameObject.AddComponent<T>();
                }
            }

            return instance;

        }
    }

    protected virtual void Awake() 
    {
        instance = this as T;
    }

}