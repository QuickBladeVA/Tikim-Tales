using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene;
    public bool isDish;
    public List<VideoClip> videos;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("Volume", 1.0f)); // Initialize the video player volume

        if (!isDish)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }

        if (isDish)
        {
            if (GameManager.Instance.score <= 75)
            {
                videoPlayer.clip = videos[0];
            }
            else if (GameManager.Instance.score >= 76 && GameManager.Instance.score <= 99)
            {
                videoPlayer.clip = videos[1];
            }
            else if (GameManager.Instance.score >= 100)
            {
                videoPlayer.clip = videos[2];
            }
            videoPlayer.Play(); // Ensure the selected clip is played
        }
    }

    private void Update()
    {
        // Update the video player volume based on the slider value
        float volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.loopPointReached -= OnVideoEnd;
        SceneManager.LoadScene(nextScene);
    }
}
