using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawTrap : MonoBehaviour
{

    public static MawTrap instance;
    public static MawTrap Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MawTrap>();
            }
            return MawTrap.instance;
        }
    }

    public List<Edible> FoodList = new List<Edible>();
    public BoxCollider boxCollider;
    public Edible key;
    public GameObject keyPrefab;
    public Transform keySpawn;
    public Transform belly;
    public Animator anim;

    public float cooldown;
    public float lastMeal;

    public bool mawTriggered;

    public Coroutine triggerRoutine;
    public bool nextToTrap;
    // Start is called before the first frame update
    void Start()
    {
        //reloaded = true;
        triggerRoutine = null;
        FoodList.Clear();
        FoodList.Add(key);
        lastMeal = Time.time;
    }

    private void Update()
    {
        if (FoodList.Count == 0 && FoodList.Count <= 2 && !FoodList.Contains(key))
        {
            FoodList.Clear();
        }
    }
    public IEnumerator _TriggerMaw() //корутина с подключением анимации укуса 
    {
        mawTriggered = true;
        Debug.Log("Maw triggered");
        anim.SetBool("BiteTime", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("BiteTime", false);

        triggerRoutine = null;
        //eloaded = true;
    }
    public static bool IsTrap(GameObject obj)
    {
        if (obj.transform.root.gameObject.GetComponent<MawTrap>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SpawnKey() //функция задействующая корутину создающую предмет и удаляющюю предмет из списка съеденых преедметов
    {
        if (FoodList.Contains(key))
        {
            StartCoroutine(_EatKey());
            FoodList.Remove(key);
        }
    }

    IEnumerator _EatKey()//корутина создающая предмет и удаляющяя предмет из списка съеденых преедметов
    {
        yield return new WaitForSeconds(3f);
        triggerRoutine = StartCoroutine(_TriggerMaw());
        yield return new WaitForSeconds(.3f);
        Instantiate(keyPrefab, keySpawn.transform.position, keySpawn.transform.rotation);

    }
    private void OnTriggerEnter(Collider other) // проверка на коллизию с пулкй и изменение кулдауна, так же спавн предмета находящегося в списке съеденых
    {
        //if (other.tag == "Player")
        //{
        //    nextToTrap = true;
        //}
        if(other.tag == "Bullet")
        {
            if (Time.time - lastMeal < cooldown)
            {
                if (!FoodList.Contains(key))
                {
                    FoodList.Clear();
                }
                return;
            }
            lastMeal = Time.time;
            Edible control = other.gameObject.transform.root.GetComponent<Edible>();
            if (control != null)
            {
                Debug.Log("Eat");
                if (!FoodList.Contains(control) && FoodList.Count <= 2)
                {
                    triggerRoutine = StartCoroutine(_TriggerMaw());

                    mawTriggered = false;
                    if (FoodList.Contains(key) && FoodList.Count <= 2)
                    {
                        control.isEaten = true;
                        control.transform.position = belly.position;
                        //Destroy(control.gameObject);
                        SpawnKey();
                    }
                    else if (FoodList.Count <= 2)
                    {
                        control.isEaten = true;
                        control.transform.position = belly.position;
                        FoodList.Add(control);
                        //Destroy(control.gameObject);
                    }
                }
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nextToTrap = false;
        }
    }

}
