using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NPCController2 : MonoBehaviour
{
    public enum States
    {
        Idle,
        Patrol,
        Trace
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
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
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
    }

    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));

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
}
