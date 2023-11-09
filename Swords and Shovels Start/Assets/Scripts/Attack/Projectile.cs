using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public event Action<GameObject, GameObject> OnCollided;

    private Rigidbody rb;
    private Vector3 velocity;
    private float distance;
    private Vector3 startPos;
    private GameObject caster;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fire(GameObject c, Vector3 v, float d)
    {
        velocity = v;
        distance = d;
        caster = c;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        // 삭제
        var pos = rb.position;
        if(Vector3.Distance(startPos, pos) > distance)
        {
            Destroy(gameObject);
            return;
        }
        pos += velocity * Time.deltaTime;
        rb.MovePosition(pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnCollided != null) // 델리게이트 null 체크
        {
            OnCollided(caster, other.gameObject);
        }

        Destroy(gameObject);
    }

    //public float force;
    //private GameObject attacker;
    //private GameObject defender;
    //private Attack attack;

    //private void Awake()
    //{
    //    Destroy(gameObject, 10f);
    //}

    //public void ThrowProjectile()
    //{
    //    Rigidbody body = GetComponent<Rigidbody>();
    //    body.isKinematic = false;
    //    body.AddForce(transform.forward * force, ForceMode.Impulse);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        var attackables = defender.GetComponents<IAttackable>();
    //        foreach (var attackable in attackables)
    //        {
    //            attackable.OnAttack(attacker, attack);
    //        }
    //        Destroy(gameObject);
    //    }
    //}

    //public void Set(GameObject attacker, GameObject defender, Attack attack)
    //{
    //    this.attacker = attacker;
    //    this.defender = defender;
    //    this.attack = attack;
    //}
}