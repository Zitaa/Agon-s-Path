using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraBehaviour : Singleton
{
    public void Init()
    {
        player = GetGame().GetPlayer();
    }

    [SerializeField] private Camera camera;
    [SerializeField] private float maxTransitionDistance;
    [SerializeField] private float transitionSmoothening;

    private Transform player;
    private Vector3 targetPos;

	#region PRIVATE FUNCTIONS
	
    private void RoamingCamera()
    {
        targetPos = new Vector3(player.position.x, player.position.y, camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
    }

    private void CombatCamera()
    {
        List<Vector3> positions = GetGame().GetCombatSystem().GetPositions();
        positions.Add(player.position);
        targetPos = GetCentroid(positions.ToArray());
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
    }

    private void SpellCamera()
    {
        /*Vector2 midPoint = ((Vector2)player.position + (Vector2)Input.mousePosition) / 2;
                Vector2 offset = midPoint - (Vector2)player.position;
                offset = Vector2.ClampMagnitude(offset, maxTransitionDistance);
                targetPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, camera.transform.position.z);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);*/
        targetPos = new Vector3(player.position.x, player.position.y, camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, transitionSmoothening);
    }

	#endregion
	
	#region PUBLIC FUNCTIONS
	
    public void CameraMovement()
    {
        GameManager.GameStates state = GetGame().GetGameState();
        switch (state)
        {
            case GameManager.GameStates.Roam:
                RoamingCamera();
                break;
            case GameManager.GameStates.Combat:
                CombatCamera();
                break;
            case GameManager.GameStates.Spell:
                CombatCamera();
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
