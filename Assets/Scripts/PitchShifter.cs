using UnityEngine;
using UnityEngine.Audio;

public class PitchShifter : MonoBehaviour
{
    [SerializeField] private AudioMixer targetMixer;
    [SerializeField] private string parameterName = "Instruments_Pitch";

    // Our player
    [SerializeField] private GameObject player;

    private AudioSource audioSource;

    public bool togglePitchShift;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate simple 3D distance between our player and this object
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Debug.Log("distance");

        if (togglePitchShift == true)
        {
            // Update the pitch value of this object's attached AudioSource
            setPitchValue_AudioSource(distance);
            targetMixer.SetFloat(parameterName, 1.0f);    // make sure our pitch on the group is back to 100% if we're experimenting back and forth here
        }
        else
        {
            // Update the pitch value of an exposed mixer parameter
            setPitchValue_Mixer(distance);
        }
    }

    // function to change pitch value of AudioSource
    public void setPitchValue_AudioSource(float value)
    {
        // Clamp value to 1 - 1000 % range for pitch slider
        float clampedValue = Mathf.Clamp(value, 1.0f, 3.0f);

        // Set pitch value directly on AudioSource
        audioSource.pitch = clampedValue;
    }

    // function to change pitch value of exposed parameter
    public void setPitchValue_Mixer(float value)
    {
        // Clamp value to 1 - 1000 % range for pitch slider
        float clampedValue = Mathf.Clamp(value, 1.0f, 1000.0f);

        // Set named exposed parameter in mixer
        targetMixer.SetFloat(parameterName, clampedValue);
    }
}
