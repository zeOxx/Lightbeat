using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed = 10;
	public int rotationSpeed = 10;
	public int maxDistance;
	CharacterController character;
	
	private Transform myTransform;
	
	void Awake() {
		myTransform = transform;
	}
	
	
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		character = GetComponent<CharacterController>();
		maxDistance = 2;   
	}
	
	void Update () {
		Vector3 dir = myTransform.position - target.position;
		dir.y = 0; // kill height differences to avoid enemy tilting
		
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
		
		//if (dir.magnitude > maxDistance) 
		{
		    character.Move(-myTransform.forward * moveSpeed * Time.deltaTime);
		}
		
		float y = 0;
		
		if(this.transform.position.y > 25){
			float temp = this.transform.position.y - 25;
			y = this.transform.position.y - temp;
		}
		else if(this.transform.position.y < 25){
			float temp = 25 - this.transform.position.y;
			y = temp * -1;
		}
		character.Move(new Vector3(0,-y,0));
	}
}