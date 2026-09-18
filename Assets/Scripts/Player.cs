using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Camera mainCam;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;

        //world space dimensions of camera
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        minX = mainCam.transform.position.x - camWidth;
        maxX = mainCam.transform.position.x + camWidth;
        minY = mainCam.transform.position.y - camHeight;
        maxY = mainCam.transform.position.y + camHeight;
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"{moveInput}");
    }

    void FixedUpdate()
    {
        //move
        Vector2 move = new Vector2(moveInput.x,moveInput.y);
        controller.Move(move * speed * Time.fixedDeltaTime);

        //clamp 
        Vector2 currPosition = transform.position;
        currPosition.y = Mathf.Clamp(currPosition.y,minY,maxY);
        currPosition.x = Mathf.Clamp(currPosition.x,minX,maxX);
        transform.position = currPosition;
    }
}
