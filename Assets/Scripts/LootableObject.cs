using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    [SerializeField] private float radius;

    private GameManager game;
    private List<Item> loot = new List<Item>();

	#region UNITY FUNCTIONS
	
	private void Start ()
	{
        game = GameBehaviour.instance.GetGame();
        loot = game.GetLootBehaviour().GenerateLoot();
        if (loot.Count <= 0) Destroy(this.gameObject);
	}
	
	private void Update () 
	{
        float distance = Vector2.Distance(transform.position, game.GetPlayer().position);
        if (distance <= radius)
        {
            if (game.GetInput().GetKeyUp(KeyCode.E) && game.GetLootBehaviour().IsActive())
            {
                print("Adding " + loot.Count + " items.");
                for (int i = 0; i < loot.Count; i++)
                {
                    game.GetInventory().AddItem(loot[i]);
                }
                game.GetLootBehaviour().Disable();
                Destroy(this.gameObject);
            }
            else if (game.GetInput().GetKeyUp(KeyCode.E))
            {
                if (!game.GetLootBehaviour().IsActive()) game.GetLootBehaviour().Enable(loot);
                else game.GetLootBehaviour().Disable();
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
