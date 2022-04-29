using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printmask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        int mask = GetComponent<Camera>().cullingMask;
        
        Debug.Log( "THIS LAYER: " + mask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
