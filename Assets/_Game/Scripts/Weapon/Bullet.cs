using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Rigidbody bulletRigidbody;
    public Transform bulletTransform;
    private Vector3 spawnPos;
    private float distanceTravelled;
    private float speed = 5f;
    private float range = 5.6f;
    private Weapon weapon;


    private void Update()
    {
        TrackBulletDistance();
    }

    public void OnInit(Weapon weapon)
    {
        bulletRigidbody.velocity = bulletTransform.forward * speed;
        spawnPos = bulletTransform.position;
        distanceTravelled = 0;
        this.weapon = weapon;
        bulletTransform.localScale = weapon.scale;
        range = weapon.range;
    }

    private void OnHit()
    {
        weapon.HitTarget();
        OnDespawn();
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void TrackBulletDistance()
    {
        distanceTravelled = Vector3.Distance(spawnPos, bulletTransform.position);
        if (distanceTravelled > range)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnHit();
        }
    }
}
