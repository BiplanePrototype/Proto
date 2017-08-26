using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey("A")||Input.GetKey(KeyCode.LeftArrow))//movement left
        {
            rb.MovePosition(new Vector2(rb.position.x + 1, rb.position.y));
        }
        if (Input.GetKey("D") || Input.GetKey(KeyCode.RightArrow))//movement right
        {
            //transform.Translate(-Vector3.right * 100000 * Time.deltaTime);
            transform.position += transform.right * 1000000;
        }
    }
    
}
