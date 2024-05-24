using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private const string VolumeKey = "Volume";

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Initialize the slider value to the stored volume.
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, AudioListener.volume);

        // Add listener for slider value changes
        volumeSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;

        // Save the volume value to PlayerPrefs.
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}