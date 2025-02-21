using UnityEngine;
using TMPro;
using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine.UI;
using UnityEditor.SearchService;

public class Customer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private SpriteRenderer sr;
    public Sprite[] characterSprites;
    private int charIndex;
    private SceneManager sm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sr = GetComponent<SpriteRenderer>();
        charIndex = 0;
        textComponent.text = string.Empty;
        StartDialogue();
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

    void StartDialogue() {
        index = 0;
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
            sm.StartMinigame("CoffeeCatching");
            // gameObject.SetActive(false);
        }
    }

    void NextCharSprite() {
        charIndex++;
        sr.sprite = characterSprites[charIndex];
    }
}
