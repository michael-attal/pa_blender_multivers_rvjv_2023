using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeToForce;
    
    [Header("Camera Controls")]
    [SerializeField] private float sensitivity;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;
    [SerializeField] private Transform firstPersonCamera;

    private Vector2 movementInput;
    private Vector2 rotation;
    private float jumpChargeTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        jumpChargeTime = 0;
    }

    void Update()
    {
        Debug.Log(Time.time);
        if (Input.GetButtonDown("Cancel"))
            SwitchCursor();

        if (Input.GetButtonDown("Jump"))
            jumpChargeTime = Time.time;
        if (Input.GetButtonUp("Jump"))
            body.velocity += new Vector3(0,  jumpForce * Mathf.Min((Time.time - jumpChargeTime) / timeToForce, 1), 0);
        
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        rotation.x += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        rotation.y += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, minYRotation, maxYRotation);
        
        transform.rotation = Quaternion.Euler(0, rotation.x, 0);
        firstPersonCamera.rotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * (speed * Time.fixedDeltaTime));
    }

    private void SwitchCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }
}
