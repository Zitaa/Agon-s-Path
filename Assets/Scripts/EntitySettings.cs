using Game;
using UnityEngine;

[System.Serializable]
public class EntitySettings 
{
    public const int SPEED_MULTIPLIER = 10;

    public void Init(EntityAI entity)
    {
        this.entity = entity;
        Currenthealth = maxHealth.GetValue();
    }

    [Range(0, 30)] public float speed;
    public Stat maxHealth;
    public float Currenthealth { get; protected set; }
    public Stat damage;
    public Stat meleeRange;
    public Stat force;
    public Stat viewRange;
    public LayerMask targetMask;

    private EntityAI entity;

    private GameManager GetGame()
    {
        return GameBehaviour.instance.GetGame();
    }

    public void DecreaseHealth(float amount)
    {
        Currenthealth -= amount;
        if (Currenthealth <= 0)
        {
            GetGame().KillEntity(entity);
        }
    }
}
