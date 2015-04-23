using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	

	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(attackTimer > 0)	{
			attackTimer -= Time.deltaTime;
		}
		
		if(attackTimer < 0) {
			attackTimer = 0;
		}
	
		if (attackTimer == 0)
		{
			AttackPlayer();
			attackTimer = coolDown;
		}
	}
	
	private void AttackPlayer() {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		//Vector3 dir = (target.transform.position - transform.position).normalized;
		
		//float direction = Vector3.Dot(dir, transform.forward);
		
		
		
		if(distance < 30f)
		{
			//Stats tempScript = target.GetComponent(typeof(Stats)) as Stats;
			//tempScript.AddjustCurrentHealth(10);
			
			PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
			eh.PlayerCurrentHealth = -10;
		}
	}
}
