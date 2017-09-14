using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameController : MonoBehaviour {

    public bool resetGame;

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
		
	}
}
