using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestructedEvents : MonoBehaviour, IDestructable
{
    public event Action OnEvent;

    public void OnDestruction(GameObject attacker)
    {
        if(OnEvent != null)
        {
            OnEvent();
        }
    }
}
