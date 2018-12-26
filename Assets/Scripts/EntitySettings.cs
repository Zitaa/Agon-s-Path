using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntitySettings
{
    public Stat speed;
    public Stat health;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public int GetSpeed() { return speed.GetValue();}

    public int GetHealth() { return health.GetValue(); }

	#endregion
}
