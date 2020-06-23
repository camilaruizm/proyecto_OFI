using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TopoBehaviour : MonoBehaviourPunCallbacks
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
        if (GetComponent<PhotonView>().IsMine)
        {
            SwitchCollider(0);
            GetComponent<PhotonView>().RPC("HitTriggerRPC", RpcTarget.All);
        }
    }

    public void DestruirObjeto()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            myParent.GetComponent<HoleBehavior>().hasMole = false;
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void HitTriggerRPC()
    {
        anim.SetTrigger("hit");
    }

    public void SwitchCollider(int num)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            col.enabled = (num == 0) ? false : true;
        }
    }

    public void GotHit()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            hitPoints--;
            if (hitPoints > 0)
            {
                col.enabled = true;
            }
            else
            {
                myParent.GetComponent<HoleBehavior>().hasMole = false;
                //MyScoreManager.AddScore(score);
                ScoreManager.Instance.GiveScore(GameLogic.Instance.playingPlayer, score);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
