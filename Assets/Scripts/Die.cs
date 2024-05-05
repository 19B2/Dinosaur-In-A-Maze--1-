using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("here we go!!");
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("yooooooo!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
