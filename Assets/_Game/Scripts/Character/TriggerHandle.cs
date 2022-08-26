using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandle : MonoBehaviour
{
    public Character character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BULLET))
        {
            character.OnHit();
        }
    }
}
