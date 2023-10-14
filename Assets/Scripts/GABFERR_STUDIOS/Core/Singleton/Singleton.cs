using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GABFERR.Core.Singleton { 
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = GetComponent<T>();

        else
            Destroy(gameObject);
    }
}
}