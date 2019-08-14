using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesAndCoinsCanvas : MonoBehaviour
{
    private void Awake()
    {
        int numGameCanvas = FindObjectsOfType<LivesAndCoinsCanvas>().Length;
        if (numGameCanvas > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
