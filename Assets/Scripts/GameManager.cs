using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : Singleton
{
    public void Init()
    {
        FPS.Init();

        ChangeGameState(GameStates.IDLE);
    }

    public enum GameStates
    {
        IDLE,
        COMBAT
    };

    [SerializeField] private Transform player;
    [SerializeField] private GameStates state;
    [SerializeField] private InputManager input;
    [SerializeField] private CameraBehaviour cameraSettings;
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private FramesPerSecondSystem FPS;
    [SerializeField] private UserInterface UI;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS

    public void ChangeGameState(GameStates state)
    {
        this.state = state;
        switch (this.state)
        {
            case GameStates.IDLE:
                break;
            case GameStates.COMBAT:
                break;
        }
    }

    public GameStates GetGameState() { return state; }

    public Transform GetPlayer() { return player; }

    public InputManager GetInput() { return input; }

    public CameraBehaviour GetCameraSettings() { return cameraSettings; }

    public CombatSystem GetCombatSystem() { return combatSystem; }
	
	public FramesPerSecondSystem GetFramesPerSecondSystem() { return FPS; }

    public UserInterface GetUserInterface() { return UI; }
	
	#endregion
}
