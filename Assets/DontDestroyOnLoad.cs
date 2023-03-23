using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static bool doesExist = false;
    void Start()
    {
        if (doesExist) Destroy(this.gameObject);
        doesExist = true;
        DontDestroyOnLoad(this.gameObject);
    }
}
