using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{

    public class OtherBallsMovement : AllBalls
    {
        // Use this for initialization
        void Start()
        {
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

                if (rb.angularVelocity.magnitude < 0.02f)
                {
                    rb.angularVelocity = Vector3.zero;
                }

                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }

                //Debug.Log("ETAT de" +rb.name+": "+fellInHole);
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
                    //Debug.Log("desactive: " + rb.name);
                    fellInHole = false;
                }

            }
        }
    }
}


