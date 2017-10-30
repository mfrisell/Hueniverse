using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	public float waitTime = 30f;
    Scenario scenario;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("scenario");
        scenario = go.GetComponent<Scenario>();
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {


        while(waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            if (!scenario.isPaused)
                waitTime--;

        }

        Destroy(this.gameObject);


    }
}
