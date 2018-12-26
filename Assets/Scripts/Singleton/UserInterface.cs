using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInterface : Singleton
{
    [SerializeField] private List<GameObject> UIElements = new List<GameObject>();

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS

    public T GetUIElement<T>(string name)
    {
        foreach (GameObject element in UIElements)
        {
            if (element.name.ToLower().Equals(name.ToLower()))
            {
                T result = element.GetComponent<T>();
                if (result != null) return result;
            }
        }
        return default(T);
    }
	
	#endregion
}
