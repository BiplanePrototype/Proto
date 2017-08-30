using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    Rigidbody2D rb;
    Vector2 prevpos;
    double width = 1.9;
    [SerializeField]
    float playerSpeed = 1f;
    // Use this for initialization
    void Start () {
         rb = GetComponent<Rigidbody2D>();
        prevpos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update () {
        var dist = (transform.position.y - Camera.main.transform.position.y);
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))//movement left
        {
                rb.AddForce(-transform.right * Time.deltaTime * playerSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//movement right
        {
                rb.AddForce(transform.right * Time.deltaTime * playerSpeed, ForceMode2D.Impulse);
        }
        if (gameObject.transform.position.x-width < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x|| gameObject.transform.position.x+width > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x)
        {
            rb.velocity = new Vector2(0, 0);
            gameObject.transform.position = prevpos;
        }
        prevpos = gameObject.transform.position;
    }
    
}
