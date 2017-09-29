using UnityEngine;
using System.Collections;

// Get the latest webcam shot from outside "Friday's" in Times Square
public class LoadTexture : MonoBehaviour {
	public string url = "http://kirikdk.ru/LifeImageVideo/Img/Catt.jpg";

	void Start () { 		
		StartCoroutine("LoadImageURL");
	}

	IEnumerator LoadImageURL() {
		// Start a download of the given URL
		WWW www = new WWW(url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;
	}
}