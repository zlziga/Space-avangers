using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    public int _score;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        //if bottom of screen, respawn at top (with a random x position)
        if (transform.position.y <= -4.48)
        {
            transform.position =
                new Vector3(Random.Range(-10.04f, 10f), 6.45f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            _player.Damage();
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(1);
        }
        Destroy(this.gameObject);
    }
}
