using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private const string VolumeKey = "Volume";

    private void Start()
    {
        volumeSlider = this.GetComponent<Slider>();

        // Initialize the slider value to the stored volume.
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, AudioListener.volume);
    }

    public void OnVolumeSliderChanged()
    {
        // Update the global audio volume when the slider is adjusted.
        float volume = volumeSlider.value;
        AudioListener.volume = volume;

        // Save the volume value to PlayerPrefs.
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}
