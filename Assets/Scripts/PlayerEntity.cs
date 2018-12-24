using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityAI {
	
	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        base.UserInput();
        base.HandlePlayerMovement();
    }

    protected void FixedUpdate()
    {
        if (GetGame().GetMisc().GetState() == Game.GameStates.SPELL)
        {

        }
        else if (GetGame().GetMisc().GetState() == Game.GameStates.COMBAT)
        {
            GetGame().GetCombatSystem().CombatCameraMovement();
        }
        else
        {
            Vector3 defaultPos = new Vector3(GetGame().GetPlayer().position.x,
            GetGame().GetPlayer().position.y,
            GetGame().GetCamera().transform.position.z);

            GetGame().GetCamera().transform.position = Vector3.Lerp(GetGame().GetCamera().transform.position, defaultPos, .1f);
        }
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
