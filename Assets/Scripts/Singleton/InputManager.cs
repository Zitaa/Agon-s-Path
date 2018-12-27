using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager : Singleton
{
    [Header("Movement")]
    public KeyCode up;
    public KeyCode right;
    public KeyCode down;
    public KeyCode left;

    [Header("Combat")]
    public KeyCode meleeAttack;
    public KeyCode spellAttack;
    public KeyCode spellActivator;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public float GetMovementInput(string direction)
    {
        if (direction.Equals("Horizontal"))
        {
            if (Input.GetKey(right)) return 1f;
            else if (Input.GetKey(left)) return -1f;
        }
        else if (direction.Equals("Vertical"))
        {
            if (Input.GetKey(up)) return 1f;
            else if (Input.GetKey(down)) return -1f;
        }
        return 0;
    }

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
	
	#endregion
}
