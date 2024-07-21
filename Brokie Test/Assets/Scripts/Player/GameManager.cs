using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    public static GameManager Instance { get; private set; }
    public bool gamePaused = false;

    public event Action<float> PlayerHealthChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InputHandlerV2.Instance.OnPause += PauseMenu;
    }

    void PauseMenu()
    {
        Time.timeScale = gamePaused ? 1 : 0;
        Menu.SetActive(!gamePaused);
        gamePaused = !gamePaused;
        //Debug.Log(gamePaused);
    }

    public void SignalPlayerHealthChange(float damage)
    {
        PlayerHealthChange.Invoke(damage);
    }

}
