using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private float fadeInSpeed;
    [SerializeField]
    private GameObject[] roundTiles;
    [SerializeField]
    private float time;
    int currentRound = 0;
    public void fadeNextIn() {
        GameObject curRound = roundTiles[++currentRound];
        curRound.SetActive(true);
        StartCoroutine(fadeNextIn(curRound));
    }
    private IEnumerator fadeNextIn(GameObject round) {
        Tilemap tilemap = round.GetComponent<Tilemap>();
        float color = 0;
        while (color < 1) {
            color += fadeInSpeed;
            for (int i = -20; i < 20; i++) {
                for (int j = -20; j < 20; j++) {
                    tilemap.SetColor(new Vector3Int(i, j, 0), new Color(1, 1, 1, color));
                }
            }
            yield return new WaitForSeconds(time);
        }
    }
    public bool isWin() {
        return currentRound == roundTiles.Length - 1;
    }
}
