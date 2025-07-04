using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class playerPigSpawner : MonoBehaviour
{
    public GameObject segmentPrefab;
    private Vector3 _bodypreviousPosition;
    private List<Transform> segments = new List<Transform>();

    void Start()
    {
        // Adiciona a cabeça da cobra como o primeiro segmento
        segments.Add(transform);
    }

    void Update()
    {
    
    }

    public void MoveSegments(Vector3 previousHeadPosition, float gridSize)
    {
        // Percorre os segmentos de trás para frente
        //for (int i = segments.Count - 1; i > 0; i--)
        //{

        //    //segments[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(segments[i - 1].position + (transform.forward * -gridSize));
        //    segments[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(_bodypreviousPosition);
        //    _bodypreviousPosition = segments[i].position + (transform.forward * -gridSize);
        //}

        // Percorre os segmentos normalmente
        for (int i = 1; i < segments.Count; i++)
        {

            //segments[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(segments[i - 1].position + (transform.forward * -gridSize));
            segments[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(_bodypreviousPosition);
            _bodypreviousPosition = segments[i].position + (transform.forward * -gridSize);
        }

        // O primeiro segmento segue a posição anterior da cabeça
        if (segments.Count > 1)
        {
            segments[1].gameObject.GetComponent<NavMeshAgent>().SetDestination(previousHeadPosition);
        }
    }

    public void AddSegment(GameObject segmentPrefab)
    {
        GameObject newSegment = Instantiate(segmentPrefab);
        newSegment.transform.position = segments[segments.Count - 1].position;
        //newSegment.gameObject.GetComponent<NavMeshAgent>().SetDestination(segments[segments.Count - 1].position);
        segments.Add(newSegment.transform);
    }
}
