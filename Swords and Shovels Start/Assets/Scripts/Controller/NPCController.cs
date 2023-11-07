using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public enum Status
    {
        Idle,
        Patrol,
        Trace
    }

    private Status currStatus;
    public Status CurrStatus // Init
    {
        get { return currStatus; }
        set
        { 
            var prevStatus = currStatus;
            currStatus = value; 

            // �⺻��
            timer = 0f;
            agent.speed = speed;
            agent.isStopped = false;

            // ���º��� �ʱ�ȭ�� �����
            switch (currStatus)
            {
                case Status.Idle:
                    agent.isStopped = true;
                    break;
                case Status.Patrol:
                    waypointIndex = (int)Mathf.Repeat(waypointIndex + 1, waypoints.Length);
                    agent.destination = waypoints[waypointIndex].position;
                    break;
                case Status.Trace:
                    agent.speed = agentSpeed; // �پ���� ����
                    agent.destination = player.transform.position;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;
        Gizmos.color = (currStatus == Status.Trace)? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = prevColor;
    }

    private float timer = 0f;
    public float idleTime = 1f;
    public float traceInterval = 0.3f;

    private float distanceToPlayer;
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    private int waypointIndex = -1; // the current waypoint index in the waypoints array

    private float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    private Transform player; // reference to the player object transform

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        { 
            agentSpeed = agent.speed;
            speed = agentSpeed / 2f;
        }
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        currStatus = Status.Idle;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        animator.SetFloat("Speed", agent.velocity.magnitude);
        switch (currStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Patrol:
                UpdatePatrol();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
        }
    }

    private void UpdateIdle()
    {
        // ���� ��ȯ �켱���� ����
        if(distanceToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        timer += Time.deltaTime;
        if(timer >= idleTime)
        {
            CurrStatus = Status.Patrol;
            return;
        }
    }

    private void UpdatePatrol()
    {
        // ���� ��ȯ �켱���� ����
        if (distanceToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            CurrStatus = Status.Idle;
            return;
        }
    }

    private void UpdateTrace()
    {
        //agent.destination = player.position; // ���� 1�ʿ� 60�� ������ �ʿ� ����
        if(distanceToPlayer > aggroRange)
        {
            CurrStatus = Status.Idle;
            return;
        }

        timer += Time.deltaTime;
        if(timer >= traceInterval)
        {
            timer = 0f;
            agent.destination = player.position;
        }
    }
}
