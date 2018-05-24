using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private int _health = 1;
    [SerializeField]
    private float _speed = 5.0f;
    private float _screenBottom = -6.22f;
    private float _screenEdgeLeft = -7.83f;
    [SerializeField]
    private GameObject _explosionAnimation = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < _screenBottom)
        {
            float randomX = Random.Range(_screenEdgeLeft, -_screenEdgeLeft);
            transform.position = new Vector3(randomX, -_screenBottom, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        
        if (other.tag == "Player" || other.tag == "Laser")
        {
            _health = _health - 1;
            if (other.tag =="Laser")
            {
                if (transform.parent != null)
                {
                    Destroy(other.transform.parent.gameObject);
                }
                Destroy(other.gameObject);
            }
            else if (other.tag =="Player")
            {
                PlayerBehavior player = other.GetComponent<PlayerBehavior>();

                if (player != null)
                {
                    player.TakeDamage();
                }
            }
        }

        if (_health < 1)
        {
            if (_explosionAnimation != null)
            {
                Debug.Log("Error: No explostionAnimation assigned to " + this.name);
            }
            Instantiate(_explosionAnimation, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
        
    }
}
