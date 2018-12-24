using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityAI {

    private new Camera camera;
    private InputManager input;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        camera = GetGame().GetCameraSettings().GetCamera();
        input = GetGame().GetInputSettings();
    }

    protected override void Update()
    {
        base.Update();
        base.UserInput();
        //input.GetUserInput();
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
            Vector3 defaultPos = new Vector3(transform.position.x,
            transform.position.y,
            camera.transform.position.z);

            camera.transform.position = Vector3.Lerp(camera.transform.position, defaultPos, .1f);
        }
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
