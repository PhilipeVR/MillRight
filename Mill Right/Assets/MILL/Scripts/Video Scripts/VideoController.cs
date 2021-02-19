using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private YoutubeExceptionListener LinkDisplayer;
    [SerializeField] private CameraToggle cameraToggle;
    [SerializeField] private ButtonInteractable buttonInteractable;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private Text Title;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YoutubePlayer youtubePlayer;
    [SerializeField] private List<string> videoClip, videoClipFR;
    [SerializeField] private List<TextAsset> subtitleClips, subtitleClipsFR;
    [SerializeField] private List<string> title, titleFR;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private YoutubeSubtitlesReader subtitlesReader;
    private bool playing = false;
    private bool[] playedOnce;
    private bool language = false;
    private int m_index;

    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private int VideoIndexOnStart;

    public int Index
    {
        get => m_index;
        set => m_index = value;
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
    }

    public bool Playing
    {
        get => playing;
    }


    // Start is called before the first frame update
    void Start()
    {
        playedOnce = new bool[videoClip.Count];
        for(int i = 0; i<videoClip.Count; i++)
        {
            playedOnce[i] = false;
        }
        //StopVideoButton.interactable = false;
        //stopButton.interactable = false;
        VideoPanel.SetActive(false);
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed;
        if (PlayOnAwake)
        {
            StartVideo(VideoIndexOnStart);
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        if (!playedOnce[m_index])
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
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StopVideo()
    {
        if (playedOnce[m_index])
        {
            videoPlayer.Stop();
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void ExitVideo()
    {
        if (m_index > -1)
        {
            cameraToggle.SwtichView();
            VideoPanel.SetActive(false);
        }
        playing = false;

    }

    public void StartVideo(int vidIndex)
    {
        if (vidIndex >= 0 && vidIndex < videoClip.Count)
        {
            VideoPanel.SetActive(true);
            int tmpIndex = cameraToggle.CameraNum;
            for (int i = 0; i < tmpIndex - 1; i++)
            {
                cameraToggle.SwtichView(); //Change camera view to improve performance(performance drops with camera controller view)
            }
            
            if (!language)
            {
                subtitlesReader.LoadSubtitles(subtitleClips[vidIndex]);
                youtubePlayer.Play(videoClip[vidIndex]);
                Title.text = title[vidIndex];
            }
            else
            {
                subtitlesReader.LoadSubtitles(subtitleClipsFR[vidIndex]);
                youtubePlayer.Play(videoClipFR[vidIndex]);
                Title.text = titleFR[vidIndex];
            }
            
            if (!playedOnce[vidIndex])
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
            m_index = vidIndex;
            LinkDisplayer.DisplayLink(!language);

        }
    }

    public void VideoPlayed(VideoPlayer video)
    {
        if (!playedOnce[m_index])
        {
            playing = false;
            playedOnce[m_index] = true;
            //StopVideoButton.interactable = true;
            //stopButton.interactable = true;
            buttonInteractable.InteractButton();
        }
        //playButton.gameObject.SetActive(true);
        //pauseButton.gameObject.SetActive(false);
    }

    public void LinkSent()
    {
        if (m_index > -1)
        {
            playedOnce[m_index] = true;
            //StopVideoButton.interactable = true;
            buttonInteractable.InteractButton();

        }
    }

    public Boolean PlayedOnce(int a)
    {
        return playedOnce[a];
    }

    public void SwitchLang()
    {
        language = !language;
    }
}

