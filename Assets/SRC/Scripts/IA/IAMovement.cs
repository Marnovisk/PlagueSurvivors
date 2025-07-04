using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class IAMovement : MonoBehaviour
{
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();        
    }

    public void Init(EnemyScriptable brain)
    {
        nav.speed = brain.speed;
    }

    public bool Chase(Transform target)
    {
        if(!target) return false;
        if(Vector3.Distance(transform.position, target.position) > nav.stoppingDistance)
        {
            nav.SetDestination(target.position);
            return true;
        }

        return false;
        
    }

    
}
