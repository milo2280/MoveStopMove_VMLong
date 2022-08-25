using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody bulletRigidbody;
    public Transform bulletTransform;
    private float speed = 5f;


    private void OnEnable()
    {
        OnInit();
    }

    private void Update()
    {
        
    }

    private void OnInit()
    {
        bulletRigidbody.velocity = bulletTransform.forward * speed;
    }

    private void OnDespawn()
    {

    }
}
