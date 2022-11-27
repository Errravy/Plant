using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public InputSystem inputSystem;
    Player player;
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    #region MonoBehaviour Methods

    private void Awake()
    {
        PlayerInputConfig();
    }

    private void Start()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    #endregion

    #region Public Methods

    public void DisableInput()
    {
        inputSystem.Player.Interact.Disable();
        inputSystem.Player.Movement.Disable();
        inputSystem.Player.Combat.Disable();
    }

    public void EnableInput()
    {
        inputSystem.Player.Interact.Enable();
        inputSystem.Player.Movement.Enable();
        inputSystem.Player.Combat.Enable();
    }

    #endregion

    #region Private Methods

    void PlayerInputConfig()
    {
        inputSystem = new InputSystem();

        inputSystem.Player.Interact.performed += ctx => Interaction(ctx);
        inputSystem.Player.Inventory.performed += ctx => Inventory(ctx);
        inputSystem.Player.PuzelInven.performed += ctx => PuzzleInven(ctx);
        inputSystem.Player.Puzzle.performed += ctx => Puzzle(ctx);
        inputSystem.Player.Dash.performed += ctx => Dash(ctx);
        inputSystem.Player.Combat.performed += ctx => Combat(ctx);
        inputSystem.Player.Collect.performed += ctx => Collect(ctx);
        inputSystem.Player.Menu.performed += ctx => Menu(ctx);
    }

    void Interaction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (player.currentNPC != null)
            {
                DisableInput();
                player.currentNPC.GetComponent<NPC>().Interact();
            }

            if (player.currentDoor != null)
                StartCoroutine(FindObjectOfType<TransitionHandler>().Transition(player.currentDoor));
        }
    }
    void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.Inventory();
        }
    }
    void PuzzleInven(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.PuzzleInventory();
        }
    }
    void Puzzle(InputAction.CallbackContext context)
    {
        if (context.performed && player.meja)
        {
            GameManager.Instance.Puzzle();
            GameManager.Instance.Inventory();
        }
    }
    void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !playerMovement.isDashing)
        {
            StartCoroutine(playerMovement.Dash());
        }
    }
    void Combat(InputAction.CallbackContext context)
    {
        if (context.performed && !playerCombat.isAttack && playerCombat.currentWeapon != null)
        {
            StartCoroutine(playerCombat.Combat());
        }
    }
    void Collect(InputAction.CallbackContext context)
    {
        if (context.performed && player.currentCollectable != null)
        {
            player.currentCollectable.GetComponent<ItemDropped>().Collect();
        }
        else if (context.performed && player.currentNPC != null)
        {
            player.currentNPC.GetComponent<NPC>().OpenShop();
        }
        else if (context.performed && player.currentPlant != null)
        {
            player.currentPlant.GetComponent<PlantDrop>().GetPlant();
        }
    }
    void Menu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.Pause();
        }
    }

    #endregion
}
