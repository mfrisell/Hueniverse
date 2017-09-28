using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunScene : MonoBehaviour {
	private string scene;
    private bool loadScene = false;
    public Material standardWhite;
    public Material standardCyan;
    public Material standardMagenta;
    private GameObject go;
    private GameObject goSta;
    private GameObject goTut;

	void OnTriggerEnter(Collider other) {
        loadScene = true;


        if (gameObject.tag == "start") {
			scene = "main";
            go = GameObject.FindGameObjectWithTag("startText");
            MeshRenderer goMesh = go.GetComponent<MeshRenderer>();
            goMesh.material = standardCyan;
        }
		else if(gameObject.tag == "tutorial") {
			scene = "tutorialScene";
            go = GameObject.FindGameObjectWithTag("tutorialText");
            MeshRenderer goMesh = go.GetComponent<MeshRenderer>();
            goMesh.material = standardMagenta;
        }

		StartCoroutine (changeScene (scene));
	}

    private void OnTriggerExit(Collider other)
    {
        loadScene = false;
        goSta = GameObject.FindGameObjectWithTag("startText");
        MeshRenderer goMeshSta = goSta.GetComponent<MeshRenderer>();
        goMeshSta.material = standardWhite;


        goTut = GameObject.FindGameObjectWithTag("tutorialText");
        MeshRenderer goMeshTut = goTut.GetComponent<MeshRenderer>();
        goMeshTut.material = standardWhite;

        //StopCoroutine(changeScene(scene));
    }

    // Run chosen scene after 3 seconds
    IEnumerator changeScene(string scene) {
        Debug.Log(scene);
		yield return new WaitForSeconds (3);

        // Run scene
        if(loadScene)
        {
            //SceneManager.LoadScene(scene, LoadSceneMode.Single);
            SteamVR_LoadLevel.Begin(scene);
        }

    }
}
