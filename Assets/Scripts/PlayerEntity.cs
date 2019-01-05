using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : EntityAI
{
    public void Init(int health = 0, int mana = 0)
    {
        if (health == 0 || mana == 0)
        {
            this.health = settings.GetHealth();
            this.mana = settings.GetMana();
        }
        else
        {
            this.health = health;
            this.mana = mana;
        }
    }

    [SerializeField] private List<Interactable> interactables = new List<Interactable>();

    private Slider healthBar;
    private Slider manaBar;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        healthBar = game.GetUserInterface().GetUIElement<Slider>("Health");
        manaBar = game.GetUserInterface().GetUIElement<Slider>("Mana");
        
        healthBar.maxValue = settings.GetHealth();
        manaBar.maxValue = settings.GetMana();
        healthBar.value = health;
        manaBar.value = mana;
    }
	
	protected override void Update () 
	{
        base.Update();
        UserInput();
	}

    #endregion

    #region PRIVATE FUNCTIONS

    private void UserInput()
    {
        base.HandlePlayerMovement();

        if (input.GetKeyUp(input.spellActivator))
        {
            if (!game.GetSpellSystem().IsActive()) game.GetSpellSystem().Start();
            else game.GetSpellSystem().End();
        }
        if (input.GetKeyUp(input.inventory))
        {
            if (!game.GetInventory().IsActive()) game.GetInventory().Enable();
            else game.GetInventory().Disable();
        }
        if (input.GetKeyUp(input.interact))
        {
            if (interactables.Count > 0)
            {
                Loot loot = interactables[0] as Loot;
                if (!game.GetLootBehaviour().IsActive()) game.GetLootBehaviour().Enable(loot.GetLoot());
                else
                {
                    game.GetLootBehaviour().TakeLoot(loot.GetLoot());
                    game.GetLootBehaviour().Disable();
                    GameObject clone = interactables[0].gameObject;
                    interactables.Remove(interactables[0]);
                    Destroy(clone);
                }
            }
        }

        switch (game.GetGameState())
        {
            case GameManager.GameStates.Roam:
                MeleeInput();
                break;
            case GameManager.GameStates.Inventory:
                break;
            case GameManager.GameStates.Combat:
                MeleeInput();
                break;
            case GameManager.GameStates.Spell:
                SpellInput(); 
                break;
        }
    }

    private void MeleeInput()
    {
        if (input.GetKeyUp(input.meleeAttack))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.Play("Attack");
                StartCoroutine(Attack());
            }
        }
    }

    private void SpellInput()
    {
        if (input.GetKeyUp(input.spellAttack))
        {
            if (DecreaseMana(game.GetSpellSystem().GetSpellProjectile(0).GetCost()))
            {
                Vector2 iconSpawnPos = game.GetCameraSettings().GetCamera().ScreenToWorldPoint(Input.mousePosition);
                GameObject clone = Instantiate(game.GetSpellSystem().GetSpellIcon(), iconSpawnPos, Quaternion.identity);
                game.GetSpellSystem().AddSpellIcon(clone);
            }
            else Debug.Log("Not enough mana.");
        }
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public override void DecreaseHealth(int amount)
    {
        base.DecreaseHealth(amount);
        healthBar.value = health;
    }

    public bool AddInteractable(Interactable interactable)
    {
        if (interactable != null)
        {
            interactables.Add(interactable);
            return true;
        }
        return false;
    }

    public bool RemoveInteractable(Interactable interactable)
    {
        if (interactable != null)
        {
            foreach (Interactable _interactable in interactables)
            {
                if (interactable.GetID().Equals(_interactable.GetID()))
                {
                    interactables.Remove(_interactable);
                    return true;
                }
            }
        }
        return false;
    }

    public bool DecreaseMana(int amount)
    {
        int difference = mana - amount;
        if (difference <= 0) return false;
        mana -= amount;
        manaBar.value = mana;
        return true;
    }

    public List<Interactable> GetInteractables() { return interactables; }

    #endregion
}
