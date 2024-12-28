using UnityEngine;

public class CupController : MonoBehaviour
{
    public int speed;
    Vector3 leftScreen;
    Vector3 rightScreen;
    Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get left and right bounds of the viewport
        mainCamera = Camera.main;
        leftScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        rightScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        //initialize speed with hardcoded value
        speed = 25;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
    }

    void HorizontalMovement(){
        //get horizontal input
        float input = Input.GetAxis("Horizontal");
        //find new position
        Vector3 newPosition = transform.localPosition + Vector3.right * Time.deltaTime * input * speed;
        //clamp position to ensure it stays in bounds
        newPosition.x = Mathf.Clamp(newPosition.x, leftScreen.x+.5f, rightScreen.x-.5f);
        //set position to equal new calculated and clamped position
        transform.localPosition = newPosition;
    }
}
