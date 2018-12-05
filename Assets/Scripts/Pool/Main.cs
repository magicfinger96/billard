using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pool
{

    public class Main : MonoBehaviour
    {
        private bool onGame;
        private bool isPaused;
        [SerializeField]
        Transform UIPanelMenu;
        public Transform UIPanel;
        public GameObject ball;
        public Vector3 positionBall;
        public Vector3[] positionOtherBalls;
        private GameObject player;
        [SerializeField]
        private Camera cameraPool;
        private GameObject[] holeFillers;
        private Vector3[] posHoop;
        private bool increaseSpeed;
        private int actualTarget;
        private int nbFallenBalls;
        [SerializeField]
        private Text textRemainingBalls;
        [SerializeField]
        private Text endText;
        [SerializeField]
        private GameObject hoop;
        private bool itsEnd;

        void Start()
        {
            isPaused = false;
            player = GameObject.FindWithTag("PlayerMain");
            positionOtherBalls = new Vector3[3];
            holeFillers = new GameObject[11];
            posHoop = new Vector3[11]{
                        new Vector3(-5.352f, 3.835f, 23.584f),
                        new Vector3(-4.168f, 3.824f, 21.396f),
                        new Vector3(-6.3f, 3.822f, 20.787f),
                        new Vector3(-6.285f,3.826f,22.706f),
                        new Vector3(-9.225f, 3.817f, 22.916f),
                        new Vector3(-10.176f, 3.828f, 22.147f),
                        new Vector3(-10.291f, 3.797f, 23.429f),
                        new Vector3(-8.734f, 3.818f, 20.898f),
                        new Vector3(-8.025f, 3.826f, 22.008f),
                        new Vector3(-11.113f, 3.826f, 20.937f),
                        new Vector3(-7.795f, 3.806f, 24.015f)
            };

            int i = 0;
            foreach (Transform child in GameObject.Find("HoleFillers").transform)
            {
                holeFillers[i++] = child.gameObject;
            }

            i = 0;
            foreach (Transform child in GameObject.Find("Balls").transform)
            {
                positionOtherBalls[i++] = child.position;
            }
            cameraPool.gameObject.SetActive(false);
            positionBall = ball.GetComponent<Transform>().position;
        }

        // Quand l'arceau a été trouvé
        public void hoopFound()
        {
            holeFillers[9].SetActive(false);
            hoop.transform.localPosition = posHoop[9];
            hoop.SetActive(true);

        }

        // Retourne vrai si le jeu est terminé!
        public bool increaseNbFallenBalls()
        {
            nbFallenBalls++;
            int nb = 3-nbFallenBalls;
            if (nb > 1) {
                textRemainingBalls.text = nb + " balles restantes";
            } else
            {
                textRemainingBalls.text = nb + " balle restante";
            }
            if (nb == 0)
            {
                return true;
            }

            return false;
        }

        // Fin de la partie !
        public void end()
        {
            Pause();
            endText.gameObject.SetActive(true);
            itsEnd = true;
        }

        // Permet de changer de cible
        public void changeTarget()
        {
            holeFillers[actualTarget].SetActive(true);
            actualTarget = Random.Range(0, holeFillers.Length);
            holeFillers[actualTarget].SetActive(false);
            hoop.transform.localPosition = posHoop[actualTarget];
        }

        public void leaveGame()
        {
            onGame = false;
            GameObject.Find("Canvas").GetComponent<GUIManager>().ShowInteractableTextContent();
            GameObject.Find("Canvas").GetComponent<GUIManager>().ShowHUDPlayer();
            cameraPool.gameObject.SetActive(false);
            player.SetActive(true);
            init(false);
            UIPanelMenu.gameObject.SetActive(false);
            UIPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        // Initialize the game, changeTarget true if the target has to be changed
        public void init(bool changeTarget)
        {
            endText.gameObject.SetActive(false);
            itsEnd = false;
            UnPause();
            UIPanel.gameObject.SetActive(true);
            ball.GetComponent<BallMovement>().stop();
            ball.transform.position = positionBall;
            int i = 0;
            foreach (Transform child in GameObject.Find("Balls").transform)
            {
                child.position = positionOtherBalls[i++];
                child.GetComponent<OtherBallsMovement>().stop();
            }

            foreach (GameObject h in holeFillers)
            {
                h.SetActive(true);
            }
            nbFallenBalls = 0;

            if (changeTarget)
            {
                actualTarget = Random.Range(0, holeFillers.Length);
                hoop.transform.localPosition = posHoop[actualTarget];
            }
            holeFillers[actualTarget].SetActive(false);

            UIPanel.gameObject.SetActive(true);
            increaseSpeed = false;
        }

        public void restart()
        {
            UnPause();
            init(true);
        }

        public void loadGame()
        {
            onGame = true;
            player.SetActive(false);
            cameraPool.gameObject.SetActive(true);
            GameObject.Find("Canvas").GetComponent<GUIManager>().HideInteractableTextContent();
            GameObject.Find("Canvas").GetComponent<GUIManager>().HideHUDPlayer();

            init(true);

        }

        // Update is called once per frame
        void Update()
        {
            if (onGame && !itsEnd) {
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
                    Pause();
                else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
                    UnPause();

                if (!isPaused)
                {
                    if (Input.GetMouseButtonDown(0) && noBallIsMoving())
                    {
                        increaseSpeed = true;
                    }

                    if (increaseSpeed && Input.GetMouseButtonUp(0) && noBallIsMoving())
                    {
                        ball.GetComponent<BallMovement>().savePrevPos();
                        ball.GetComponent<BallMovement>().move();
                        increaseSpeed = false;
                    }

                    if (increaseSpeed)
                    {
                        ball.GetComponent<BallMovement>().increaseSpeedFct();
                    }


                    RaycastHit hit;

                    /*Ray ray = cameraPool.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.name == "Table" && noBallIsMoving())
                        {
                            Vector3 pos = (new Vector3(hit.point.x, 0, hit.point.z) - new Vector3(ball.transform.position.x, 0, ball.transform.position.z)).normalized;
                        }
                    }*/
                }
            }


        }

        public bool noBallIsMoving()
        {
            foreach (Transform child in GameObject.Find("Balls").transform)
            {
                if (child.GetComponent<OtherBallsMovement>().isMoving())
                {
                    return false;
                }
            }
            return !ball.GetComponent<BallMovement>().isMoving();
        }

        public void Pause()
        {
            increaseSpeed = false;
            isPaused = true;
            UIPanelMenu.gameObject.SetActive(true); //turn on the pause menu
            Time.timeScale = 0f; //pause the game
        }

        // Return if the player is playing pool
        public bool isOnGame()
        {
            return onGame;
        }

        public void UnPause()
        {
            isPaused = false;
            UIPanelMenu.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f; //resume game
        }
        

        public bool isPausedFct()
        {
            return isPaused;
        }

    }
}
