using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputSystem inputSystem;
    Player player;
    Rigidbody2D rb;
    AudioSource audioSource;

    [Header("Movement")]
    [SerializeField] AudioClip footstepAudioClip;
    public float moveSpeed;
    public float dashSpeed;
    public float dashCooldown;
    [HideInInspector] public bool isDashing = false;

    Vector2 movement;

    #region MonoBehaviour Methods

    private void Start()
    {
        inputSystem = GetComponent<PlayerInput>().inputSystem;
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #endregion

    #region Public Methods

    public IEnumerator Dash()
    {
        if (isDashing)
            yield break;

        isDashing = true;
        moveSpeed += dashSpeed;

        yield return new WaitForSeconds(0.1f);

        moveSpeed -= dashSpeed;

        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }

    public void Flip(float x)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    #endregion

    #region Private Methods

    void Movement()
    {
        movement.x = inputSystem.Player.Movement.ReadValue<Vector2>().x;
        movement.y = inputSystem.Player.Movement.ReadValue<Vector2>().y;

        if (movement != Vector2.zero)
        {
            // audioSource.PlayOneShot(footstepAudioClip);
            player.anim.SetBool("IsMoving", true);
            player.anim.SetFloat("MoveHorizontal", movement.x);
            player.anim.SetFloat("MoveVertical", movement.y);
        }
        else
        {
            player.anim.SetBool("IsMoving", false);
        }

        if (movement.x < 0)
            Flip(-0.6734f);
        else if (movement.x > 0)
            Flip(0.6734f);

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    #endregion
}
