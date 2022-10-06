using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    HealthSystem healthSystem = new HealthSystem(100);

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
