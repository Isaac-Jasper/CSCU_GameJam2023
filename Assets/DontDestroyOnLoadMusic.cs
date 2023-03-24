using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadMusic : MonoBehaviour
{
    public static bool isLoad = false;

    private void Awake() {
        if (isLoad)
            Destroy(this.gameObject);
        else
            isLoad = true;
        DontDestroyOnLoad(this.gameObject);
    }
}
