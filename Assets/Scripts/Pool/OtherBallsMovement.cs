using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{

    public class OtherBallsMovement : MonoBehaviour
    {
        private Main game;
        private bool fellInHole;
        private Rigidbody rb;

        // Use this for initialization
        void Start()
        {

            game = GameObject.Find("Table").GetComponent<Main>();
            fellInHole = false;
            rb = GetComponent<Rigidbody>();

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
                    if (!game.increaseNbFallenBalls())
                    {
                        gameObject.SetActive(false);
                        game.changeTarget();
                    }
                    else
                    {
                        game.end();
                    }
                    fellInHole = false;
                }

            }
        }

        public void stop()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public bool isMoving()
        {
            return !(rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0);
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "Bottom")
            {
                fellInHole = true;
            }
        }
    }
}


