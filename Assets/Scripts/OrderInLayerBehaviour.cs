using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerBehaviour : MonoBehaviour
{
    [SerializeField] protected List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	#region UNITY FUNCTIONS
	
	protected virtual void Start ()
	{

	}
	
	protected virtual void Update () 
	{
        int y = (int)transform.position.y;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = GetSortingOrder(transform.position, 1);
        }
	}

    #endregion

    #region PRIVATE FUNCTIONS

    private int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = 5000)
    {
        return (int)(baseSortingOrder - position.y) + offset;
    }

    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
