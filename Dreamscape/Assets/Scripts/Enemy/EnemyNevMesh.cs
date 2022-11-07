using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNevMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //радиус сферы

    public Transform centrePoint; //центр перемещения агента

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //когда завершил путь
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //вводим центральную точку и радиус области
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //рисуем гизмосом
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //радномная точка в сфере
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //1.0f - это максимальное расстояние от случайной точки до точки на навмеше
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
