using UnityEngine;

public class CupController : MonoBehaviour
{
    public int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 900;
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
        newPosition.x = Mathf.Clamp(newPosition.x, -387.5f, 387.5f);
        //set position to equal new calculated and clamped position
        transform.localPosition = newPosition;
    }
}
