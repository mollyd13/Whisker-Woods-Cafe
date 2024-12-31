using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] drops;
    Camera mainCamera;
    Vector3 leftScreen;
    Vector3 rightScreen;
    [SerializeField] int milkDropCount;
    [SerializeField] int coffeeDropCount;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //initialize milk and coffee drop counts
        milkDropCount = 0;
        coffeeDropCount = 0;
        //get left and right bounds of the viewport
        mainCamera = Camera.main;
        leftScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        rightScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        InvokeRepeating("Spawn", 0, 2);
    }

    // Update is called once per frame
    void Spawn() {
        int index;
        //if all drops have been dropped cancel the call to Spawn and return
        if (coffeeDropCount == 10 && milkDropCount == 10){
            CancelInvoke();
            gameManager.GameOver();
            return;
        }
        //if enough coffee has been dropped, only drop milk
        if (coffeeDropCount == 10){
            index = 1;
            milkDropCount++;
        }
        //if enough milk has been dropped, only drop coffee
        else if (milkDropCount == 10){
            index = 0;
            coffeeDropCount++;
        }
        //if less than 10 milk and coffee has been dropped, randomly choose one
        else {
            index = Random.Range(0, drops.Length);
            if (index == 0)
            {
                coffeeDropCount++;
            }
            else
            {
                milkDropCount++;
            }
        }
        //spawn specified prefab at a random x position above the viewport as a child of SpawnManager GO
        Instantiate(drops[index], new Vector3(Random.Range(leftScreen.x+.5f, rightScreen.x-.5f), gameObject.transform.position.y, 0), gameObject.transform.rotation, gameObject.transform);
    }
}
