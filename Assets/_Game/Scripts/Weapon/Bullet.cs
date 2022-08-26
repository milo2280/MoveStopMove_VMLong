using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody bulletRigidbody;
    public Transform bulletTransform;
    private Vector3 spawnPos;
    private float distanceTravelled;
    private float speed = 5f;
    private float charAttackRange = 5.6f;


    private void OnEnable()
    {
        OnInit();
    }

    private void Update()
    {
        TrackBulletDistance();
    }

    public void OnInit()
    {
        bulletRigidbody.velocity = bulletTransform.forward * speed;
        spawnPos = bulletTransform.position;
        distanceTravelled = 0;
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this.gameObject);
    }

    private void TrackBulletDistance()
    {
        distanceTravelled = Vector3.Distance(spawnPos, bulletTransform.position);
        if (distanceTravelled > charAttackRange)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnDespawn();
        }
    }
}
