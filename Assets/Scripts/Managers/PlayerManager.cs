using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public enum CharacterType { Red, Blue, Yellow };
	public CharacterType currentCharacter = CharacterType.Red;

	public GameObject[] characters;
	PolygonCollider2D polygonCollider;
	LineRenderer[] lineRenderers = new LineRenderer[3];
	
	List<GameObject> collidingObjects = new List<GameObject>();
	
	public GameObject graphics;
	int originalNumObjects = 0;

//	[System.NonSerialized]
	public Vector2[] offsets = new Vector2[3];

	// Use this for initialization
	void Start () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		
		for (int i = 0; i < 3; i++) {
			characters[i].GetComponent<Character>().playerManager = this;
			lineRenderers[i] = characters[i].GetComponent<LineRenderer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCollider();
		UpdateLineDisplay();
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
	
	void OnTap (TapGesture gesture) {			
//		Vector2 startPosition = Camera.main.ScreenToWorldPoint(gesture.StartPosition);		
//		RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero);
//		
//		if (hit.collider != null) {
//			if(hit.collider == polygonCollider) {
//				Trigger();
//			}
//		}
	}
	
	void OnFingerDown(FingerDownEvent e) {
		for(int i = 0; i < characters.Length; i++) {
			if(e.Selection == characters[i]) {
				characters[i].GetComponent<Character>().fingerIndex = e.Finger.Index;
				offsets[e.Finger.Index] = (Vector2)characters[i].transform.position - (Vector2)Camera.main.ScreenToWorldPoint(e.Finger.Position);
				//currentCharacter = characters[i].GetComponent<Character>().myCharacter;
				SetCharacterType(characters[i].GetComponent<Character>().myCharacter);
			}
		}
	}
	
	void OnFingerUp(FingerUpEvent e) {
		for (int i = 0; i < characters.Length; i++) {
			if (characters[i].GetComponent<Character>().fingerIndex == e.Finger.Index) {
				SetCharacterType(characters[i].GetComponent<Character>().myCharacter);
				Trigger();
				
				characters[i].GetComponent<Character>().fingerIndex = -1;
			}
		}
	}
	
	void Trigger() {
		originalNumObjects = collidingObjects.Count;
		for (int i = 0; i < originalNumObjects; i++) {
			collidingObjects[i].SendMessage("Trigger");
		}
		RemoveDestroyedEnemies ();
		graphics.SendMessage("Trigger");
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			collidingObjects.Add(other.transform.root.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log ("ontriggerexit : " + collidingObjects.Count);
		for (int i = 0; i < collidingObjects.Count; i++) {
			if (collidingObjects[i].GetComponentInChildren<Collider2D>() == other) {
				collidingObjects.Remove(collidingObjects[i]);
				break;
			}
		}
	}

	public void RemoveDestroyedEnemies() {

	
		for (int i = 0; i < originalNumObjects; i++) {
			if (collidingObjects [0].GetComponent<EnemyRoamer> ().enemyType == currentCharacter) {
				collidingObjects [0].GetComponent<EnemyRoamer> ().Explode ();
				collidingObjects.Remove (collidingObjects [0]);
			}

		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawLine(characters[0].transform.position, characters[1].transform.position);
		Gizmos.DrawLine(characters[1].transform.position, characters[2].transform.position);
		Gizmos.DrawLine(characters[2].transform.position, characters[0].transform.position);
	}
}
