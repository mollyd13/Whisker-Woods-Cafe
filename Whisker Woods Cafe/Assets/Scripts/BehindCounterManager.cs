using System.Collections;
using UnityEngine;

public class BehindCounterManager : MonoBehaviour
{
    [SerializeField] Customer[] customers = new Customer[2];
    [SerializeField] int currCustomer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If a minigame saved which customer started it, restore that index and clear the key
        if (PlayerPrefs.HasKey("currCustomer"))
        {
            currCustomer = PlayerPrefs.GetInt("currCustomer", 0);
            PlayerPrefs.DeleteKey("currCustomer");
        }
        else
        {
            currCustomer = 0;
        }

        // ensure each Customer knows its index so they can save/restore themselves
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].customerIndex = i;
            // deactivate others to be safe
            customers[i].gameObject.SetActive(i == currCustomer);
        }

        StartCoroutine(StartDialogueAfterInit());
    }

    IEnumerator StartDialogueAfterInit()
    {
        yield return null; // Wait one frame to ensure all Start() methods have run
        customers[currCustomer].StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCustomer()
    {
        customers[currCustomer].gameObject.SetActive(false);
        currCustomer++;
        if (currCustomer < customers.Length)
        {
            customers[currCustomer].gameObject.SetActive(true);
            StartCoroutine(StartDialogueAfterInit());
        }
    }


    public void devClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
