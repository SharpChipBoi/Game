using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObject
{
    public float AutoDestroyTime = 5f;
    public float MoveSpeed = 2f;
    public int Damage = 5;
    public Rigidbody Rigidbody;
    protected Transform Target;
    public bool isAttacking;
    public HealthSystem healthSystem;

    protected const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
    }

    public virtual void Spawn(Vector3 Forward, int Damage, Transform Target)
    {
        isAttacking = true;
        this.Damage = Damage;
        this.Target = Target;
        Rigidbody.AddForce(Forward * MoveSpeed, ForceMode.VelocityChange);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAttacking)
            {
                healthSystem.Damage(Damage);
                Debug.Log(healthSystem.GetHealth());
            }
        }
        Disable();
    }

    //protected virtual void OnTriggerEnter(Collider other)
    //{
    //    IDamageable damageable;

    //    if (other.TryGetComponent<IDamageable>(out damageable))
    //    {
    //        damageable.TakeDamage(Damage);
    //    }

    //    Disable();
    //}

    protected void Disable()
    {
        isAttacking = false;
        CancelInvoke(DISABLE_METHOD_NAME);
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
