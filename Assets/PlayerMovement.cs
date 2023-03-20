using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float TILE_SIZE = 1.5f
                , MOVE_SPEED = 1;
    [SerializeField]
    private Rigidbody2D RB;
    [SerializeField]
    private GameObject nextObject;
    [SerializeField]
    private RoundController roundController;
    private Node nextNode;
    private Vector3 nextPosition;
    private Vector3 nextPositionNormalized;
    [SerializeField]
    private int nextDirection = (int)directions.left
        , curLevel = 0;
    bool isPaused = false;
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
        if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(curLevel);
        if (isPaused) return;
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
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "food") {
            Destroy(collision.gameObject);
            roundController.fadeNextIn();
        }
    }
    private void lose() {
        nextDirection = (int) directions.noDir;
        isPaused = true;
        if (roundController.isWin())
            Debug.Log("you win!");
        else 
            Debug.Log("you died");
        //death noise
        //death animation
    }
}
