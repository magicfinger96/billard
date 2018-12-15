using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pool
{

    public class Main : MonoBehaviour
    {
        [SerializeField]
        private AudioSource sourceFireplace;
        private float sourceVolumeSave;
        [SerializeField]
        private PlayerAttributes playerAttributs;

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
        private Text nbHitsText;
        [SerializeField]
        private GameObject hoop;
        private bool itsEnd;
        [SerializeField]
        private GameObject lamp;
        private bool chooseIntensity;
        [SerializeField]
        private Slider intensity;
        private float timeIntensity;
        [SerializeField]
        private GameObject cornerGroup;
        [SerializeField]
        private GameObject holeFillerGroup;
        [SerializeField]
        private GameObject ballGroup;
        private GameObject line;
        private int nbHits;


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
            foreach (Transform child in holeFillerGroup.transform)
            {
                holeFillers[i++] = child.gameObject;
            }

            i = 0;
            foreach (Transform child in ballGroup.transform)
            {
                positionOtherBalls[i++] = child.position;
            }
            cameraPool.gameObject.SetActive(false);
            positionBall = ball.GetComponent<Transform>().position;
            line = new GameObject();
            line.AddComponent<LineRenderer>();
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
            cameraPool.GetComponent<CameraController>().playWinSound();
            endText.gameObject.SetActive(true);
            itsEnd = true;
            playerAttributs.Xp += Mathf.Max(50 - nbHits, 0);
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
            sourceFireplace.volume = sourceVolumeSave;
            Cursor.visible = false;
            onGame = false;
            GameObject.Find("Canvas").GetComponent<GUIManager>().ShowInteractableTextContent();
            GameObject.Find("Canvas").GetComponent<GUIManager>().ShowHUDPlayer();
            cameraPool.gameObject.SetActive(false);
            player.SetActive(true);
            init(false);
            line.SetActive(false);
            nbHitsText.gameObject.SetActive(false);
            UIPanelMenu.gameObject.SetActive(false);
            UIPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
            lamp.SetActive(true);
            stopAllSounds();
        }

        // Initialize the game, changeTarget true if the target has to be changed
        public void init(bool changeTarget)
        {
            nbHits = 0;
            stopAllBalls();
            textRemainingBalls.text = 3 + " balles restantes";
            nbHitsText.text = "Nombre de coups: 0";
            nbHitsText.gameObject.SetActive(true);
            cameraPool.GetComponent<CameraController>().stopSound();
            endText.gameObject.SetActive(false);
            cameraPool.GetComponent<CameraController>().makeMoveLeft(false);
            cameraPool.GetComponent<CameraController>().makeMoveRight(false);
            itsEnd = false;
            UIPanel.gameObject.SetActive(true);
            ball.GetComponent<BallMovement>().stop();
            ball.transform.position = positionBall;
            int i = 0;
            foreach (Transform child in ballGroup.transform)
            {
                child.position = positionOtherBalls[i++];
                child.gameObject.SetActive(true);
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

            cameraPool.GetComponent<CameraController>().reinit();
            chooseIntensity = false;
            intensity.gameObject.SetActive(false);
            
            UnPause();
        }

        public void restart()
        {
            stopAllSounds();
            stopAllBalls();
            UnPause();
            StartCoroutine(reinitBeforeRestart());
            

        }

        // Wait the end of the loop of the others objects to avoid issue
        IEnumerator reinitBeforeRestart()
        {
            yield return 0;
            yield return 0;
            yield return 0;
            init(true);
        }

        public void loadGame()
        {
            sourceVolumeSave = sourceFireplace.volume;
            sourceFireplace.volume = 0.09f;
            Cursor.visible = true;
            onGame = true;
            player.SetActive(false);
            cameraPool.gameObject.SetActive(true);
            GameObject.Find("Canvas").GetComponent<GUIManager>().HideInteractableTextContent();
            GameObject.Find("Canvas").GetComponent<GUIManager>().HideHUDPlayer();
            lamp.SetActive(false);
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
                    if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                    {
                        cameraPool.GetComponent<CameraController>().makeZoomIn();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                    {
                        cameraPool.GetComponent<CameraController>().makeZoomOut();

                    } else if (Input.GetKeyDown(KeyCode.Q))
                    {
                        cameraPool.GetComponent<CameraController>().makeMoveLeft(true);
                    } else if (Input.GetKeyDown(KeyCode.D))
                    {
                        cameraPool.GetComponent<CameraController>().makeMoveRight(true);
                    } else if (Input.GetKeyUp(KeyCode.Q))
                    {
                        cameraPool.GetComponent<CameraController>().makeMoveLeft(false);
                    } else if (Input.GetKeyUp(KeyCode.D))
                    {
                        cameraPool.GetComponent<CameraController>().makeMoveRight(false);
                    }

                    RaycastHit hit;

                    Ray ray = cameraPool.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if ((hit.collider.gameObject.name == "Borders" || hit.collider.gameObject.name == "Bed" || hit.collider.transform.IsChildOf(cornerGroup.transform)
                            || hit.collider.transform.IsChildOf(holeFillerGroup.transform) 
                            || hit.collider.transform.IsChildOf(ballGroup.transform)) && noBallIsMoving())
                        {
                            Vector3 dir = (new Vector3(hit.point.x, 0, hit.point.z) - new Vector3(ball.transform.position.x, 0, ball.transform.position.z)).normalized;
                            Ray ray2 = new Ray(ball.transform.position,dir);
                            RaycastHit hit2;
                            Physics.Raycast(ray2, out hit2);
                            DrawLine(ball.transform.position, hit2.point);
                            line.SetActive(true);
                        } else
                        {
                            line.SetActive(false);
                        }
                    }

                    if (Input.GetMouseButtonDown(0) && noBallIsMoving())
                    {
                        chooseIntensity = true;
                        timeIntensity = 0;
                        intensity.value = 0;
                        intensity.gameObject.SetActive(true);
                    }

                    if (chooseIntensity && Input.GetMouseButtonUp(0) && noBallIsMoving())
                    {
                        chooseIntensity = false;
                        if (ball.transform.localPosition.y >= 3f) {
                            ball.GetComponent<BallMovement>().savePrevPos();
                        }
                        ball.GetComponent<BallMovement>().move();
                        intensity.gameObject.SetActive(false);
                        line.SetActive(false);
                        nbHits++;
                        nbHitsText.text = "Nombre de coups: "+nbHits;
                    }

                    if (chooseIntensity)
                    {
                        timeIntensity += Time.deltaTime;
                        if (timeIntensity >= 0.05f)
                        {
                            ball.GetComponent<BallMovement>().increaseSpeedFct();
                            intensity.value = ball.GetComponent<BallMovement>().getIntensity();
                            timeIntensity = 0f;
                        }
                        
                        
                    }
                }
            }


        }

        void DrawLine(Vector3 start, Vector3 end)
        {
            line.transform.position = start;            
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.material.color = new Color(1,1,1,0.50f);
            lr.startWidth = 0.01f;
            lr.endWidth = 0.01f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            //GameObject.Destroy(myLine, duration);
        }

        public bool noBallIsMoving()
        {
            foreach (Transform child in ballGroup.transform)
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
            intensity.gameObject.SetActive(false);
            Time.timeScale = 0f; //pause the game
            increaseSpeed = false;
            isPaused = true;
            //nbHitsText.gameObject.SetActive(false);
            UIPanelMenu.gameObject.SetActive(true); //turn on the pause menu
            pauseAllSounds(true);
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
            nbHitsText.gameObject.SetActive(true);
            Time.timeScale = 1f; //resume game
            pauseAllSounds(false);
        }
        

        public bool isPausedFct()
        {
            return isPaused;
        }

        public void stopAllSounds()
        {
            ball.GetComponent<BallMovement>().stopSounds();
        }

        public void pauseAllSounds(bool state)
        {
            ball.GetComponent<BallMovement>().pauseSounds(state);
        }

        public void reinitAllFall()
        {
            foreach (Transform child in ballGroup.transform)
            {
                child.GetComponent<OtherBallsMovement>().reinitFall();
            }
            ball.GetComponent<BallMovement>().reinitFall();
        }

        public void stopAllBalls()
        {
            foreach (Transform child in ballGroup.transform)
            {
                child.GetComponent<OtherBallsMovement>().stop();
            }
            ball.GetComponent<BallMovement>().stop();
        }
   

    }
}
