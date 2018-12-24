using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager : SingletonPattern {
    
    [Header("Movement")]
    public KeyCode Up;
    public KeyCode right;
    public KeyCode down;
    public KeyCode left;

    [Header("Combat")]
    public KeyCode meleeAttack;
    public KeyCode spellAttack;
    public KeyCode spellActivator;

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

    public bool GetKey(KeyCode key)
    {
        if (Input.GetKey(key)) return true;
        return false;
    }

    public bool GetKeyUp(KeyCode key)
    {
        if (Input.GetKeyUp(key)) return true;
        return false;
    }

    public float GetMovementInput(string direction)
    {
        if (direction.Equals("Horizontal"))
        {
            if (GetKey(right)) return 1f;
            else if (GetKey(left)) return -1f;
        }
        else if (direction.Equals("Vertical"))
        {
            if (GetKey(Up)) return 1f;
            else if (GetKey(down)) return -1f;
        }
        return 0f;
    }

    #endregion
}
