using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatSystem : SingletonPattern
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float smoothTime;

    private EntityAI entityInCombatWith;
    private bool inCombat;

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

    public void CombatCameraMovement()
    {
        Vector2 playerPos = GetGame().GetPlayer().position;
        Vector2 entityPos = entityInCombatWith.transform.position;

        Vector2 midPoint = (playerPos + entityPos) * .5f;
        Vector2 offset = midPoint - playerPos;
        offset = Vector2.ClampMagnitude(offset, maxDistance);

        Vector3 targetPos = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y, GetGame().GetCamera().transform.position.z);
        GetGame().GetCamera().transform.position = Vector3.Lerp(GetGame().GetCamera().transform.position, targetPos, smoothTime);
    }

    public void EnterCombat(EntityAI entity)
    {
        inCombat = true;
        entityInCombatWith = entity;
        GetGame().GetMisc().ChangeGameState(Game.GameStates.COMBAT);
    }

    public void ExitCombat()
    {
        entityInCombatWith = null;
        inCombat = false;
        GetGame().GetMisc().ChangeGameState(Game.GameStates.IDLE);
    }

    public bool InCombat() { return inCombat; }

    public Transform GetEntityTransform() { return entityInCombatWith.transform; }
	
	#endregion
}
