using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tipleShotPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score = 0;
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isShieldActive = false;
    [SerializeField] private GameObject _shield;
    private SpawnManager _spawnManager;
    private float _nextFire = -4.0f;
    private UI_Manager _uiManager;
    //variable reference to shield visualiser


    void Start() {
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _shield = GameObject.Find("Shield");
        _shield.SetActive(false);


    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) {
            CooldownSystem();
        }
    }

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new Vector3(1,0,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 6.1f), 0);

        if (transform.position.x >= 10.2f) {
            transform.position = new Vector3(-10.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.2f) {
            transform.position = new Vector3(10.2f, transform.position.y, 0);
        }
    }

    void CooldownSystem() {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 2;
        _nextFire = Time.time + _fireRate;

        if (_isTripleShotActive == true) {
            Instantiate(_tipleShotPrefab, spawnPos, Quaternion.identity);
        }
        else {
            Instantiate(_laserPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void Damage() {
        if (_isShieldActive == true) {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverSequence();
        }
    }

    public void TripleShotActive() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostActive() {
        _speed *= 2;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public void ShieldActive() {
        _shield.SetActive(true);
        _isShieldActive = true;
    }

    IEnumerator SpeedBoostPowerDownRoutine() {
        yield return new WaitForSeconds(5);
        _speed /= 2;
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    //add 10 to the score
    //communicate with the ui to add the score
    public void AddScore(int points) {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public int getPlayerLives() {
        return _lives;
    }
}
