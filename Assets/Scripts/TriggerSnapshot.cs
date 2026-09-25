using UnityEngine;

public class TriggerSnapshot : MonoBehaviour
{
    private string currentTag = "SnapshotTrigger";

    // This runs the moment an overlap begins
    private void OnTriggerEnter(Collider other)
    {
        // Check if the overlapping object is tagged "Cube" or has a specific name
        if (other.gameObject.CompareTag(currentTag))
        {
            Debug.Log("Overlapping our cube: " + other.gameObject.name);

            // Do stuff..

        }
    }

    // Optional: Runs continuously while staying inside the overlap area
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(currentTag))
        {
            Debug.Log("Still inside the cube area.");

            // Do stuff..

        }
    }

    // Optional: Runs when you exit the overlap area
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(currentTag))
        {
            Debug.Log("Left the cube area.");

            // Do stuff..

        }
    }
}