using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LootBehaviour : Singleton
{
    [SerializeField] private GameObject lootUI;
    [SerializeField] private List<Image> slots;
    [SerializeField] private List<Item> lootPool = new List<Item>();
    [SerializeField] private float dropRatio;
    
    private bool isActive = false;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public void Enable(List<Item> items)
    {
        isActive = true;
        GetGame().SetGameState();
        GetGame().GetUserInterface().EnableUIElement("Loot");
        
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].sprite = items[i].icon;
            slots[i].enabled = true;
        }
    }

    public void Disable()
    {
        isActive = false;
        GetGame().SetGameState();
        GetGame().GetUserInterface().DisableUIElement("Loot");

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].enabled = false;
        }
    }

    public List<Item> GenerateLoot()
    {
        List<Item> loot = new List<Item>();
        for (int i = 0; i < lootPool.Count; i++)
        {
            if (loot.Count >= 3) break;
            if (dropRatio >= Random.value)
            {
                int index = Random.Range(0, lootPool.Count);
                loot.Add(lootPool[index]);
            }
        }
        return loot;
    }

    public bool IsActive() { return isActive; }
	
	#endregion
}
