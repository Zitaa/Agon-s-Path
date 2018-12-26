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
        base.HandlePlayerMovement();
	}

    #endregion

    #region PRIVATE FUNCTIONS
    


    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
