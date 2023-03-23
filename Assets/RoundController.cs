using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private float fadeInSpeed,
        transitionSpeed;
    [SerializeField]
    private GameObject[] roundTiles;
    [SerializeField]
    private GameObject fadeObject, player;
    [SerializeField]
    private CanvasGroup textFadeGroup;
    private PlayerMovement playerMovement;
    int currentRound = 0;
    private void Start() {
        playerMovement = player.GetComponent<PlayerMovement>();
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
    public IEnumerator changeScene(int scene) {
        playerMovement.setPaused(true);
        Vector2 endPos = new(0, 1);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, Mathf.Log10(transitionSpeed)) * Vector2.up;
            yield return null;
        }
        playerMovement.setPaused(true);
    }
    private IEnumerator startScene() {
        playerMovement.setPaused(true);
        while (textFadeGroup.alpha < 0.9) {
            textFadeGroup.alpha = Mathf.Lerp(textFadeGroup.alpha, 1, transitionSpeed / 10 * Time.deltaTime);
            yield return null;
        }
        textFadeGroup.alpha = 1;

        yield return new WaitForSeconds(1);

        while (textFadeGroup.alpha > 0.05) {
            textFadeGroup.alpha = Mathf.Lerp(textFadeGroup.alpha, 0, transitionSpeed / 5 * Time.deltaTime);
            yield return null;
        }
        textFadeGroup.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        Vector2 endPos = new(0, -14);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, Mathf.Log10(transitionSpeed)) * Vector2.up;
            yield return null;
        }
        playerMovement.setPaused(false);
    }
}
