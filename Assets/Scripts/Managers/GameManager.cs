using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject enemy;
	public GameObject enemyStraight;
	public GameObject enemyPursue;
	float seconds = 0;
	float maxSeconds = 10;
	float totalSeconds = 13;
	float incrementalSec = 10;
	public int totalEnemies = 0;
	public bool gamePlaying = true;
	int intervalToSpawnStraight = 8;
	float elapsedTime = 0;
	float startTime = 0;

	Vector3 screenPosition; 
	Vector2 pos;
	bool firstCall = true; // generate pursing 

	public GameObject restartObject;
//	public PlayerManager playerManager;
	// Use this for initialization

	private GUIStyle guiStyle = new GUIStyle(); 
	private GUIStyle guiStyleTitle = new GUIStyle(); 

	public int totalPoints = 0;
	void Start () {
		startTime = Time.time;
		screenPosition = new Vector3 (Screen.width/2, Screen.height/2,0);
		screenPosition = Camera.main.ScreenToWorldPoint (pos);
		Debug.Log ("screenpos " + screenPosition);
		StartCoroutine (GenerateEnemy ());
		//InvokeRepeating("EnemySpawner",0,2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	Rect textArea = new Rect(0,0,Screen.width, Screen.height);

	void OnGUI() {
		guiStyle.fontSize =  (int) (Screen.width * 0.04f); //change the font size
		guiStyleTitle.fontSize =  (int) (Screen.width * 0.09f); 
		if (gamePlaying == true) {
			GUI.Label (textArea, "Points: " + totalPoints);
		} else {
			
			GUI.Label (new Rect((Screen.width/2)-35,(Screen.height/2)-50 ,Screen.width, Screen.height+20), "Restart",guiStyleTitle);
			GUI.Label (new Rect((Screen.width/2)-30,(Screen.height/2)-5 ,Screen.width, Screen.height+20), "Points: " + totalPoints,guiStyle);

			GUI.Label (new Rect((Screen.width/2)-30,(Screen.height/2)+13 ,Screen.width, Screen.height+20), "High Score: " + PlayerPrefs.GetInt("highscore",0),guiStyle);
		}
	
	}

	void EnemySpawner(){
		int randomColor = Random.Range (0, 3);

		if (randomColor == 0) {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Red;
			enemy.GetComponentInChildren<SpriteRenderer>().color = Color.red;
		} else if (randomColor == 1) {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Blue;
			enemy.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
		} else {
			enemy.GetComponent<EnemyRoamer> ().enemyType = PlayerManager.CharacterType.Yellow;
			enemy.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
		}
		float x = 0;
		float y = 0;
		int pickSide = Random.Range (1, 3); 
		int pickSide2 = Random.Range (1, 3); 
		int rotation = 0;
	
//		Debug.Log (pickSide+ " "+ pickSide2);
		if (pickSide == 1) {
			if (pickSide2 == 1) {
				y = screenPosition.y-2;   // suppose to be- 25 but shifted a little forward so it doesnt just "appear" 
				x = Random.Range (screenPosition.x, 0-screenPosition.x); //-33, 33
				if (x < 0) {  // bottom left
					rotation = Random.Range (0, 90);
				}
				else{ // bottom right
					rotation = Random.Range (90,180); 
				}
			} else { 
				y = 0-screenPosition.y+2;
				x = Random.Range (screenPosition.x, 0-screenPosition.x);
				if (x < 0) {  // top left
					rotation = Random.Range (0, -90);
				}
				else{ // top right
					rotation = Random.Range (180,270);
				}
			}

		} else {
			if (pickSide2 == 1) {
				x = screenPosition.x-2; // suppose to be -33
				y = Random.Range (screenPosition.y, 0-screenPosition.y);
				if (y < 0) {  // left bottom
					rotation = Random.Range (0, 90);
				}
				else{ // left top
					rotation = Random.Range (0,-90);
				}
			} else {
				x = 0-screenPosition.x+2;
				y = Random.Range (screenPosition.y, 0 - screenPosition.y);
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
		if (firstCall == true) {
			enemy = enemyStraight;
//			Debug.Log (" in first "+ enemy.name);
			while (gamePlaying && firstCall ==true) {
//				Debug.Log (" in second");
				seconds = Random.Range (1, maxSeconds);
				EnemySpawner ();
				//if (totalSeconds 
				totalEnemies += 1;
				if ((Time.time > (totalSeconds + incrementalSec)) && (totalEnemies > 10 - maxSeconds) &&
					maxSeconds >= 1) { //have it so it spawns more at shorter intervals
					//	Debug.Log (" total : " + totalSeconds + " incrememtSec : " + incrementalSec+ " maxsex:  "+maxSeconds);
					incrementalSec -= 1; 
					maxSeconds -= 1;
					totalSeconds = Time.time;
					totalEnemies = 0;
				}
				yield return new WaitForSeconds (seconds);
				//totalSeconds += seconds; 
				if((Time.time -startTime)>20){
					Debug.Log (" 20 seconds have passed");
					firstCall = false;
					 seconds = 0;
					 maxSeconds = 10;
					 totalSeconds = 13;
					 incrementalSec = 10;
					 totalEnemies = 0;
					StartCoroutine (GenerateEnemy ());
				}

			}

			   
		} else {
			enemy = enemyPursue;
			elapsedTime = Time.time;
			while (gamePlaying) {
				seconds = Random.Range (1, maxSeconds);
				Debug.Log ("range " + seconds);
				EnemySpawner ();
				if (seconds > intervalToSpawnStraight) { //to spawn some straights for variety
					Debug.Log("enemystriangts");
					enemy = enemyStraight;
					EnemySpawner ();
					enemy = enemyPursue;
					}
				//if (totalSeconds 
				totalEnemies += 1;
				if ((Time.time > (totalSeconds + incrementalSec)) && (totalEnemies > 10 - maxSeconds) &&
				   maxSeconds >= 1) { //have it so it spawns more at shorter intervals
					//	Debug.Log (" total : " + totalSeconds + " incrememtSec : " + incrementalSec+ " maxsex:  "+maxSeconds);
					incrementalSec -= 1; 
					maxSeconds -= 1;
					totalSeconds = Time.time;
					totalEnemies = 0;
				}
				//totalSeconds += seconds; 
				if (Time.time - elapsedTime > 10) {
					Debug.Log("elapstedtime");
					elapsedTime = Time.time;
					intervalToSpawnStraight -= 1;
				}
				yield return new WaitForSeconds (seconds);
			}
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
