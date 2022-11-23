using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BikeController : MonoBehaviour
{
    public float linearSpeed = 10;
    public float rotationSpeed = 50;

    public Transform backWheel;

    public Action onKilled;
    public Action onReachedEndOfLevel;

    private Rigidbody2D rb;
    private float wheelRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wheelRadius = GetComponent<CircleCollider2D>().radius;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveBackward();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            DoStoppie();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            DoWheelie();
        }
    }

    private void MoveForward()
    {
        if (IsGrounded())
        {
            var right = transform.right;
            rb.velocity += new Vector2(right.x * linearSpeed, right.y * linearSpeed) * Time.deltaTime;
        }
    }

    private void MoveBackward()
    {
        if (IsGrounded())
        {
            var right = transform.right;
            rb.velocity -= new Vector2(right.x * linearSpeed, right.y * linearSpeed) * Time.deltaTime;
        }
    }

    private void DoWheelie()
    {
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);
    }

    private void DoStoppie()
    {
        rb.MoveRotation(rb.rotation - rotationSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircleAll(backWheel.position, wheelRadius * 1.1f).Length > 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Finish"))
        {
            onReachedEndOfLevel?.Invoke();
        }
        else if (other.gameObject != gameObject)
        {
            Debug.Log("Killed");
            onKilled?.Invoke();
        }
    }
}
