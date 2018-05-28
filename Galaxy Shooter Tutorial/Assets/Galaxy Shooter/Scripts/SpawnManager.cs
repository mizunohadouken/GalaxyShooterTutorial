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

    private GameManager _GameManager = null;

    // Use this for initialization
    void Start ()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
	}
	
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_GameManager.GetGameStatus())
        {
            Instantiate(_enemyShip, new Vector3(Random.Range(-8.9f, 8.9f), 4.2f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(_EnemySpawnRate);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (_GameManager.GetGameStatus())
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_a_PowerUps[randomPowerUp], new Vector3(Random.Range(-8.9f, 8.9f), 4.2f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(_PowerUpSpawnRate);
        }
    }
}
