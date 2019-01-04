using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FramesPerSecondSystem : Singleton
{
    public void Init()
    {
        text = GetGame().GetUserInterface().GetUIElement<Text>("FPS");
    }

    [SerializeField] private float maxFrames;
    [SerializeField] private float frames;

    private Text text;
    private float deltaTime;

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void CalculateFrames(float unscaledDeltaTime)
    {
        deltaTime += (unscaledDeltaTime - deltaTime) * .1f;
        frames = 1f / deltaTime;
        text.text = frames.ToString("0");
    }

    public float GetFrames() { return frames; }

    public float GetMaxFrames() { return maxFrames; }

	#endregion
}
