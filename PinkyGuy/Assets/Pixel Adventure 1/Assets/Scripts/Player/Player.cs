using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 10;
    private Rigidbody2D _rigidbody2D;

    private bool isJumping;
    private bool doubleJump = true;
    private void Awake()
    {
        TryGetComponent(out _rigidbody2D);
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                isJumping = true;
                _rigidbody2D.AddForce(new Vector2(1, jumpHeight), ForceMode2D.Impulse);
                return;
            }

            if (doubleJump)
            {
                _rigidbody2D.AddForce(new Vector2(1, jumpHeight), ForceMode2D.Impulse);
                doubleJump = false;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 3)
        {
            doubleJump = true;
            isJumping = false;
        }
    }
}
