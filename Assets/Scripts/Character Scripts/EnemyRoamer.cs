using UnityEngine;
using System.Collections;

public class EnemyRoamer : MonoBehaviour {
	// This Enemy roams around, pursues player in a radius, and has a melee weapon.

	bool changeStatus = true; 
	string state = "none";

	void Start () {

	}

	void Update () {

		CheckStatus ();

		if (changeStatus) {
			this.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z); 
		}
	}

	void CheckStatus(){
		// Check what action to do next
		if (
	}

	void Wander(){


	}

	void CheckForPlayer(){
		
	}

	void PursuePlayer(){
	
	}



}
