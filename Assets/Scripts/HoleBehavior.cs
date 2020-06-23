using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class HoleBehavior : MonoBehaviour {
  //lista de GameObject 
    public GameObject[] topos;
    public bool hasMole;

    void Start()
    {
        if(!hasMole)
        {
        //Sirve para llamar un método por un tiempo definido
         Invoke("Spawn", Random.Range(0f,7f));
        }
    }

    void Spawn()
    {
         if(!hasMole && GameManager.instance.myTurn)
         {
            //Se especifica cual es el GameObject que queremos hacer aparecer
             //int num = Random.Range(0,topos.Length);
             int num = CalculateRarity();
            //Instanciamos el arreglo de topos y le pasamos el random, el segundo parámetro corresponde a lo que queremos hacer con este GameObject
             GameObject topo = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", topos[num].name), transform.position, Quaternion.Euler(0f, -180f, 0f));
             topo.GetComponent<TopoBehaviour>().myParent = gameObject;
             hasMole = true;
         }
        Invoke("Spawn",Random.Range(0f,7f));
    }

    int CalculateRarity()
    {
        int numm = Random.Range(1,101);
        if(numm<=1)
        {
            return 3;
        }
        else if(numm<=5)
        {
            return 4;
        }
        else if(numm<= 20 )
        {
            return 2;
        }
        else if(numm<=50)
        {
            return 1;
        }
        
        return 0;
    }
}
