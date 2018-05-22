using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _WeaponToSpawn = null;
    [SerializeField]
    private GameObject _SpecialWeapon = null;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private bool _bWrapMap = true;   // toggle this value true to enable screen wrapping
    [SerializeField]
    private float _fireRate = 0.3f;

    public bool bEnableTripleShot = false;

    private float _nextFire = 0.0f;

    // Use this for initialization
    void Start ()
    {
        if (_WeaponToSpawn == null)
        {
            Debug.Log("No weapon prefab assigned");
        }
        if (_SpecialWeapon == null)
        {
            Debug.Log("No special weapon prefab assigned");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();

        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetMouseButtonDown(0)))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            Vector3 WeaponSpawnCenter = transform.position + new Vector3(0.0f, 1.04f, 0.0f);
            Quaternion WeaponSpawnRotation = Quaternion.identity;

            if (bEnableTripleShot == true)
            {

                Instantiate(_SpecialWeapon, transform.position, WeaponSpawnRotation);
                /*
                Vector3 WeaponSpawnLeft = transform.position + new Vector3(-.55f, .18f, 0.0f);
                Vector3 WeaponSpawnRight = transform.position + new Vector3(.55f, .18f, 0.0f);

                Instantiate(_WeaponToSpawn, WeaponSpawnLeft, WeaponSpawnRotation);
                Instantiate(_WeaponToSpawn, WeaponSpawnRight, WeaponSpawnRotation);
                Instantiate(_WeaponToSpawn, WeaponSpawnCenter, WeaponSpawnRotation);
                */
            }
            else
            {
                Instantiate(_WeaponToSpawn, WeaponSpawnCenter, WeaponSpawnRotation);
            }

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


