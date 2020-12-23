using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private ButtonInteractable buttonInteractable;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Boolean playedOnce;
    [SerializeField] private int videoFPS;

    // Start is called before the first frame update
    void Start()
    {
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false);
        videoPlayer.Stop();
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
            buttonInteractable.InteractButton();
        }
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public Boolean PlayedOnce
    {
        get => playedOnce;
    }


}
