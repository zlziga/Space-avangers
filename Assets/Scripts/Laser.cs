using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Laser laser;

    [SerializeField]
    private float _speed = 8.0f;

    void Update()
    {
        //translate laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser position on the y is greater than 8
        //destroy object
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
