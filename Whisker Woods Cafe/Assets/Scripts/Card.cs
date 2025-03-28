using UnityEngine;

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
        cover.SetActive(false);
    }

    void OnMouseDown()
    {
        cover.SetActive(false);
        Debug.Log("clicked");
    }
}
