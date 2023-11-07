using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestroyGameObject : MonoBehaviour, IDestructable
{
    public void OnDestruction(GameObject attacker)
    {
        Destroy(gameObject);
    }
}
