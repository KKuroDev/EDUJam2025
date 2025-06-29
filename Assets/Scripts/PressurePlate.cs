using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Static event that triggers when the plate is activated.
    public static event Action<Transform> OnActionDone;

    // The object that should respond to this pressure plate.
    [SerializeField] private Transform target;

    [SerializeField] private string tagToCompare;

    // Flag to ensure the plate is only activated once.
    [SerializeField] private bool IsActivated;

    // Triggered when another collider enters this trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Only respond to objects tagged "Player" and only once.
        if (other.gameObject.CompareTag(tagToCompare) && !IsActivated)
        {
            IsActivated = true;

            // Trigger any subscribed object's action with the target.
            OnActionDone?.Invoke(target);

            // Visually press down the plate.
            transform.position += new Vector3(0, 0, 0.25f);
        }
    }
}