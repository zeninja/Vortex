using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public PlayerManager.CharacterType myCharacter = PlayerManager.CharacterType.Red;
	
	[System.NonSerialized] public PlayerManager playerManager;
	[System.NonSerialized] public bool moving;

	bool canMove = false;
	Collider2D myCollider;
	
	Vector2 positionOffset;

	Vector3 screenPosition;
	[System.NonSerialized]
	public int fingerIndex = -1;
	
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponentInChildren<Collider2D>();
		screenPosition = new Vector3 (Screen.width/2, Screen.height/2,0);

		screenPosition = Camera.main.ScreenToWorldPoint (pos);
//		Debug.Log ("screenpos " + screenPosition);
	}
	
	// Update is called once per frame
	Vector2 pos;
	void Update () {
		if(fingerIndex >= 0) {
			
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(FingerGestures.GetFinger(fingerIndex).Position) + playerManager.offsets[fingerIndex];
			pos = new Vector2 (transform.position.x, transform.position.y);
			if (transform.position.x <= screenPosition.x){  //-32
				pos.x = screenPosition.x;
			}
			if (transform.position.x >= 0-screenPosition.x) {
				pos.x = 0-screenPosition.x; 
			}
			if (transform.position.y <= screenPosition.y) {
				pos.y = screenPosition.y;
			}
			if(transform.position.y >= 0 -screenPosition.y) {
				pos.y = 0-screenPosition.y;
			}
			transform.position = pos;
		}
	}
}
