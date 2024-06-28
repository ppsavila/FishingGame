using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isPaused = true;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }
    }
}
