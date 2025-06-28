using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class OtherGridMovement : MonoBehaviour
{
    // Allows you to hold down a key for movement.
    [SerializeField] private bool isRepeatedMovement = false;
    // Time in seconds to move between one grid position and the next.
    [SerializeField] private float moveDuration = 0.1f;
    // The size of the grid
    [SerializeField] private float gridSize = 1f;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask obstacleStumbleLayer;

    [Header("Devil Mirror Movement")]
    public bool mirrorMovement = false;

    private bool isMoving = false;

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

            if (inputFunction(KeyCode.UpArrow) || inputFunction(KeyCode.W))
                StartCoroutine(Move(mirrorMovement ? Vector2.down : Vector2.up));
            else if (inputFunction(KeyCode.DownArrow) || inputFunction(KeyCode.S))
                StartCoroutine(Move(mirrorMovement ? Vector2.up : Vector2.down));
            else if (inputFunction(KeyCode.LeftArrow) || inputFunction(KeyCode.A))
                StartCoroutine(Move(mirrorMovement ? Vector2.right : Vector2.left));
            else if (inputFunction(KeyCode.RightArrow) || inputFunction(KeyCode.D))
                StartCoroutine(Move(mirrorMovement ? Vector2.left : Vector2.right));
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

        // Check if obstacle is in Moved Direction
        if (Physics.CheckBox(endPosition, Vector3.one * 0.4f, Quaternion.identity, obstacleLayer))
        {
            isMoving = false;
            yield break;
        }
        // Check if StumbleObstacle is in movement direction
        if (Physics.CheckBox(endPosition, Vector3.one * 0.4f, Quaternion.identity, obstacleStumbleLayer))
        {
            isMoving = false;
            // TODO: IMPLEMENT STUMBLE MECHANIC
            yield break;
        }

        Collider[] hitColliders = Physics.OverlapBox(endPosition, Vector3.one * 0.4f);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Angel"))
            {
                SceneManager.LoadScene("main_menu");
                yield break;
            }
        }

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        // Make sure we end up exactly where we want.
        transform.position = endPosition;

        Collider[] overlapNow = Physics.OverlapBox(transform.position, Vector3.one * 0.4f);
        foreach (var hit in overlapNow)
        {
            if (hit.CompareTag("Angel"))
            {
                SceneManager.LoadScene("main_menu");
                yield break;
            }
        }

        // We're no longer moving so we can accept another move input.
        isMoving = false;
    }
}