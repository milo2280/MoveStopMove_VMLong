using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Rigidbody bulletRigidbody;
    public Transform bulletTransform;
    private Vector3 spawnPos;
    private float distanceTravelled;
    private float speed;
    private float range;
    private Weapon weapon;

    private void Update()
    {
        TrackBulletDistance();
    }

    public void OnInit(Weapon weapon)
    {
        this.weapon = weapon;
        range = weapon.range;
        speed = weapon.bulletSpeed;
        bulletTransform.localScale = weapon.scale;

        distanceTravelled = 0;
        spawnPos = bulletTransform.position;
        bulletRigidbody.velocity = bulletTransform.forward * speed;
    }

    private void OnHit(Collider target)
    {
        weapon.HitTarget(target);
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
            OnHit(other);
        }
    }
}
