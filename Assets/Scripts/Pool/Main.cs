using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{

    public class Main : MonoBehaviour
    {
        private bool onGame;
        private bool isPaused;
        [SerializeField]
        Transform UIPanel;
        public GameObject ball;
        public Vector3 positionBall;
        public Vector3 positionCue;
        public Quaternion rotationCue;
        private GameObject player;
        private GameObject cameraPool;
        private GameObject[] holeFillers;
        private Vector3[] posHoop;
        private bool increaseSpeed;

        void Start()
        {
            isPaused = false;
            player = GameObject.FindWithTag("PlayerMain");
            cameraPool = GameObject.FindWithTag("CameraPool");
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
            cameraPool.SetActive(false);
            //UIPanel.gameObject.SetActive(false);
            positionBall = ball.GetComponent<Transform>().position;
        }

        public void loadGame()
        {
            onGame = true;
            increaseSpeed = false;
            int holeFillerToRemove = Random.Range(0, holeFillers.Length);
            holeFillers[holeFillerToRemove].SetActive(false);
            GameObject.Find("hoop").transform.localPosition = posHoop[holeFillerToRemove];

            player.SetActive(false);
            cameraPool.SetActive(true);
            GameObject.Find("Canvas").GetComponent<GUIManager>().HideInteractableTextContent();
            //cameraPool.GetComponent<Main>().setInGame(true);
            ball.transform.position = positionBall;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }

        // Update is called once per frame
        void Update()
        {
            if (onGame) {
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
                    Pause();
                else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
                    UnPause();

                if (Input.GetMouseButtonDown(0) && !ball.GetComponent<BallMovement>().isMoving())
                {
                    increaseSpeed = true;
                }

                if (Input.GetMouseButtonUp(0) && !ball.GetComponent<BallMovement>().isMoving())
                {
                    ball.GetComponent<BallMovement>().savePrevPos();
                    ball.GetComponent<BallMovement>().move();
                    increaseSpeed = false;
                }

                if (increaseSpeed)
                {
                    ball.GetComponent<BallMovement>().increaseSpeedFct();
                }
            }


        }

        public void Pause()
        {
            isPaused = true;
            UIPanel.gameObject.SetActive(true); //turn on the pause menu
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
            UIPanel.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f; //resume game
        }

        public void Restart()
        {
            loadGame();
            UnPause();
        }

        public bool isPausedFct()
        {
            return isPaused;
        }

    }
}
