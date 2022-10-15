using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyRadius : MonoBehaviour
{   
    public float lookRadius = 10f;
    public GameObject player;
    public int damageAmount = 100;
    public float attackRange = 1.5f;
    public HealthSystem healthSystem;
    public bool isAttacking;
    public HealthBar healthBar;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        Debug.Log(healthSystem.GetHealth());
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
                isAttacking = true;
                agent.velocity = Vector3.zero;
            }
            else isAttacking = false;
        }
        if (healthSystem.isGameOver)
        {
            SceneManager.LoadScene("Customization");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAttacking)
            {
                healthSystem.Damage(damageAmount);
                Debug.Log(healthSystem.GetHealthPercent());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
