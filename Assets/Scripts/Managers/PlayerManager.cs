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
	
	public Vector2[] startingPositions = new Vector2[3];
	public Vector2[] currentPositions  = new Vector2[3];
	
	public GameObject graphics;
	
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
		UpdateInputs();
	}
	
	void UpdateInputs() {
		
	}
	
	void OnFingerDown(FingerDownEvent e) {
		startingPositions[e.Finger.Index] = e.Position;
	}
	
	void OnFingerMove(FingerMotionEvent e) {
		currentPositions[e.Finger.Index] = e.Position;
	}
	
	void OnFingerUp(FingerUpEvent e) {
		
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
	
	void OnDrawGizmos() {
		Gizmos.DrawLine(characters[0].transform.position, characters[1].transform.position);
		Gizmos.DrawLine(characters[1].transform.position, characters[2].transform.position);
		Gizmos.DrawLine(characters[2].transform.position, characters[0].transform.position);
	}
}
