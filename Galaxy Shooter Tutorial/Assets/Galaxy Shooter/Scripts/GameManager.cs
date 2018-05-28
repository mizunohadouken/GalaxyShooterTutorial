using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameStarted = false;
    [SerializeField]
    private GameObject player;
    private UIManager _UIManager;

    private void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (_isGameStarted == false)
        {
            if ((Input.GetKeyDown(KeyCode.Space)))
            {
                StartGame();
            }
        }
    }

    public bool GetGameStatus()
    {
        return _isGameStarted;
    }

    public void ToggleGameOver()
    {
        _isGameStarted = false;
        if (_UIManager != null)
        {
            _UIManager.ToggleMenu(_isGameStarted);
        }
    }

    private void StartGame()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
        _isGameStarted = true;
        if (_UIManager != null)
        {
            _UIManager.ToggleMenu(_isGameStarted);
        }
    }
}
