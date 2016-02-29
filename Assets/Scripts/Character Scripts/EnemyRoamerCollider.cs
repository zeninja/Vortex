using UnityEngine;
using System.Collections;

public class EnemyRoamerCollider : MonoBehaviour {


	public bool characterInRange = false;
	public Collider2D charcterToPursue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Character")) {
			charcterToPursue = other;
			characterInRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag("Character")) {
			characterInRange = false;
		}
	
	}
}
