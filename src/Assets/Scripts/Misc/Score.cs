using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public int finalScore;
	
	public void Start() {
		finalScore = 0;
		DontDestroyOnLoad(this);
	}
	
	public int FinalScore {
		get { return this.finalScore; }
		set { this.finalScore += value; }
	}
}
