using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingManager : MonoBehaviour
{
    public GameObject[] cards;
    public Color[] colors;
    public int flippedCount;
    public int pairsFound;
    public List<GameObject> flippedCards;
    public int timeSec;
    public int timeMin;
    public GameObject timePanel;
    public TextMeshProUGUI timeText;
    public GameObject gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText = timePanel.GetComponent<TextMeshProUGUI>();
        flippedCount = 0;
        pairsFound = 0;
        timeSec = 0;
        timeMin = 0;
        InvokeRepeating("IncrementTime", 1, 1);
        //get all the card game objects
        for (int i = 0; i < gameObject.transform.childCount; i++){
            GameObject currentCard = gameObject.transform.GetChild(i).gameObject;
            cards[i] = currentCard;
        }
        //shuffle cards so the colors are random
        ShuffleCards();
        //assign a color to each card
        AssignCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (flippedCount == 2){
            CheckCards();
        }
    }

    public void CheckCards(){
        if (flippedCards[0].GetComponent<UnityEngine.UI.Image>().color == flippedCards[1].GetComponent<UnityEngine.UI.Image>().color){
            CheckWin();
        }
        else{
            StartCoroutine("FlipDelay");
        }
        flippedCount = 0;
        flippedCards = new List<GameObject>();
    }

    public void FlipCard(GameObject card){
        flippedCount++;
        flippedCards.Add(card);
    }

    public void ShuffleCards(){
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, gameObject.transform.childCount);
            GameObject temp = cards[i];
            cards[i] = cards[randIndex];
            cards[randIndex] = temp;
        }
    }

    public void AssignCards(){
        colors = new Color[]{Color.red,
        Color.white,
        Color.black, 
        Color.blue, 
        Color.green, 
        Color.yellow, 
        Color.cyan, 
        Color.magenta, 
        Color.gray,
        new Color(1f, 0.5f, 0f)};

        int colorIndex = 0;
        for (int i = 0; i < cards.Length; i+=2){
            cards[i+1].GetComponent<UnityEngine.UI.Image>().color = colors[colorIndex];
            cards[i].GetComponent<UnityEngine.UI.Image>().color = colors[colorIndex];
            colorIndex++;
        }

    }

    public IEnumerator FlipDelay()
    {
        GameObject card1 = flippedCards[0];
        GameObject card2 = flippedCards[1];
        yield return new WaitForSeconds(0.5f);
        card1.GetComponent<Card>().cover.SetActive(true);
        card2.GetComponent<Card>().cover.SetActive(true);
    }

    public void CheckWin()
    {
        pairsFound++;
        if (pairsFound >= cards.Length / 2)
        {
            CancelInvoke("IncrementTime");
            Debug.Log("You win!");
            float scoreVal = (timeSec + timeMin * 60 <= 45) ? 15f : 0f;
            // store score in the in-memory GameManager (assume it exists)
            GameManager.Instance.SetScore("Matching", scoreVal);
            gameOverScreen.SetActive(true);
            gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Time: " + timeMin.ToString() + ":" + (timeSec < 10 ? "0" : "") + timeSec.ToString();
        }
    }

    public void IncrementTime()
    {
        timeSec++;

        if (timeSec >= 60)
        {
            timeMin++;
            timeSec = 0;
        }

        if (timeSec < 10)
        {
            timeText.text = timeMin.ToString() + ":0" + timeSec.ToString();
        }
        else
        {
            timeText.text = timeMin.ToString() + ":" + timeSec.ToString();
        }
    }
    
    public void devSkip()
    {
        GameManager.Instance.SetScore("Matching", 15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("BehindCounter");
    }
}
