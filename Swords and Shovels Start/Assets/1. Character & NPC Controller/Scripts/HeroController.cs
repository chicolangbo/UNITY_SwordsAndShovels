using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private GameObject attackTarget;
    public AttackDefinition attackDefinition;
    private Coroutine coMoveAndAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude); // 이동을 담당하는 agent, magnitude를 하면 크기만 스칼라값으로 넘김
    }

    public void AttackTarget(GameObject target)
    {
        // 범위 안에 들어올 때까지 이동
        if(coMoveAndAttack != null) // 코루틴 중첩 예외처리
        {
            StopCoroutine(coMoveAndAttack);
            coMoveAndAttack = null;
        }
        
        attackTarget = target;
        coMoveAndAttack = StartCoroutine(CoMoveAndAttack());
    }

    private IEnumerator CoMoveAndAttack()
    {
        agent.isStopped = false;
        var distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        while(distance > attackDefinition.range)
        {
            agent.destination = attackTarget.transform.position;
            yield return new WaitForSeconds(0.1f);
            distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        }
        agent.isStopped = true;
        animator.SetTrigger("Attack");
    }
    
    public void Hit()
    {
        var aStats = GetComponent<CharacterStats>();
        var dStats = attackTarget.GetComponent<CharacterStats>();
        var attack = attackDefinition.CreateAttack(aStats, dStats);

        var attackable = attackTarget.GetComponent<IAttackable>();
        attackable.OnAttack(gameObject, attack);
    }
}
