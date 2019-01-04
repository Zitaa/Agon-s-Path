using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DynamicEnvironment : Singleton
{	
    public void Init(int time)
    {
        currentTime = time;
        text = GetGame().GetUserInterface().GetUIElement<Text>("Time");
        rain = rainEffect.GetComponent<ParticleSystem>();
        groundEffect = rainEffect.transform.GetChild(0).GetComponent<ParticleSystem>();

        GameBehaviour.instance.StartCoroutine(AdvanceTime());
    }

    [Header("Time Settings")]
    [SerializeField] Light sun;
    [SerializeField] private int currentTime;
    [SerializeField] private float cycleSpeed;
    [SerializeField] private int dayStart;
    [SerializeField] private int nightStart;

    [Header("Weather Settings")]
    [SerializeField] private GameObject rainEffect;

    private Text text;
    private ParticleSystem rain;
    private ParticleSystem groundEffect;
    private int dayLength = 1440;
    private string timeString;

	#region UNITY FUNCTIONS

    public IEnumerator AdvanceTime()
    {
        CalculateSunIntensity();
        while (true)
        {
            currentTime += 1;
            if (currentTime > dayLength) currentTime = 0;

            int hours = Mathf.RoundToInt(currentTime / 60);
            int minutes = currentTime % 60;

            string _hours = (hours < 10) ? string.Format("0{0}", hours) : hours.ToString();
            string _minutes = (minutes < 10) ? string.Format("0{0}", minutes) : minutes.ToString();
            timeString = string.Format("{0}:{1}", _hours, _minutes);
            text.text = timeString;

            if (hours >= 18) sun.intensity -= .005f;
            else if (hours >= 6 && sun.intensity < 1) sun.intensity += .005f;

            ParticleSystem.MainModule main = rain.main;
            main.simulationSpeed = (cycleSpeed >= 1) ? 1 : 1 / cycleSpeed;

            yield return new WaitForSeconds(cycleSpeed / Time.timeScale);
        }
    }
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private void CalculateSunIntensity()
    {
        if (currentTime > (18*60) && currentTime < (21*60+20))
        {
            int timeDifference = currentTime - (18*60);
            float intensityDifference = .005f * timeDifference;
            sun.intensity = 1;
            sun.intensity -= intensityDifference;
        }
        else if (currentTime > (6*60) && currentTime < (9*60+20))
        {
            int timeDifference = currentTime - (6*60);
            float intensityDifference = .005f * timeDifference;
            sun.intensity = 0;
            sun.intensity += intensityDifference;
        }
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void UpdateWeatherPosition()
    {
        Vector3 cameraPos = GetGame().GetCameraSettings().GetCamera().transform.position;
        Vector3 pos = new Vector3(cameraPos.x, cameraPos.y, -.3f);
        rainEffect.transform.position = pos;
    }

    public string GetTimeString() { return timeString; }

    public int GetTime() { return currentTime; }
	
	#endregion
}
