using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    private Player _player;
    float _rand;
    [SerializeField] private int powerupID;

    // Start is called before the first frame update
    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        //move down at the speed of 3, when leave the screen - destroy, check for collision - only collectable by the player (use tags), on collected destroy
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        _rand = Random.Range(-10.04f, 10f);
        if (transform.position.y <= -5) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {

            switch (powerupID) {
                case 0:
                    _player.TripleShotActive();
                    break;
                case 1:

                    _player.SpeedBoostActive();
                    break;
                case 2:
                    _player.ShieldActive();
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
