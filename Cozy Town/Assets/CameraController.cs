using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 1.0f;
    public CinemachineVirtualCamera virtualCamera;
    public AnimationCurve zoom_Fov;
    public float zoomLevel = 0.5f;

    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.parent = null;
        GameManager.PlayerInputMap.Camera.RotateCamera.started += HandleCameraRotationInput;
        GameManager.PlayerInputMap.Camera.RotateCamera.canceled += HandleCameraRotationInput;
        GameManager.PlayerInputMap.Camera.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (isRotating)
        {
            transform.Rotate(Vector3.up, GameManager.PlayerInputMap.Camera.MouseMovement.ReadValue<Vector2>().x, Space.World);
        }
        Zoom();
        PlaceOnGround();
    }

    private void Movement()
    {
        float horizontal = GameManager.PlayerInputMap.Camera.Horizontal.ReadValue<float>();
        float vertical = GameManager.PlayerInputMap.Camera.Vertical.ReadValue<float>();

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        // Move depending on main camera's forward direction
        Vector3 moveDirection = Camera.main.transform.forward * direction.z + Camera.main.transform.right * direction.x;
        moveDirection.y = 0;
        transform.position += moveDirection * cameraSpeed * Time.deltaTime;
    }

    public void PlaceOnGround()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + new Vector3(0,10,0),5, Vector3.down, out hit, 100, groundLayer))
        {
            transform.position = new Vector3(transform.position.x,hit.point.y,transform.position.z);
        }
    }
    public void Zoom()
    {   
        // Get the UI Element the mouse is over
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Mouse.current.position.ReadValue();
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        if(results.Count > 0)
        {
            if(results[0].gameObject.GetComponent<ScrollRectScrollable>() != null)
            {
                return;
            }
        }
        zoomLevel -= GameManager.PlayerInputMap.Camera.Zoom.ReadValue<Vector2>().y * 0.05f;
        zoomLevel = Mathf.Clamp(zoomLevel, 0, 1);
        virtualCamera.m_Lens.FieldOfView = zoom_Fov.Evaluate(zoomLevel);
    }


    private bool isRotating = false;

    public void HandleCameraRotationInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (ctx.canceled)
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    private void OnDisable()
    {
        GameManager.PlayerInputMap.Camera.RotateCamera.started -= HandleCameraRotationInput;
        GameManager.PlayerInputMap.Camera.RotateCamera.canceled -= HandleCameraRotationInput;
    }
}
