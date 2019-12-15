using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour, IDamageable
{

    public float startingHealth = 20;
    protected float health;
    protected bool Dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // Do some stuff here with hit var
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !Dead)
        {
            Die();
        }
    }

    protected virtual void Update()
    {
        if (transform.position.y < -1.5f)
        {
            Die();
        }
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        Dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}