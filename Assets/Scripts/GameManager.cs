using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [System.Serializable]
    public class GameManager : SingletonPattern
    {
        public void Init()
        {
            GetFPSSettings().Init();
            GetUI().EnableUI();
        }

        [SerializeField] private Transform player;
        [SerializeField] FramesPerSecondSettings FPS;
        [SerializeField] UserInterface UI;

        #region PRIVATE FUNCTIONS



        #endregion

        #region PUBLIC FUNCTIONS

        public Transform GetPlayer()
        {
            return player;
        }

        public FramesPerSecondSettings GetFPSSettings()
        {
            return FPS;
        }

        public UserInterface GetUI()
        {
            return UI;
        }

        #endregion
    }

    [System.Serializable]
    public class FramesPerSecondSettings : SingletonPattern
    {
        public void Init()
        {
            framesDisplay = GetGame().GetUI().GetUIElement("FPS").GetComponent<Text>();
        }

        [SerializeField][Range(0, 250)] private float maxFrames;
        [SerializeField] private float frames;
        [SerializeField] private bool displayFrames;

        [SerializeField] private float goodFrames;
        [SerializeField] private Color goodFramesColor;

        [SerializeField] private float okFrames;
        [SerializeField] private Color okFramesColor;

        [SerializeField] private float badFrames;
        [SerializeField] private Color badFramesColor;

        private Text framesDisplay;

        private float deltaTime;

        public void CalculateFrames(float unscaledDeltaTime)
        {
            deltaTime += (unscaledDeltaTime - deltaTime) * .1f;
            frames = 1.0f / deltaTime;

            if (frames > maxFrames) frames = maxFrames;
            framesDisplay.text = frames.ToString();

            if (frames >= goodFrames) GetGame().GetUI().GetUIElement("FPS").ChangeTextColor(goodFramesColor);
            else if (frames < goodFrames && frames >= okFrames) GetGame().GetUI().GetUIElement("FPS").ChangeTextColor(okFramesColor);
            else if (frames < badFrames) GetGame().GetUI().GetUIElement("FPS").ChangeTextColor(badFramesColor);
        }

        public bool DisplayFrames()
        {
            return displayFrames;
        }

        public float GetFrames()
        {
            return frames;
        }

        public float GetMaxFrames()
        {
            return maxFrames;
        }
    }
}
