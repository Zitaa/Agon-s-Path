using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    private static GameHandler game;
    private void Awake()
    {
        if (game == null) game = this;
        else if (game != null) Destroy(this);
    }

    [SerializeField] private Transform player;

    #region UNITY FUNCTIONS

    private void Update()
    {
        
    }

    #endregion

    #region PRIVATE FUNCTIONS
    #endregion

    #region PUBLIC FUNCTIONS

    public GameManager GetGame()
    {
        return (GameManager)GameManager.Instance;
    }

    #endregion
}
