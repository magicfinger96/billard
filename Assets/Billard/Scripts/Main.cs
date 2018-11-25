using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    private bool isPaused;
    [SerializeField]
    Transform UIPanel;
    private GameObject balls;
    public GameObject ball;
    public GameObject cue;
    public GameObject prefabsBalls;
    public Transform origin;
    public Vector3 positionBall;
    public Vector3 positionCue;
    public Quaternion rotationCue;

    void Start () {
        isPaused = false;
        UIPanel.gameObject.SetActive(false);
        positionBall = ball.GetComponent<Transform>().position;
        positionCue = cue.GetComponent<Transform>().position;
        rotationCue = cue.GetComponent<Transform>().rotation;
        loadGame();
    }

    public void loadGame()
    {
        if (balls != null) {
            Destroy(balls);
        }
        balls = Instantiate(prefabsBalls,origin);
        ball.transform.position= positionBall;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cue.transform.position = positionCue;
        cue.transform.rotation = rotationCue;
        cue.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            UnPause();
    }

    public void Pause()
    {
        isPaused = true;
        UIPanel.gameObject.SetActive(true); //turn on the pause menu
        Time.timeScale = 0f; //pause the game
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
