using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class BallMovement : AllBalls
    {
        public float speed;
        public new Camera camera;
        private Vector3 prevPosition;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            speed = 0.05f;
            prevPosition = transform.localPosition;
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

                if ((fellInHole || transform.localPosition.y <= 1.8f) && game.noBallIsMoving())
                {
                    resetToPrevPos();
                    fellInHole = false;
                }
            }

        }

        public float getIntensity()
        {
            return speed;
        }

        // Return the previous position of the ball
        public Vector3 getPrevPos()
        {
            return prevPosition;
        }


        public void resetToPrevPos()
        {
            transform.localPosition = prevPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void savePrevPos()
        {
            prevPosition = transform.localPosition;
        }

        public void increaseSpeedFct()
        {
            speed += 0.05f;
            if (speed > 1.2f)
            {
                reinitSpeed();
            }
        }

        public void move()
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                Vector3 vec = new Vector3(hit.point.x, hit.point.y + transform.localScale.y / 2, hit.point.z);
                Vector3 direction = vec - transform.position;
                rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
            }
            reinitSpeed();
        }

        public void reinitSpeed()
        {
            speed = 0.05f;
        }
    }
}
