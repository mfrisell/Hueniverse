using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using Valve.VR;

public class GameController : MonoBehaviour {

    public bool resetGame;
	public int score = 0;
	public string name = "Spansk";
	public string gameMode = "demo";
	public float gameTime = 0f;

    // Use this for initialization
    void Start () {

        resetGame = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(resetGame)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

		gameTime += Time.deltaTime;

		if (Input.GetKeyDown ("k")) {
			Debug.Log ("NU TRÖCK DU PÅ K");
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
			
	}


	void SaveHighscore() {
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

	}

	void ReadHighscore(){
		List<string> listA;
		using(var reader = new StreamReader(@".\Highscore.csv"))
		{
			listA = new List<string>();
			List<string> listB = new List<string>();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(',');

				listA.Add(values[0]);
				listB.Add(values[1]);
			}
		}
		for (int i = 0; i<listA.size(); i++) {
			Debug.Log (ListA [i]);
		}
	}

}
