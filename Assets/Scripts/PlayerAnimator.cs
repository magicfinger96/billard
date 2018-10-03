using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    [SerializeField]
    float locomotionAnimationSmoothTime = 0.1f;

    private PlayerController controller;
    private Animator animator;

	// Use this for initialization
	void Start () {
        controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedPercent;
        if (controller.CurrentSpeed == 0)
        {
            speedPercent = 0;
        }
        else
        {
            speedPercent = controller.velocityGlobal.magnitude / controller.CurrentSpeed;
        }
        
        Debug.Log(speedPercent);
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
