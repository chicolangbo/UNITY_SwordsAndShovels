using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackedScrollingText : MonoBehaviour, IAttackable
{
    public ScrollingText prefab;
    public Color color = Color.white;
    public float addY = 2f;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var position = transform.position;
        position.y += addY;

        var text = Instantiate(prefab, position, Quaternion.identity);
        text.Set(attack.Damage.ToString(), color);
    }
}
