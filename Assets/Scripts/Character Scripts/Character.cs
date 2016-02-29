using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public PlayerManager.CharacterType myCharacter = PlayerManager.CharacterType.Red;
	
	[System.NonSerialized] public PlayerManager playerManager;
	[System.NonSerialized] public bool moving;

	bool canMove = false;
	Collider2D myCollider;
	
	Vector2 positionOffset;
	
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponentInChildren<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position += playerManager[ 
//		playerManager.inputs[
	}
	
	void OnFingerDown(FingerDownEvent fingerDown) {
		
		Debug.Log(fingerDown.Selection);
		
		// THIS IS PROBABLY A LITTLE BIT TOO INACCURATE FOR WHAT WE ACTUALLY NEED
	
		if(fingerDown.Selection == gameObject) {
//			canMove = true;
			playerManager.SetCharacterType(myCharacter);
			positionOffset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(fingerDown.Position);
		}

		//		if (gesture.Phase == ContinuousGesturePhase.Started) {
		//			Vector2 startPosition = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
		//			int characterLayer = 1 << LayerMask.NameToLayer("Characters");
		//						
		//			RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero, 0, characterLayer);
		//			
		//			if (hit.collider != null) {
		//				if(hit.collider == myCollider) {
		//					playerManager.SetCharacterType(myCharacter);
		//					
		//					canMove = true;
		//					positionOffset = startPosition - (Vector2)transform.position;
		//				}
		//			}
		//		}
	}
	
	void OnFingerMove(FingerMotionEvent fingerMove) {
	
////		if(fingerMove.Selection == gameObject) {
//			if(canMove) {
//			
////				if(fingerMove.Finger.Index) {
////					
////				}
//				transform.position = (Vector2)Camera.main.ScreenToWorldPoint(fingerMove.Position) + positionOffset;
//			}
////		}
	}
	
	void OnFingerUp(FingerUpEvent fingerUp) {
		if (fingerUp.Selection == gameObject) {
//			canMove = false;	
		}

//		moving = false;
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
}
