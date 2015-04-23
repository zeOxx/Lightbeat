using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	public GameObject target;
	
	void Awake() {
		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = new Vector3(target.transform.position.x,this.transform.position.y, target.transform.position.z);
	}
}
