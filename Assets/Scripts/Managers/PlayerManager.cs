using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public enum CharacterType { Red, Blue, Yellow };
	public CharacterType currentCharacter = CharacterType.Red;

	public GameObject[] characters;
	PolygonCollider2D polygonCollider;
	LineRenderer[] lineRenderers = new LineRenderer[3];
	
	float[] currentSlopes;
	float[] prevSlopes;
	float[] comparedSlopes;
	
	float[] changingSlopes;
	
	List<GameObject> collidingObjects = new List<GameObject>();
	
	public GameObject graphics;
	
//	Vector2[] comparedSlopes;

	// Use this for initialization
	void Start () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		
		for (int i = 0; i < 3; i++) {
			characters[i].GetComponent<Character>().playerManager = this;
			lineRenderers[i] = characters[i].GetComponent<LineRenderer>();
		}
		
		currentSlopes  = new float[3];
		prevSlopes 	   = new float[3];
		
		comparedSlopes = new float[3];
		

	}
	
	// Update is called once per frame
	void Update () {
		UpdateCollider();
		UpdateLineDisplay();
		CalculateSlopesBetweenCharacters();
	}
	
	public void SetCharacterType(CharacterType latestCharacter) {
		currentCharacter = latestCharacter;
		
		graphics.GetComponent<GraphicsManager>().UpdateGraphics();
	}
	
	void UpdateLineDisplay() {
		lineRenderers[0].SetPosition(0, characters[0].transform.position);
		lineRenderers[0].SetPosition(1, characters[1].transform.position);
		
		lineRenderers[1].SetPosition(0, characters[1].transform.position);
		lineRenderers[1].SetPosition(1, characters[2].transform.position);
		
		lineRenderers[2].SetPosition(0, characters[2].transform.position);
		lineRenderers[2].SetPosition(1, characters[0].transform.position);
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

//		comparedSlopes[0].x = currentSlopes[0] - currentSlopes[1];
//		comparedSlopes[0].y = currentSlopes[0] - currentSlopes[2];
//		
//		comparedSlopes[1].x = currentSlopes[1] - currentSlopes[0];
//		comparedSlopes[1].y = currentSlopes[1] - currentSlopes[2];
//		
//		comparedSlopes[2].x = currentSlopes[2] - currentSlopes[0];
//		comparedSlopes[2].y = currentSlopes[2] - currentSlopes[1]

//		prevSlopes[0] = (characters[1].transform.position.y - characters[0].transform.position.y)/(characters[1].transform.position.x - characters[0].transform.position.x);
//		prevSlopes[1] = (characters[2].transform.position.y - characters[1].transform.position.y)/(characters[2].transform.position.x - characters[1].transform.position.x);
//		prevSlopes[2] = (characters[0].transform.position.y - characters[2].transform.position.y)/(characters[0].transform.position.x - characters[2].transform.position.x);

//		int movingIndex = 0;
//		
//		for(int i = 0; i < 3; i++) {
//			if(characters[i].GetComponent<Character>().moving) {
//				movingIndex = i;
//			}
//		}
//		
//		if (movingIndex == 0) {
//			changingSlopes[0] = currentSlopes[0];
//			changingSlopes[1] = currentSlopes[1];
//		} else if (movingIndex == 0) {
//			
//		}
	}
	
	void OnTap (TapGesture gesture) {			
		Vector2 startPosition = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
//		int colliderLayer = 1 << LayerMask.NameToLayer("Player");
		
		RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero);
		
		if (hit.collider != null) {
			if(hit.collider == polygonCollider) {
				Trigger();
			}
		}
	}
	
	void Trigger() {
		for (int i = 0; i < collidingObjects.Count; i++) {
			collidingObjects[i].SendMessage("Trigger");
		}
		graphics.SendMessage("Trigger");
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			collidingObjects.Add(other.transform.root.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		for (int i = 0; i < collidingObjects.Count; i++) {
			if (collidingObjects[i].GetComponentInChildren<Collider2D>() == other) {
				collidingObjects.Remove(collidingObjects[i]);
			}
		}
	}

	public void RemoveDestroyedEnemy(Collider2D other) {
		for (int i = 0; i < collidingObjects.Count; i++) {
			if (collidingObjects[i].GetComponentInChildren<Collider2D>() == other) {
				collidingObjects.Remove(collidingObjects[i]);
			}
		}
	}


	void OnDrawGizmos() {
		Gizmos.DrawLine(characters[0].transform.position, characters[1].transform.position);
		Gizmos.DrawLine(characters[1].transform.position, characters[2].transform.position);
		Gizmos.DrawLine(characters[2].transform.position, characters[0].transform.position);
	}
}
