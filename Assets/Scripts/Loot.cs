using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    [SerializeField] private float lifeTime;

    private List<Item> loot = new List<Item>();

    #region UNITY FUNCTIONS

    protected override void Start()
    {
        base.Start();
        loot = game.GetLootBehaviour().GenerateLoot();
        if (loot.Count <= 0) Invoke("Terminate", 0);
        Invoke("Terminate", lifeTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    #endregion

    #region PRIVATE FUNCTIONS

    protected override void Interact()
    {
        base.Interact();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public override int GetID()
    {
        return base.GetID();
    }

    public void Terminate()
    {
        player.RemoveInteractable(this);
        Destroy(this.gameObject);
    }

    public List<Item> GetLoot() { return loot; }

    #endregion
}
