using UnityEngine;
using System.Collections;

/// <summary>
/// TEMPORARRRYYYYYYYYYYYYYYYYYYYYYYYYYY
/// </summary>
public class GraphicsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Trigger() {
		GetComponent<SpriteRenderer>().enabled = true;
		iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", .5f, "onupdate", "UpdateAlpha"));
	}
	
	void UpdateAlpha(float newAlpha) {
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newAlpha);
	}
}
