using UnityEngine;
using System.Collections;

public class TogleSwith : MonoBehaviour {
	public GameObject ToggObjA; public GameObject ToggObjB;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if(  ToggObjA.activeSelf == true )
		{
			ToggObjB.SetActive(false);
		}
		else{
			ToggObjB.SetActive(true);
		}
	}
}
