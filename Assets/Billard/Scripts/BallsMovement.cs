using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour {

    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (rb.velocity.magnitude < 0.05)
        {
            rb.velocity = Vector3.zero;
        }

        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

    }
}
