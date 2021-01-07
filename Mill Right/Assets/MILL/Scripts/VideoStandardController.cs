using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStandardController : MonoBehaviour
{
    [SerializeField] private CameraToggle cameraToggle;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private Text Title;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoClip, videoClipFR;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private string title, titleFR;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private bool StartOnAwake;
    private bool playedOnce;
    private bool language = true;


    // Start is called before the first frame update
    void Start()
    {
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false);
        videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed;
        if (StartOnAwake)
        {
            StartVideo();
        }

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
            cameraToggle.SwtichView(); //Change camera back to origin
            VideoPanel.SetActive(false);
        }
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
        int tmpIndex = cameraToggle.CameraNum;
        for (int i = 0; i < tmpIndex -1; i++)
        {
            cameraToggle.SwtichView(); //Change camera view to improve performance(performance drops with camera controller view)
        }
        youtubePlayer.Links(videoClip, videoClipFR);
        youtubePlayer.Lang = language;
        if (language)
        {
            Title.text = title;
        }
        else
        {
            Title.text = titleFR;
        }
        youtubePlayer.PlayYoutubeVid();
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

    public void SwitchLang()
    {
        language = !language;
    }
}
