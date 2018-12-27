using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : EntityAI
{
    private bool inCombat = false;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        settings.speed.AddModifier(200);
	}
	
	protected override void Update () 
	{
        base.Update();
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= 5)
        {
            EnterCombat();
            base.HandleAIMovement();
        }
        else ExitCombat();
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private void EnterCombat()
    {
        if (!inCombat) game.GetCombatSystem().EnterCombat(this);
        inCombat = true;
    }

    private void ExitCombat()
    {
        if (inCombat) game.GetCombatSystem().RemoveEntity(this);
        inCombat = false;
        rb2d.velocity = Vector2.zero;
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	
	
	#endregion
}
