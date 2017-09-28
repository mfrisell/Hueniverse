using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour {

    void OnTriggerEnter (Collider other)
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameController gco = go.GetComponent<GameController>();
        gco.score -= 50;
        if (gco.score < 0)
            gco.score = 0;
    }
}
