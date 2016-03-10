using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public PlayerManager.CharacterType myCharacter = PlayerManager.CharacterType.Red;
	
	[System.NonSerialized] public PlayerManager playerManager;
	[System.NonSerialized] public bool moving;

	bool canMove = false;
	Collider2D myCollider;
	
	Vector2 positionOffset;
	
	[System.NonSerialized]
	public int fingerIndex = -1;
	
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponentInChildren<Collider2D>();
	}
	
	// Update is called once per frame
	Vector2 pos;
	void Update () {
		if(fingerIndex >= 0) {
			
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(FingerGestures.GetFinger(fingerIndex).Position) + playerManager.offsets[fingerIndex];
			pos = new Vector2 (transform.position.x, transform.position.y);
			if (transform.position.x <= -33){
				pos.x = -32;
			}
			if (transform.position.x >= 33) {
				pos.x = 32; 
			}
			if (transform.position.y <= -25) {
				pos.y = -24;
			}
			if(transform.position.y >= 25) {
				pos.y= 24;
			}
			transform.position = pos;
		}
	}
}
