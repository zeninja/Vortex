using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	public PlayerManager.CharacterType myType = PlayerManager.CharacterType.Red;

	[System.NonSerialized]
	public PlayerManager playerManager;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
