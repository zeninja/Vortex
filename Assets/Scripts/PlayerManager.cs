using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject[] characters;
	PolygonCollider2D polygonCollider;
	
	float[] currentSlopes;
	float[] prevSlopes;
	
	Vector2[] comparedSlopes;

	// Use this for initialization
	void Start () {
		currentSlopes = new float[3];
		prevSlopes = new float[3];
		comparedSlopes = new Vector2[3];
	
		polygonCollider = GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCollider();
		
		CalculateSlopesBetweenCharacters();
	}
	
	void UpdateCollider() {
		Vector2[] colliderPath = new Vector2[3];
		
		for(int i = 0; i < characters.Length; i++) {
			colliderPath[i] = characters[i].transform.position;
		}
		
		polygonCollider.SetPath(0, colliderPath);
	}
	
	void CalculateSlopesBetweenCharacters() {
		currentSlopes[0] = (characters[1].transform.position.y - characters[0].transform.position.y)/(characters[1].transform.position.x - characters[0].transform.position.x);
		currentSlopes[1] = (characters[2].transform.position.y - characters[1].transform.position.y)/(characters[2].transform.position.x - characters[1].transform.position.x);
	    currentSlopes[2] = (characters[0].transform.position.y - characters[2].transform.position.y)/(characters[0].transform.position.x - characters[2].transform.position.x);
		
//		int numSlopesChanged = 0;
//		
//		for (int i = 0; i < 3; i++) {
//			if (currentSlopes[i] != prevSlopes[i]) {
//				numSlopesChanged++;
//			}
//		}
//		
//		if(numSlopesChanged == 2) {
//			Debug.Log("Crossed over!!!");
//		}

		comparedSlopes[0].x = currentSlopes[0] - currentSlopes[1];
		comparedSlopes[0].y = currentSlopes[0] - currentSlopes[2];
		
		comparedSlopes[1].x = currentSlopes[1] - currentSlopes[0];
		comparedSlopes[1].y = currentSlopes[1] - currentSlopes[2];
		
		comparedSlopes[2].x = currentSlopes[2] - currentSlopes[0];
		comparedSlopes[2].y = currentSlopes[2] - currentSlopes[1];

		prevSlopes[0] = (characters[1].transform.position.y - characters[0].transform.position.y)/(characters[1].transform.position.x - characters[0].transform.position.x);
		prevSlopes[1] = (characters[2].transform.position.y - characters[1].transform.position.y)/(characters[2].transform.position.x - characters[1].transform.position.x);
		prevSlopes[2] = (characters[0].transform.position.y - characters[2].transform.position.y)/(characters[0].transform.position.x - characters[2].transform.position.x);
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawLine(characters[0].transform.position, characters[1].transform.position);
		Gizmos.DrawLine(characters[1].transform.position, characters[2].transform.position);
		Gizmos.DrawLine(characters[2].transform.position, characters[0].transform.position);
	}
}
