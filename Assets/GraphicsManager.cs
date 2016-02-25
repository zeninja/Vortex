using UnityEngine;
using System.Collections;

/// <summary>
/// TEMPORARRRYYYYYYYYYYYYYYYYYYYYYYYYYY
/// </summary>
public class GraphicsManager : MonoBehaviour {

	[System.NonSerialized]
	public Vector3[] corners;
	
	Mesh myMesh;

	// Use this for initialization
	void Start () {
		myMesh = new Mesh();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
//	void Trigger() {
//		
//	}
	
	void Trigger() {
		GetComponent<SpriteRenderer>().enabled = true;
		iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .5f, "onupdate", "UpdateAlpha"));
	}
	
	void UpdateAlpha(float newAlpha) {
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newAlpha);
	}
}
