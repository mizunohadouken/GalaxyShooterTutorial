using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    // toggle this value true to enable screen wrapping
    public bool bWrapMap = true;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovement(); 
    }

    private void PlayerMovement()
    {
        // movement component
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * HorizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * VerticalInput * Time.deltaTime);

        // bounding player position
        if (transform.position.y > 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0.0f);
        }

        if (bWrapMap)
        {
            if (transform.position.x < -8.9f)
            {
                transform.position = new Vector3(9.41f, transform.position.y, 0);
                while (transform.position.x > 8.3)
                {
                    transform.Translate(-Vector3.right * speed * Time.deltaTime);
                }
            }
            else if (transform.position.x > 8.9f)
            {
                transform.position = new Vector3(-9.41f, transform.position.y, 0);
                while (transform.position.x < -8.3)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
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


