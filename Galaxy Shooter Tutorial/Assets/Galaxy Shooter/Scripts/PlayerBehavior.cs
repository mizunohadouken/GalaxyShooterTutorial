using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _WeaponToSpawn = null;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private bool _bWrapMap = true;   // toggle this value true to enable screen wrapping
    [SerializeField]
    private float _fireRate = 0.3f;

    private float _nextFire = 0.0f;

    // Use this for initialization
    void Start ()
    {
        if (_WeaponToSpawn == null)
        {
            Debug.Log("No weapon prefab assigned");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();
        Shoot();
    }

    private void Shoot()
    {
        if (((Input.GetKeyDown(KeyCode.Space)) || (Input.GetMouseButtonDown(0))) && Time.time > _nextFire)
        {
            Vector3 WeaponSpawnLocation = transform.position + new Vector3(0.0f, 1.04f, 0.0f);
            Quaternion WeaponSpawnRotation = Quaternion.identity;
            Instantiate(_WeaponToSpawn, WeaponSpawnLocation, WeaponSpawnRotation);

            _nextFire = _fireRate + Time.time;
        }
    }

    private void PlayerMovement()
    {
        // movement component
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * HorizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * VerticalInput * Time.deltaTime);

        // bounding player position
        if (transform.position.y > 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0.0f);
        }

        if (_bWrapMap)
        {
            if (transform.position.x < -8.9f)
            {
                transform.position = new Vector3(9.41f, transform.position.y, 0);
                while (transform.position.x > 8.3)
                {
                    transform.Translate(-Vector3.right * _speed * Time.deltaTime);
                }
            }
            else if (transform.position.x > 8.9f)
            {
                transform.position = new Vector3(-9.41f, transform.position.y, 0);
                while (transform.position.x < -8.3)
                {
                    transform.Translate(Vector3.right * _speed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (transform.position.x < -8.3f)
            {
                transform.position = new Vector3(-8.3f, transform.position.y, 0);
            }
            else if (transform.position.x > 8.3f)
            {
                transform.position = new Vector3(8.3f, transform.position.y, 0);
            }
        }

    }
}


