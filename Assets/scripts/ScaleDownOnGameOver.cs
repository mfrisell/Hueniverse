using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownOnGameOver : MonoBehaviour {

    private GameObject go;
    private GameController gc;
    private bool notScaled = true;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        go = GameObject.FindGameObjectWithTag("GameController");
        gc = go.GetComponent<GameController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(gc.gameOver && notScaled)
        {
            notScaled = false;

            Transform firstChild = transform.GetChild(0);

            MeshRenderer mesh = firstChild.GetComponent<MeshRenderer>();
            mesh.enabled = false;

            Instantiate(explosion, transform.position, transform.rotation);


        }
    }


}
