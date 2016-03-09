using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject enemy;
	float seconds = 0;
	float maxSeconds = 10;
	float totalSeconds = 13;
	float incrementalSec = 10;
	public int totalEnemies = 0;
	public bool gamePlaying = true;

	public GameObject restartObject;
//	public PlayerManager playerManager;
	// Use this for initialization

	public int totalPoints = 0;
	void Start () {

		StartCoroutine (GenerateEnemy ());
		//InvokeRepeating("EnemySpawner",0,2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	Rect textArea = new Rect(0,0,Screen.width, Screen.height);

	void OnGUI() {
		if (gamePlaying == true) {
			GUI.Label (textArea, "Points: " + totalPoints);
		} else {
			GUI.Label (new Rect((Screen.width/2)-17,(Screen.height/2)-5 ,Screen.width, Screen.height+20), "Points: " + totalPoints);
			GUI.Label (new Rect((Screen.width/2)-26,(Screen.height/2)+13 ,Screen.width, Screen.height+20), "High Score: " + PlayerPrefs.GetInt("highscore",0));
		}
	
	}

	void EnemySpawner(){
		int randomColor = Random.Range (0, 3);
//		Debug.Log (randomColor);
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
		int x = 0;
		int y = 0;
		int pickSide = Random.Range (1, 3); 
		int pickSide2 = Random.Range (1, 3); 
		int rotation = 0;
	
//		Debug.Log (pickSide+ " "+ pickSide2);
		if (pickSide == 1) {
			if (pickSide2 == 1) {
				y = -27;   // suppose to be 25 but shifted a little forward so it doesnt just "appear" 
				x = Random.Range (-33, 33);
				if (x < 0) {  // bottom left
					rotation = Random.Range (0, 90);
				}
				else{ // bottom right
					rotation = Random.Range (90,180); 
				}
			} else { 
				y = 27;
				x = Random.Range (-33, 33);
				if (x < 0) {  // top left
					rotation = Random.Range (0, -90);
				}
				else{ // top right
					rotation = Random.Range (180,270);
				}
			}

		} else {
			if (pickSide2 == 1) {
				x = -35; // suppose to be -33
				y = Random.Range (-25, 25);
				if (y < 0) {  // left bottom
					rotation = Random.Range (0, 90);
				}
				else{ // left top
					rotation = Random.Range (0,-90);
				}
			} else {
				x = 35;
				y = Random.Range (-25, 25);
				if (y < 0) {  // right bottom
					rotation = Random.Range (90, 180);
				}
				else{ // right top
					rotation = Random.Range (-90,-180);
				}
			}
		}
		//Debug.Log ("rotatino " + rotation);
		Quaternion randomRotation = Quaternion.Euler( 0 , 0 , rotation);
		//enemy.transform.rotation = randomRotation;


		Object newEnemy = Instantiate(enemy, new Vector3(x, y, 0),
			randomRotation); //fix spawn locations
		

	}

	IEnumerator GenerateEnemy(){

		while (gamePlaying) {
			seconds = Random.Range (1, maxSeconds);
			EnemySpawner ();
			//if (totalSeconds 
			totalEnemies +=1;
			if ((Time.time > (totalSeconds +incrementalSec)) && (totalEnemies > 10-maxSeconds)&&
				maxSeconds >=1){ //have it so it spawns more at shorter intervals
				//	Debug.Log (" total : " + totalSeconds + " incrememtSec : " + incrementalSec+ " maxsex:  "+maxSeconds);
					incrementalSec -= 1; 
					maxSeconds -= 1;
					totalSeconds = Time.time;
					totalEnemies = 0;
			}
			//totalSeconds += seconds; 
			yield return new WaitForSeconds (seconds);
		}
	}

	public void GameOver(){
		StoreHighscore (totalPoints);
		gamePlaying = false;
		Restart ();
		//
		//playerManager.gamePlaying = false;
	}

	void Restart(){
		restartObject.SetActive (true);
		//OnGui ();
	}

	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore", 0);    
		if (newHighscore > oldHighscore) {
			PlayerPrefs.SetInt ("highscore", newHighscore);
			PlayerPrefs.Save ();
		}
	}
}
