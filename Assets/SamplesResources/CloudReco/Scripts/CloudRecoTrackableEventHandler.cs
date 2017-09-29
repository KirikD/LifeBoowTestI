/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2010-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary> // CinemaMoskow Most4K  IceWorldStorm
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class CloudRecoTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{   public string url = "http://kirikdk.ru/LifeImageVideo/Img/Catt.jpg";
	public string[] ListOfObjects; // имена обектов с таргетами из вуфории!
	public string LoadBaseTXT;
	public string[] NamesMarkers; 
	public string[] PathVideo; 
	public string[] SizeVideoW;  	public string[] SizeVideoH;  private string NameMark = "d";

	public string[] ImageLogoVideo; public string[] TextDiscriptionVideo;

	public GameObject VideoObj;
	public GameObject ImportVideo; 
	public Renderer RendVideo;
	public  GameObject PlacerObj; public  GameObject PlacerCamm;  public  GameObject  FullScreenTrigger;

	public  GameObject BlackFonn;

	public Toggle FullScreenToggle;

	public  GameObject MenyDublicate; public  GameObject Favors;

	public  GameObject DiscriptImage; public  GameObject DiscriptText;

    #region PUBLIC_MEMBERS
    /// <summary>
    /// The scan-line rendered in overlay when Cloud Reco is in scanning mode.
    /// </summary>
    public ScanLine scanLine;
    #endregion // PUBLIC_MEMBERS



    #region PRIVATE_MEMBERS
    private TrackableBehaviour mTrackableBehaviour;
    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
	{ BlackFonn.SetActive(false);
		StartCoroutine(Check()); // считали текстовый документ сразу при старте приложения

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
		StartCoroutine("LoadImageURL");
    }
	void LateUpdate () {
		try {
			if (FullScreenTrigger.active == true)
			{

			VideoObj.transform.position = PlacerObj.transform.position + new Vector3 (0,0.1f,0); // размер видео равен размеру позиции PlacerCamm
			VideoObj.transform.rotation = PlacerObj.transform.rotation; 
			VideoObj.transform.localScale = PlacerObj.transform.localScale;
				VideoObj.transform.SetParent(PlacerObj.transform.parent); /// childObject.transform.parent.gameObject
				BlackFonn.SetActive(false);
			} else {

			VideoObj.transform.position = PlacerCamm.transform.position;
			VideoObj.transform.rotation = PlacerCamm.transform.rotation; 
				VideoObj.transform.SetParent(PlacerCamm.transform.parent);
				BlackFonn.SetActive(true);
			}
		}	catch 	{	}
	}
	IEnumerator LoadImageURL() {
		// Start a download of the given URL
		WWW www = new WWW(url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
		RawImage RawImag = DiscriptImage.GetComponent<RawImage>();
		RawImag.texture = www.texture;

		//DiscriptImage.GetComponent<RawImage>().texture = 

	}
	#endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.UNKNOWN &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            // Ignore this specific combo
            return;
        }
        else
        {
            OnTrackingLost();
        }
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        // Stop finder since we have now a result, finder will be restarted again when we lose track of the result
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
        {
            objectTracker.TargetFinder.Stop();

            // Stop showing the scan-line
            ShowScanLine(false);
        }
//// важно тут имена маркеров которые трекаем.
		for(int i = 0; i < ListOfObjects.Length; i++)
		{ 
			if (mTrackableBehaviour.TrackableName == NamesMarkers[i] )
			{	if ( NameMark  != NamesMarkers[i])
				{ NameMark = NamesMarkers[i]; 
					// блокируем исполнение этого кода если навели на тот же маркер Application.LoadLevel(Application.loadedLevel);
					try {
						VideoObj = GameObject.Find("ImportedVideo");
						//Destroy(VideoObj);
						VideoObj.name = "OldMoveFalse" ; VideoObj.SetActive(false); 
					} catch {}
				PlayerPrefs.SetString("VideoPathStr", PathVideo[i]);  // путь взять из базы!

				Instantiate(ImportVideo, new Vector3(0, 0, 0), Quaternion.identity); 
				VideoObj = GameObject.Find("VideoManager(Clone)");
				VideoObj.name = "ImportedVideo" ; // NameVidObj = VideoObj.name;
				RendVideo = VideoObj.GetComponent<Renderer>();
				RendVideo.enabled = true;
				VideoObj.transform.SetParent(this.transform);
				VideoObj.transform.position = PlacerObj.transform.position + new Vector3 (0,0.1f,0); // размер видео равен размеру позиции
				VideoObj.transform.rotation = PlacerObj.transform.rotation; 
				VideoObj.transform.localScale = PlacerObj.transform.localScale;

					FullScreenToggle.isOn = true;
				}
			}
		}
		//if (mTrackableBehaviour.TrackableName == "ice" )
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found"); //если трекаемый обджект равен имени из массива то импортируем соответствующий видео префаб.
    }

    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
		
        // Start finder again if we lost the current trackable
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
		{
            objectTracker.TargetFinder.ClearTrackables(false);
            objectTracker.TargetFinder.StartRecognition();

            // Start showing the scan-line
            ShowScanLine(true);
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    private void ShowScanLine(bool show)
    {
        // Toggle scanline rendering
        if (scanLine != null)
        {
            Renderer scanLineRenderer = scanLine.GetComponent<Renderer>();
            if (show)
            {
                // Enable scan line rendering
                if (!scanLineRenderer.enabled)
                    scanLineRenderer.enabled = true;

                scanLine.ResetAnimation();
            }
            else
            {
                // Disable scanline rendering
                if (scanLineRenderer.enabled)
                    scanLineRenderer.enabled = false;
            }
        }
    }
    #endregion //PRIVATE_METHODS
	private IEnumerator Check()
	{
		WWW w = new WWW("http://kirikdk.ru/MainBaseLifeImage.txt");
		yield return w;
		if (w.error != null)
		{
			Debug.Log("Error .. " +w.error);
			// for example, often 'Error .. 404 Not Found'
		}
		else
		{
			Debug.Log("Found ... ==>" +w.text +"<==");
			// don't forget to look in the 'bottom section'
			// of Unity console to see the full text of
			// multiline console messages.
		}
		LoadBaseTXT = w.text;
		string[] Lines = LoadBaseTXT.Split('\n');//splits via a space in the string
		ListOfObjects = new string[Lines.Length]; 
		NamesMarkers = new string[Lines.Length];  PathVideo = new string[Lines.Length];  SizeVideoW = new string[Lines.Length]; SizeVideoH = new string[Lines.Length];
		ImageLogoVideo = new string[Lines.Length];  TextDiscriptionVideo = new string[Lines.Length]; 

		// запомним позицию первого окна перед циклом
		float MenyDxMin = MenyDublicate.GetComponent<RectTransform>().offsetMin.x;
		float MenyDyMin = MenyDublicate.GetComponent<RectTransform>().offsetMin.y;
		float MenyDxMax = MenyDublicate.GetComponent<RectTransform>().offsetMax.x;
		float MenyDyMax = MenyDublicate.GetComponent<RectTransform>().offsetMax.y;
		for(int i = 0; i < ListOfObjects.Length; i++)
		{
			string[] SliptedNum = Lines[i].Split(' ');  //StartCoroutine("LoadImageURL");
			NamesMarkers[i] = SliptedNum[0];
			PathVideo[i] = SliptedNum[1];

			SizeVideoW[i] = SliptedNum[2];
			SizeVideoH[i] = SliptedNum[3];
			// изображение и текст описания с видео
			ImageLogoVideo[i] = SliptedNum[4];  
			TextDiscriptionVideo[i] = SliptedNum[5];

			MenyDublicate.name = "VideoMem" + i ; 
			Instantiate(MenyDublicate, new Vector3(0, 0, 0), Quaternion.identity); 
			MenyDublicate = GameObject.Find( "VideoMem" + i + "(Clone)" );
			MenyDublicate.transform.SetParent( Favors.transform );
			MenyDublicate.GetComponent<RectTransform>().offsetMin = new Vector2(MenyDxMin,  MenyDyMin );   		  // влево //  вниз  //
			MenyDublicate.GetComponent<RectTransform>().offsetMax = new Vector2(MenyDxMax , MenyDyMax );     // вправо  //  вверх  //
			MenyDublicate.transform.localScale  =   new Vector3(1,1,1);


		}
		/* example code to separate all that text in to lines:
         longStringFromFile = w.text
         List<string> lines = new List<string>(
             longStringFromFile
             .Split(new string[] { "\r","\n" },
             StringSplitOptions.RemoveEmptyEntries) );
         // remove comment lines...
         lines = lines
             .Where(line => !(line.StartsWith("//")
                             || line.StartsWith("#")))
             .ToList();
         */
		
	}
}
