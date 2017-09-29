using UnityEngine;
using System.Collections;

public class PlayOnSow : MonoBehaviour {
	public MediaPlayerCtrl scrMedia;
	public int VideoPosPlay;
	public int AdderPl = 0; public int AdderSt = 0;
	// Use this for initialization
	void Start () {
		scrMedia.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		if ( this.GetComponent<Renderer> ().enabled == true) {
			AdderPl = AdderPl + 1; AdderSt = 0;
			if (AdderPl == 1) 
			{
				scrMedia.Play(); 
			}
			//scrMedia.Play();
			//VideoPosPlay = scrMedia.GetSeekPosition();
			//if (VideoPosPlay < 3)
			///{        scrMedia.Play();   
				//FindObjPoser = GameObject.Find("FindObjPoser");
			//}
		}
		else
		{

			AdderPl = 0; AdderSt = AdderSt + 1;
			if (AdderSt == 1) 
			{
			scrMedia.Pause();
			}
		}
	}
}
