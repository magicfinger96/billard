using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class AllBalls : MonoBehaviour
    {
        public AudioClip sinking;
        protected AudioSource source;
        protected float volLowRange = .5f;
        protected float volHighRange = 1.0f;
        protected bool isPlayingSikingNoise;
        protected Rigidbody rb;
        protected Main game;
        protected bool fellInHole;

        void Awake()
        {
            source = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
            isPlayingSikingNoise = false;
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

        public void pauseSounds(bool state)
        {
            if (state)
            {
                source.Pause();
            }
            else
            {
                source.UnPause();
            }
        }

        public void stopSounds()
        {
            source.Stop();
            StopCoroutine("makeSikingNoise");
            isPlayingSikingNoise = false;
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
            }
        }
    }
}
