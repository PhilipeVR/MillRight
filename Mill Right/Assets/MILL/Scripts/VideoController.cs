using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEditor;

public class VideoController : MonoBehaviour
{
    [SerializeField] private CameraToggle cameraToggle;
    [SerializeField] private ButtonInteractable buttonInteractable;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private Text Title;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private List<string> videoClip, videoClipFR;
    [SerializeField] private List<string> title, titleFR;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    private bool[] playedOnce;
    private bool language = true;
    private int m_index;

    //For Unity Editor
    [SerializeField] private bool playOnAwake = false;
    [HideInInspector] public int VideoIndexOnStart;

    public int Index
    {
        get => m_index;
        set => m_index = value;
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
    }


    // Start is called before the first frame update
    void Start()
    {
        playedOnce = new bool[videoClip.Count];
        for(int i = 0; i<videoClip.Count; i++)
        {
            playedOnce[i] = false;
        }
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false);
        videoPlayer.Stop();
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
        if (playedOnce[m_index])
        {
            videoPlayer.Stop();
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void ExitVideo()
    {
        if (playedOnce[m_index])
        {
            cameraToggle.SwtichView();
            VideoPanel.SetActive(false);
        }
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
            youtubePlayer.Links(videoClip[vidIndex], videoClipFR[vidIndex]);
            youtubePlayer.Lang = language;
            if (language)
            {
                Title.text = title[vidIndex];
            }
            else
            {
                Title.text = titleFR[vidIndex];
            }
            youtubePlayer.PlayYoutubeVid();
            
            if (!playedOnce[vidIndex])
            {
                StopVideoButton.interactable = false;
                stopButton.interactable = false;
            }
            else
            {
                StopVideoButton.interactable = true;
                stopButton.interactable = true;

            }
            m_index = vidIndex;
        }
    }

    public void VideoPlayed(VideoPlayer video)
    {
        if (!playedOnce[m_index])
        {
            playedOnce[m_index] = true;
            StopVideoButton.interactable = true;
            stopButton.interactable = true;
            buttonInteractable.InteractButton();
        }
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
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

[CustomEditor(typeof(VideoController))]
public class VideoControllerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var controller = (VideoController) target;
        if (controller.PlayOnAwake)
        {
            controller.VideoIndexOnStart = EditorGUILayout.IntField("Video Index On Start", controller.VideoIndexOnStart);
        }
    }
}