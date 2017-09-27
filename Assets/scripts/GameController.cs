using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public bool resetGame, gameOver;
	public int score = 0;
	public string name = "Spansk";
	public string gameMode = "demo";
	public float gameTime = 0f;
	public float maxGameTime = 180f;

	private GameObject sun;
	private float distanceZ;
	private float deltaDistance;
	public float sunStopFromPlayer = 30;

	private bool GOrunning = false;
	public GameObject GameOverModel;

    // Use this for initialization
    void Start () {
		
        resetGame = false;
		gameOver = false;

		sun = GameObject.FindGameObjectWithTag ("sun");
		distanceZ = sun.transform.position.z;
		deltaDistance = distanceZ / maxGameTime;

	}
	
	// Update is called once per frame
	void Update () {
		if (gameTime > maxGameTime)
			gameOver = true;
		else
			gameTime += Time.deltaTime;

		if(resetGame)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        float percentComplete = (gameTime / maxGameTime);
        AsteroidManager.asteroidFrequency = (percentComplete / 3) + 0.2f;
        AsteroidManager.mixedPercentage = percentComplete / 3 + 0.1f; //Go linearly from 10 to 43 percent


        if (Input.GetKeyDown ("k")) {
			Debug.Log ("Saving Highscore");
			/*Destroy (GameObject.FindWithTag ("blue"));
			Destroy (GameObject.FindWithTag ("red"));
			Destroy (GameObject.FindWithTag ("green"));
			Destroy (GameObject.FindWithTag ("yellow"));
			Destroy (GameObject.FindWithTag ("cyan"));
			Destroy (GameObject.FindWithTag ("magenta"));*/
			SaveHighscore ();
		}
		if (Input.GetKeyDown ("j")) {
			ReadHighscore ();
		}

        moveSunCloser();

		if (gameOver && !GOrunning) {
			GOrunning = true;
			animateGameOver ();
		}
    }

	public void SaveHighscore() {
		string filePath = @"./Highscore.csv";  
		string delimiter = ",";   
		string[][] output;
		string scoreStr = score.ToString();
		string timeStamp = DateTime.Now.ToString();
		string gameTimeStr = gameTime.ToString ();
		string[] resultRow = new string[]{ name, scoreStr, gameMode, timeStamp, gameTimeStr };

		if (!File.Exists (filePath)) {
			Debug.Log ("No highscore file found, creating a new one");
			output = new string[][] {
				new string[]{ "name", "score", "game mode", "timestamp", "gametime" },  
				resultRow
			}; 
		} else {
			output = new string[][] {
				resultRow
			}; 
		}

		int length = output.GetLength(0);  
		StringBuilder sb = new StringBuilder();  
		for (int index = 0; index < length; index++)  
			sb.AppendLine(string.Join(delimiter, output[index]));  

		File.AppendAllText(filePath, sb.ToString());    

		SceneManager.LoadScene(0, LoadSceneMode.Single);

	}

	void ReadHighscore(){
		List<string[]> highScores = new List<string[]>();

		using(var reader = new StreamReader(@"./Highscore.csv"))
		{ 
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(',');
				highScores.Add(values);
			}
		}
		foreach(var highScore in highScores) {
			var highScorePrint = "";
			foreach (var column in highScore) {
				highScorePrint += " : " + column;
			}
			print (highScorePrint);
		}
	}

	void moveSunCloser() {

		float distanceChange = deltaDistance * Time.deltaTime;
		Vector3 pos = sun.transform.position;
		float distanceChangeNew = distanceChange;
	

		if (pos.z < 100 + sunStopFromPlayer) {
			distanceChangeNew = (distanceChange * (pos.z - sunStopFromPlayer)) / 100;
		}

		Vector3 tmp = new Vector3 (pos.x, pos.y, pos.z - distanceChangeNew);
		sun.transform.position = tmp;
	}

	void animateGameOver() {
		Debug.Log ("GAME OVER");

		GameObject goModel = Instantiate (GameOverModel, new Vector3 (0, 10, 20), Quaternion.Euler(20, 180, 0)) as GameObject;
	}
}
