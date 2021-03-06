﻿using UnityEngine;
using System.Collections;

public class EnemyRoamer : MonoBehaviour {
	// This Enemy roams around, pursues player in a radius, and has a melee weapon.

	public PlayerManager.CharacterType enemyType = PlayerManager.CharacterType.Red;
	PlayerManager playerManager;
	bool changeStatus = false; 
//	string state = "none";
	enum state { none, Pursue, Wander };
	state enemyState = state.none;
	float xValue = 0;
	float yValue = 0;
	public float moveSpeed = 0.5f;
	public float rotationSpeed = 0.5f;
	bool enemyInRange= false;


	EnemyRoamerCollider sightCollider; 
	Collider2D characterToPursue;

	Vector3 upAxis = new Vector3 (0f, 0f, -1f);

	Transform myTransform;

	void Start () {
	//	xValue = Random.Range(-1f,1f);
	//	yValue = Random.Range(-1f,1f);
		Quaternion randomRotation = Quaternion.Euler( 0 , 0 , Random.Range(0, 360));
		transform.rotation = randomRotation;


		playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
		sightCollider = transform.GetComponentInChildren<EnemyRoamerCollider> ();
		myTransform = transform;

	}

	void Update () {

		CheckStatus ();

		if (changeStatus) {
			switch (enemyState) {
			case state.Pursue:
				Vector3 targetHeading = characterToPursue.transform.position - myTransform.position;
	
				float angle = Mathf.Atan2(targetHeading.y, targetHeading.x) * Mathf.Rad2Deg;
				myTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
//				Debug.Log ("insidecheckforplayer");
				characterToPursue = sightCollider.charcterToPursue;
				changeStatus = true; 
				enemyState = state.Pursue;

		} else {
			if (enemyState != state.Wander) {
				enemyState = state.Wander;
			}
		}
	}


	bool CheckForPlayer(){
		return sightCollider.characterInRange;
		//	return false; // temp

	}

	void Wander(){
//		Debug.Log ("wandeR" + xValue);
	//	myTransform.position = new Vector3 (myTransform.position.x + xValue *moveSpeed, myTransform.position.y+ yValue *moveSpeed, myTransform.position.z); 
		myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;


	}

	void Trigger() {
		if (playerManager.currentCharacter == enemyType) {
			Explode (); 
		}
	}


	void Explode(){
		//Remove from collidingObjects in PlayerManager
	//	playerManager.RemoveDestroyedEnemy(gameObject.GetComponent<Collider2D>());
		Destroy (gameObject);
	}

	void PursuePlayer(){
		myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Character")){
			// Destroys characters on collision
			//Debug.Log("name: " +other.transform.gameObject.name);
			other.transform.gameObject.GetComponentInParent<Character>().SendMessage ("Destroy");
		
		}
	
	}



}
