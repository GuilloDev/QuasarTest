using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    public float playerSpeed;
    private float moveX;
    private float moveZ;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 playerInput = new Vector3(moveX, 0, moveZ);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        playerInput = transform.TransformDirection(playerInput);

        if (playerInput != Vector3.zero)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        if (!characterController.isGrounded)
            playerInput += Physics.gravity;

        characterController.Move(playerInput * playerSpeed * Time.deltaTime);
    }
}
