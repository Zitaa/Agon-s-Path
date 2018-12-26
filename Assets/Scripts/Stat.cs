using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int value;

    private List<int> modifiers = new List<int>();
    
    public void AddModifier(int modifier)
    {
        if (modifier != 0) modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0) modifiers.Remove(modifier);
    }

    public int GetValue()
    {
        int result = value;
        modifiers.ForEach(x => result += x);
        return result;
    }
}
