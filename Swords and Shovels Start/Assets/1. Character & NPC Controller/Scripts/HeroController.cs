using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude); // �̵��� ����ϴ� agent, magnitude�� �ϸ� ũ�⸸ ��Į������ �ѱ�
    }
}
