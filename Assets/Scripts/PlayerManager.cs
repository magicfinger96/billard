using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttributes))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour {

    #region ComponentRequired
    private new Rigidbody rigidbody;
    private PlayerAttributes attributs;
    private PlayerGraphics graphic;
    #endregion

    #region StateAttributs

    // Attribut that give the information if the player
    //  is currrently interacting with something (and can't move)
    private bool isInteracting;
    public bool IsInteracting
    {
        get { return isInteracting; }
        set { isInteracting = value; }
    }

    // Attribut that give the information if the player
    //  is currrently moving or not
    private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    #endregion

    private new Camera camera;
    private Vector3 localCameraPositionSave;
    private Quaternion localCameraRotationSave;

    private void Start () {
        this.attributs = GetComponent<PlayerAttributes>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.graphic = GetComponentInChildren<PlayerGraphics>();
	}
	
	private void Update () {
        ManageMovementUpdate();
        ManageRotationUpdate();
	}

    private void ManageMovementUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            isMoving = true;
            Vector3 verticalDirection = transform.right * horizontal;
            Vector3 horizontalDirection = transform.forward * vertical;
            Vector3 direction = (verticalDirection + horizontalDirection).normalized;

            if (vertical > 0 && horizontal == 0)
            {
                attributs.CurrentSpeed = attributs.SpeedForward;
            }
            else
            {
                attributs.CurrentSpeed = attributs.SpeedBackward;
            }

            graphic.VelocityHorizontalAnimation = horizontal;
            graphic.VelocityVerticalAnimation = vertical;
            rigidbody.MovePosition(rigidbody.position + direction * attributs.CurrentSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            attributs.CurrentSpeed = 0f;
            graphic.VelocityHorizontalAnimation = 0f;
            graphic.VelocityVerticalAnimation = 0f;
        }
    }

    private void ManageRotationUpdate()
    {
        float rotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotationVector = new Vector3(0f, rotation, 0f);
        Vector3 rotationVectorSpeed = rotationVector * attributs.RotationSpeed * Time.deltaTime;
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotationVector));
    }
}
