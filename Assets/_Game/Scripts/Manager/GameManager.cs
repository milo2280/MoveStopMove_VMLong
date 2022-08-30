using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Pause }

public class GameManager : Singleton<GameManager>
{
    private GameState state;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;

        // Init data

        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }

    public void ChangeState(GameState state)
    {
        this.state = state;
    }

    public bool IsState(GameState state)
    {
        return this.state == state;
    }
}
