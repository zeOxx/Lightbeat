using UnityEngine;
using System.Collections;

public class Derp : MonoBehaviour {
	
	public GUISkin myStyle;

	// Use this for initialization
	void Start () {
		Debug.Log("sd234g");
	}
	
	void OnGui() {
		GUI.skin = myStyle;
		GameObject derp = (GameObject)GameObject.FindGameObjectWithTag("Score");
		var herp = derp.GetComponent (typeof(Score)) as Score;
		
		Debug.Log("Score" + herp.FinalScore);
		
		GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height) / 2 -50, 100, 20), "Score: " + herp.FinalScore);
	}
}
