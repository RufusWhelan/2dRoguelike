using UnityEngine;
public class playerInputScript : MonoBehaviour
{
    public WeaponControllerScript weaponController;
    [SerializeField] private Camera playerCamera;
    [HideInInspector] public Vector2 playerMovementInput;
    [HideInInspector] public bool dashInput;
    public bool attackInput;
    [HideInInspector] public bool breakAttackInput;

    private float dashBuffer;
    private float dashBufferLength = 0.15f;
    public Vector3 mousePosition;
    public void PlayerInput()
    {
        playerMovementInput = playerMovementInput.normalized;

        playerMovementInput.x = Input.GetAxisRaw("Horizontal");
        playerMovementInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift)) dashPlayerInput();
        if (dashBuffer > 0 && (playerMovementInput.x != 0 || playerMovementInput.y != 0)) dashInput = true;

        if (Input.GetKeyDown(KeyCode.Mouse0)) attackInput = true;
        if (Input.GetKeyDown(KeyCode.Mouse1)) breakAttackInput = true;
    }

    public void mousePlayerInput()
    {
        mousePosition = Input.mousePosition;
    }
    public void dashPlayerInput()
    {
        dashBuffer = dashBufferLength;
    }

    public void Update()
    {
        mousePlayerInput();

        if (Input.GetKeyDown(KeyCode.T))
        {
            weaponController.AddCell(0);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            weaponController.AddCell(1);
        }
    }
    public void FixedUpdate()
    {
        dashBuffer = dashBuffer - Time.deltaTime;
    }
}   
