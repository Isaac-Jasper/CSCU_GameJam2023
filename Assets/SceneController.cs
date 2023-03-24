using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController i { get; private set; }

    [SerializeField]
    private float transitionSpeed;
    private CanvasGroup textFadeGroup;
    private GameObject fadeObject;
    private PlayerMovement playerMovement;
    private void Awake() {
        if (i != null && i != this) {
            Destroy(this.gameObject);
        } else {
            i = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start() {
        initializeObjects();
        StartCoroutine(startScene());
    }
    private void initializeObjects() {
        textFadeGroup = GameObject.FindGameObjectWithTag("IntroText").GetComponent<CanvasGroup>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        fadeObject = GameObject.FindGameObjectWithTag("CrossFade");
        Debug.Log("intiialized!");
    }
    public IEnumerator changeScene(int scene) {
        playerMovement.setPaused(true);
        Vector2 endPos = new(0, 1);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, Mathf.Log10(transitionSpeed)) * Vector2.up;
            yield return null;
        }
        SceneManager.LoadScene(scene);
        initializeObjects();
        startScene();
    }
    public IEnumerator RestartScene() {
        playerMovement.setPaused(true);
        Vector2 endPos = new(0, 1);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 1) {
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, Mathf.Log10(transitionSpeed)) * Vector2.up;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerMovement.setPaused(true);
        initializeObjects();
        endPos = new(0, -14);
        while (Vector2.Distance(endPos, fadeObject.transform.position) > 1) {
            Debug.Log(Vector2.Distance(endPos, fadeObject.transform.position));
            fadeObject.transform.position = Mathf.Lerp(endPos.y, fadeObject.transform.position.y, Mathf.Log10(transitionSpeed)) * Vector2.up;
            yield return null;
        }
        playerMovement.setPaused(false);
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
