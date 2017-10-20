using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private int framesCounter = 0;
    private float fpsTimer = 0f;

    public bool resetGame;
    public bool gameOver;
	public int score = 0;
	public string name = "Spansk";
	public string gameMode = "demo";
	public float gameTime = 0f;
	public float maxGameTime = 180f;
	public int lifes = 3;
	public float powerUp = 0; // A value between 0 and 1. 
	public bool powerUpAvailable = false;
    public float timeToPowerUp = 30;
    public float timeToPowerDown = 5;
    public bool shieldActivated = false;
    public bool showFps = false;

	private bool GOrunning = false;
	public GameObject GameOverModel;
	public GameObject superExplosion;

    // Use this for initialization
    void Start () {
		
        resetGame = false;
		gameOver = false;
        

    }
	
	// Update is called once per frame
	void Update () {

        framesCounter += 1;
        fpsTimer += Time.deltaTime;
        if (fpsTimer > 1f)
        {
            if(showFps) {
                Debug.Log("FPS: " + framesCounter.ToString());
            }
            framesCounter = 0;
            fpsTimer = 0f;
        }

		if (gameTime > maxGameTime)
			gameOver = true;
		else
			gameTime += Time.deltaTime;

		if(resetGame)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        float percentComplete = (gameTime / maxGameTime);
        AsteroidManager.asteroidFrequency = (percentComplete / 3f) + 0.1f;
        AsteroidManager.mixedPercentage = (percentComplete / 3) + 0.1f; //Go linearly from 10 to 43 percent


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

		if (gameOver && !GOrunning) {
			GOrunning = true;
			animateGameOver ();
		}

		if (lifes <= 0) {
			gameOver = true;
		}

        updatePower();
    }

    public void updatePower()
    {
        if(!shieldActivated)
        {
            if (powerUp < 1)
            {
                powerUp += Time.deltaTime / timeToPowerUp;
            }
            else
            {
                powerUp = 1;
            }
        } else
        {
            if(powerUp>0)
            {
                powerUp -= Time.deltaTime / timeToPowerDown;
            } else
            {
                powerUp = 0;
                shieldActivated = false;
                DestroyAllObjects();
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            shieldActivated = true;
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

	void animateGameOver() {

		GameObject goModel = Instantiate (GameOverModel, new Vector3 (0, 10, 20), Quaternion.Euler(20, 180, 0)) as GameObject;
		GameObject goExplosion = Instantiate (superExplosion, new Vector3 (0, 5.5f, 18), Quaternion.Euler(0, 0, 0)) as GameObject;
	}

    void DestroyAllObjects()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("shieldHolder");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

}
