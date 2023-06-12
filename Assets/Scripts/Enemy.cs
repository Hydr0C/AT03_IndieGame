using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStateMachine;
using UnityEngine.AI;
using Random = System.Random;

/// <summary>
///  Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
///  - https://www.youtube.com/watch?v=c8Nq19gkNfs
/// </summary>

public class Enemy : MonoBehaviour
{
    //Define all necessary variables
    private NavMeshAgent agent;

    public Notes noteScript;
    public Player playerScript;

    [SerializeField] GameObject player;
    public MenuManager menuManager;

    //List of Waypoints for Patrol + other needed variables
    public Transform[] waypoints;
    private int waypointNumber = 0;
    private Vector3 waypointTarget;

    [SerializeField]
    float stoppingDistance,
        detectionDistance,
        walkSpeed,
        runSpeed;
    [SerializeField] 
    int timerMin,
        timerMax;

    private float timer;
    private bool hitPlayer;

    public StateMachine StateMachine { get; private set; } //Get the state machine 

    private void Awake()
    {
        StateMachine = new StateMachine(); //Set up the state machine

        if(!TryGetComponent<NavMeshAgent>(out agent)) //make sure we've got a navmesh agent
        {
            Debug.LogError("Needs a NavMesh Agent!!!");
        }

        // get Animator component
    }

    void Start()
    {
        StateMachine.SetState(new IdleState(this)); //Starts AI off in idle
        agent.isStopped = true; //makes sure AI isn't doing anything yet
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.OnUpdate(); // does the stuff we set up in enemy states script
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT SMTH");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("it was the player");
            hitPlayer = true;
        }
    }

    public abstract class EnemyMoveState : IState //Set up a state that will act as a folder
    {
        protected Enemy instance;
        public EnemyMoveState (Enemy _instance)
        {
            instance = _instance;
        }

        public virtual void OnEnter()
        { }

        public virtual void OnExit()
        { }

        public virtual void OnUpdate()
        { }
    }

    public class MoveState : EnemyMoveState //The state that will move between positions
    {
        public MoveState(Enemy _instance) : base(_instance)
        { }

        public override void OnEnter()
        {
            Debug.Log("Entering Move State");
            instance.agent.isStopped = false;

            //Set animator to walk

            instance.agent.speed = instance.walkSpeed;
        }

        public override void OnUpdate()
        {
            //Set the target postion and move towards it
           // Debug.Log("Move State Update");

            if(Vector3.Distance(instance.transform.position, instance.player.transform.position) < instance.detectionDistance)
            {
                //If within range of the player, AI will being chasing
                //Debug.Log("Can see player");
                instance.StateMachine.SetState(new ChaseState(instance));
            }
            else if(instance.noteScript.endGame)
            {
                instance.StateMachine.SetState(new ChaseState(instance));
            }
            else
            {
                //Debug.Log("setting desti");
                instance.waypointTarget = instance.waypoints[instance.waypointNumber].position;
                instance.agent.SetDestination(instance.waypointTarget);

                if (Vector3.Distance(instance.transform.position, instance.waypointTarget) < instance.stoppingDistance)
                {
                    instance.waypointNumber++;
                    if(instance.waypointNumber == instance.waypoints.Length)
                    {
                        instance.waypointNumber = 0;
                    }
                    instance.StateMachine.SetState(new IdleState(instance));
                }
            }

        }

        public override void OnExit()
        {
            //reset animation
        }
    }

    public class IdleState : EnemyMoveState // the state for when our guy is just vibing
    {
        public IdleState(Enemy _instance) : base(_instance)
        { }

        Random r = new Random();
        
        public override void OnEnter()
        {
            Debug.Log("Entering Idle State");
            instance.agent.isStopped = true;

            //set animation to idle

            instance.timer = r.Next(instance.timerMin, instance.timerMax);
        }

        public override void OnUpdate()
        {

            if (Vector3.Distance(instance.transform.position, instance.player.transform.position) < instance.detectionDistance)
            {
                //If within range of the player, AI will being chasing
                instance.StateMachine.SetState(new ChaseState(instance));
            }
            else if (instance.noteScript.endGame)
            {
                instance.StateMachine.SetState(new ChaseState(instance));
            }
            else if (instance.timer <= 1)
            {
                instance.StateMachine.SetState(new MoveState(instance));
            }
            else
            {
                instance.timer -= Time.deltaTime;
            }
        }

        public override void OnExit()
        {
            //reset animation
        }
    }

    public class ChaseState : EnemyMoveState
    {
        public ChaseState(Enemy _instance) : base(_instance)
        { }

        public override void OnEnter()
        {
            Debug.Log("Entering Chase State");
            instance.agent.isStopped = false;
            instance.agent.speed = instance.runSpeed;

            //set animation to run
        }

        public override void OnUpdate()
        {
            if(Vector3.Distance(instance.transform.position, instance.player.transform.position) < instance.detectionDistance)
            {
                instance.agent.SetDestination(instance.player.transform.position);
            }
            else
            {
                if (!instance.noteScript.endGame)
                {
                    instance.StateMachine.SetState(new IdleState(instance));
                }
                else if(instance.noteScript.endGame)
                {
                    instance.agent.SetDestination(instance.player.transform.position);
                }
            }
            if(instance.hitPlayer)
            {
                Debug.Log("get rekt");
                instance.menuManager.GameOver();
            }
            if(instance.playerScript.attack)
            {
                instance.StateMachine.SetState(new StunState(instance));
            }
        }

        public override void OnExit()
        {
            //reset animations
        }
    }

    public class StunState : EnemyMoveState
    {
        public StunState(Enemy _instance) : base(_instance)
        { }

        public override void OnEnter()
        {
            Debug.Log("ouchies im stunned");
            instance.timer = 3.5f;
            instance.agent.isStopped = true;
        }

        public override void OnUpdate()
        {
            if(instance.timer >= 0f)
            {
                //play animation stunned
                instance.timer -= Time.deltaTime;
            }
            else if(instance.timer <= 0f)
            {
                instance.playerScript.attack = false;
                Debug.Log("attack = " + instance.playerScript.attack);
                instance.StateMachine.SetState(new IdleState(instance));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance); 
    }
}
