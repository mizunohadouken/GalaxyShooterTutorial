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
    private GameObject _ShieldsObject = null;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private bool _bWrapMap = false;   // toggle this value true to enable screen wrapping
    [SerializeField]
    private float _fireRate = 0.3f;
    [SerializeField]
    private float _PowerUpCoolDown = 5.0f;
    [SerializeField]
    private bool _bEnableTripleShot = false;
    [SerializeField]
    private bool _bEnableSpeedBoost = false;
    [SerializeField]
    private bool _bEnableShields = false;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject _explosionAnimation = null;
    
    private UIManager _UIManager = null;
    private GameManager _GameManager = null;
    private SpawnManager _SpawnManager = null;
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

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager != null)
        {
            _UIManager.UpdatePlayerLives(_health);
        }

        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_SpawnManager != null)
        {
            _SpawnManager.StartSpawnRoutines();
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

            if (_bEnableTripleShot == true)
            {
                Instantiate(_SpecialWeapon, transform.position, WeaponSpawnRotation);
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

        if(_bEnableSpeedBoost)
        {
            _speed = 20.0f;
        }
        else
        {
            _speed = 5.0f;
        }
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

    public void EnablePowerUp(int powerUpID)
    {
        switch (powerUpID)
        {
            case 0:
                _bEnableTripleShot = true;
                break;
            case 1:
                _bEnableSpeedBoost = true;
                break;
            case 2:
                _bEnableShields = true;
                _ShieldsObject.SetActive(true);
                break;
        }
        StartCoroutine(PowerUpCoolDownRoutine(powerUpID));
    }

    IEnumerator PowerUpCoolDownRoutine(int powerUpID)
    {
        yield return new WaitForSeconds(_PowerUpCoolDown);
        switch (powerUpID)
        {
            case 0:
                _bEnableTripleShot = false;
                break;
            case 1:
                _bEnableSpeedBoost = false;
                break;
            case 2:

                break;
        }
    }

    public void TakeDamage()
    {
        if (_bEnableShields == true)
        {
            _bEnableShields = false;
            _ShieldsObject.SetActive(false);
            return;
        }

        _health = _health - 1;
        _UIManager.UpdatePlayerLives(_health);
        if (_health < 1)
        {
            if (_GameManager != null)
            {
                _GameManager.ToggleGameOver();
            }
            Instantiate(_explosionAnimation, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}


