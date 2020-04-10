using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    //handle to the text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _LivesImage;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        //assign the text component to the handle
        _scoreText.text = " ";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager =
            GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerScore) {
        _scoreText.text = "Rezultat " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives) {
        //display img sprite and give it a new one based on lives index
        _LivesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0) {
            GameOverSequence();
        }
    }

    public void GameOverSequence() {
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(TextBlinkRoutine());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator TextBlinkRoutine() {
        while (true) {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
