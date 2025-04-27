using System.Text.RegularExpressions;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject cover;
    public MatchingManager matchingManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        matchingManager = GameObject.Find("BG").GetComponent<MatchingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealCard(){
        cover.SetActive(false);
        matchingManager.FlipCard(gameObject);
    }
}
