using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject proyectilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
            if (Input.GetMouseButtonDown(0)) {

                Debug.Log("Pressed primary button.");
                GameObject bullet = Instantiate(proyectilPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            }
        
    }
}
