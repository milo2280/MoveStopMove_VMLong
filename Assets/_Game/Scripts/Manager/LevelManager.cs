using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Vector3 playerPos { get; private set; }
    public float playerRange;

    private int counter;

    private void Update()
    {
        playerPos = player.charTransform.position;
    }

    private void OnInit()
    {

    }
}
