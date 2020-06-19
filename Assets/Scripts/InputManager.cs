using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject corona;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Topo")
                {
                    Instantiate(corona,hit.point, Quaternion.identity);
                   /* TopoBehaviour topo = hit.collider.gameObject.GetComponent<TopoBehaviour>();
                    topo.SwitchCollider(0);
                    topo.anim.SetTrigger("hit");*/
                   
                }
            }
        }
    }
}
