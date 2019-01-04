using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntitySettings
{
    public string name;
    public Stat speed;
    public Stat health;
    public Stat mana;
    public Stat damage;
    public Stat meleeRange;
    public Stat viewRange;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public int GetSpeed() { return speed.GetValue();}

    public int GetHealth() { return health.GetValue(); }

    public int GetMana() { return mana.GetValue(); }

    public int GetDamage() { return damage.GetValue(); }

    public int GetMeleeRange() { return meleeRange.GetValue(); }

    public int GetViewRange() { return viewRange.GetValue(); }

	#endregion
}
