using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : OrderInLayerBehaviour
{
    [SerializeField] [Range(0, 1)] private float opacity;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();   
	}
	
	protected override void Update () 
	{
        base.Update();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) DecreaseTransperancy();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) IncreaseTransperancy();
    }

    #endregion

    #region PRIVATE FUNCTIONS

    private void DecreaseTransperancy()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Color color = spriteRenderers[i].material.color;
            color.a = opacity;
            spriteRenderers[i].material.color = color;
        }
    }

    private void IncreaseTransperancy()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Color color = spriteRenderers[i].material.color;
            color.a = 1f;
            spriteRenderers[i].material.color = color;
        }
    }

    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
