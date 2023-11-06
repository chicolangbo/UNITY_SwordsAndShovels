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
        animator.SetFloat("Speed", agent.velocity.magnitude); // 이동을 담당하는 agent, magnitude를 하면 크기만 스칼라값으로 넘김
    }
}
