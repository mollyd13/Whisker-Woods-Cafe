using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject cover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealCard(){
        Debug.Log("clicked");
        cover.SetActive(false);
    }
}
