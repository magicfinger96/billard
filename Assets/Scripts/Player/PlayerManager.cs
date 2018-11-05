using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAttributes))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerQuestManager))]
public class PlayerManager : MonoBehaviour {

    #region ComponentRequired
    private new Rigidbody rigidbody;

    private PlayerAttributes attributs;
    public PlayerAttributes Attributs
    {
        get { return attributs; }
        private set { }
    }

    private PlayerGraphics graphic;
    public PlayerGraphics Graphic
    {
        get { return graphic; }
        private set { }
    }

    private PlayerQuestManager quests;
    public PlayerQuestManager Quests
    {
        get { return quests; }
        private set { }
    }

    #endregion
    #region ObjectReferenceRequired

    [SerializeField]
    private GUIManager guiManager;
    [SerializeField]
    private InteractableNPCManager npcManager;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private GameObject menuPanel;

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
    private bool isInMenu;
    public bool IsInMenu
    {
        get { return isInMenu; }
        set { isInMenu = value; }
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
    #region CameraSaveAttributs
    private Vector3 localCameraPositionSave;
    private Quaternion localCameraRotationSave;
    #endregion

    #region InteractionAttributs
    [SerializeField]
    private LayerMask layerToInteractable;
    private Interactable interactable;
    private KeyCode interactableKeyCode;
    #endregion

    private void Start () {
        this.isMoving = false;
        this.isInMenu = false;
        this.interactable = null;
        this.attributs = GetComponent<PlayerAttributes>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.quests = GetComponent<PlayerQuestManager>();
        this.graphic = GetComponentInChildren<PlayerGraphics>();
        this.localCameraPositionSave = camera.transform.localPosition;
        this.localCameraRotationSave = camera.transform.localRotation;
    }
	
	private void Update () {
        if (!npcManager.DiscussionInProcess())
        {
            ManageMenuInteraction();
            if(!IsInMenu)
            {
                ManageMovementUpdate();
                ManageRotationUpdate();
                ManageInteractableEnvironnment();
                if (interactable != null && Input.GetKeyDown(interactableKeyCode))
                {
                    if (interactable.IsNPC())
                    {
                        guiManager.HideInteractableTextContent();
                        guiManager.HideHUDPlayer();
                        graphic.gameObject.SetActive(false);
                    }
                    interactable.Interact();
                }
            }
        }
	}

    public void ExitInteractionWithNPC()
    {
        graphic.gameObject.SetActive(true);
        guiManager.ShowHUDPlayer();
        camera.transform.localPosition = localCameraPositionSave;
        camera.transform.localRotation = localCameraRotationSave;
    }

    public void AcceptQuest(NPC_Quest_Scriptable_Object quest)
    {
        quests.AddNewQuest(quest);
    }

    private void CheckInteractableNear()
    {
        interactable = null;
        RaycastHit hit;
        Vector3 addingToCenter = new Vector3(0f, transform.localScale.y / 2, 0f);
        if (Physics.Raycast(transform.position + addingToCenter, transform.forward, out hit, attributs.InteractionDistance, layerToInteractable))
        {
            interactable = hit.transform.gameObject.GetComponentInChildren<Interactable>();
            if(interactable != null && interactable.IsInteractable())
            {
                interactableKeyCode = interactable.GetKeyInteract();
            }
        }
    }

    private void ManageMenuInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInMenu = !isInMenu;
            menuPanel.SetActive(isInMenu);
            if(!isInMenu)
            {
                guiManager.ShowHUDPlayer();
                quests.CloseDetailedQuest();
            }
            else
            {
                guiManager.HideHUDPlayer();
            }
        }
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

    private void ManageInteractableEnvironnment()
    {
        CheckInteractableNear();
        if (interactable != null && interactable.IsInteractable())
        {
            guiManager.SetInteractableTextContent(interactable.GetInteractableText());
            guiManager.ShowInteractableTextContent();
        }
        else
        {
            guiManager.HideInteractableTextContent();
        }
    }
}
