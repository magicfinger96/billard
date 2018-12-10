using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class AllBalls : MonoBehaviour
    {
        [SerializeField]
        protected AudioClip sinking;
        [SerializeField]
        protected AudioClip touchingBall;
        [SerializeField]
        protected AudioClip touchingHoleFiller;
        [SerializeField]
        protected AudioSource source;
        [SerializeField]
        protected AudioSource touchingHoleFillerSource;
        [SerializeField]
        protected AudioSource touchingBallSource;

        protected float volLowRange = .5f;
        protected float volHighRange = 1.0f;
        protected bool isPlayingSikingNoise;
        protected bool isPlayingTouchingBallSound;
        protected bool isPlayingTouchingHoleSound;
        protected Rigidbody rb;
        protected Main game;
        protected bool fellInHole;
        [SerializeField]
        protected GameObject ballGroup;
        [SerializeField]
        protected GameObject holeFillerGroup;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            isPlayingSikingNoise = false;
            isPlayingTouchingBallSound = false;
            isPlayingTouchingHoleSound = false;
            game = GameObject.Find("Table").GetComponent<Main>();
            fellInHole = false;
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator makeSikingNoise()
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(sinking, vol);
            yield return new WaitWhile(() => source.isPlaying);
            fellInHole = true;
            isPlayingSikingNoise = false;
        }

        IEnumerator makeTouchingBallSound()
        {
            float vol = Random.Range(volLowRange, volHighRange);
            touchingBallSource.PlayOneShot(touchingBall, vol);
            yield return new WaitWhile(() => touchingBallSource.isPlaying);
            isPlayingTouchingBallSound = false;
        }

        IEnumerator makeTouchingHoleFillerSound()
        {
            float vol = Random.Range(volLowRange, volHighRange);
            touchingHoleFillerSource.PlayOneShot(touchingHoleFiller, vol);
            yield return new WaitWhile(() => touchingHoleFillerSource.isPlaying);
            isPlayingTouchingHoleSound = false;
        }


        public void pauseSounds(bool state)
        {
            if (state)
            {
                source.Pause();
                touchingBallSource.Pause();
                touchingHoleFillerSource.Pause();
            }
            else
            {
                source.UnPause();
                touchingBallSource.UnPause();
                touchingHoleFillerSource.UnPause();
            }
        }

        public void stopSounds()
        {
            source.Stop();
            StopCoroutine("makeSikingNoise");
            isPlayingSikingNoise = false;

            touchingBallSource.Stop();
            StopCoroutine("makeTouchingBallSound");
            isPlayingTouchingBallSound = false;

            isPlayingTouchingHoleSound = false;
            touchingHoleFillerSource.Stop();
            StopCoroutine("makeTouchingHoleFillerSound");

        }

        public bool isMoving()
        {
            return rb.velocity.magnitude > 0.01f || rb.angularVelocity.magnitude > 0.01f;
        }

        public void stop()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void reinitFall()
        {
            fellInHole = false;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "Bottom" && !isPlayingSikingNoise)
            {
                fellInHole = true;
                isPlayingSikingNoise = true;
                StartCoroutine("makeSikingNoise");

            } else if ((col.gameObject.name == "Ball" || col.gameObject.transform.IsChildOf(ballGroup.transform)) && !isPlayingTouchingBallSound)
            {
                isPlayingTouchingBallSound = true;
                touchingBallSource.volume = (rb.velocity.magnitude);
                StartCoroutine("makeTouchingBallSound");
            } else if ( col.gameObject.transform.IsChildOf(holeFillerGroup.transform) && !isPlayingTouchingHoleSound)
            {
                isPlayingTouchingHoleSound = true;
                touchingHoleFillerSource.volume = rb.velocity.magnitude;
                StartCoroutine("makeTouchingHoleFillerSound");
            }
        }
    }
}
