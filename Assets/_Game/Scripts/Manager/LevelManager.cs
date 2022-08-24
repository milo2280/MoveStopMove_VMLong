using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Vector3 playerPos;
    public float playerRange;

    private void Update()
    {
        playerPos = player.charTransform.position;
    }
}
