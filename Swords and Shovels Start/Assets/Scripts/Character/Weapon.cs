using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        // 애니메이션 -> 타격
        if(defender == null) // 게임오브젝트 남아있어도 죽은 상태일 수 있음
        {
            return;
        }

        // 거리
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        // 방향
        // 예각 -> 양수, 둔각 -> 음수
        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();
        var dot = Vector3.Dot(dir, attacker.transform.forward);
        if(dot < 0.5f)
        {
            return;
        }

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(aStats, dStats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
