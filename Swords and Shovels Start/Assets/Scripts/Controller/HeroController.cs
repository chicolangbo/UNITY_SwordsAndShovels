using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HeroController : MonoBehaviour
{
    public AttackDefinition skillAttack;

    public float stompRange;
    private List<Collider> stompEnemies = new List<Collider>();
    private Inventory inventory = new Inventory();

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private GameObject attackTarget;
    private Coroutine coMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude); // 이동을 담당하는 agent, magnitude를 하면 크기만 스칼라값으로 넘김
    }

    public void SetDestination(Vector3 destination)
    {
        // 코루틴 돌고 있으면 정지
        if (coMove != null) // 코루틴 중첩 예외처리
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        // 타겟 클리어
        attackTarget = null;
        agent.isStopped = false;
        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        // 범위 안에 들어올 때까지 이동
        if(coMove != null) // 코루틴 중첩 예외처리
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        
        attackTarget = target;
        coMove = StartCoroutine(CoMoveAndAttack());
    }

    public void DoSkill(Vector3 destination)
    {
        if (coMove != null) // 코루틴 중첩 예외처리
        {
            StopCoroutine(coMove);
            coMove = null;
        }

        attackTarget = null;
        coMove = StartCoroutine(CoMoveAndSkill(destination));
    }

    //public void AttackArea()
    //{
    //    stompEnemies.Clear();
    //    animator.SetTrigger("StompAttack");
    //    // temp에 넣어놓고
    //    var enemies = Physics.OverlapSphere(transform.position, stompRange);
    //    // 콜라이더 태그가 enemy면 stompEnemies에 넣기
    //    foreach(var enemy in enemies)
    //    {
    //        if(enemy.gameObject.tag == "Enemy")
    //        {
    //            stompEnemies.Add(enemy);
    //        }
    //    }
    //}

    private IEnumerator CoMoveAndAttack()
    {
        var range = 2f;
        if(inventory != null && inventory.CurrentWeapon != null)
        {
            range = inventory.CurrentWeapon.range;
        }

        agent.isStopped = false;
        var distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        while(distance > range)
        {
            agent.destination = attackTarget.transform.position;
            yield return new WaitForSeconds(0.1f);
            distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        }
        agent.isStopped = true;

        if (inventory != null && inventory.CurrentWeapon != null)
        {
            var lookPos = attackTarget.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            animator.SetTrigger("Attack");
        }
    }

    private IEnumerator CoMoveAndSkill(Vector3 destination)
    {
        // 이동 후 스톰프 어택
        agent.isStopped = false;
        agent.destination = destination;
        var distance = Vector3.Distance(transform.position, destination);
        while (Vector3.Distance(transform.position, destination) > agent.stoppingDistance)
        {
            yield return null;
        }
        agent.isStopped = true;
        animator.SetTrigger("StompAttack");
    }

    public void Hit()
    {
        if (inventory == null || inventory.CurrentWeapon == null)
        {
            return;
        }

        if(attackTarget != null)
        {
            inventory.CurrentWeapon.ExecuteAttack(gameObject, attackTarget);
        }
    }

    public void HitStomp()
    {
        if(skillAttack != null)
        {
            skillAttack.ExecuteAttack(gameObject, null);
        }

        //// attack 생성
        //foreach(var enemy in stompEnemies)
        //{
        //    var aStats = GetComponent<CharacterStats>();
        //    var dStats = enemy.GetComponent<CharacterStats>();
        //    if(dStats != null)
        //    {
        //        var attackDefinition = new AttackDefinition();
        //        var attack = attackDefinition.CreateAttack(aStats, dStats);
        //        // 데미지 닳게 하기
        //        var attackables = enemy.GetComponents<IAttackable>();
        //        foreach (var attackable in attackables)
        //        {
        //            attackable.OnAttack(gameObject, attack);
        //        }
        //    }
        //}
    }
}
