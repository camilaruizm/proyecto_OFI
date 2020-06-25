using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPeces : MonoBehaviour
{
    public GameObject PezA;
    public GameObject PezAz;
    public GameObject PezV;
    public GameObject PezR;
    public GameObject PezAn;

    public int xPos;
    public int zPos;
    public int numPeces;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(FishDrop());
        //transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    IEnumerator FishDrop()
    {
        while (numPeces < 20)
        {
            xPos = Random.Range(-20, 20);
            zPos = Random.Range(-10, 25);
            
            if (numPeces < 5)
            {
                Instantiate(PezA, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
                numPeces += 1;
            }
            else if (numPeces < 10 && numPeces >= 5)
            {
                Instantiate(PezAz, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
                numPeces += 1;
            }
            else if (numPeces < 15 && numPeces >= 10)
            {
                Instantiate(PezV, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
                numPeces += 1;
            }
            else if (numPeces < 20 && numPeces >= 15)
            {
                Instantiate(PezR, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
                numPeces += 1;
            }
            /*
            Instantiate(PezA, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
            yield return new WaitForSeconds(0.1f);
            numPeces += 1;
            */
            yield return new WaitForSeconds(0.1f);
        }
        xPos = Random.Range(-20, 20);
        zPos = Random.Range(-10, 25);
            
        Instantiate(PezAn, new Vector3(xPos, -10, zPos), Quaternion.Euler(0, 90, 0));
    }
}
