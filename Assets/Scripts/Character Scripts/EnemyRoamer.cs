using UnityEngine;
using System.Collections;

public class EnemyRoamer : MonoBehaviour {
	// This Enemy roams around, pursues player in a radius, and has a melee weapon.

	bool changeStatus = false; 
//	string state = "none";
	enum state { none, Pursue, Wander };
	state enemyState = state.none;
	float xValue = 0;
	float yValue = 0;
	public float speed = 0.5f;

	void Start () {
		 xValue = Random.Range(-1f,1f);
		 yValue = Random.Range(-1f,1f);

	}

	void Update () {

		CheckStatus ();

		if (changeStatus) {
			switch (enemyState) {
			case state.Pursue:
				PursuePlayer ();
				changeStatus = false;
				break;
			default:
				break;
			}
		} else {
			Wander ();
		}

	}

	void CheckStatus(){
		// Check what action to do next
		if (CheckForPlayer ()) {
			if (enemyState != state.Pursue) {
				changeStatus = true; 
				enemyState = state.Pursue;
			}
		} else {
			if (enemyState != state.Wander) {
				enemyState = state.Wander;
			}
		}
	}

	void Wander(){
		Debug.Log ("wandeR" + xValue);
//		this.transform.position = new Vector3 (transform.position.x+ Random.Range(-1f,1f), transform.position.y+ Random.Range(-1f,1f), transform.position.z); 
		this.transform.position = new Vector3 (transform.position.x + xValue *speed, transform.position.y+ yValue *speed, transform.position.z); 

		//transform.position += moveDirection;

	}

	void Trigger() {
		Debug.Log ("trigger");
	}

	bool CheckForPlayer(){
		return false; // temp
		
	}

	void PursuePlayer(){
	
	}



}
