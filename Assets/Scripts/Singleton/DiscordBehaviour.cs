using System.Collections.Generic;
using DiscordRPC;
using DiscordRPC.Logging;
using UnityEngine;

[System.Serializable]
public class DiscordBehaviour : Singleton
{
    public void Init()
    {
        client = new DiscordRpcClient(appID, steamID, false, (int)pipe, new DiscordRPC.Unity.DiscordNativeNamedPipe());
        DiscordPresence presence = new DiscordPresence();
        
        client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
        
        client.OnReady += (sender, e) =>
        {
            Debug.Log(string.Format("Received Ready from user {0}", e.User.Username));
        };

        client.OnPresenceUpdate += (sender, e) =>
        {
            Debug.Log(string.Format("Received Update! {0}", e.Presence));
        };

        client.Initialize();

        client.SetPresence(new DiscordPresence()
        {
            state = "Roaming",
            details = string.Empty,
            smallAsset = new DiscordAsset() { image = "smallimage" },
            largeAsset = new DiscordAsset() { image = "largeimage" },
            startTime = new DiscordTimestamp(Time.realtimeSinceStartup)
        }.ToRichPresence());
    }

    public enum DiscordPipes
    {
        FirstAvailable = -1,
        Pipe0 = 0,
        Pipe1 = 1,
        Pipe2 = 2,
        Pipe3 = 3,
        Pipe4 = 4,
        Pipe5 = 5,
        Pipe6 = 6,
        Pipe7 = 7,
        Pipe8 = 8,
        Pipe9 = 9
    };

    [SerializeField] private DiscordRpcClient client;
    [SerializeField] private string appID;
    [SerializeField] private string steamID;
    [SerializeField] private DiscordPipes pipe = DiscordPipes.FirstAvailable;

	#region UNITY FUNCTIONS
	
	private void Start ()
	{
		
	}
	
	private void Update () 
	{
		
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void InvokeCallbacks() { client.Invoke(); }

    public void Terminate() { client.Dispose(); }

    public void UpdateState(GameManager.GameStates state)
    {
        string stateString = "";
        switch (state)
        {
            case GameManager.GameStates.Roam:
                stateString = "Roaming";
                break;
            case GameManager.GameStates.Combat:
                stateString = "Battling";
                break;
            case GameManager.GameStates.Spell:
                stateString = "Casting spells";
                break;
        }
        client.UpdateState(stateString);
    }

    #endregion
}
