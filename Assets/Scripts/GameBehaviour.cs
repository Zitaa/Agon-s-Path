using Game;
using System.Collections;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(this);

        GetGame().Init();
    }

    [SerializeField] private GameManager game;

    private InputManager input;
    private Event KeyEvent;
    private KeyCode newKey;
    private bool waitingForKey = false;

    #region UNITY FUNCTIONS

    private void Start ()
	{
        input = GetGame().GetInputSettings();
    }
	
	private void Update () 
	{
        Application.targetFrameRate = (int)GetGame().GetFPSSettings().GetMaxFrames();
        GetGame().GetFPSSettings().CalculateFrames(Time.unscaledDeltaTime);

        if (GetGame().GetFPSSettings().DisplayFrames()) GetGame().GetUI().GetUIElement("FPS").Enable();
        else GetGame().GetUI().GetUIElement("FPS").Disable();
	}

    private void OnGUI()
    {
        if (KeyEvent.isKey && waitingForKey)
        {
            newKey = KeyEvent.keyCode;
            waitingForKey = false;
        }
    }

    private IEnumerator WaitForKey()
    {
        while (!KeyEvent.isKey) yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();

        switch (keyName)
        {
            case "WALK UP":
                input.Up = newKey;
                break;
            case "WALK RIGHT":
                input.right = newKey;
                break;
            case "WALK DOWN":
                input.down = newKey;
                break;
            case "WALK LEFT":
                input.left = newKey;
                break;
            case "MELEE ATTACK":
                input.meleeAttack = newKey;
                break;
            case "SPELL ATTACK":
                input.spellActivator = newKey;
                break;
            case "SPELL ACTIVATOR":
                input.spellActivator = newKey;
                break;
        }


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

    #region UI FUNCTIONS

    public void ChangeKeyBinding(string keyName)
    {
        if (!waitingForKey) StartCoroutine(AssignKey(keyName));
    }

    #endregion
}
