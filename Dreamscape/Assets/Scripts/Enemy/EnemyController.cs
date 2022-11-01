using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public GameObject player;
    public bool isAttacking;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        agent.speed = 1.5f;
        if (distance <= lookRadius)
        {
            agent.speed = 3.5f;
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                isAttacking = true;
                FaceTarget();
            }
            else isAttacking = false;
        }
    }

    void FaceTarget()
    {
        Vector3 diraction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(diraction.x, 0, diraction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }




}
