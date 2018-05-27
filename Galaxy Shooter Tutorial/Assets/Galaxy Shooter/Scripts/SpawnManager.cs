using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip;
    [SerializeField]
    private GameObject[] _a_PowerUps;
    [SerializeField]
    private float _EnemySpawnRate = 3.0f;
    [SerializeField]
    private float _PowerUpSpawnRate = 5.0f;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            Instantiate(_enemyShip, new Vector3(Random.Range(-8.9f, 8.9f), 4.2f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(_EnemySpawnRate);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (true)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_a_PowerUps[randomPowerUp], new Vector3(Random.Range(-8.9f, 8.9f), 4.2f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(_PowerUpSpawnRate);
        }
    }
}
