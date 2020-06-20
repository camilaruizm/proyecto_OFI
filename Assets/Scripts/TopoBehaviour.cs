using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoBehaviour : MonoBehaviour
{
    Collider col;
    public int hitPoints = 1;
    public int score = 1;
    [HideInInspector] public GameObject myParent;
    [HideInInspector] public Animator anim;

    public GameObject popUpText;  

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void HitTrigger()
    {
        SwitchCollider(0);
        anim.SetTrigger("hit");
    }

    public void DestruirObjeto()
    {
        myParent.GetComponent<HoleBehavior>().hasMole = false;
        Destroy(gameObject);
    }

    public void SwitchCollider(int num)
    {
        col.enabled = (num == 0) ? false : true;
    }

    public void GotHit()
    {
        hitPoints--;

        if (hitPoints > 0)
        {
            col.enabled = true;
        }
        else
        {
            myParent.GetComponent<HoleBehavior>().hasMole = false;
            ScoreManager.AddScore(score);

         

            Destroy(gameObject);
        }
    }
}
