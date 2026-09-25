using UnityEngine;
using UnityEngine.Audio;

public class PewPewTriggers : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string parameterName_wet = "PewPews_Wet";
    [SerializeField] private string parameterName_room = "PewPews_Room";

    [Header("Reverb (Wet) Target Values")]
    [SerializeField] private float normalValue_wet = -80f;  // Typical minimum for volume/wet levels in dB
    [SerializeField] private float targetValue_wet = 0f;    // The increased value inside the zone

    [Header("Reverb (Room) Target Values")]
    [SerializeField] private float normalValue_room = -80f;  // Typical minimum for volume/wet levels in dB
    [SerializeField] private float targetValue_room = 0f;    // The increased value inside the zone


    [Header("Player Settings")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioMixer.SetFloat(parameterName_wet, targetValue_wet);

            audioMixer.SetFloat(parameterName_room, targetValue_room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioMixer.SetFloat(parameterName_wet, normalValue_wet);

            audioMixer.SetFloat(parameterName_wet, normalValue_room);
        }
    }
}
