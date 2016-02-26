using UnityEngine;
using System.Collections;

public class LineManager : MonoBehaviour {

	PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		playerManager = transform.parent.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<LineRenderer>().material.color = playerManager.graphics.GetComponent<GraphicsManager>().currentColor;
	}
}
