using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject drop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Spawn", 0, 1);
    }

    // Update is called once per frame
    void Spawn() {
        Instantiate(drop, new Vector3(Random.Range(-12, 12), gameObject.transform.position.y, 0), gameObject.transform.rotation, gameObject.transform);
    }
}
