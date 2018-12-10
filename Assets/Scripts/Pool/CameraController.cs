using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject ball;
    private Vector3 offset;
    private bool isAbove;
    private Vector3 posAbove;
    private Quaternion rotationAbove;
    private Quaternion rotationNear;
    private bool switchAbove;
    private bool switchNear;
    private bool moveRight;
    private bool moveLeft;
    private Vector3 zoomInOut;
    private float zoomValue;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startingLerping;
    private Vector3 defaultPos;
    private Quaternion defaultRotation;

    [SerializeField]
    protected AudioClip winSound;
    [SerializeField]
    protected AudioSource winSoundSource;


    // Use this for initialization
    void Start () {
        posAbove = new Vector3(-7.78f, 8.22f, 22.33f);
        rotationAbove = Quaternion.Euler(90.22699f, -90.93201f, 89.16f);
        defaultPos = transform.localPosition;
        defaultRotation = transform.localRotation;
        reinit();
    }

    // Réinitialise la rotation et position de la caméra
    public void reinit()
    {
        rotationNear = Quaternion.Euler(51.997f, -90.93001f, 0f);
        offset = new Vector3(2.4f, 1.7f, 10.7f) - ball.transform.position;
        switchAbove = false;
        switchNear = false;
        zoomInOut = Vector3.zero;
        zoomValue = 0f;
        transform.localPosition = defaultPos;
        transform.localRotation = defaultRotation;
        showNearView();
    }
	
	// Update is called once per frame
	void LateUpdate () {

            if (switchAbove)
            {
                float time = Time.time - startingLerping;
                float percentComplet = time / 0.7f;
                transform.localPosition = Vector3.Lerp(startPosition, posAbove, percentComplet);
                transform.localRotation = Quaternion.Lerp(startRotation, rotationAbove, percentComplet);
                if (percentComplet >= 1)
                {
                    switchAbove = false;
                }

            } else if (switchNear)
            {
                float time = Time.time - startingLerping;
                float percentComplet = time / 0.7f;
                Vector3 posTmp = ball.transform.position + offset;
                transform.position = Vector3.Lerp(startPosition, posTmp, percentComplet);
                transform.localRotation = Quaternion.Lerp(startRotation, rotationNear, percentComplet);

                if (percentComplet >= 1)
                {
                    switchNear = false;
                }
            }

            if (!isAbove && !switchNear)
            {

                transform.position = ball.transform.position + offset;
                if (zoomValue != 0)
                {
                    zoomInOut = transform.forward * Time.deltaTime * zoomValue;
                    offset = (transform.position - zoomInOut) - ball.transform.position;
                    transform.position = ball.transform.position + offset;
                    zoomValue = 0;
                }

                if (moveRight) {
                    transform.RotateAround(ball.transform.position, Vector3.up, 100 * Time.deltaTime);
                    offset = transform.position - ball.transform.position;

                } else if (moveLeft)
                {
                    transform.RotateAround(ball.transform.position, Vector3.up, -100 * Time.deltaTime);
                    offset = transform.position - ball.transform.position;
                }

            }
        
	}

    // Play the win sound
    public void playWinSound()
    {
        winSoundSource.PlayOneShot(winSound, 1f);
    }

    public void stopSound()
    {
        winSoundSource.Stop();
    }

    // Show the game above the pool
    public void showAboveView()
    {
        rotationNear = transform.localRotation;
        startingLerping = Time.time;
        isAbove = true;
        switchNear = false;
        switchAbove = true;
        moveRight = false;
        moveLeft = false;
        startRotation = transform.localRotation;
        startPosition = transform.localPosition;

    }

    // Show the game near the ball
    public void showNearView()
    {
        isAbove = false;
        switchAbove = false;
        switchNear = true;
        startRotation = transform.localRotation;
        startPosition = transform.position;
        startingLerping = Time.time;
        moveRight = false;
        moveLeft = false;
    }

    // Move to the left of the ball or stop it
    public void makeMoveRight(bool state)
    {
        moveRight = state;
    }

    // Move to the right of the ball or stop it
    public void makeMoveLeft(bool state)
    {
        moveLeft = state;
    }

    // Zoom in if state = true
    public void makeZoomIn()
    {
        if (!switchNear && !switchAbove) {
            if (isAbove && !switchNear)
            {
                showNearView();
            } else
            {
                if (Vector3.Distance(ball.transform.position, transform.position) >= 0.3f)
                {
                    zoomValue = -2;
                }
            }
        }

    }

    // Zoom out if state = true
    public void makeZoomOut()
    {
        if (!switchNear && !switchAbove) {
            if (Vector3.Distance(ball.transform.position, transform.position) <= 1.048f)
            {
                zoomValue = 2;
            } else if (!isAbove && !switchAbove)
            {
                showAboveView();
            }
        }
    }
}
