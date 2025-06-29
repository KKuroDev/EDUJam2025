using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public List<OtherGridMovement> devilPlayers = new List<OtherGridMovement>();
    // Allows you to hold down a key for movement.
    [SerializeField] private bool isRepeatedMovement = false;
    // Time in seconds to move between one grid position and the next.
    [SerializeField] private float moveDuration = 0.1f;
    // The size of the grid
    [SerializeField] private float gridSize = 1f;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask obstacleStumbleLayer;
    private bool isMoving = false;
    private void Start()
    {
        AudioManager.instance.PlayMusic();
    }

    // Update is called once per frame
    private void Update()
    {
        // Only process on move at a time.
        if (!isMoving)
        {
            // Accomodate two different types of moving.
            System.Func<KeyCode, bool> inputFunction;
            if (isRepeatedMovement)
            {
                // GetKey repeatedly fires.
                inputFunction = Input.GetKey;
            }
            else
            {
                // GetKeyDown fires once per keypress
                inputFunction = Input.GetKeyDown;
            }

            // If the input function is active, move in the appropriate direction.
            if (inputFunction(KeyCode.UpArrow) || inputFunction(KeyCode.W))
            {
                StartCoroutine(Move(Vector2.up));
            }
            else if (inputFunction(KeyCode.DownArrow) || inputFunction(KeyCode.S))
            {
                StartCoroutine(Move(Vector2.down));
            }
            else if (inputFunction(KeyCode.LeftArrow) || inputFunction(KeyCode.A))
            {
                StartCoroutine(Move(Vector2.left));
            }
            else if (inputFunction(KeyCode.RightArrow) || inputFunction(KeyCode.D))
            {
                StartCoroutine(Move(Vector2.right));
            }
        }
    }

    // Smooth movement between grid positions.
    private IEnumerator Move(Vector2 direction)
    {
        // Record that we're moving so we don't accept more input.
        isMoving = true;

        // Make a note of where we are and where we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        // Set rotation to face movement direction
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Check if obstacle is in Moved Direction
        if ( Physics.CheckBox(endPosition, Vector3.one * 0.4f, Quaternion.identity, obstacleLayer))
        {
            isMoving = false;
            yield break;
        }
        // Check if StumbleObstacle is in movement direction
        if (Physics.CheckBox(endPosition, Vector3.one * 0.4f, Quaternion.identity, obstacleStumbleLayer))
        {
            isMoving = false;

            foreach (var devil in devilPlayers)
            {
                if (devil != null)
                {
                    devil.CancelNextMove();
                }
            }

            yield break;
        }

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, percent);
            yield return null;
        }

        // Make sure we end up exactly where we want.
        transform.position = endPosition;

        // We're no longer moving so we can accept another move input.
        isMoving = false;
    }
}