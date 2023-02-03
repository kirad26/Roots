using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 30f;
    [SerializeField] private float leftRotationConstraint = 50f;
    [SerializeField] private float rightRotationConstraint = -50f;

    private float smoothReturnValue = 25f;
    private float horizontalRotating = 0f;
    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalRotating = -Input.GetAxisRaw("Horizontal") * rotatingSpeed;
    }

    private void FixedUpdate()
    {
        HandlePlayerTreeRotation();
    }

    private void HandlePlayerTreeRotation()
    {
        if (rigidBody2D.rotation > leftRotationConstraint)
        {
            rigidBody2D.rotation = Mathf.Lerp(rigidBody2D.rotation, leftRotationConstraint, smoothReturnValue * Time.fixedDeltaTime);
        }
        if (rigidBody2D.rotation < rightRotationConstraint)
        {
            rigidBody2D.rotation = Mathf.Lerp(rigidBody2D.rotation, rightRotationConstraint, smoothReturnValue * Time.fixedDeltaTime);
        }
        else
        {
            rigidBody2D.rotation += horizontalRotating * Time.fixedDeltaTime;
        }
    }

    public void LimitTreeTilting(float rotationConstraint)
    {
        leftRotationConstraint += rotationConstraint;
        rightRotationConstraint -= rotationConstraint;
        Debug.Log(leftRotationConstraint);
        Debug.Log(rightRotationConstraint);
    }
}
