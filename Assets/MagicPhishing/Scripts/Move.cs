using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 12.0f;

    private Vector3 posInicial = new Vector3(0.0f, -2.6f, 0.0f);
    private Vector3 pescar = new Vector3(0.0f, -13.33f, 0.0f);
    public float pescaSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        
        player.Move(playerInput * playerSpeed * Time.deltaTime);

        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Vector3.MoveTowards(transform.position, pescar, pescaSpeed);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = Vector3.MoveTowards(transform.position, posInicial, pescaSpeed);
        }
    }
}