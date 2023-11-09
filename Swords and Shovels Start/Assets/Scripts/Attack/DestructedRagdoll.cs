using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedRagdoll : MonoBehaviour, IDestructable
{
    public GameObject ragdoll;
    public float power;
    public float liftY;

    public void OnDestruction(GameObject attacker)
    {
        var go = Instantiate(ragdoll, transform.position, transform.rotation);
        var body = go.GetComponent<Ragdoll>().rigid;

        var direction = transform.position - attacker.transform.position;
        direction.Normalize();
        direction.y = 0f;
        direction.y += liftY;

        body.AddForce(direction * power);
    }
}
