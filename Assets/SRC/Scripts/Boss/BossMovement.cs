using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    private NavMeshAgent nav;
    public List<Transform> MovePositions;
    public List<Vector3> MPositions;

    private EnemyScriptable zeusBrain;

    private Vector3 Position1 = new Vector3(4,2,5);
    private Vector3 Position2 = new Vector3(-5,2,5);
    private Vector3 Position3 = new Vector3(0,0,0);

    private float MoveTime = 0f;
    private float Cooldown = 3f;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();        
    }

    public void Init(EnemyScriptable brain)
    {
        nav.speed = brain.speed;
        zeusBrain = brain;
        MPositions.Add(Position1);
        MPositions.Add(Position2);
        MPositions.Add(Position3);
    }

    public bool BossMove(Transform target)
    {
        if(!target) return false;   

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > nav.stoppingDistance)
        {
            if (Time.time >= MoveTime + Cooldown)
            {
                MoveTime = Time.time;
                nav.SetDestination(target.position);
                //Vector3 nextPosition = MPositions[Random.Range(0, MPositions.Count)];
                //this.gameObject.transform.position = nextPosition;
                Debug.Log("Movendo");
            }

            return true;
        }

        // if(Time.time >= Cooldown + MoveTime)
        // {
        //     MoveTime = Time.time;
        //     this.gameObject.transform.position = MPositions[Random.Range(0,2)];
        //     Debug.Log("Movendo");
        // }
        
        return false;        
    }
    
}
