using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackedForce : MonoBehaviour, IAttackable
{
    public float force = 0.3f;
    public float liftY = 0.01f;
    private Rigidbody rigidBody;
    private NavMeshAgent navMeshAgent;
    private bool isGrounded;
    private Coroutine switchToNavMeshAgent;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        rigidBody.isKinematic = false;
        navMeshAgent.enabled = false;
        var dir = gameObject.transform.position - attacker.transform.position;
        dir.Normalize();
        dir.y += liftY;
        rigidBody.AddForce(dir * force, ForceMode.Impulse);

        if (switchToNavMeshAgent != null) // �ڷ�ƾ ��ø ����ó��
        {
            StopCoroutine(switchToNavMeshAgent);
            switchToNavMeshAgent = null;
        }

        switchToNavMeshAgent = StartCoroutine(SwitchToNavMeshAgent());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HardFloor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "HardFloor")
        {
            isGrounded = false;
        }
    }

    public IEnumerator SwitchToNavMeshAgent()
    {
        // Rigidbody�� ���߿� �� ���� ������ ���
        yield return new WaitUntil(() => rigidBody.velocity.y > 0);

        // Rigidbody�� �ٴڿ� ���� ������ ���
        while(!isGrounded)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("�ٴڿ� ���� ������ �����");
        }

        rigidBody.isKinematic = true;
        navMeshAgent.enabled = true;
    }
}
