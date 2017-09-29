using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenyScript : MonoBehaviour {
	public GameObject MainPanel;  public GameObject FavoritesPanel;  public GameObject TriggerObj; 
	private float smoothTime = 0.2F;
	private Vector3 velocity = Vector3.zero;
	private float NextPos = 0.0f; private float FavorsPos = 0.0f; private Vector3 SmouthedLR;
	private int AdderA = 10; private int AdderB = 10; private float DragKoof;  

	private int AdderVideo = 100; public GameObject CameraObj;

	public Toggle ButtonMainPanel;  public Toggle ToggleFullScreenPanel; 
	// Use this for initialization
	void Start () { 		
		CameraObj  = GameObject.Find("CameraTarg");
		HidgeFavoritesPanel (); HidgeMainPanel ();  smoothTime = 0.2F;
	}
	
	// Update is called once per frame
	void Update () { DragKoof = 45.11f;
		Vector3 targetPosition = new Vector3(0, FavorsPos, NextPos); //  плавно
		SmouthedLR = Vector3.SmoothDamp(SmouthedLR, targetPosition, ref velocity, smoothTime);

		MainPanel.GetComponent<RectTransform>().offsetMin = new Vector2(SmouthedLR.z*-DragKoof, 100);   		  // влево //  вниз  //
		MainPanel.GetComponent<RectTransform>().offsetMax = new Vector2(SmouthedLR.z*-DragKoof + 430, -8);     // вправо  //  вверх  //

		FavoritesPanel.GetComponent<RectTransform>().offsetMin = new Vector2(SmouthedLR.y*-DragKoof, 60);   		  // влево //  вниз  //
		FavoritesPanel.GetComponent<RectTransform>().offsetMax = new Vector2(SmouthedLR.y*-DragKoof + 430, -95);     // вправо  //  вверх  //

		if (TriggerObj.activeSelf == true) {
			AdderA = AdderA + 1; AdderB = 0;
			if (AdderA == 1) {
				HidgeMainPanel ();
			}
		} else {
			AdderB = AdderB + 1; AdderA = 0;
			if (AdderB == 1) {
				ShowMainPanel ();  
			}
		}

		if ( Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown )
		{ DragKoof = 40.0f; }
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight || Input.deviceOrientation == DeviceOrientation.LandscapeLeft) 
		{  DragKoof = 72.3f; }

		/*AdderVideo = AdderVideo + 1;
		if (AdderVideo == 8) {  
			Renderer[] rendererComponents = CameraObj.GetComponentsInChildren<Renderer> (true);
			Collider[] colliderComponents = CameraObj.GetComponentsInChildren<Collider> (true);
			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}
			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}
		}*/
	}

	public void ShowMainPanel()
	{
		NextPos = 0.0f;  

		Renderer[] rendererComponents = CameraObj.GetComponentsInChildren<Renderer> (true);
		Collider[] colliderComponents = CameraObj.GetComponentsInChildren<Collider> (true);
		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}
		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
		ToggleFullScreenPanel.isOn = true;
	}
	public void HidgeMainPanel()
	{
		NextPos = 10.0f;
	}


	public void HidgeFavoritesPanel()
	{
		FavorsPos = 10.0f; ShowMainPanel ();
	}
	public void ShowFavoritesPanel()
	{
		FavorsPos = 0.0f; HidgeMainPanel (); smoothTime = 0.3F;
	}

	public void ShowVideo()
	{  
		HidgeFavoritesPanel (); HidgeMainPanel (); 
		//ButtonMainPanel.onClick.Invoke();
		ButtonMainPanel.isOn = true;

	
	}

}
