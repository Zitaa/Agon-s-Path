using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceElement : MonoBehaviour
{
    [SerializeField] private bool userEnabled;
    
	#region UNITY FUNCTIONS
	
	private void Start ()
	{
		
	}
	
	private void Update () 
	{
		
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS

    public void ChangeTextColor(Color newColor)
    {
        Text text = GetComponent<Text>();
        if (text != null)
        {
            text.color = new Color(newColor.r, newColor.g, newColor.b, 1);
        }
    }

    public void Enable()
    {
        userEnabled = true;
        gameObject.SetActive(userEnabled);
    }

    public void Disable()
    {
        userEnabled = false;
        gameObject.SetActive(userEnabled);
    }

    public bool IsUserEnabled()
    {
        return userEnabled;
    }
	
	#endregion
}
