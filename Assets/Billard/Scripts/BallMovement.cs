using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
    public float speed;
    private Rigidbody rb;
    public new Camera camera;
    private Main game;


    // Use this for initialization
    void Start () {
        Vector3 defaultPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        speed = 5;
        game = GameObject.Find("PoolTable").GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!game.isPausedFct())
        {
            if (rb.velocity.magnitude < 0.05)
            {
                rb.velocity = Vector3.zero;
            }

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            if (rb.position.y < 0.8)
            {
                Debug.Log("lol "+ rb.velocity.y);
                game.loadGame();
            }
        }

    }

    public void increaseSpeedFct()
    {
        speed += 20;
    }

    public void move()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            Vector3 vec = new Vector3(hit.point.x, hit.point.y + transform.localScale.y / 2, hit.point.z);
            Vector3 direction = vec - transform.position;
            rb.AddForce(direction.normalized * speed);
        }
        speed = 5;
    }

    public bool isMoving()
    {
        return !(rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0);
    }

    public void reinitSpeed()
    {
        speed = 5;
    }
}
