using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : Singleton
{
    public void Init()
    {
        FPS.Init();
        dynamicEnvironment.Init();

        SetGameState();
    }

    public enum GameStates
    {
        IDLE,
        COMBAT,
        SPELL
    };

    [SerializeField] private int entities = 0;
    [SerializeField] private Transform player;
    [SerializeField] private GameStates state;
    [SerializeField] private InputManager input;
    [SerializeField] private CameraBehaviour cameraSettings;
    [SerializeField] private DynamicEnvironment dynamicEnvironment;
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private SpellSystem spellSystem;
    [SerializeField] private FramesPerSecondSystem FPS;
    [SerializeField] private UserInterface UI;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS

    public void IncreaseEntities() { entities++; }

    public void SetGameState()
    {
        state = GameStates.IDLE;
        if (combatSystem.GetEntities() > 0) state = GameStates.COMBAT;
        if (spellSystem.IsActive()) state = GameStates.SPELL;
    }

    public int GetEntites() { return entities; }

    public GameStates GetGameState() { return state; }

    public Transform GetPlayer() { return player; }

    public InputManager GetInput() { return input; }

    public CameraBehaviour GetCameraSettings() { return cameraSettings; }

    public DynamicEnvironment GetDynamicEnvironment() { return dynamicEnvironment; }

    public CombatSystem GetCombatSystem() { return combatSystem; }

    public SpellSystem GetSpellSystem() { return spellSystem; }
	
	public FramesPerSecondSystem GetFramesPerSecondSystem() { return FPS; }

    public UserInterface GetUserInterface() { return UI; }
	
	#endregion
}
