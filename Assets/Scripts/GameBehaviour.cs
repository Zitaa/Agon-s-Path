using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(this);
    }

    [SerializeField] private GameManager game;
	
	#region UNITY FUNCTIONS
	
	private void Start ()
	{
        game.Init();
        StartCoroutine(game.GetDynamicEnvironment().AdvanceTime());
	}
	
	private void Update () 
	{
        game.GetFramesPerSecondSystem().CalculateFrames(Time.unscaledDeltaTime);
        game.GetCameraSettings().CameraMovement();
        game.GetDynamicEnvironment().UpdateWeatherPosition();
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public GameManager GetGame() { return game; }
	
	#endregion
}
