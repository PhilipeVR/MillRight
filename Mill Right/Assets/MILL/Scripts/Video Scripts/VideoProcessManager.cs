using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoProcessManager : MonoBehaviour
{
    [SerializeField] private YoutubeExceptionListener LinkDisplayer;
    [SerializeField] private CameraToggle cameraToggle;
    [SerializeField] private ProcessAnimationController operationController;
    [SerializeField] private DialogueTrigger dialogueInterface;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Text title;
    [SerializeField] private List<string> videoClips, videoClipsFR;
    [SerializeField] private List<Sprite> operationSprite;
    [SerializeField] private List<int> animatorIndex;
    [SerializeField] private List<int> dialogueIndex;
    [SerializeField] private List<string> titles, titlesFR;
    [SerializeField] private Image exitImage;
    private bool playing = false;
    private List<bool> playedOnces;
    private int m_index = -1;
    public bool language = true;

    // Start is called before the first frame update
    void Start()
    {
        playedOnces = new List<bool>(); //List to keep track of every video that has been played
        for (int i = 0; i < videoClips.Count; i++)
        {
            playedOnces.Add(false);
        }
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false); //Deactivates youtube link on video panel
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed; //Adds listener to video player event system, activates if video has ended
    }

    public int Index
    {
        get => m_index;
        set => m_index = value;
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StopVideo()
    {
        if (m_index > -1)
        {
            if (playedOnces[m_index])
            {
                videoPlayer.Stop();
                playButton.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(false);

            }
        }
    }

    public void ExitVideo()
    {
        if (m_index > -1)
        {
            playing = false;

            if (playedOnces[m_index])
            {
                cameraToggle.SwtichView(); //Change camera back to origin
                VideoPanel.SetActive(false);
                if (m_index != -1)
                {
                    operationController.ChangeAnimator(animatorIndex[m_index]); //Trigger operation animation
                    dialogueInterface.TriggerDialogue(dialogueIndex[m_index]); //Trigger operation dialogue
                    m_index = -1; //reset index
                }

            }
        }
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
    }

    public void VideoPlayed(VideoPlayer video) //Method played when video has ended (event listener)
    {
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
            {
                playedOnces[m_index] = true;
                StopVideoButton.interactable = true;
                stopButton.interactable = true;
            }
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
        playing = false;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
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
    }

    public void PlayVideoClip(int index)
    {
        if (index >= 0 && index < videoClips.Count)
        {
            int tmpIndex = cameraToggle.CameraNum;
            for (int i = 0; i< tmpIndex -1; i++)
            {
                cameraToggle.SwtichView(); //Change camera view to improve performance(performance drops with camera controller view)
            }

            VideoPanel.SetActive(true);
            exitImage.sprite = operationSprite[index]; //Change exit button sprite depending on operation performed
            youtubePlayer.Links(videoClips[index], videoClipsFR[index]);
            youtubePlayer.Lang = language;

            if (!playedOnces[index])
            {
                StopVideoButton.interactable = false;
                stopButton.interactable = false;
            }
            else
            {
                StopVideoButton.interactable = true;
                stopButton.interactable = true;

            }
            if (language)
            {
                title.text = titles[index];
            }
            else
            {
                title.text = titlesFR[index];
            }
            m_index = index;
            youtubePlayer.PlayYoutubeVid();
            playing = true;
            LinkDisplayer.DisplayLink(language);

        }
    }

    public bool Playing
    {
        get => playing;
    }

    public void LinkSent()
    {
        if (m_index > -1)
        {
            playedOnces[m_index] = true;
            StopVideoButton.interactable = true;
        }
    }

    public void SwitchLang()
    {
        language = !language;
    }
}
