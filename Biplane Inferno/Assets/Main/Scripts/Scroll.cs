using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    /* SERIALIZED FIELDS */
    [SerializeField]
    private float scrollSpeed; // speed of scrolling background

    /* PRIVATE VARIABLES */
    private Renderer render; // renderer of material

	// Use this for initialization
	void Start () {
        render = GetComponent<Renderer>(); // get the material renderer for the background
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);

        render.material.mainTextureOffset = offset;
	}
}
