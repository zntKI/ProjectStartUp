using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector3.Normalize(new Vector3(horizontalMovement, 0, verticalMovement));
        velocity *= Time.deltaTime * speed;

        rb.velocity = velocity;
    }
}
