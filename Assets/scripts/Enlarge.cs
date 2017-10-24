﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlarge : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(ScaleUp());

    }

    IEnumerator ScaleUp()
    {


        for (int i = 0; i < 10; i++)
        {
            float scaleValue = i / 50f;
            rb.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            yield return null;

        }

    }

}
