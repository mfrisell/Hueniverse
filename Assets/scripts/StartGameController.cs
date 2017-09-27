using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour {

	private GameObject[] go;
	private Transform nameUI;
	private Transform scoreUI;

	// Use this for initialization
	void Start () {

		go = GameObject.FindGameObjectsWithTag("HSplayer");
		ReadHighscore ();
	}

	void ReadHighscore(){
		List<string[]> highScores = new List<string[]>();

		// Read scores from csv-file
		using(var reader = new StreamReader(@"./Highscore.csv"))
		{ 
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(',');
				highScores.Add(values);
			}
		}

		// Remove header row
		highScores.RemoveAt (0);

		// Sort highscore list
		highScores.Sort(SortByScore);

		// Top 10 scores and player names
		for (int i = 0; i < 10; i++) {
			
			String name = highScores[i][0];
			String score = highScores[i][1];

			// index = 0 => last child in hierarchy, index = 9 => first child in hierarchy
			int index = 9 - i;
			UpdateUIHS (go[index], name, score);
		}
	}

	// Sort function, biggest first
	static int SortByScore(string[] p1, string[] p2)
	{
		int p1Score = int.Parse(p1[1]);
		int p2Score = int.Parse(p2[1]);
		return p2Score.CompareTo(p1Score);
	}
		
	// Update the UI
	void UpdateUIHS(GameObject UIPlayer, String name, String score) {

		foreach (Transform childUI in UIPlayer.transform) if (childUI.CompareTag("HSname")) {
			nameUI = childUI;
		} 

		foreach (Transform childUI in UIPlayer.transform) if (childUI.CompareTag("HSscore")) {
			scoreUI = childUI;
		} 

		Text nameUIText = nameUI.GetComponent<Text>();
		nameUIText.text = name;

		Text scoreUIText = scoreUI.GetComponent<Text>();
		scoreUIText.text = score;
		
	}
}
