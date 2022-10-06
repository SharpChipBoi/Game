using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRadius : MonoBehaviour
{
    public HealthBar healthBar;
    private HealthSystem healthSystem = new HealthSystem(100);
    public float lookRadius = 10f;
    public GameObject player;
    public int damageAmount = 15;
    public float attackRange = 1.5f;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.Setup(healthSystem);

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
            if (distance < attackRange)
            {
                agent.velocity = Vector3.zero;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach(Collider objectNear in colliders)
        {
            if(objectNear.tag == "Player")
            {
                HealthSystem.Damage(damageAmount);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
