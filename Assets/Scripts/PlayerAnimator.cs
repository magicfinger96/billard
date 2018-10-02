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
        float speedPercent = controller.velocityGlobal.magnitude / controller.Speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
