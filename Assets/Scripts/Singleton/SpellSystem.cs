using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[System.Serializable]
public class SpellSystem : Singleton
{
    public void Init()
    {
        volume = GetGameBehaviour().GetComponent<PostProcessVolume>();
    }

    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject spellIcon;
    [SerializeField] private List<GameObject> spells = new List<GameObject>();
    [SerializeField] private int cinematicBarsHeight;

    private GameObject top, bottom;
    private PostProcessVolume volume;
    private List<GameObject> spellIcons = new List<GameObject>();
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

    private void SpawnSpellProjectiles()
    {
        GameObject spell = spells[Random.Range(0, 3)];
        for (int i = 0; i < spellIcons.Count; i++)
        {
            Transform player = GetGame().GetPlayer();
            GameObject clone = MonoBehaviour.Instantiate(spell, player.position, Quaternion.identity);

            Vector3 target = spellIcons[i].transform.position;
            target.x = target.x - clone.transform.position.x;
            target.y = target.y - clone.transform.position.y;
            target.z = 0;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void TerminateSpellIcons()
    {
        foreach (GameObject icon in spellIcons)
        {
            Object.Destroy(icon);
        }
        spellIcons = new List<GameObject>();
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void Start()
    {
        isActive = true;
        GetGame().SetGameState();
        InitializeCinematicBars();
        volume.weight = 1;
        Time.timeScale = .2f;
    }

    public void End()
    {
        isActive = false;
        GetGame().SetGameState();
        TerminateCinematicBars();
        volume.weight = 0;
        Time.timeScale = 1;
        SpawnSpellProjectiles();
        TerminateSpellIcons();
    }

    public void AddSpellIcon(GameObject icon)
    {
        spellIcons.Add(icon);
    }

    public bool IsActive() { return isActive; }

    public GameObject GetSpellIcon() { return spellIcon; }

    public SpellProjectile GetSpellProjectile(int index) { return spells[index].GetComponent<SpellProjectile>(); }
	
	#endregion
}
