using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // in-memory minigame scores by name (e.g. "CoffeeCatching" -> 12.5f)
    private Dictionary<string, float> minigameScores = new Dictionary<string, float>();

    // index of the customer who started the last minigame; -1 means none
    public int lastCustomerIndex = -1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(string minigame, float score)
    {
        minigameScores[minigame] = score;
    }

    public bool TryGetScore(string minigame, out float score)
    {
        return minigameScores.TryGetValue(minigame, out score);
    }

    public void ClearScore(string minigame)
    {
        if (minigameScores.ContainsKey(minigame)) minigameScores.Remove(minigame);
    }

    // optional convenience to clear all stored scores
    public void ClearAllScores()
    {
        minigameScores.Clear();
    }
}
