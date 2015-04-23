using UnityEngine;
using System.Collections;

public class Item {
	
	public Weapon weapon;
	public GameObject mehs;
	public GameObject bullet;
	public bool picked;
	public bool destroyed;
	public float active; 		// time active
	
	private int type;		// 0 == ammo, 1 == weapon, 2 == obstacle, 3 == medic
	private int val;
	private Vector3 pos;
	private GameObject p1;
	Player playerScript;
	PlayerHealth ph;
	CharacterController character;
	public GameObject lightGameObject;
	
	public Item(int type, int val, Weapon weapon, GameObject mehs, GameObject bullet) {
		this.type 	= type;
		this.val 	= val;
		this.weapon = weapon;
		this.mehs 	= mehs;
		this.bullet = bullet;
		picked 		= false;
		
		active = 0;
		
		p1 = GameObject.FindGameObjectWithTag("Player") as GameObject;
		ph = p1.GetComponent(typeof(PlayerHealth)) as PlayerHealth;
		playerScript = p1.GetComponent(typeof(Player)) as Player;
	}
	
	public void setPosition(Vector3 pos) {
		this.pos = pos;
		mehs.transform.position = pos;
		
		if (type != 2) {
		 	lightGameObject = new GameObject("ObjectLight");
	        lightGameObject.AddComponent<Light>();
	        lightGameObject.light.color = Color.white;
			lightGameObject.light.range = 20.0f;
			lightGameObject.light.intensity = 8f;
	        lightGameObject.transform.position = new Vector3(pos.x, pos.y + 20, pos.z);
		}
		
		mehs = (GameObject)GameObject.Instantiate(mehs, pos, Quaternion.identity);
	}
	
	public void assignMaterial(Material material) {
		mehs.renderer.material = material;
	}
	
	public void Update() {
		active += Time.deltaTime;
		
		float distance = Vector3.Distance(pos, p1.transform.position);
		
		if (type != 2) {
			if (distance < 30) {
				if (type == 0) {
					for (int i = 0; i < playerScript.weapons.Count; i++) {
						if (playerScript.weapons[i].inUse) {
							if (playerScript.weapons[i].ammo < playerScript.weapons[i].maxAmmo) {
								playerScript.weapons[i].gainAmmo(val);
								picked = true;
							}
						}
					}
				}
				else if (type == 1) {
					playerScript.addWeapon (weapon);
					picked = true;
				}
				else if (type == 3) {
					ph.PlayerCurrentHealth = val;
					picked = true;
				}
			}
		}
	}
}
