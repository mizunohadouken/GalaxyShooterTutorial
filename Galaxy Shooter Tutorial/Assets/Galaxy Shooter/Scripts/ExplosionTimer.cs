using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    private float _destroyAfterTime = 4.0f;
    [SerializeField]
    private AudioClip _explosionSound = null;

	// Use this for initialization
	void Start ()
    {
        if (_explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position);
        }

        Destroy(this.gameObject, _destroyAfterTime);
	}
	
}
