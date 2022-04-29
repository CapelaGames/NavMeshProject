using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printmask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.layer);
        //int water = LayerMask.NameToLayer("Water");

        int cullingmask = GetComponent<Camera>().cullingMask;


        Debug.Log(cullingmask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
