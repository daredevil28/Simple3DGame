using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent _agent;

    public float stoppingDistance;

    public float movementSpeed;

    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = stoppingDistance;
        _agent.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = playerTransform.position;
        transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
    }
}
