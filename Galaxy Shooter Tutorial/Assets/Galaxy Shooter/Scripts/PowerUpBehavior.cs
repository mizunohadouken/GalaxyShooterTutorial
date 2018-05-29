using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private int _powerUpID; // 0 triple shot, 1 speed boost, 2 shields
    [SerializeField]
    private AudioClip _powerUpSound = null;

    private int _bottomScreen = -7;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
        if (transform.position.y < _bottomScreen)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag =="Player")
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (_powerUpSound != null)
            {
                AudioSource.PlayClipAtPoint(_powerUpSound, Camera.main.transform.position);
            }

            if (player != null)
            {
                player.EnablePowerUp(_powerUpID);
            }
            Destroy(this.gameObject);
        }
    }
}
