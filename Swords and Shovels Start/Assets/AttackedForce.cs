using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedForce : MonoBehaviour, IAttackable
{
    public float force = 0.3f;
    public float liftY = 0.01f;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var dir = gameObject.transform.position - attacker.transform.position;
        dir.Normalize();
        dir.y += liftY;
        rigidBody.AddForce(dir * force, ForceMode.Impulse);
    }
}
