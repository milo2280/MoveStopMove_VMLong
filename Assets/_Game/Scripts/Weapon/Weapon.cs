using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float attackSpeed { get; private set; }
    public GameObject bulletPrefab;
    public Transform weaponTransform;

    public virtual void Attack(Vector3 position, Quaternion rotation) 
    {
        SimplePool.Spawn(bulletPrefab, position, rotation);
    }
}
