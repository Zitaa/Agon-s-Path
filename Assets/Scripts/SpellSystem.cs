using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using CodeMonkey.Utils;
using UnityEngine.UI;

[System.Serializable]
public class SpellSystem : SingletonPattern
{
    [SerializeField] private Transform UITransform;
    [SerializeField] private PostProcessVolume PPVolume;
    [SerializeField] private GameObject spellIcon;
    [SerializeField] private float maxDistance;
    [SerializeField] private float smoothTime;
    [SerializeField] private bool spellActive = false;

    private GameObject top, bottom;
    private ColorGrading CG;
    private List<GameObject> spellIcons = new List<GameObject>();

    #region PRIVATE FUNCTIONS

    private void AnimateSpellEffect(float start, float end, float animateTime)
    {
        float time = 0f;
        FunctionUpdater.Create(() =>
        {
            time += Time.unscaledDeltaTime / animateTime;
            PPVolume.weight = Mathf.Lerp(start, end, animateTime);
            return time >= 1f;
        });
    }

    private void AnimateTimeEffect(float start, float end, float animateTime)
    {
        float time = 0f;
        FunctionUpdater.Create(() =>
        {
            time += Time.unscaledDeltaTime / animateTime;
            Time.timeScale = Mathf.Lerp(start, end, animateTime);
            return time >= 1f;
        });
    }

    private void SpawnSpellProjectiles()
    {
        int spellIndex = Random.Range(0, 3);
        for (int i = 0; i < spellIcons.Count; i++)
        {
            Transform player = GetGame().GetPlayer();
            GameObject spell = GetGame().GetMisc().spells[spellIndex];
            GameObject clone = Object.Instantiate(spell, player.position, Quaternion.identity);

            Vector3 target = spellIcons[i].transform.position;
            target.x = target.x - clone.transform.position.x;
            target.y = target.y - clone.transform.position.y;
            target.z = 0;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void InitializeCinematicBars()
    {
        top = new GameObject("TopBar", typeof(Image));
        bottom = new GameObject("BottomBar", typeof(Image));

        top.transform.SetParent(UITransform, false);
        top.transform.SetAsFirstSibling();
        top.GetComponent<Image>().color = Color.black;
        RectTransform topRect = top.GetComponent<RectTransform>();
        topRect.anchorMin = new Vector2(0, 1);
        topRect.anchorMax = new Vector2(1, 1);
        topRect.sizeDelta = new Vector2(0, 120);

        bottom.transform.SetParent(UITransform, false);
        bottom.transform.SetAsFirstSibling();
        bottom.GetComponent<Image>().color = Color.black;
        RectTransform bottomRect = bottom.GetComponent<RectTransform>();
        bottomRect.anchorMin = new Vector2(0, 0);
        bottomRect.anchorMax = new Vector2(1, 0);
        bottomRect.sizeDelta = new Vector2(0, 120);
    }

    private void TerminateCinematicBars()
    {
        if (top != null && bottom != null)
        {
            MonoBehaviour.Destroy(top);
            MonoBehaviour.Destroy(bottom);
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

    public void Init()
    {
        PPVolume.profile.TryGetSettings(out CG);
        CG.enabled.value = spellActive;
    }

    public void Start()
    {
        InitializeCinematicBars();
        Time.timeScale = .2f;
        spellActive = true;
        CG.enabled.value = spellActive;
        GetGame().GetMisc().ChangeGameState(Game.GameStates.SPELL);
    }

    public void End()
    {
        TerminateCinematicBars();
        Time.timeScale = 1f;
        spellActive = false;
        CG.enabled.value = spellActive;
        SpawnSpellProjectiles();
        TerminateSpellIcons();
        GetGame().GetMisc().ChangeGameState(Game.GameStates.IDLE);
    }

    public void AddSpellIcon(GameObject icon)
    {
        spellIcons.Add(icon);
    }

    public bool IsActive()
    {
        return spellActive;
    }

    public GameObject GetSpellIcon()
    {
        return spellIcon;
    }

    #endregion
}
