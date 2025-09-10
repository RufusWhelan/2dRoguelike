using UnityEngine;
public class playerInputScript : MonoBehaviour
{
    public WeaponControllerScript weaponController;
    [SerializeField] private Camera playerCamera;
    [HideInInspector] public Vector2 playerMovementInput;
    [HideInInspector] public bool dashInput;
    [HideInInspector] public bool attackInput;

    private float dashBuffer;
    private float dashBufferLength = 0.15f;
    public Vector3 mousePosition;
    public void PlayerInput()
    {
        playerMovementInput.x = Input.GetAxisRaw("Horizontal");
        playerMovementInput.y = Input.GetAxisRaw("Vertical");
        playerMovementInput = playerMovementInput.normalized;
        if (Input.GetKeyDown(KeyCode.LeftShift)) dashPlayerInput();
        if (dashBuffer > 0 && (playerMovementInput.x != 0 || playerMovementInput.y != 0)) dashInput = true;
        if (Input.GetKeyDown(KeyCode.Mouse0)) attackInput = true;
    }

    public void mousePlayerInput()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
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
