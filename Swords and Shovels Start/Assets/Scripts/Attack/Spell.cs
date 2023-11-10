using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attack/Spell", fileName = "Spell.asset")]
public class Spell : AttackDefinition
{
    public Projectile projectilePrefab;
    public float speed;
    public float distance;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        var pos = attacker.transform.position;
        int layer = attacker.layer;
        if(LayerMask.NameToLayer("Enemy") == layer)
        {
            var npcCtrl = attacker.GetComponent<NPCController2>();
            if (npcCtrl != null)
            {
                pos = npcCtrl.projectileDummy.position;
            }
        }

        var targetPos = defender.transform.position;
        targetPos.y += 1f;

        var direction = (targetPos - pos).normalized;
        var velocity = direction * speed;
        var projectile = Instantiate(projectilePrefab, pos, Quaternion.LookRotation(direction));
        projectile.gameObject.layer = layer;
        projectile.OnCollided += OnProjectileCollided;
        projectile.Fire(attacker, velocity, distance);
    }

    public void OnProjectileCollided(GameObject attacker, GameObject defender)
    {
        if(attacker == null || defender == null)
        {
            return;
        }

        var astats = attacker.GetComponent<CharacterStats>();
        var dstats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(astats, dstats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach( var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
