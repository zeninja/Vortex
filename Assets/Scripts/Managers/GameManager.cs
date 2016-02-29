using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject enemy;
	// Use this for initialization
	void Start () {
		InvokeRepeating("EnemySpawner",0,3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void EnemySpawner(){
		int randomColor = Random.Range (0, 3);
		Debug.Log (randomColor);
		if (randomColor == 0) {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Red;
			enemy. GetComponentInChildren<SpriteRenderer> ().color = Color.red;
		} else if (randomColor == 1) {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Blue;
			enemy.GetComponentInChildren<SpriteRenderer> ().color = Color.blue;
		} else {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Yellow;
			enemy.GetComponentInChildren<SpriteRenderer> ().color = Color.yellow;


		}
		Object newEnemy = Instantiate(enemy, new Vector3(Random.Range (-30, 30), Random.Range (-25, 25), 0),
			Quaternion.identity); //fix spawn locations
		

	}
}
