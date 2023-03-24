using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private float fadeInSpeed,
        transitionSpeed;
    [SerializeField]
    private GameObject[] roundTiles;
    [SerializeField]
    private CanvasGroup textFadeGroup;
    private GameObject fadeObject;
    private PlayerMovement playerMovement;
    int currentRound = 0;

    public void fadeNextIn() {
        GameObject curRound = roundTiles[++currentRound];
        curRound.SetActive(true);
        StartCoroutine(fadeNextIn(curRound));
    }
    private IEnumerator fadeNextIn(GameObject round) {
        Tilemap tilemap = round.GetComponent<Tilemap>();
        float color = 0;
        SoundManager.PlaySound(SoundManager.Sound.Level_NextRound);
        while (color < 1) {
            color += fadeInSpeed;
            tilemap.color = new Color(1, 1, 1, color);
            yield return null;
        }
    }
    public bool isWin() {
        return currentRound == roundTiles.Length - 1;
    }
}
