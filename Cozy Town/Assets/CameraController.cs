using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.PlayerInputMap.Camera.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        print(GameManager.PlayerInputMap.Camera.Vertical.ReadValue<float>());
        float horizontal = GameManager.PlayerInputMap.Camera.Horizontal.ReadValue<float>();
        float vertical = GameManager.PlayerInputMap.Camera.Vertical.ReadValue<float>();

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        // Move depending on main camera's forward direction
        Vector3 moveDirection = Camera.main.transform.forward * direction.z + Camera.main.transform.right * direction.x;
        moveDirection.y = 0;
        transform.position += moveDirection * cameraSpeed * Time.deltaTime;
    }
}
