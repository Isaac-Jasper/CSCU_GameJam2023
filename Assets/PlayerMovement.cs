using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float TILE_SIZE = 1.5f
                , MOVE_SPEED = 1;
    [SerializeField]
    private Rigidbody2D RB;

    void Update()
    {
        if (Input.GetKeyDown("w"))
            RB.velocity = Vector2.up * MOVE_SPEED;
        if (Input.GetKeyDown("s"))
            RB.velocity = Vector2.down * MOVE_SPEED;
        if (Input.GetKeyDown("d"))
            RB.velocity = Vector2.right * MOVE_SPEED;
        if (Input.GetKeyDown("a"))
            RB.velocity = Vector2.left * MOVE_SPEED;
    }
}
