using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centroid : MonoBehaviour
{
    public List<Transform> transforms = new List<Transform>();

    private Camera cam;
    private List<Vector3> points = new List<Vector3>();
	
	#region UNITY FUNCTIONS
	
	private void Start ()
	{
        cam = Camera.main;
	}
	
	private void Update () 
	{
        for (int i = 0; i < transforms.Count; i++)
        {
            points.Add(transforms[i].position);
        }
        cam.transform.position = GetCentroid(points.ToArray());
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private Vector3 GetCentroid(Vector3[] points)
    {
        Vector3 centroid = new Vector3(0, 0, -10);
        foreach (Vector3 point in points)
        {
            centroid += point;
        }
        centroid /= points.Length;
        return centroid;
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	
	
	#endregion
}
