using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public PlayerManager.CharacterType myCharacter = PlayerManager.CharacterType.Red;
	
	[System.NonSerialized] public PlayerManager playerManager;
	[System.NonSerialized] public bool moving;

	bool canMove = false;
	Collider2D myCollider;
	
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponentInChildren<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnDrag(DragGesture gesture) {
		Vector2 positionOffset = new Vector2();
		
		if (gesture.Phase == ContinuousGesturePhase.Started) {
			Vector2 startPosition = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
			int characterLayer = 1 << LayerMask.NameToLayer("Characters");
						
			RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero, 0, characterLayer);
			
			if (hit.collider != null) {
				if(hit.collider == myCollider) {
					playerManager.SetCharacterType(myCharacter);
					
					canMove = true;
					positionOffset = startPosition - (Vector2)transform.position;
				}
			}
		}
		
		if(gesture.Phase == ContinuousGesturePhase.Updated && canMove) {
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(gesture.Position) + positionOffset;
			moving = true;
		}
		
		if(gesture.Phase == ContinuousGesturePhase.Ended) {
			canMove = false;
			moving = false;
		}
	}

	void Destroy(){
		Debug.Log ("GAMEOVER");
		//Destroy (gameObject);
	}
}
