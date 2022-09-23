using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public bool isPlayer;
    public Character character;

    private Character target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            target = Cache<Character>.Get(other);
            character.AddTarget(target);
        }

        if (other.CompareTag(Constant.TAG_BULLET))
        {
            character.OnHit();

            if (isPlayer)
            {
                Bullet bullet = Cache<Bullet>.Get(other);
                LevelManager.Ins.killer = bullet.GetCharName();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            target = Cache<Character>.Get(other);
            character.RemoveTarget(target);
        }
    }
}
