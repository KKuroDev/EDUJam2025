using System.Collections;
using UnityEngine;

// Enum defining possible actions the object can perform when triggered.
public enum ObjAction
{
    OpenDoor,
    MoveUp,
    MoveDown,
    MoveRight,
    MoveLeft
}

public class ObjectAction : MonoBehaviour
{
    // The size of the grid (how far the object moves per step).
    private float gridSize = 1f;

    // Selectable action for the object to perform when triggered.
    [SerializeField] private ObjAction action;

    // Time in seconds to move between one grid position and the next.
    [SerializeField] private float moveDuration = 0.1f;

    // Speed at which a door object opens.
    [SerializeField] private float doorMoveSpeed = 1f;

    // Indicates whether the door animation has finished.
    [SerializeField] private bool IsDoorAnimFinish;

    // Reference to the object that triggered the pressure plate.
    private Transform pressureTarget;

    // Subscribes to the OnActionDone event when the object is enabled.
    private void OnEnable()
    {
        PressurePlate.OnActionDone += ObjectMove;
    }

    // Unsubscribes from the OnActionDone event when the object is disabled.
    private void OnDisable()
    {
        PressurePlate.OnActionDone -= ObjectMove;
    }

    // Update is called once per frame.
    private void Update()
    {
        // If the door animation isn't active, do nothing.
        if (!IsDoorAnimFinish) return;

        // Only move if this object was the one targeted by the pressure plate.
        if (pressureTarget == null || pressureTarget != transform) return;

        // Simulate door opening by moving it downward over time.
        transform.localPosition -= new Vector3(0, 1, 1) * doorMoveSpeed * Time.deltaTime;

        // Stop door animation after a short time.
        StartCoroutine(OpenDoorTimeLapse());
    }

    // Called when the pressure plate triggers this object.
    private void ObjectMove(Transform target)
    {
        pressureTarget = target;

        // If the pressure plate did not target this object, exit.
        if (pressureTarget != transform) return;

        // Perform the appropriate action based on the selected enum.
        switch (action)
        {
            case ObjAction.OpenDoor:
                IsDoorAnimFinish = true;
                break;
            case ObjAction.MoveUp:
                StartCoroutine(Move(Vector2.up));
                break;
            case ObjAction.MoveDown:
                StartCoroutine(Move(Vector2.down));
                break;
            case ObjAction.MoveRight:
                StartCoroutine(Move(Vector2.right));
                break;
            case ObjAction.MoveLeft:
                StartCoroutine(Move(Vector2.left));
                break;
        }
    }

    // Smoothly moves the object in a given direction over time.
    private IEnumerator Move(Vector2 direction)
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        // Snap to final position in case of small inaccuracies.
        transform.position = endPosition;
    }

    // Coroutine that waits before resetting the door animation flag.
    private IEnumerator OpenDoorTimeLapse()
    {
        yield return new WaitForSecondsRealtime(moveDuration);
        IsDoorAnimFinish = false;
    }
}