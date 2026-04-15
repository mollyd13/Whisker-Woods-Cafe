using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public BehindCounterManager behindCounterManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BehindCounter")
        {
            behindCounterManager = GameObject.Find("BehindCounterManager").GetComponent<BehindCounterManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMinigame(string gameName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameName);
    }

    public void BackToCounter(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("BehindCounter");
    }

    public void toMissingBoard()
    {
        GameManager.Instance.lastCustomerIndex = behindCounterManager.currCustomer;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MissingBoard");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
