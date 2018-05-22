using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 30.0f;
    [SerializeField]
    private float _weaponOffScreenPos = 5.55f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > _weaponOffScreenPos)
        {
            Destroy(this.gameObject);
        }
	}
}
