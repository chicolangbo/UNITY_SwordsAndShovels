using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        CheckAttackPossible(attacker, defender);

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(aStats, dStats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public void ExecuteLongDistanceAttack(GameObject attacker, GameObject defender, Transform weaponDummy)
    {
        //CheckAttackPossible(attacker, defender);

        //var aStats = attacker.GetComponent<CharacterStats>();
        //var dStats = defender.GetComponent<CharacterStats>();
        //var attack = CreateAttack(aStats, dStats);

        //ThrowRock(attacker, defender, weaponDummy, attack);
    }

    public void CheckAttackPossible(GameObject attacker, GameObject defender)
    {
        // �ִϸ��̼� -> Ÿ��
        if (defender == null) // ���ӿ�����Ʈ �����־ ���� ������ �� ����
        {
            return;
        }

        // �Ÿ�
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
        {
            return;
        }

        // ����
        // ���� -> ���, �а� -> ����
        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();
        var dot = Vector3.Dot(dir, attacker.transform.forward);
        if (dot < 0.5f)
        {
            return;
        }
    }

    public void ThrowRock(GameObject attacker, GameObject defender, Transform weaponDummy, Attack attack)
    {
        //var projectile = Instantiate(prefab, weaponDummy.position, Quaternion.identity);
        //projectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //var look = defender.transform.position;
        //look.y = weaponDummy.position.y + 2f;
        //projectile.transform.LookAt(look);
        //projectile.GetComponent<Projectile>().ThrowProjectile();
        //projectile.GetComponent<Projectile>().Set(attacker, defender, attack);
    }
}
