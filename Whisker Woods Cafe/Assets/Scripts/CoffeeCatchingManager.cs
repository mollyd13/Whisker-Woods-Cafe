using System;
using TMPro;
using UnityEngine;

public class CoffeeCatchingManager : MonoBehaviour
{

    private ScoreManager scoreManager;
    [SerializeField] GameObject gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        Tuple<float, float> score = scoreManager.getSliderVal();
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Coffee caught:\n" + (int)(score.Item1*100) + "%\n" + "Milk caught:\n" + (int)(score.Item2 * 100) + "%";
        PlayerPrefs.SetFloat("CoffeeCatchingScore", (score.Item1 + score.Item2) * 10);
        Debug.Log(PlayerPrefs.GetFloat("CoffeeCatchingScore"));
    }
}
