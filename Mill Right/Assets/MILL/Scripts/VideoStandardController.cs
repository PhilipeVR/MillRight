using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStandardController : MonoBehaviour
{
    [SerializeField] private YoutubeExceptionListener LinkDisplayer;
    [SerializeField] private CameraToggle cameraToggle;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private Text Title;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoClip, videoClipFR;
    [SerializeField] private TextAsset subtitleClip, subtitleClipFR;
    [SerializeField] private YoutubePlayer youtubePlayer;
    [SerializeField] private string title, titleFR;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private bool StartOnAwake;
    [SerializeField] private YoutubeSubtitlesReader subtitlesReader;

    private bool playing = false; 
    private bool playedOnce;
    private bool language = false;


    // Start is called before the first frame update
    void Start()
    {
        //StopVideoButton.interactable = false;
        //stopButton.interactable = false;
        VideoPanel.SetActive(false);
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed;
        if (StartOnAwake)
        {
            StartVideo();
        }

    }

    public void PlayVideo()
    {
        playing = true;
        videoPlayer.Play();
        if (!playedOnce)
        {
            //StopVideoButton.interactable = false;
            //stopButton.interactable = false;
        }
        else
        {
            //StopVideoButton.interactable = true;
            //stopButton.interactable = true;

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
            //playButton.gameObject.SetActive(true);
           // pauseButton.gameObject.SetActive(false);
        }
    }

    public void ExitVideo()
    {
        playing = false;
        playedOnce = true;
        cameraToggle.SwtichView(); //Change camera back to origin
        VideoPanel.SetActive(false);
 
    }

    public bool Playing
    {
        get => playing;
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
        int tmpIndex = cameraToggle.CameraNum;
        for (int i = 0; i < tmpIndex -1; i++)
        {
            cameraToggle.SwtichView(); //Change camera view to improve performance(performance drops with camera controller view)
        }
        if (!language)
        {
            subtitlesReader.LoadSubtitles(subtitleClip);
            youtubePlayer.Play(videoClip);
            Title.text = title;
        }
        else
        {
            subtitlesReader.LoadSubtitles(subtitleClipFR);
            youtubePlayer.Play(videoClipFR);
            Title.text = titleFR;
        }
        if (!playedOnce)
        {
            //StopVideoButton.interactable = false;
            //stopButton.interactable = false;
        }
        else
        {
            //StopVideoButton.interactable = true;
            //stopButton.interactable = true;

        }
        playing = true;
        LinkDisplayer.DisplayLink(!language);


    }

    public void VideoPlayed(VideoPlayer video)
    {
        if (!playedOnce)
        {
            playedOnce = true;
            //StopVideoButton.interactable = true;
            //stopButton.interactable = true;
        }
        //playButton.gameObject.SetActive(true);
        //pauseButton.gameObject.SetActive(false);
        playing = false;

    }

    public void LinkSent()
    {
        playedOnce = true;
        StopVideoButton.interactable = true;
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
