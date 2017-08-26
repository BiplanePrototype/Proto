using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    Rigidbody2D rb;

    [SerializeField]
    float playerSpeed = 1f;
    // Use this for initialization
    void Start () {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))//movement left
        {
            rb.AddForce(-transform.right * Time.deltaTime * playerSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//movement right
        {
            rb.AddForce(transform.right * Time.deltaTime * playerSpeed, ForceMode2D.Impulse);
        }
    }
    
}
