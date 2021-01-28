using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = current ? current : this;
    }

    public event Action onShootTriggerEnter;
    public void ShootTriggerEnter()
    {
        if (onShootTriggerEnter != null)
        {
            onShootTriggerEnter();
        }
    }
}
