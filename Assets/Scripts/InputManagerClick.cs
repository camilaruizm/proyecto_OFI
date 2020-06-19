using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerClick : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           
        }
    }
}
