using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarHaciaTarget : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Laser");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(this.target.transform.position);
    }
}
