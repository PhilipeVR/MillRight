using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private Camera_Toggle camera_Toggle;
    [SerializeField] private ProcessController operationController;
    [SerializeField] private TriggerDialogueInterface dialogueInterface;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Text title;
    [SerializeField] private InputField youtubeLink;
    [SerializeField] private List<string> YoutubeLinks;
    [SerializeField] private List<string> YoutubeLinksFR;
    [SerializeField] private List<Sprite> operationSprite;
    [SerializeField] private List<int> animatorIndex;
    [SerializeField] private List<int> dialogueIndex;
    [SerializeField] private List<string> titles, titlesFR;
    [SerializeField] private Image exitImage;
    private List<bool> playedOnces;
    private int m_index = -1;
    public bool language = true;

    // Start is called before the first frame update
    void Start()
    {
        playedOnces = new List<bool>(); //List to keep track of every video that has been played
        for(int i = 0; i<YoutubeLinks.Count; i++)
        {
            playedOnces.Add(false);
        }
        StopVideoButton.interactable = false;
        stopButton.interactable = false; 
        VideoPanel.SetActive(false); //Deactivates youtube link on video panel
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed; //Adds listener to video player event system, activates if video has ended
        youtubeLink.gameObject.SetActive(false); //Deactivates youtube link on video panel title screen
        VideoEvents.current.youtubePlayerException += LinkSent; //Adds listener to Video Event System, activates if youtube link produces error


    }

    public void LinkSent() //Method called if error with video playback occurs
    {
        if(m_index > -1)
        {
            playedOnces[m_index] = true;
            StopVideoButton.interactable = true;
        }
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
            if (playedOnces[m_index])
            {
                camera_Toggle.ChangeCamForVid(false); //Change camera back to origin
                VideoPanel.SetActive(false);
                if (m_index != -1)
                {
                    operationController.ChangeAnimator(animatorIndex[m_index]); //Trigger operation animation
                    dialogueInterface.TriggerDialogue(dialogueIndex[m_index]); //Trigger operation dialogue
                    m_index = -1; //reset index
                }
                youtubeLink.gameObject.SetActive(false);

            }
        }
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
    }

    public void VideoPlayed(VideoPlayer video) //Method played when video has ended (event listener)
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

    public async void PlayYoutubeVideo(int index)
    {
        if( index >= 0 && index < YoutubeLinks.Count)
        {
            camera_Toggle.ChangeCamForVid(true); //Change camera view to improve performance(performance drops with camera controller view)
            VideoPanel.SetActive(true);
            
            exitImage.sprite = operationSprite[index]; //Change exit button sprite depending on operation performed
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
                m_index = index;
                title.text = titles[index];
                youtubeLink.text = "Video not working? Use this link to watch it: " + YoutubeLinks[index]; //youtube link that will be displayed if errror with video occurs
                await youtubePlayer.PlayVideoAsync(YoutubeLinks[index]); //play youtube vid
            }
            else
            {
                m_index = index;
                title.text = titlesFR[index];
                youtubeLink.text = "Le vidéo ne fonctionne pas? Utilise ce lien pour le regarder: " + YoutubeLinksFR[index]; //youtube link that will be displayed if errror with video occurs
                await youtubePlayer.PlayVideoAsync(YoutubeLinksFR[index]); //play youtube vid
            }
        }
    }

    public void SwitchLang()
    {
        language = !language;
    }

}
