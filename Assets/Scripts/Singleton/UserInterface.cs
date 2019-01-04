using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInterface : Singleton
{
    [SerializeField] private List<GameObject> UIElements = new List<GameObject>();

    #region PRIVATE FUNCTIONS

    private GameObject GetElement(string name)
    {
        foreach (GameObject element in UIElements)
        {
            if (element.name.ToLower() == name.ToLower()) return element;
        }
        return null;
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void EnableUIElement(string name) { GetElement(name).SetActive(true); }

    public void DisableUIElement(string name) { GetElement(name).SetActive(false); }

    public void EnableAll()
    {
        foreach (GameObject element in UIElements)
        {
            element.SetActive(true);
        }
    }

    public void DisableAll()
    {
        foreach (GameObject element in UIElements)
        {
            element.SetActive(false);
        }
    }

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
