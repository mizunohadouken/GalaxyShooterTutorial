using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _PlayerLivesSprite;
    [SerializeField]
    private Image _livesImageDisplay;
    [SerializeField]
    private GameObject _MainMenuImage;
    [SerializeField]
    private Text _ScoreText;
    private int _score = 0;


    public void UpdatePlayerLives(int currentLives)
    {
        _livesImageDisplay.sprite = _PlayerLivesSprite[currentLives];
    }

    public void UpdateScore(int added_points)
    {
        _score += added_points;
        _ScoreText.text = "Score: " + _score;
    }

    public void ToggleMenu(bool isGameStarted)
    {
        if (isGameStarted == true)
        {
            _MainMenuImage.SetActive(false);
            _score = 0;
            _ScoreText.text = "Score: " + _score;
        }
        else if (isGameStarted == false)
        {
            _MainMenuImage.SetActive(true);
        }
    }

}
