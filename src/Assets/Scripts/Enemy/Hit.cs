using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hit : MonoBehaviour {
	
	public int health;
		
	
	// Use this for initialization
	void Start () {
		health = 30;
	}
	
	// Update is called once per frame
	void Update () {
		var temp = GameObject.FindGameObjectsWithTag("Projectile");
		
		foreach(GameObject bullet in temp){
			checkDistance(bullet);
		}
	}
	
	private void checkDistance(GameObject bullet)
	{
		//Vector3 dir = this.transform.position - target.transform.position;
		
		float distance = Vector3.Distance(this.transform.position, bullet.transform.position);
		
		if(distance < 10){
			health -= 5;
			Projectile tempScript = bullet.GetComponent(typeof(Projectile)) as Projectile;
			tempScript.hit = true;
			//Destroy(bullet);
		}
	}
}
