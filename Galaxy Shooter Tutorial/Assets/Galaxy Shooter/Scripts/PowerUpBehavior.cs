using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag =="Player")
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (player != null)
            {
                // enable power up
                player.TripleShotPowerUp();
            }
            Destroy(this.gameObject);
        }
    }
}
