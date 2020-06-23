using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    //public float upDownMove;
    public CharacterController player;

    //public float playerSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        //transform.position = new Vector3(3.3f, -2.6f, 3.6f);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        //upDownMove = Input.GetAxis("Axis 1");

        player.Move(new Vector3(horizontalMove, verticalMove, 0) /* playerSpeed * Time.deltaTime*/);
    }
}
