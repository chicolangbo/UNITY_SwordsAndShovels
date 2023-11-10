using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attack/Aoe", fileName ="Aoe.asset")]
public class Aoe : AttackDefinition
{
    public float radius; // 광역공격 영역
    public GameObject effectPrefab;
    public float effectDuration;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if(attacker == null)
        {
            return;
        }

        // 이펙트 생성
        var pos = attacker.transform.position;
        var newGo = Instantiate(effectPrefab, pos, Quaternion.identity);
        Destroy(newGo, effectDuration);

        var mask = LayerMask.GetMask(LayerMask.LayerToName(attacker.layer));
        var layer = ~0 ^ mask;
        var cols = Physics.OverlapSphere(pos, radius);
        var aStats = attacker.GetComponent<CharacterStats>();

        foreach (var col in cols )
        {
            var dStats = col.GetComponent<CharacterStats>();
            if( dStats == null )
            {
                continue;
            }

            var attackables = attacker.GetComponents<IAttackable>();
            foreach( var attackable in attackables )
            {
                var attack = CreateAttack(aStats, dStats);
                attackable.OnAttack(attacker, attack);
            }
        }
    }
}
