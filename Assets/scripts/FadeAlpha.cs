using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAlpha : MonoBehaviour {
	[SerializeField] private float fadePerSecond = 100f;
	public float alphaChange;

	private void Update() {
		var material = GetComponent<Renderer>().material;
		var color = material.color;

		alphaChange += Time.deltaTime/fadePerSecond;

		material.color = new Color(color.r, color.g, color.b, alphaChange);
	}

}
