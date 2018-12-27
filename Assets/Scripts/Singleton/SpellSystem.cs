using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpellSystem : Singleton
{
    [SerializeField] private Transform canvas;
    [SerializeField] private int cinematicBarsHeight;

    private GameObject top, bottom;
    private bool isActive;

    #region PRIVATE FUNCTIONS

    private void InitializeCinematicBars()
    {
        top = new GameObject("TopBar", typeof(Image));
        bottom = new GameObject("BottomBar", typeof(Image));

        top.transform.SetParent(canvas, false);
        top.transform.SetAsFirstSibling();
        top.GetComponent<Image>().color = Color.black;
        RectTransform topRect = top.GetComponent<RectTransform>();
        topRect.anchorMin = new Vector2(0, 1);
        topRect.anchorMax = new Vector2(1, 1);
        topRect.sizeDelta = new Vector2(0, cinematicBarsHeight);

        bottom.transform.SetParent(canvas, false);
        bottom.transform.SetAsFirstSibling();
        bottom.GetComponent<Image>().color = Color.black;
        RectTransform bottomRect = bottom.GetComponent<RectTransform>();
        bottomRect.anchorMin = new Vector2(0, 0);
        bottomRect.anchorMax = new Vector2(1, 0);
        bottomRect.sizeDelta = new Vector2(0, cinematicBarsHeight);
    }

    private void TerminateCinematicBars()
    {
        if (top != null && bottom != null)
        {
            MonoBehaviour.Destroy(top);
            MonoBehaviour.Destroy(bottom);
        }
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void Start()
    {
        isActive = true;
        GetGame().SetGameState();
        InitializeCinematicBars();
    }

    public void End()
    {
        isActive = false;
        GetGame().SetGameState();
        TerminateCinematicBars();
    }

    public bool IsActive() { return isActive; }
	
	#endregion
}
