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

        game.Init();
    }

    [SerializeField] private GameManager game;
	
	#region UNITY FUNCTIONS
	
	private void Start ()
	{

	}
	
	private void Update () 
	{
        Application.targetFrameRate = (int)game.GetFramesPerSecondSystem().GetMaxFrames();
        game.GetFramesPerSecondSystem().CalculateFrames(Time.unscaledDeltaTime);
        game.GetCameraSettings().CameraMovement();
        game.GetDynamicEnvironment().UpdateWeatherPosition();
        game.GetDiscord().InvokeCallbacks();
	}

    private void OnDisable()
    {
        GetGame().GetDiscord().Terminate();
    }

    #endregion

    #region PRIVATE FUNCTIONS

    private void Save() { GetGame().GetData().Save(); }

    #endregion

    #region PUBLIC FUNCTIONS

    /*public void RequestRespondYes()
    {
        game.GetDiscord().RequestRespondYes();
    }

    public void RequestRespondNo()
    {
        game.GetDiscord().RequestRespondNo();
    }

    public void ReadyCallback(ref DiscordRpc.DiscordUser connectedUser)
    {
        game.GetDiscord().ReadyCallback(ref connectedUser);
    }

    public void DisconnectedCallback(int errorCode, string message)
    {
        game.GetDiscord().DisconnectedCallback(errorCode, message);
    }

    public void ErrorCallback(int errorCode, string message)
    {
        game.GetDiscord().ErrorCallback(errorCode, message);
    }

    public void JoinCallback(string secret)
    {
        game.GetDiscord().JoinCallback(secret);
    }

    public void SpectateCallback(string secret)
    {
        game.GetDiscord().SpectateCallback(secret);
    }

    public void RequestCallback(ref DiscordRpc.DiscordUser request)
    {
        game.GetDiscord().RequestCallback(ref request);
    }*/ 

    public GameManager GetGame() { return game; }
	
	#endregion
}
