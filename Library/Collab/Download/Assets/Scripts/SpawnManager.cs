using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _powerUpPrefab;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update() {

    }
    //spawn a game object every 5seconds
    //create a corutine of type IEnumerator - yield events
    //while loop


    IEnumerator SpawnEnemyRoutine() {
        yield return null; //počaka en okvir
        while (_stopSpawning == false) {
            Vector3 spawnPos = new Vector3(Random.Range(-10.4f, 10f), 6.45f, 0);
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
    IEnumerator SpawnPowerUpRoutine() {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false) {
            Vector3 spawnPos = new Vector3(Random.Range(-10.4f, 10f), 6.45f, 0);
            Instantiate(_powerUpPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4f, 9f));
        }
    }
    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
