using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDebug : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if(attack.IsCritical)
        {
            Debug.Log("CRITICAL");
        }
        Debug.Log($"{attacker.name} attacked {gameObject.name} for {attack.Damage} damage");
    }
}
