using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueMovement : MonoBehaviour {
    private Rigidbody rb;
    private bool move;
    private bool moveForward;
    private float zOrigine;
    [SerializeField] private BallMovement ballMovement;
    public new Camera camera;
    private Vector3 previousBallPos;
    private Main game;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        move = false;
        moveForward = false;
        previousBallPos = ballMovement.transform.position;
        game = GameObject.Find("PoolTable").GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!game.isPausedFct()) {
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 8 && !ballMovement.isMoving())
                {
                    Vector3 pos = (new Vector3(hit.point.x, 0, hit.point.z) - new Vector3(ballMovement.transform.position.x, 0, ballMovement.transform.position.z)).normalized;
                    Vector3 cueDir = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
                    float angle = Vector3.Angle(pos, cueDir);
                    Vector3 cross = Vector3.Cross(pos, cueDir);
                    if (angle != 180)
                    {
                        if (cross.y < 0) {
                            transform.RotateAround(ballMovement.GetComponent<Rigidbody>().transform.position, -transform.right, 180.0f - angle);
                        } else
                        {
                            transform.RotateAround(ballMovement.GetComponent<Rigidbody>().transform.position, transform.right, 180.0f - angle);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && !ballMovement.isMoving())
            {
                move = true;
                zOrigine = transform.position.z;
            }

            if (move)
            {
                Vector3 newPosition = transform.position + (new Vector3(transform.forward.x, 0, transform.forward.z).normalized);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 0.3f);
                ballMovement.increaseSpeedFct();
                if (Vector3.Distance(transform.position, ballMovement.transform.position) >= 1)
                {
                    move = false;
                }
            }

            if (moveForward)
            {
                Vector3 newPosition = transform.position - (new Vector3(transform.forward.x, 0, transform.forward.z).normalized);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2f);
                if (Vector3.Distance(transform.position, ballMovement.transform.position) - transform.localScale.z - 0.01f <= ballMovement.transform.localScale.z / 2)
                {
                    moveForward = false;
                    ballMovement.move();
                    previousBallPos = ballMovement.transform.position;
                }
            }

            if (previousBallPos != ballMovement.transform.position && !ballMovement.isMoving())
            {

                transform.position = ballMovement.transform.position;
                transform.position -= new Vector3(transform.forward.x, 0, transform.forward.z).normalized * (-transform.localScale.z - (ballMovement.transform.localScale.z + 0.01f));
                previousBallPos = ballMovement.transform.position;
            }

            if (Input.GetMouseButtonUp(0) && !ballMovement.isMoving())
            {
                move = false;
                moveForward = true;
            }
        }
    }
}
