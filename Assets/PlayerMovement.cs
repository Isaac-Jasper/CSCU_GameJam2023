using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float TILE_SIZE = 1.5f
                , MOVE_SPEED = 1;
    [SerializeField]
    private Rigidbody2D RB;
    [SerializeField]
    private GameObject nextObject;
    private Node nextNode;
    private Vector3 nextPosition;
    private Vector3 nextPositionNormalized;
    [SerializeField]
    private int nextDirection = (int)directions.left;
    private enum directions {
        noDir = -1,
        up,
        down,
        left,
        right
    }
    private void Start() {
        nextNode = nextObject.GetComponent<Node>();
        nextPosition = nextObject.transform.position;
        nextPositionNormalized = (transform.position - nextPosition).normalized;
    }
    void Update() {
        if (Input.GetKeyDown("w"))
            nextDirection = (int) directions.up;
        if (Input.GetKeyDown("s"))
            nextDirection = (int) directions.down;
        if (Input.GetKeyDown("d"))
            nextDirection = (int) directions.right;
        if (Input.GetKeyDown("a"))
            nextDirection = (int) directions.left;

        if (nextDirection == (int) directions.noDir) {
            //stay still
        }
        else if (Vector3.Distance(transform.position, nextPosition) > MOVE_SPEED * 0.01) {
            transform.position = transform.position - nextPositionNormalized * MOVE_SPEED * Time.deltaTime;
        } else {
            transform.position = nextPosition;
            GameObject tempObject = nextNode.getNode(nextDirection);
            nextNode = tempObject.GetComponent<Node>();
            if (nextNode.isWall())
                lose();
            else {
                nextPosition = tempObject.transform.position;
                nextPositionNormalized = (transform.position - nextPosition).normalized;
            }
        }
    }
    private void lose() {
        Debug.Log("you died");
        nextDirection = (int) directions.noDir;
        //death noise
        //death animation
        //restart level quickly
    }
}
