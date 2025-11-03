using System.Collections;
using UnityEngine;

public class BehindCounterManager : MonoBehaviour
{
    [SerializeField] Customer[] customers = new Customer[2];
    [SerializeField] int currCustomer=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("behindCounterManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.DeleteAll();
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
