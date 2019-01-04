using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : Singleton
{
    public void Init()
    {
        data.Init();
        FPS.Init();
        spellSystem.Init();
        cameraSettings.Init();
        discord.Init();

        SetGameState();
    }

    public enum GameStates
    {
        //Idle,
        Roam,
        Inventory,
        Loot,
        Combat,
        Spell
    };
    
    [SerializeField] private int entities = 0;
    [SerializeField] private Transform player;
    [SerializeField] private GameStates state;
    [SerializeField] private InputManager input;
    [SerializeField] private CameraBehaviour cameraSettings;
    [SerializeField] private DynamicEnvironment dynamicEnvironment;
    [SerializeField] private Inventory inventory;
    [SerializeField] private LootBehaviour loot;
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private SpellSystem spellSystem;
    [SerializeField] private FramesPerSecondSystem FPS;
    [SerializeField] private UserInterface UI;
    [SerializeField] private PersistentData data;
    [SerializeField] private DiscordBehaviour discord;

    private ItemSerialization itemSerialization = new ItemSerialization();

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS

    public void IncreaseEntities() { entities++; }

    public void SetGameState()
    {
        state = GameStates.Roam;
        if (combatSystem.GetEntities() > 0) state = GameStates.Combat;
        if (spellSystem.IsActive()) state = GameStates.Spell;
        if (inventory.IsActive()) state = GameStates.Inventory;
        if (loot.IsActive()) state = GameStates.Loot;

        discord.UpdateState(state);
    }

    public int GetEntites() { return entities; }

    public GameStates GetGameState() { return state; }

    public Transform GetPlayer() { return player; }

    public InputManager GetInput() { return input; }

    public CameraBehaviour GetCameraSettings() { return cameraSettings; }

    public DynamicEnvironment GetDynamicEnvironment() { return dynamicEnvironment; }

    public Inventory GetInventory() { return inventory; }

    public LootBehaviour GetLootBehaviour() { return loot; }

    public CombatSystem GetCombatSystem() { return combatSystem; }

    public SpellSystem GetSpellSystem() { return spellSystem; }
	
	public FramesPerSecondSystem GetFramesPerSecondSystem() { return FPS; }

    public UserInterface GetUserInterface() { return UI; }

    public PersistentData GetData() { return data; }

    public DiscordBehaviour GetDiscord() { return discord; }

    public ItemSerialization GetItemSerialization() { return itemSerialization; }
	
	#endregion
}
