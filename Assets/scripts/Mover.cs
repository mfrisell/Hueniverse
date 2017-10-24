using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    private void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

}
