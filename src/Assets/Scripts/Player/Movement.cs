
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float speed = 60;
	
	public Camera MainCamera;
	public Vector3 lastPosition;
	CharacterController character;
	public AudioClip runningSound; 
	
	void Awake() {
		MainCamera = Camera.mainCamera.camera;
	}
	
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.(new Vector3(this.transform.position.x, 25, this.transform.position.z));
		
		this.movement();
		
		//play walking sound while moving
		if(lastPosition != this.transform.position){
			if(audio.isPlaying == false){
				audio.clip = runningSound;
				audio.Play ();
			}
		}
		else if(lastPosition == this.transform.position){
			audio.Pause();
		}
		lastPosition = this.transform.position;
	}
	
	void movement(){
		//Move player x and z
	    float x = Input.GetAxis("Horizontal") * Time.smoothDeltaTime * speed;
    	float z = Input.GetAxis("Vertical") * Time.smoothDeltaTime * speed;
		float y = 0;
		if(this.transform.position.y > 25){
			float temp = this.transform.position.y - 25;
			y = this.transform.position.y - temp;
		}
		else if(this.transform.position.y < 25){
			float temp = 25 - this.transform.position.y;
			y = temp * -1;
		}
		character.Move(new Vector3(x,-y,z));
		//Rotate player around y
		Vector3 ScreenMouse;
		ScreenMouse.x = Input.mousePosition.x;
		ScreenMouse.y = Input.mousePosition.y;
		ScreenMouse.z = 1;
		
		Vector3 worldMouse = MainCamera.camera.ScreenToWorldPoint(ScreenMouse);
		
		worldMouse -= this.transform.position;
		
		Vector3 target = new Vector3(-worldMouse.x + this.transform.position.x, this.transform.position.y, -worldMouse.z + this.transform.position.z);
		
		transform.LookAt (target);
		
	}
}
