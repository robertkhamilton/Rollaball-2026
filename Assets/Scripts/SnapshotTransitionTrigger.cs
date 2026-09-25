using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SnapshotTransitionTrigger : MonoBehaviour
{
    [Header("Mixer Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot defaultSnapshot;
    [SerializeField] private AudioMixerSnapshot triggerSnapshot;
    [SerializeField] private float transitionDuration = 1.5f;

    [Header("Player Settings")]
    [SerializeField] private string playerTag = "Player";

    private Coroutine activeTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartTransition(triggerSnapshot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartTransition(defaultSnapshot);
        }
    }

    private void StartTransition(AudioMixerSnapshot targetSnapshot)
    {
        if (activeTransition != null)
        {
            StopCoroutine(activeTransition);
        }
        activeTransition = StartCoroutine(TransitionRoutine(targetSnapshot));
    }

    private IEnumerator TransitionRoutine(AudioMixerSnapshot targetSnapshot)
    {
        // Define the snapshots to interpolate between
        AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[] { defaultSnapshot, triggerSnapshot };

        // Target weights depending on which snapshot we are moving toward
        float targetTriggerWeight = (targetSnapshot == triggerSnapshot) ? 1f : 0f;

        // Retrieve current actual weights to prevent a sudden jump if the player toggles mid-transition
        // Note: Because Unity doesn't expose active weights directly via API, we track current time linearly.
        float startTriggerWeight = (targetSnapshot == triggerSnapshot) ? 0f : 1f;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Interpolate the weight
            float currentTriggerWeight = Mathf.Lerp(startTriggerWeight, targetTriggerWeight, t);
            float currentDefaultWeight = 1f - currentTriggerWeight;

            float[] weights = new float[] { currentDefaultWeight, currentTriggerWeight };

            // Apply the blended weights across the snapshot array
            audioMixer.TransitionToSnapshots(snapshots, weights, 0f);

            yield return null;
        }

        // Snap fully to the final state at completion
        float[] finalWeights = new float[] { 1f - targetTriggerWeight, targetTriggerWeight };
        audioMixer.TransitionToSnapshots(snapshots, finalWeights, 0f);
    }
}