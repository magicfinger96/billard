using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerGraphics : MonoBehaviour {

    private Animator animator;

    #region AnimationAttributs

    [SerializeField]
    private float locomotionAnimationSmoothTime;
    public float LocomotionAnimationSmoothTime
    {
        get { return locomotionAnimationSmoothTime; }
        set { locomotionAnimationSmoothTime = value; }
    }

    private float velocityHorizontalAnimation;
    public float VelocityHorizontalAnimation
    {
        get { return velocityHorizontalAnimation; }
        set { velocityHorizontalAnimation = value; }
    }

    private float velocityVerticalAnimation;
    public float VelocityVerticalAnimation
    {
        get { return VelocityVerticalAnimation; }
        set { velocityVerticalAnimation = value; }
    }

    #endregion

    // Use this for initialization
    void Start () {
        this.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("here");
        animator.SetFloat("Horizontal", velocityHorizontalAnimation, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("Vertical", velocityVerticalAnimation, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
