using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private float fadeInSpeed,
        time,
        transitionSpeed;
    [SerializeField]
    private GameObject[] roundTiles;
    [SerializeField]
    private GameObject fadeObject;
    private CanvasGroup canvasGroupFade;
    int currentRound = 0;
    private void Start() {
        canvasGroupFade = fadeObject.GetComponent<CanvasGroup>();
        StartCoroutine(startScene());
    }
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
            tilemap.color = new Color(1, 1, 1, color);
            yield return new WaitForSeconds(time);
        }
    }
    public bool isWin() {
        return currentRound == roundTiles.Length - 1;
    }
    public IEnumerator changeScene(int scene) {
        Vector2 endPos = new(0, 0);
        Debug.Log(Vector2.Distance(endPos, fadeObject.transform.position));
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 0.1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, transitionSpeed) * Vector2.up;
            yield return null;
        }
    }
    private IEnumerator startScene() {
        Vector2 endPos = new(0, -13);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 0.1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, transitionSpeed) * Vector2.up;
            yield return null;
        }
    }
    private IEnumerator resartScene() {
        
    }
}
