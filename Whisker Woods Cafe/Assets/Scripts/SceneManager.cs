using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
