using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : EntityAI {

    private Transform target;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        target = GetGame().GetPlayer();
	}

    protected override void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= settings.viewRange.GetValue())
        {
            base.HandleAIMovement(transform, target);
            GetGame().GetCombatSystem().EnterCombat(this);
        }
        else if (distance > settings.viewRange.GetValue() && GetGame().GetCombatSystem().InCombat())
            GetGame().GetCombatSystem().ExitCombat();
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
