using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon {
	
	public int damage;
	public int level;
	public int ammo;
	public int xpNeeded;
	public GameObject bulletMesh;
	public GameObject derp;
	public bool inUse;
	
	private Camera mainCamera;
	private int maxLevel;
	public int maxAmmo;
	private int ammoIncrement;	// <- this is unique for every weapon
	private int xp;				//  0,      1,       2,		3
	private int bulletSpeed;
	private float bulletMaxActiveTime;
	private float timeBetweenShots;	// <- in seconds.
	private float elapsed;			// also seconds.
	private enum weaponNames { pistol, shotgun, ak, crossbow, grenadeL, UZI, grenade }
	private enum projectileTypes { bullet, rocket, grenade, none };
	private weaponNames weaponName;
	private projectileTypes pType;
	private List<GameObject> bullets;
	private GameObject iSpawnObj;
	private ItemSpawner iSpawn; 
	
	public Weapon(bool inUse, int weaponName, int damage, int ammo, int maxAmmo, int ammoIncrement, float timeBetweenShots, float bulletMaxActiveTime, int bulletSpeed, int projectileType, GameObject bulletMesh) {
		this.damage 				= damage;
		this.ammo 					= ammo;	
		this.maxAmmo 				= maxAmmo;
		this.ammoIncrement 			= ammoIncrement;
		this.timeBetweenShots 		= timeBetweenShots;
		this.bulletMaxActiveTime 	= bulletMaxActiveTime;
		this.bulletSpeed 			= bulletSpeed;
		this.bulletMesh 			= bulletMesh;
		this.inUse 					= inUse;
		
		derp = GameObject.FindGameObjectWithTag("Player");
		iSpawnObj = GameObject.FindGameObjectWithTag("ItemSpawner") as GameObject;
		iSpawn = iSpawnObj.GetComponent(typeof(ItemSpawner)) as ItemSpawner;
		
		mainCamera = Camera.mainCamera.camera;
		
		elapsed = 0;
		bullets = new List<GameObject>();
		
		maxLevel 	= 5;
		xp 			= 0;
		xpNeeded 	= 50;
		
		// Checks to see if the projectileType value is valid
		if (projectileType < 0)
			pType = projectileTypes.bullet;
		else if (projectileType > 3)
			pType = projectileTypes.none;
		else {
			switch (projectileType) {
			case 0:
				pType = projectileTypes.bullet;
				break;
			case 1:
				pType = projectileTypes.rocket;
				break;
			case 2:
				pType = projectileTypes.grenade;
				break;
			case 3:
				pType = projectileTypes.none;
				break;
			}
		}
		
		// Assigns correct weapon names
		switch (weaponName) {
			case 0: 
				this.weaponName = weaponNames.pistol;
				break;
			case 1:
				this.weaponName = weaponNames.shotgun;
				break;
			case 2:
				this.weaponName = weaponNames.ak;
				break;
			case 3:
				this.weaponName = weaponNames.crossbow;
				break;
			case 4:
				this.weaponName = weaponNames.grenadeL;
				break;
			case 5:
				this.weaponName = weaponNames.UZI;
				break;
			case 6:
				this.weaponName = weaponNames.grenade;
				break;
		}
	}
	
	public void Update() {
		elapsed += Time.deltaTime;
		
		// XP CHECK
		if (xp > xpNeeded) {
			int xpOverflow = xp - xpNeeded;
			
			if (level < maxLevel) {
				level++;
				xpNeeded = xpNeeded + (int)(xpNeeded * 0.3);
				
				xp = xpOverflow;
				
				// Ammo increase
				maxAmmo += ammoIncrement;
			}
			else { 
				xp = xpNeeded;
			}
		}
		
		// AMMO CHECK
		if (ammo > maxAmmo)
			ammo = maxAmmo;
		else if (ammo < 0)
			ammo = 0;
		
		if (level > 2) {
			iSpawn.spawnWeapon(bulletMesh);
		}
		
		updateBulletList();
	}
	
	// Updates projectiles
	private void updateBulletList() {
		if (pType != projectileTypes.none) {
			if (bullets.Count > 0) {
				for (int i = 0; i < bullets.Count; i++) {
					Projectile tempScript = bullets[i].GetComponent(typeof(Projectile)) as Projectile;
					if (tempScript.timeActive > tempScript.maxTimeActive 
						|| tempScript.hit) {
						GameObject.Destroy(bullets[i]);
						bullets.RemoveAt(i);
					}
				}
			}
		}
	}
	
	public void gainXP(int xp) {
		this.xp += xp;
	}
	
	public void gainAmmo(int ammo) {
		this.ammo += ammo;
	}
	
	public void makeBullet(AudioClip pistolSound) {
		if (elapsed > timeBetweenShots) {
			if (ammo > 0) {
				elapsed = 0;
			
				GameObject temp = (GameObject)GameObject.Instantiate(bulletMesh, derp.transform.position, derp.transform.rotation);
				
				Projectile temp2 = temp.GetComponent(typeof(Projectile)) as Projectile;
				
				temp2.Speed = bulletSpeed;
				
				Vector3 ScreenMouse;
				
				ScreenMouse.x = Input.mousePosition.x;
				ScreenMouse.y = Input.mousePosition.y;
				ScreenMouse.z = 1;
				
				Vector3 worldMouse = mainCamera.camera.ScreenToWorldPoint(ScreenMouse);
				
				Vector3 direction = derp.transform.position - worldMouse;
				//direction = direction.normalized;
				
				//Vector3 target = new Vector3(worldMouse.x + derp.transform.position.x, derp.transform.position.y, worldMouse.z + derp.transform.position.z);
				
				temp2.Direction = -direction;
				
				temp2.maxTimeActive = bulletMaxActiveTime;
				
				bullets.Add (temp);
				
				//audio.PlayOneShot(pistolSound, 1);
				derp.audio.PlayOneShot(pistolSound, 1);
				
				ammo--;
			}
		}
	}
}
