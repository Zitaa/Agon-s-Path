using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatSystem : Singleton
{
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    private List<EntityAI> entities = new List<EntityAI>();

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void EnterCombat(EntityAI entity)
    {
        GetGame().ChangeGameState(GameManager.GameStates.COMBAT);
        entities.Add(entity);
        transforms.Add(entity.transform);
    }

    public void ExitCombat()
    {
        
    }

    public void RemoveEntity(EntityAI entity)
    {
        if (entity != null)
        {
            entities.Remove(entity);
            transforms.Remove(entity.transform);
        }
    }

    public int GetEntities() { return entities.Count; }

    public List<Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < transforms.Count; i++)
        {
            positions.Add(transforms[i].position);
        }
        return positions;
    }

    public Transform[] GetTransforms() { return transforms.ToArray(); }
	
	#endregion
}
