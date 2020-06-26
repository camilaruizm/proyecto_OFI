using UnityEngine;
using Photon.Pun;

public class Walls : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            if (collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                BeerGameLogic.Instance.TerminarTurno();

                PhotonNetwork.Destroy(collider.gameObject);
            }
        }
    }
}
