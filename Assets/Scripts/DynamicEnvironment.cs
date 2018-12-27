using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DynamicEnvironment : Singleton
{	
    public void Init()
    {
        text = GetGame().GetUserInterface().GetUIElement<Text>("Time");
        
        rain = rainEffect.GetComponent<ParticleSystem>();
        groundEffect = rainEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
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

	#region UNITY FUNCTIONS

    public IEnumerator AdvanceTime()
    {
        while (true)
        {
            currentTime += 1;
            if (currentTime > dayLength) currentTime = 0;

            int hours = Mathf.RoundToInt(currentTime / 60);
            int minutes = currentTime % 60;

            string _hours = (hours < 10) ? string.Format("0{0}", hours) : hours.ToString();
            string _minutes = (minutes < 10) ? string.Format("0{0}", minutes) : minutes.ToString();
            text.text = string.Format("{0}:{1}", _hours, _minutes);

            if (hours >= 18) sun.intensity -= .005f;
            else if (hours >= 6 && sun.intensity < 1) sun.intensity += .005f;

            ParticleSystem.MainModule main = rain.main;
            main.simulationSpeed = (cycleSpeed >= 1) ? 1 : 1 / cycleSpeed;

            yield return new WaitForSeconds(cycleSpeed);
        }
    }
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void UpdateWeatherPosition()
    {
        Vector3 cameraPos = GetGame().GetCameraSettings().GetCamera().transform.position;
        Vector3 pos = new Vector3(cameraPos.x, cameraPos.y, -.3f);
        rainEffect.transform.position = pos;
    }
	
	#endregion
}
