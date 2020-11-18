using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private bool playedOnce;

    // Start is called before the first frame update
    void Start()
    {
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(true);
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed;

    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        if (!playedOnce)
        {
            StopVideoButton.interactable = false;
            stopButton.interactable = false;
        }
        else
        {
            StopVideoButton.interactable = true;
            stopButton.interactable = true;

        }
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StopVideo()
    {
        if (playedOnce)
        {
            videoPlayer.Stop();
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void ExitVideo()
    {
        if (playedOnce)
        {
            VideoPanel.SetActive(false);
        }
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
    }

    public void VideoPlayed(VideoPlayer video)
    {
        if (!playedOnce)
        {
            playedOnce = true;
            StopVideoButton.interactable = true;
            stopButton.interactable = true;
        }
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public bool PlayedOnce
    {
        get => playedOnce;
    }
}
