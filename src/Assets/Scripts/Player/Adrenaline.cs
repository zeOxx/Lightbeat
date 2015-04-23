using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Adrenaline : MonoBehaviour {

	public List<GameObject> enemies;
	public float lightDistance;
	public int enemyCount;
	public float lightIntensity;
	public float blackout;
	public float lastBlackout;
	public int lightRange;
	public AudioClip heartbeatSound;
	private bool playSound;
	
	void Start () {

		lightDistance = 200;
		enemyCount = 0;
		lightIntensity = 1;
		lightRange = 250;
		blackout = 0.001f;
		playSound = false;
		lastBlackout = Time.timeSinceLevelLoad;
		this.light.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		lightRange = 250;
		enemyCount = 0;
		//count enemies
		var temp = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < temp.Length; i++){
			enemies.Add(temp[i]);
		}
		
		foreach(GameObject enemy in enemies){
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			if(distance < lightDistance){
				enemyCount++;
			}
		}
		
		lightRange -= enemyCount * 15;
		if(lightRange < 125 ){
			lightRange = 125;
		}
		
		//calculate intensity strength
		if((enemyCount/5) >= lightIntensity && lightIntensity < 2.5f){
			lightIntensity += 5 * Time.deltaTime;
		}
		else if(((enemyCount == 0 || (enemyCount/5) <= lightIntensity)) && lightIntensity > 0.8){
			lightIntensity -= 0.1f;
		}
		
		//increase light after blackout is over
		if(lastBlackout < Time.timeSinceLevelLoad - (blackout * lightRange)){
			if(this.light.intensity < lightIntensity){
				this.light.intensity += (float)(lightIntensity / (0.000005f * Math.Pow(lightRange, 3f)));
				this.light.range = lightRange;
			}
		}
		else if(this.light.intensity >= 0.01){
			this.light.intensity -= (lightIntensity / 10 );
			//lastBlackout = Time.timeSinceLevelLoad;
		}
		
		if(playSound){
			audio.PlayOneShot(heartbeatSound, 1);
			playSound = false;
		}
		//start new blackout
		if(this.light.intensity >= lightIntensity){
			lastBlackout = Time.timeSinceLevelLoad;
			playSound = true;

		}

		enemies.Clear();
	}
	
	
}
