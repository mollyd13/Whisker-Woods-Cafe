using UnityEngine;
using TMPro;
using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine.UI;
using UnityEditor.SearchService;
using System;

public class Customer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public string iLikeIt;
    public string iDontLikeIt;
    public string minigame;
    public float textSpeed;

    private int index;
    private SpriteRenderer sr;
    public Sprite[] characterSprites;
    private int charIndex;
    private SceneManager sm;
    private BehindCounterManager bcm;
    public bool minigameComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minigameComplete = false;
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        bcm = GameObject.Find("BehindCounterManager").GetComponent<BehindCounterManager>();
        sr = GetComponent<SpriteRenderer>();
        charIndex = 0;
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            if (textComponent.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue() {
        if (PlayerPrefs.HasKey(minigame + "Score")){
            minigameComplete = true;
            if (PlayerPrefs.GetFloat(minigame + "Score") >= 15){
                lines = new string[] {iLikeIt};
            }
            else {
                lines = new string[] {iDontLikeIt};
            }
            PlayerPrefs.DeleteKey(minigame + "Score");
        }
        index = 0;
        StopAllCoroutines();
        textComponent.text = String.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        //type each character one by one

        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            NextCharSprite();
        }
        else {
            if (minigameComplete){
                bcm.NextCustomer();
            }
            else {
                sm.StartMinigame(minigame);
            }
        }
    }

    void NextCharSprite() {
        charIndex++;
        sr.sprite = characterSprites[charIndex];
    }
}
