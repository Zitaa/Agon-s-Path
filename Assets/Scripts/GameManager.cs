using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Game
{
    public enum GameStates
    {
        IDLE = 0,
        COMBAT = 1,
        SPELL = 2
    };

    [System.Serializable]
    public class GameManager : SingletonPattern
    {
        [SerializeField] private uint entities = 0;

        public void Init()
        {
            GetFPSSettings().Init();
            GetUI().Init();
            GetSpellSystem().Init();
        }

        [SerializeField] private Camera camera;
        [SerializeField] private Transform player;
        [SerializeField] private FramesPerSecondSettings FPS;
        [SerializeField] private UserInterface UI;
        [SerializeField] private CombatSystem combatSystem;
        [SerializeField] private SpellSystem spellSystem;
        [SerializeField] private Miscellaneous miscellaneous;

        #region PRIVATE FUNCTIONS



        #endregion

        #region PUBLIC FUNCTIONS

        public void IncreaseEntities() { entities++; }

        public void KillEntity(EntityAI entity) { MonoBehaviour.Destroy(entity.gameObject); }

        public uint GetEntities() { return entities; }

        public Camera GetCamera() { return camera; }

        public Transform GetPlayer() { return player; }

        public FramesPerSecondSettings GetFPSSettings() { return FPS; }

        public UserInterface GetUI() { return UI; }

        public CombatSystem GetCombatSystem() { return combatSystem; }

        public SpellSystem GetSpellSystem() { return spellSystem; }

        public Miscellaneous GetMisc() { return miscellaneous; }

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

    [System.Serializable]
    public class Miscellaneous : SingletonPattern
    {
        [SerializeField] private GameStates currentState;

        public List<GameObject> spells = new List<GameObject>();

        public void ChangeGameState(GameStates state)
        {
            if (currentState == state) return;

            currentState = state;
            switch (currentState)
            {
                case GameStates.IDLE:
                    break;
                case GameStates.COMBAT:
                    break;
                case GameStates.SPELL:
                    break;
            }
        }

        public GameStates GetState() { return currentState; }
    }
}
