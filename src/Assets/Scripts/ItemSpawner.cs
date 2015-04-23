using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour {
	
	public GameObject mesh;
	public GameObject envMesh;
	public GameObject medPack;
	
	public GameObject shotgun;
	public GameObject uzi;
	public GameObject crossbow;
	public GameObject grenadeL;
	public GameObject grenade;
	public GameObject ak;
	
	private float maxActiveTime;
	private bool medic;
	private List<Item> obstacleList;
	private List<Item> itemList;
	private List<Item> weaponList;
	private int currentWeapon;
	private float elapsed;
	private float timeBetweenSpawn;
	private int maxItems;
	private int maxObstacles;
	private Vector3 randomPosition;
	private GameObject p1;
	
	public Material[] materials = new Material[3];
	
	void Start () {
		p1 = GameObject.FindGameObjectWithTag("Player") as GameObject;
		
		elapsed 			= 10.0f;
		timeBetweenSpawn 	= 3.5f;
		maxItems 			= 10;
		maxObstacles 		= 1000;
		
		maxActiveTime = 20;
		
		obstacleList 	= new List<Item>();
		itemList 		= new List<Item>();
		weaponList 		= new List<Item>();
		
		medic = false;
		
		currentWeapon = 0;
		
		for (int i = 0; i < maxObstacles; i++) {
			Item tempItem = new Item(2, 0, null, envMesh, null);
			randomPosition = new Vector3(Random.Range(-2500, 2500), 25, Random.Range(-2500, 2500));
			tempItem.setPosition(randomPosition);
			
			tempItem.assignMaterial(materials[Random.Range (0, 3)]);
			
			obstacleList.Add(tempItem);
		}
	}
	
	void Update () {
		elapsed += Time.deltaTime;
		
		for (int i = 0; i < itemList.Count; i++) {
			itemList[i].Update ();
		}
		
		if (elapsed > timeBetweenSpawn) {
			if (itemList.Count < maxItems) {
				Item temp;
				if (Random.Range(0, 10) > 3){
					temp = new Item(0, 10, null, mesh, null);
				} else {
					temp = new Item(3, 50, null, medPack, null);
				}
				
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				
				List<GameObject> objectList = new List<GameObject>();
				var templist = GameObject.FindGameObjectsWithTag("EnvironmentObject");
				
				for (int i = 0; i < templist.Length; i++) {
					objectList.Add(templist[i]);
				}
				
				for (int i = 0; i < objectList.Count; i++) {
					if (randomPosition == objectList[i].transform.position) {
						randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
						i = 0;
					}
				}
				
				temp.setPosition(randomPosition);
				itemList.Add(temp);
				elapsed = 0;
			}
		}
		
		for (int i = 0; i < itemList.Count; i++) {
			if (itemList[i].picked || itemList[i].active > maxActiveTime) {
				GameObject.Destroy(itemList[i].mehs);
				GameObject.Destroy (itemList[i].lightGameObject);
				itemList.RemoveAt(i);
			}
		}
	}
	
	public void spawnWeapon(GameObject mesh) {
		Weapon tempWeapon = null;
		Item tempItem = null;
			
		switch(currentWeapon) {
			case 0:
				tempWeapon = new Weapon(false, 1, 15, 10, 50, 5, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, shotgun, mesh);
			    randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
			case 1:
				tempWeapon = new Weapon(false, 2, 12, 30, 90, 10, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, ak, mesh);
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
			case 2:
				tempWeapon = new Weapon(false, 3, 20, 5, 10, 2, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, crossbow, mesh);
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
			case 3:
				tempWeapon = new Weapon(false, 4, 25, 5, 10, 1, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, grenadeL, mesh);
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
			case 4:
				tempWeapon = new Weapon(false, 5, 8, 30, 90, 10, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, uzi, mesh);
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
			case 5:
				tempWeapon = new Weapon(false, 6, 20, 2, 5, 1, 0.2f, 5, 3, 0, mesh);
				tempItem = new Item(1, 0, tempWeapon, grenade, mesh);
				randomPosition = new Vector3(Random.Range(p1.transform.position.x - 200, p1.transform.position.x + 200), 25, Random.Range(p1.transform.position.z - 200, p1.transform.position.z + 200));
				tempItem.setPosition(randomPosition);
				break;
		}
		
		currentWeapon++;
	}
}
