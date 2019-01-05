using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected int ID;

    protected GameManager game;
    protected PlayerEntity player;
    protected float distanceToPlayer;

	#region UNITY FUNCTIONS
	
	protected virtual void Start ()
	{
        game = GameBehaviour.instance.GetGame();
        player = game.GetPlayer().GetComponent<PlayerEntity>();
        ID = Random.Range(1, 1000000);
	}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) player.AddInteractable(this);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) player.RemoveInteractable(this);
    }

    #endregion

    #region PRIVATE FUNCTIONS

    protected virtual void Interact()
    {

    }

    #endregion

    #region PUBLIC FUNCTIONS

    public virtual int GetID() { return ID; }

    public virtual float DistanceToPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        distanceToPlayer = distance;
        return distanceToPlayer;
    }

    #endregion
}
