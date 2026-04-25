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
    public string[] iLikeIt;
    public string[] iDontLikeIt;
    public string minigame;
    public float textSpeed;
    public int customerIndex = 0;
    private int index;
    private SpriteRenderer sr;
    public Sprite[] characterSprites;
    private int charIndex;
    private SceneManager sm;
    private BehindCounterManager bcm;
    public bool minigameComplete = false;
    public AudioSource source;
    public AudioClip soundEffect;
    public GameObject tip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        bcm = GameObject.Find("BehindCounterManager").GetComponent<BehindCounterManager>();
        sr = GetComponent<SpriteRenderer>();
        charIndex = 0;
        textComponent.text = string.Empty;
        source = gameObject.GetComponent<AudioSource>();
        soundEffect = source.GetComponent<AudioClip>();
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
        // Check in-memory GameManager for a score saved by a minigame
        if (GameManager.Instance != null && GameManager.Instance.TryGetScore(minigame, out float score))
        {
            Debug.Log("Minigame score found in GameManager: " + score);
            minigameComplete = true;
            if (score >= 15){
                lines = iLikeIt;
            }
            else {
                lines = iDontLikeIt;
            }
            tip.SetActive(true);
            GameManager.Instance.ClearScore(minigame);
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
            source.Play();
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
                // save which customer started the minigame in GameManager so we can restore them after the scene reloads
                GameManager.Instance.lastCustomerIndex = customerIndex;
                sm.StartMinigame(minigame);
            }
        }
    }

    void NextCharSprite()
    {
        if (charIndex >= characterSprites.Length - 1)
        {
            charIndex = 0;
            Debug.Log(charIndex);
            sr.sprite = characterSprites[charIndex];
        }
        else
        {  
            charIndex++;
            Debug.Log(charIndex);
            sr.sprite = characterSprites[charIndex];
        }
    }
}
