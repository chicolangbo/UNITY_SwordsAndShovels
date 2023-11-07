using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHp;
    public int damage;
    public float armor;
    public int Hp { get; set; }

    private void Awake()
    {
        Hp = maxHp;
    }

}