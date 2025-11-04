using System.Collections;
using UnityEngine;

public class BehindCounterManager : MonoBehaviour
{
    [SerializeField] Customer[] customers = new Customer[2];
    [SerializeField] int currCustomer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ensure each Customer knows its index so they can save/restore themselves
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].customerIndex = i;
            // deactivate others to be safe; we'll activate the correct one below
            customers[i].gameObject.SetActive(false);
        }

        // Restore the customer index from GameManager (in-memory). If not set, start at 0.
        if (GameManager.Instance != null && GameManager.Instance.lastCustomerIndex >= 0)
        {
            currCustomer = GameManager.Instance.lastCustomerIndex;
            GameManager.Instance.lastCustomerIndex = -1; // clear after restoring
        }
        else
        {
            currCustomer = 0;
        }

        // activate only the current customer
        if (currCustomer >= 0 && currCustomer < customers.Length)
            customers[currCustomer].gameObject.SetActive(true);

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
}
