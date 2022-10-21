using UnityEngine;

public class CamController : MonoBehaviour
{
    public float camSpeed;
    public Transform target;
    public Transform player;
    float mouseX;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * camSpeed;

        transform.LookAt(target);
        target.rotation = Quaternion.Euler(0, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
