using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NPCController2 : MonoBehaviour
{
    public AttackDefinition attackDef;

    public enum States
    {
        Idle,
        Patrol,
        Trace,
        Attack
    }

    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;
        Gizmos.color = (stateManager.currentStateName == States.Trace) ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = prevColor;
    }


    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
        stateManager.currentStateName = newState;
    }

    public float idleTime = 1f;
    public float traceInterval = 0.3f;
    public float aggroRange = 10f; // distance in scene units below which the NPC will increase speed and seek the player
    public float attackRange = 2f; // distance in scene units below which the NPC will increase speed and seek the player

    public Weapon weapon;
    public Transform projectileDummy;
    public Transform tempDummy;
    public Transform targetTr;

    public bool RaycastToTarget
    {
        get
        {
            if(targetTr != null)
            {
                // 손 위치 말고 근사치로 세팅
                var origin = transform.position;
                origin.y += 1f;

                var target = targetTr.position;
                target.y += 1f;

                var direction = target - origin;
                direction.Normalize();

                // 전체 레이어 - 내 레이어 - 타겟 레이어
                var mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer), LayerMask.LayerToName(targetTr.gameObject.layer));
                var layer = ~0 ^ mask;
                return Physics.Raycast(origin, direction, attackDef.range, layer);
            }
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        var origin = transform.position;
        origin.y += 1f;

        var target = targetTr.position;
        target.y += 1f;

        var direction = target - origin;
        direction.Normalize();

        Gizmos.DrawRay(origin, direction * attackDef.range);
    }

    public Transform[] waypoints; // collection of waypoints which define a patrol area
    public int waypointIndex = -1; // the current waypoint index in the waypoints array

    [System.NonSerialized]
    public Animator animator; // reference to the animator component
    [System.NonSerialized]
    public NavMeshAgent agent; // reference to the NavMeshAgent

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        targetTr = GameObject.FindWithTag("Player").transform;

        if (weapon.prefab != null && projectileDummy != null)
        {
            Instantiate(weapon.prefab, projectileDummy);
        }
    }

    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));
        states.Add(new AttackState(this));

        SetState(States.Idle);
    }

    private void Update()
    {
        stateManager.currentState.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DieFloor")
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        // 내 코드
        //if (weapon.prefab == null) // 주먹 공격
        //{
        //    weapon.ExecuteAttack(gameObject, player.gameObject);
        //}
        //else // 발사체 공격
        //{
        //    weapon.ExecuteLongDistanceAttack(gameObject, player.gameObject, tempDummy);
        //}

        if(targetTr == null)
        {
            return;
        }

        attackDef.ExecuteAttack(gameObject, targetTr.gameObject);
    }
}
