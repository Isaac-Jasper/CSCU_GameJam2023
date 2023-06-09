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
    private int nextDirection = (int) directions.left, 
        prevDirection = (int) directions.left,
        curLevel = 0;
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
            SceneController.i.RestartScene();
        if (isPaused) return;
        if (Input.GetKeyDown("w")) {
            prevDirection = nextDirection;
            nextDirection = (int)directions.up;
        }
        if (Input.GetKeyDown("s")) {
            prevDirection = nextDirection;
            nextDirection = (int)directions.down;
        }
        if (Input.GetKeyDown("d")) {
            prevDirection = nextDirection;
            nextDirection = (int)directions.right;
        }
        if (Input.GetKeyDown("a")) {
            prevDirection = nextDirection;
            nextDirection = (int)directions.left;
        }

        if (nextDirection == (int) directions.noDir) {
            //stay still
        }
        else if (Vector3.Distance(transform.position, nextPosition) > MOVE_SPEED * 0.01) {
            transform.position = transform.position - nextPositionNormalized * MOVE_SPEED * Time.deltaTime;
        } else {
            if (prevDirection != nextDirection) SoundManager.PlaySound(SoundManager.Sound.Player_Turn);
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
    public void setPaused(bool set) {
        if (set) { nextDirection = (int)directions.noDir; prevDirection = nextDirection; }
        isPaused = set;
    }
    private void lose() {
        setPaused(true);
        isPaused = true;
        if (roundController.isWin()) {
            StartCoroutine(SceneController.i.changeScene(curLevel + 1));
        } else {
            SoundManager.PlaySound(SoundManager.Sound.Player_Death);
            StartCoroutine(SceneController.i.RestartScene());
        }
        //death animation
    }
}
