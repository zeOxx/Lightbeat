using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float timeActive;
	public float maxTimeActive;
	public GameObject derp;
	public bool hit;
	
	private Vector3 direction;
	private Vector3 target;
	private int speed;
	
	public Vector3 Direction {
		get { return this.direction; }
		set { this.direction = value; }
	}
	
	public int Speed {
		get { return this.speed; }
		set { this.speed = value; }
	}
	
	public void Awake() {
		derp = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void Update() {
		timeActive += Time.deltaTime;

		direction = new Vector3(direction.x, 0, direction.z).normalized;
		direction.y = 0;
		
		this.transform.position += Direction * speed;
	}
}
