using UnityEngine;
using System.Collections;

public class HelpV2Drag : MonoBehaviour {

	public GameObject HelpP1; 	public GameObject HelpP2; private float NextPos = 0.0f; private Vector3 SmouthedLR;

	// ниже сглаживание
	//public Transform target;
	private float smoothTime = 0.2F;
	private Vector3 velocity = Vector3.zero; private float DragKoof; private int AdderFstPuskk = 0; 
	// Use this for initialization

	public Vector2 TochPos;  	public Vector2 OldTochPos; public float deltaT;  public float deltaTxDir; public float SmouthDirect; public float FinalDir;
	private int AdderC; private int AdderD; private int SwipeIntDir; private int TimerResetDir; private int TimerAdder;
	void Awake() {
		Screen.orientation = ScreenOrientation.Portrait;
		HelpP1.active = false;
		HelpP2.active = false;
		if (PlayerPrefs.GetString ("FstSaveH") != "FstH") {
			HelpP1.active = true;
			HelpP2.active = true;
			Screen.orientation = ScreenOrientation.Portrait;
		} 
	}
	void Start () {
		//NextPos = 40;
		smoothTime = 0.4F; 
		//Screen.orientation = ScreenOrientation.AutoRotation;
		FinalDir = 140; SmouthDirect = 10;
	}
	
	// Update is called once per frame
	void Update () {  
		AdderFstPuskk = AdderFstPuskk + 1;
		if (AdderFstPuskk == 5) {
			// выполнить эту строчку только единожды при первом запуске
			if (PlayerPrefs.GetString ("FstSaveH") != "FstH") {
				HelpP1.active = true;
				HelpP2.active = true;
				Screen.orientation = ScreenOrientation.Portrait;
			} else {
				Screen.orientation = ScreenOrientation.AutoRotation;
				HelpP1.active = false;
				HelpP2.active = false;
			}
			PlayerPrefs.SetString ("FstSaveH", "FstH"); 
		}

		DragKoof = 80;
		Vector3 targetPosition = new Vector3(0, 0, NextPos); //  плавно
		SmouthedLR = Vector3.SmoothDamp(SmouthedLR, targetPosition, ref velocity, smoothTime);

		HelpP1.GetComponent<RectTransform>().offsetMin = new Vector2(SmouthedLR.z*-DragKoof, 0);     // влево //  вниз  //
		HelpP1.GetComponent<RectTransform>().offsetMax = new Vector2(SmouthedLR.z*-DragKoof, 0);     // вправо  //  вверх  //

		HelpP2.GetComponent<RectTransform>().offsetMin = new Vector2(SmouthedLR.z*-DragKoof+800, 0);     // влево //  вниз  //
		HelpP2.GetComponent<RectTransform>().offsetMax = new Vector2(SmouthedLR.z*-DragKoof+800, 0);     // вправо  //  вверх  //

		if ( Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown )
		{ DragKoof = 80.0f; }
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight || Input.deviceOrientation == DeviceOrientation.LandscapeLeft) 
		{  DragKoof = 72.3f + (72.3f/2); }
	
	// свайп модуль
		if (NextPos > 9) {
			for (var i = 0; i < Input.touchCount; ++i) {
				Touch touch = Input.GetTouch (i);

				if (touch.phase == TouchPhase.Began) {
					if (touch.position.x > Screen.width) {
						//do something
					}
				}
				TochPos = touch.position;
			}
			TochPos = Input.mousePosition;
			//deltaT = Mathf.Sqrt ( (TochPos.x - OldTochPos.x)*(TochPos.x - OldTochPos.x)  );
			deltaTxDir = TochPos.x - OldTochPos.x;
			OldTochPos = Input.mousePosition;

			SmouthDirect = (SmouthDirect + deltaTxDir) * 0.9f;
			if (SmouthDirect > 100 || SmouthDirect < -100) {
				SmouthDirect = 0;
			}
			FinalDir = (FinalDir + SmouthDirect) * 0.98f;
			SwipeIntDir = 0;
			if (FinalDir > 180) {	
				AdderC = AdderC + 1;
				AdderD = 0; 
				if (AdderC == 1) {
					SwipeIntDir = -1;
					PadgesSwipe ();
					TimerResetDir = 1;
				}

			}
			if (FinalDir < -180) {	
				AdderD = AdderD + 1;
				AdderC = 0;
				if (AdderD == 1) {
					SwipeIntDir = 1;
					PadgesSwipe ();
					TimerResetDir = 1;
				}
			}

			if (TimerResetDir == 1) {
			
				TimerAdder = TimerAdder + 1;
				if (TimerAdder == 20) {
					TimerResetDir = 0;
					TimerAdder = 0; 
					AdderC = 0;
					AdderD = 0;
					FinalDir = 0;
					SmouthDirect = 0;
				}
		
			}
		}// свайп модлуь
		if (NextPos == 40) {
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
	}
	void LateUpdate () {
		if (NextPos == 40) {
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
	}

	public void PadgesSwipe()
	{
		if (SwipeIntDir == 1) { Debug.Log ("Left" + FinalDir);
			NextPos = NextPos + 10.0f;
		}
		if (SwipeIntDir == -1) { Debug.Log ("Right" + FinalDir);
			NextPos = NextPos - 10.0f;
		}
		if (NextPos < 0) { NextPos = 0; }
		if (NextPos > 15) { NextPos = 40; }
	}
	public void NextP2()
	{
		NextPos = 10.0f;
	}
	public void NextToStartGame()
	{
		NextPos = 20.0f;
		Screen.orientation = ScreenOrientation.AutoRotation;
	}

	public void ShowHelpPanel()
	{
		HelpP1.active = true;
		HelpP2.active = true;
		NextPos = 0.0f; Screen.orientation = ScreenOrientation.Portrait;
	}
}
