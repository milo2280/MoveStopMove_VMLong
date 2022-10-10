using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public MeshRenderer circle;

    private bool isEnabled;

    private void Update()
    {
        if (isEnabled)
        {
            if (GameManager.Ins.IsState(GameState.MainMenu))
            {
                circle.enabled = false;
                isEnabled = false;
            }
        }
        else
        {
            if (GameManager.Ins.IsState(GameState.Gameplay))
            {
                circle.enabled = true;
                isEnabled = true;
            }
        }
    }
}
