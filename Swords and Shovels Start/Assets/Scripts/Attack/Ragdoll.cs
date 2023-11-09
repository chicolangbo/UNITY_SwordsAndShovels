using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public float duration = 3f;
    public Rigidbody rigid;

    private void Awake()
    {
        Destroy(gameObject, duration);
    }

    public void AddForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Impulse);
    }
}
