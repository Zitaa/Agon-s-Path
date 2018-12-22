using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInterface : SingletonPattern
{
    public void Init()
    {
        EnableUI();
    }

    [SerializeField] private List<GameObject> Elements = new List<GameObject>();
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public void EnableUI()
    {
        foreach (GameObject element in Elements)
        {
            UserInterfaceElement UIE = element.GetComponent<UserInterfaceElement>();
            if (UIE.IsUserEnabled()) UIE.Enable();
        }
    }

    public void DisableUI()
    {
        foreach (GameObject element in Elements)
        {
            element.GetComponent<UserInterfaceElement>().Disable();
        }
    }

    public void EnableUIElement(string name)
    {
        UserInterfaceElement UIE = GetUIElement(name);
        if (UIE.IsUserEnabled()) UIE.Enable();
    }

    public void DisableUIElement(string name)
    {
        GetUIElement(name).Disable();
    }

	public UserInterfaceElement GetUIElement(string name)
    {
        foreach (GameObject element in Elements)
        {
            if (element.name.ToLower().Equals(name.ToLower())) return element.GetComponent<UserInterfaceElement>();
        }
        return null;
    }
	
	#endregion
}
