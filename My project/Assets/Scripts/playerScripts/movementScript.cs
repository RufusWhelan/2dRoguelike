using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public playerInputScript InputScript;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private float playerMovementSpeed;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash;

    void Start()
    {
        canDash = true;
    }
    void Update()
    {
        if (isDashing)
            return;
        InputScript.PlayerInput();
    }

    void FixedUpdate()
    {
        if (isDashing)
            return;
        MovePlayer();
        if (InputScript.dashInput && canDash == true)
        {
            StartCoroutine(Dash());
        }

    }

    private void MovePlayer()
    {
        playerBody.linearVelocity = new Vector2(InputScript.playerMovementInput.x * playerMovementSpeed, InputScript.playerMovementInput.y * playerMovementSpeed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        InputScript.dashInput = false;
        isDashing = true;
        playerBody.linearVelocity = new Vector2(InputScript.playerMovementInput.x * dashForce, InputScript.playerMovementInput.y * dashForce);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
