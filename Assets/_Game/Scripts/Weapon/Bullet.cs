using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Rigidbody m_Rigidbody;
    public Transform m_Transform;

    private Vector3 spawnPos;
    private float distanceTravelled;
    private float speed;
    private Character character;

    private void Update()
    {
        TrackBulletDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnHit();

            IHit<Character> hit;
            hit = Cache<Character>.Get(other);
            hit.OnHit(character);
        }
    }

    public void OnInit(Character character)
    {
        this.character = character;
        speed = character.bulletSpeed;
        m_Transform.localScale = character.scale;

        distanceTravelled = 0;
        spawnPos = m_Transform.position;
        m_Rigidbody.velocity = m_Transform.forward * speed;
    }

    private void OnHit()
    {
        character.OnKill();
        OnDespawn();
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void TrackBulletDistance()
    {
        distanceTravelled = Vector3.Distance(spawnPos, m_Transform.position);
        if (distanceTravelled > character.range)
        {
            OnDespawn();
        }
    }
}
