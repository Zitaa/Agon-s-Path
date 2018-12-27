using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityAI
{
	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        settings.speed.AddModifier(500);
	}
	
	protected override void Update () 
	{
        base.Update();
        base.HandlePlayerMovement();

        if (input.GetKeyUp(input.meleeAttack))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.Play("Attack");
                StartCoroutine(Attack()); 
            }
        }

        if (input.GetKeyUp(input.spellActivator))
        {
            if (!game.GetSpellSystem().IsActive()) game.GetSpellSystem().Start();
            else game.GetSpellSystem().End();
        }
	}

    #endregion

    #region PRIVATE FUNCTIONS
    


    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
