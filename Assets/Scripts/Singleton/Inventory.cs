using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Inventory : Singleton
{
    public void Init(List<ItemSerialization.Container> items = null)
    {
        if (items == null) return;

        for (int i = 0; i < items.Count; i++)
        {
            Item item = ScriptableObject.CreateInstance<Item>();
            item.name = items[i].GetName();
            item.icon = (Sprite)AssetDatabase.LoadAssetAtPath(items[i].GetIconPath(), typeof(Sprite));
            AddItem(item);
        }
    }

    [SerializeField] private Transform inventoryTransform;
    [SerializeField] private List<Item> inventoryItems = new List<Item>();
    [SerializeField] private int capacity;

    private bool isActive = false;

	#region PRIVATE FUNCTIONS
	
	private void UpdateInventory()
    {
        InventorySlot[] slots = inventoryTransform.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryItems.Count) slots[i].AddItem(inventoryItems[i]);
            else slots[i].Clear();
        }
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public void Enable()
    {
        isActive = true;
        GetGame().SetGameState();
        Time.timeScale = 0;
        GetGame().GetUserInterface().EnableUIElement("Inventory");
    }

    public void Disable()
    {
        isActive = false;
        GetGame().SetGameState();
        Time.timeScale = 1;
        GetGame().GetUserInterface().DisableUIElement("Inventory");
    }

	public void AddItem(Item newItem)
    {
        if (inventoryItems.Count >= capacity) return;
        if (newItem != null)
        {
            inventoryItems.Add(newItem);
            UpdateInventory();
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (itemToRemove != null)
        {
            inventoryItems.Remove(itemToRemove);
            UpdateInventory();
        }
    }

    public bool IsActive() { return isActive; }

    public Item GetItem(int index)
    {
        InventorySlot[] slots = inventoryTransform.GetComponentsInChildren<InventorySlot>();
        return slots[index].GetItem();
    }

    public List<Item> GetItems() { return inventoryItems; }
	
	#endregion
}
