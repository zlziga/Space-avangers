using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;

    private float _rand;

    private Player _player;





    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        //move down at the speed of 3, when leave the screen - destroy, check for collision - only collectable by the player (use tags), on collected destroy
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        _rand = Random.Range(-10.04f, 10f);
        if(transform.position.y <= -5) {
            Destroy(this.gameObject);
        }    
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            _player.TripleShotActive();
            //communicate with the player script(var other) 
            Destroy(this.gameObject);
        }
    }

}
