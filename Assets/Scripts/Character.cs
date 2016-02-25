using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	bool canMove = false;
	Collider2D myCollider;
	
	[System.NonSerialized] public bool moving;

	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponentInChildren<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void HandleInput() {
		transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0));
	}
	
	void OnDrag(DragGesture gesture) {
		Vector2 positionOffset = new Vector2();
		
		if (gesture.Phase == ContinuousGesturePhase.Started) {
			Vector2 startPosition = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
			int characterLayer = 1 << LayerMask.NameToLayer("Characters");
						
			RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero, 0, characterLayer);
			
			if (hit.collider != null) {
				if(hit.collider == myCollider) {
					canMove = true;
					positionOffset = startPosition - (Vector2)transform.position;
				}
			}
		}
		
		if(gesture.Phase == ContinuousGesturePhase.Updated && canMove) {
//			transform.position = (Vector2)transform.position + gesture.DeltaMove;
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(gesture.Position) + positionOffset;
			moving = true;
		}
		
		if(gesture.Phase == ContinuousGesturePhase.Ended) {
			canMove = false;
			moving = false;
		}
	}
}
