using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject drop;
    Camera mainCamera;
    Vector3 leftScreen;
    Vector3 rightScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get left and right bounds of the viewport
        mainCamera = Camera.main;
        leftScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        rightScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        InvokeRepeating("Spawn", 0, 2);
    }

    // Update is called once per frame
    void Spawn() {
        Instantiate(drop, new Vector3(Random.Range(leftScreen.x+.5f, rightScreen.x-.5f), gameObject.transform.position.y, 0), gameObject.transform.rotation, gameObject.transform);
    }
}
