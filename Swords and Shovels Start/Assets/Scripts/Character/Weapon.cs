using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        // �ִϸ��̼� -> Ÿ��
        if(defender == null) // ���ӿ�����Ʈ �����־ ���� ������ �� ����
        {
            return;
        }

        // �Ÿ�
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        // ����
        // ���� -> ���, �а� -> ����
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
