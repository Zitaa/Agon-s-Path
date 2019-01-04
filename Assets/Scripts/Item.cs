using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    new public string name = "Name";
    public Sprite icon = null;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public virtual void Use()
    {

    }

    public void RemoveFromInventory()
    {

    }
	
	#endregion
}
