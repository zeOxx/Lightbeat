using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemy;
	
	private float elapsed;				// both in
	private float timeBetweenSpawns;	//  seconds
	private int maxEnemies;
	private int enemyIncrement;
	private List<Vector3> positions;
	private List<GameObject> enemies;
	private GameObject player;
	private Player playerScript;
	
	public void Start() {
		player = GameObject.FindGameObjectWithTag("Player") as GameObject;
		playerScript = player.GetComponent (typeof(Player)) as Player;
		
		positions = new List<Vector3>();
		enemies = new List<GameObject>();
		
		var temp = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < temp.Length; i++) {
			enemies.Add (temp[i]);
		}
		
		elapsed = 0;
		
		maxEnemies = 10;
		enemyIncrement = 5;
		
		timeBetweenSpawns = 10;
	}
	
	public void Update() {
		elapsed += Time.deltaTime;
		// Spawns enemies either when the timer allows it, or when all enemies are killed
		if (elapsed >= timeBetweenSpawns) {
			while (enemies.Count < maxEnemies) {
				spawnEnemy(generateNewPosition());
			}
			maxEnemies += enemyIncrement;
			elapsed = 0;
		}
		else if ((enemies.Count <= 0)) {
			while (enemies.Count < maxEnemies) {
				spawnEnemy(generateNewPosition());
			}
		}
		
		// Checks enemies for health and removes them accordingly
		for (int i = 0; i < enemies.Count; i++) {
			var enemyScript = enemies[i].GetComponent (typeof(Hit)) as Hit;
			
			if (enemyScript.health <= 0) {
				var statscript = player.GetComponent(typeof(PlayerHealth)) as PlayerHealth;
				statscript.Score = 100;
				GameObject.Destroy(enemies[i]);
				enemies.RemoveAt (i);
			}
		}
		
		positions.Clear();
	}
	
	public Vector3 generateNewPosition() {
		Vector3 tempPosition = new Vector3(Random.Range(player.transform.position.x - 400, player.transform.position.x + 400), 
											player.transform.position.y, 
											Random.Range(player.transform.position.z - 400, player.transform.position.z + 400));
		
		if (positions.Count > 0) {
			for (int i = 0; i < positions.Count; i++) {
				if (tempPosition == positions[i]) {
					tempPosition = new Vector3(Random.Range(player.transform.position.x - 400, player.transform.position.x + 400), 
											player.transform.position.y, 
											Random.Range(player.transform.position.z - 400, player.transform.position.z + 400));
					i = 0;
				}
			}
		}
		if(tempPosition.x < (player.transform.position.x + 50) || tempPosition.x > (player.transform.position.x - 50)){
			tempPosition.x += 100;
		}
		
		if(tempPosition.z < (player.transform.position.z + 50) || tempPosition.z > (player.transform.position.z - 50)){
			tempPosition.z += 100;
		}
		
		positions.Add (tempPosition);
		
		return positions[positions.Count - 1];
	}
	
	public void spawnEnemy(Vector3 position) {
		GameObject temp = (GameObject)GameObject.Instantiate(enemy, position, player.transform.rotation);
		enemies.Add(temp);
	}
}
