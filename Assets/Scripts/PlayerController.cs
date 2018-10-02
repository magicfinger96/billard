using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rg;
    private new MeshRenderer renderer;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float speedRotation = 2.0f;
    [SerializeField]
    private float distanceInteract = 2.0f;

    [SerializeField]
    private Interactable_NPC_Manager NPC_Manager;
    [SerializeField]
    private Camera TPSCamera;
    private Transform saveCameraInfo;

    [SerializeField]
    private Text interact;
    private Interactable_NPC PNJToInteract;

	void Start ()
    {
        PNJToInteract = null;
        rg = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();
	}
	
	void Update ()
    {
        if(!NPC_Manager.DiscussionInProcess())
        { 
            float x = Input.GetAxisRaw("Horizontal");
            float yRotation = Input.GetAxisRaw("Mouse X");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 verticalMovement = transform.right * x;
            Vector3 horizontalMovement = transform.forward * z;

            Vector3 velocity = (verticalMovement + horizontalMovement).normalized * speed;
            Vector3 rotation = new Vector3(0f, yRotation, 0f) * speedRotation;

            PerformMovement(velocity);
            PerformRotation(rotation);

            if(IsNearPNJ())
            {
                interact.gameObject.SetActive(true);
            }
            else
            {
                interact.gameObject.SetActive(false);
            }
        }

        if(interact.enabled && Input.GetKeyDown(KeyCode.F))
        {
            EnterInteractionWithNPC();
        }
    }

    public void EnterInteractionWithNPC()
    {
        interact.gameObject.SetActive(false);
        saveCameraInfo = TPSCamera.transform;
        Debug.Log(saveCameraInfo.position);
        PNJToInteract.Interact();
        renderer.gameObject.SetActive(false);
    }

    public void ExitInteractionWithNPC()
    {
        renderer.gameObject.SetActive(true);
        Debug.Log(saveCameraInfo.position);
        TPSCamera.transform.position = saveCameraInfo.position;
        TPSCamera.transform.rotation = saveCameraInfo.rotation;
    }

    private bool IsNearPNJ()
    {
        int layerMask = LayerMask.GetMask("NPC");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceInteract, layerMask))
        {
            if(hit.transform.tag == "NPC")
            {
                PNJToInteract = hit.transform.gameObject.GetComponent<Interactable_NPC>();
                return true;
            }
        }
        return false;
    }

    void PerformMovement(Vector3 velocity)
    {
        rg.MovePosition(rg.position + velocity * Time.deltaTime);
    }

    void PerformRotation(Vector3 rotation)
    {
        rg.MoveRotation(rg.rotation * Quaternion.Euler(rotation));
    }
}
