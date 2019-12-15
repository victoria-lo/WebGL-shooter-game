using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Life
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    public ParticleSystem deathEffect;

    public NavMeshAgent pathfinder;
    Transform target;
    public Animator anim;

    float atkDistThreshold = 2.5f;
    float timeBtwAtk = 1;
    float damage = 1;

    float nextAtkTime;
    float myCollisionRad;
    float targetCollisionRad;

    bool hasTarget;

    Life tgtEntity;

    void Awake()
    {
        pathfinder = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            tgtEntity = target.GetComponent<Life>();

            myCollisionRad = GetComponent<CapsuleCollider>().radius;
            targetCollisionRad = target.GetComponent<CapsuleCollider>().radius;
        }
    }
    protected override void Start()
    {
        base.Start();
        anim.SetFloat("Speed", 1.0f);
        if (hasTarget)
        {
            currentState = State.Chasing;
            tgtEntity.OnDeath += OnTargetDeath;

            StartCoroutine(UpdatePath());
        }
    }


    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    protected override void Update()
    {
        base.Update();
        if (hasTarget)
        {
            if (Time.time > nextAtkTime)
            {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(atkDistThreshold + myCollisionRad + targetCollisionRad, 2))
                {
                    nextAtkTime = Time.time + timeBtwAtk;
                    StartCoroutine(Attack());
                }

            }
        }

    }

    IEnumerator Attack()
    {

        pathfinder = GetComponent<NavMeshAgent>();
        currentState = State.Attacking;
        pathfinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRad);
        float attackSpeed = 3;
        float percent = 0;

        bool hasAppliedDamage = false;
        while (percent <= 1)
        {
            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                tgtEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        float refreshRate = .25f;

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRad + targetCollisionRad + atkDistThreshold / 2);
                if (!Dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
