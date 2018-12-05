using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class BallMovement : MonoBehaviour
    {
        public float speed;
        private Rigidbody rb;
        public new Camera camera;
        private Main game;
        private bool onTable;
        private Vector3 prevPosition;
        private bool fellInHole;


        // Use this for initialization
        void Start()
        {
            Vector3 defaultPosition = transform.position;
            rb = GetComponent<Rigidbody>();
            speed = 5;
            onTable = true;
            prevPosition = transform.localPosition;
            game = GameObject.Find("Table").GetComponent<Main>();
            fellInHole = false;
        }

        // Return yes if the ball has collision
        public bool hasCollision()
        {
            return onTable;
        }

        // Update is called once per frame
        void Update()
        {

            if (!game.isPausedFct() && game.isOnGame())
            {
                if (rb.velocity.magnitude < 0.05)
                {
                    stop();
                }

                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }

                if (fellInHole && game.noBallIsMoving())
                {
                    resetToPrevPos();
                    fellInHole = false;
                }
            }

        }

        public void stop()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void resetToPrevPos()
        {
            transform.localPosition = prevPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void savePrevPos() {
            prevPosition = transform.localPosition;
        }

        public void increaseSpeedFct()
        {
            speed += 20;
            if (speed > 100)
            {
                speed = 5;
            }
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

        // If the ball isn't in collision with the table.
        void OnCollisionExit(Collision col)
        {
            if (col.gameObject.name == "Table") {
                onTable = false;
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "Bottom")
            {
                fellInHole = true;
            }
        }

        public void reinitSpeed()
        {
            speed = 5;
        }
    }
}
