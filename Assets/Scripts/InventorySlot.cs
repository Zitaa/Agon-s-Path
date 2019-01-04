using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button removeButton;

    private Item item;

    #region UNITY FUNCTIONS

    private void Start ()
	{

    }
	
	private void Update () 
	{
		
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void Clear()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void RemoveFromInventory()
    {
        GameBehaviour.instance.GetGame().GetInventory().RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null) item.Use();
    }

    public Item GetItem() { return item; }
	
	#endregion
}
