using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float moveSpeed = 4f; 
    public float rotationSpeed = 180f;

    void Update()
    {
        Move();
        if (Input.GetMouseButton(0))
        {
            RotateTowardsMouse();
        }
    }

    void Move()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in coordinates
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPosition = hitInfo.point;
            targetPosition.y = transform.position.y; // Leave the current height level (y)

            // Calculate the direction from the player to the mouse
            Vector3 direction = targetPosition - transform.position;

            // Calculate the angle to turn
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}