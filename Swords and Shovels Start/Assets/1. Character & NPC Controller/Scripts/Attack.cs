using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한번 공격했을 때
public struct Attack
{
    public int Damage { get; private set; }
    public bool IsCritical { get; private set; }
    
    public Attack(int damage, bool critical)
    {
        Damage = damage;
        IsCritical = critical;
    }
}
