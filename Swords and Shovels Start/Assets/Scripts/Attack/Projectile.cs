using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force;
    private GameObject attacker;
    private GameObject defender;
    private Attack attack;

    private void Awake()
    {
        Destroy(gameObject, 10f);
    }

    public void ThrowProjectile()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var attackables = defender.GetComponents<IAttackable>();
            foreach (var attackable in attackables)
            {
                attackable.OnAttack(attacker, attack);
            }
            Destroy(gameObject);
        }
    }

    public void Set(GameObject attacker, GameObject defender, Attack attack)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.attack = attack;
    }
}
