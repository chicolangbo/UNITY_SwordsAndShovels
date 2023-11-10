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
        animator.SetFloat("Speed", agent.velocity.magnitude); // �̵��� ����ϴ� agent, magnitude�� �ϸ� ũ�⸸ ��Į������ �ѱ�
    }

    public void SetDestination(Vector3 destination)
    {
        // �ڷ�ƾ ���� ������ ����
        if (coMove != null) // �ڷ�ƾ ��ø ����ó��
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        // Ÿ�� Ŭ����
        attackTarget = null;
        agent.isStopped = false;
        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        // ���� �ȿ� ���� ������ �̵�
        if(coMove != null) // �ڷ�ƾ ��ø ����ó��
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        
        attackTarget = target;
        coMove = StartCoroutine(CoMoveAndAttack());
    }

    public void DoSkill(Vector3 destination)
    {
        if (coMove != null) // �ڷ�ƾ ��ø ����ó��
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
    //    // temp�� �־����
    //    var enemies = Physics.OverlapSphere(transform.position, stompRange);
    //    // �ݶ��̴� �±װ� enemy�� stompEnemies�� �ֱ�
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
        // �̵� �� ������ ����
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

        //// attack ����
        //foreach(var enemy in stompEnemies)
        //{
        //    var aStats = GetComponent<CharacterStats>();
        //    var dStats = enemy.GetComponent<CharacterStats>();
        //    if(dStats != null)
        //    {
        //        var attackDefinition = new AttackDefinition();
        //        var attack = attackDefinition.CreateAttack(aStats, dStats);
        //        // ������ ��� �ϱ�
        //        var attackables = enemy.GetComponents<IAttackable>();
        //        foreach (var attackable in attackables)
        //        {
        //            attackable.OnAttack(gameObject, attack);
        //        }
        //    }
        //}
    }
}
