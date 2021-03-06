﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatSystem : Singleton
{
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    private List<EntityAI> entities = new List<EntityAI>();

    private bool inCombat = false;

	#region PRIVATE FUNCTIONS
	
	private bool Exists(EntityAI entity)
    {
        foreach (EntityAI ai in entities)
        {
            if (ai.GetID().Equals(entity.GetID())) return true;
        }
        return false;
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void EnterCombat(EntityAI entity)
    {
        if (!Exists(entity))
        {
            inCombat = true;
            GetGame().SetGameState();
            entities.Add(entity);
            transforms.Add(entity.transform);
        }
    }

    public void ExitCombat()
    {
        inCombat = false;
        GetGame().SetGameState();
        transforms = new List<Transform>();
        entities = new List<EntityAI>();
    }

    public void RemoveEntity(EntityAI entity)
    {
        entities.Remove(entity);
        transforms.Remove(entity.transform);
        if (entities.Count <= 0) ExitCombat();
    }

    public int GetEntities() { return entities.Count; }

    public bool InCombat() { return inCombat; }

    public List<Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < transforms.Count; i++)
        {
            if (transforms[i] != null) positions.Add(transforms[i].position);
        }
        return positions;
    }

    public Transform[] GetTransforms() { return transforms.ToArray(); }
	
	#endregion
}
