using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public int enemyWave = 1;
	public int score = 0;
	public int playerMaxHealth = 100;
	public int playerCurrentHealth = 100;
	public float healthBarLength = 0;
	
	private bool dead;
	private float elapsed;
	
	public GUISkin myStyle;
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 2;
		Score = -500;
		dead = false;
		//Debug.Log(playerCurrentHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCurrentHealth > playerMaxHealth){
			playerCurrentHealth = playerMaxHealth;	
		}
		if (dead) {
			elapsed += Time.deltaTime;
			
			if (elapsed > 10)
				Application.LoadLevel("LightBeat");
		}
		else if(playerCurrentHealth < 1){
			playerCurrentHealth = 0;
			Screen.showCursor = true;
			
			var temp = (GameObject)GameObject.FindGameObjectWithTag("Score");
			var temp2 = temp.GetComponent(typeof(Score)) as Score;
			
			temp2.FinalScore = Score;
			dead = true;
			
			//Application.LoadLevel("EndScreen");
		}
	}
	
	void OnGUI () {
		GUI.skin = myStyle;
		healthBarLength = (Screen.width / 2) * (playerCurrentHealth / (float)playerMaxHealth);
		GUI.Box(new Rect(10,10,healthBarLength, 20), playerCurrentHealth + "/" + playerMaxHealth);
		GUI.Label(new Rect(Screen.width - 110, 10, 100, 20), "Score: " + Score);
		
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		var weapon = player.GetComponent(typeof(Player)) as Player;
		
		int maxAmmo = weapon.weapons[0].maxAmmo;
		int currentAmmo= weapon.weapons[0].ammo;
		
		
		GUI.Label(new Rect(Screen.width - 240, 10, 100, 20), "Ammo: " + currentAmmo + " / " + maxAmmo);
		
		if (dead) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height /2 - 10, 100, 20), "DEAD");
		}
	}
	
	public int PlayerCurrentHealth {
		get{ return playerCurrentHealth; }
		set{ playerCurrentHealth += value; }
	}
	
	public int Score {
		get { return this.score; }
		set { this.score += value; }
	}
}

