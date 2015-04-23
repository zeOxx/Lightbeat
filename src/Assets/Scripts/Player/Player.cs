using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public List<Weapon> weapons;
	
	public GameObject bulletMesh;
	
	private Camera mainCamera;
	public AudioClip pistolSound;
	
	public void Start() {
		weapons = new List<Weapon>();
		firstWeapon();
		
		mainCamera = Camera.mainCamera.camera;
	}
	
	public void firstWeapon() {
		
		Weapon tempWeapon = new Weapon(true, 0, 10, 10, 50, 2 ,0.1f, 5.0f, 3, 0, bulletMesh);
		
		addWeapon(tempWeapon);
	}
	
	public void addWeapon(Weapon weapon) {
		weapons.Add(weapon);
	}
	
	public void Update() {
		foreach (Weapon w in weapons){
			w.Update();
		}
		
		if (Input.GetMouseButtonDown(0)) {
			for (int i = 0; i < weapons.Count; i++) {
				if (weapons[i].inUse){		
					weapons[i].makeBullet(pistolSound);
				}
			}
		}
	}
}