using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraBehaviour : Singleton
{
    [SerializeField] private Camera camera;
    [SerializeField] private float maxTransitionDistance;
    [SerializeField] private float transitionSmoothening;

	#region PRIVATE FUNCTIONS
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public void CameraMovement()
    {
        GameManager.GameStates state = GetGame().GetGameState();
        Transform player = GetGame().GetPlayer();
        Vector3 targetPos = new Vector3();

        switch (state)
        {
            case GameManager.GameStates.Idle:
                targetPos = new Vector3(player.position.x, player.position.y, camera.transform.position.z);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
                break;
            case GameManager.GameStates.Combat:
                List<Vector3> positions = GetGame().GetCombatSystem().GetPositions();
                positions.Add(player.position);
                targetPos = GetCentroid(positions.ToArray());
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
                break;
            case GameManager.GameStates.Spell:
                Vector2 midPoint = ((Vector2)player.position + (Vector2)Input.mousePosition) / 2;
                Vector2 offset = midPoint - (Vector2)player.position;
                offset = Vector2.ClampMagnitude(offset, maxTransitionDistance);
                targetPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, camera.transform.position.z);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
                break;
        }
    }

    public float GetTransitionDistance() { return maxTransitionDistance; }

    public float GetTransitionSmoothening() { return transitionSmoothening; }

    public Vector3 GetCentroid(Vector3[] points)
    {
        Vector3 centroid = new Vector3(0, 0, -10);
        foreach (Vector3 point in points)
        {
            centroid += point;
        }
        centroid /= points.Length;
        return centroid;
    }

    public Camera GetCamera() { return camera; }
	
	#endregion
}
