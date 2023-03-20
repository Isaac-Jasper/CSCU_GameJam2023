using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private GameObject upNode, downNode, leftNode, rightNode;
    enum TileType {
        emptyTile,
        wallTile,
        foodTile
    };
    [SerializeField]
    private TileType tileType;
    private void Start() {
        if (tileType != TileType.wallTile && (upNode == null || downNode == null || leftNode == null || rightNode == null)) {
            //Debug.LogError("Error: Nodes require a connection in all directions unless they are a wall");
        }
    }
    public GameObject getNode(int direction) {
        switch (direction) {
            case 0:
                return upNode;
            case 1:
                return downNode;
            case 2:
                return leftNode;
            case 3:
                return rightNode;
            default:
                return null;
        }
    }
    public bool isWall() {
        return tileType == TileType.wallTile;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Wall") {
            tileType = TileType.wallTile;
        }

        /* string dir = collision.name;
        switch (dir) {
            case "Down":
                upNode = collision.transform.parent.gameObject;
                break;
            case "Up":
                downNode = collision.transform.parent.gameObject;
                break;
            case "Right":
                leftNode = collision.transform.parent.gameObject;
                break;
            case "Left":
                rightNode = collision.transform.parent.gameObject;
                break;
        } */
    }
}
