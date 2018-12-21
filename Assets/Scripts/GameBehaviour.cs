using Game;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(this);

        GetGame().Init();
    }

    [SerializeField] private GameManager game;

    #region UNITY FUNCTIONS

    private void Start ()
	{

    }
	
	private void Update () 
	{
        Application.targetFrameRate = (int)GetGame().GetFPSSettings().GetMaxFrames();
        GetGame().GetFPSSettings().CalculateFrames(Time.unscaledDeltaTime);

        if (GetGame().GetFPSSettings().DisplayFrames()) GetGame().GetUI().GetUIElement("FPS").Enable();
        else GetGame().GetUI().GetUIElement("FPS").Disable();
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	

	
	#endregion
	
	#region PUBLIC FUNCTIONS

    public GameManager GetGame()
    {
        return game;
    }
	
	#endregion
}
