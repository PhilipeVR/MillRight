using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;

public class VideoManager : MonoBehaviour
{

    [SerializeField] private ProcessController operationController;
    [SerializeField] private TriggerDialogueInterface dialogueInterface;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Text title;
    [SerializeField] private List<string> YoutubeLinks;
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
        playedOnces = new List<bool>(YoutubeLinks.Capacity);
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false);
        //videoPlayer.Stop();
        videoPlayer.loopPointReached += VideoPlayed;

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
                VideoPanel.SetActive(false);
                if (m_index != -1)
                {
                    operationController.ChangeAnimator(animatorIndex[m_index]);
                    dialogueInterface.TriggerDialogue(dialogueIndex[m_index]);

                }
            }
        }
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
    }

    public void VideoPlayed(VideoPlayer video)
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
            VideoPanel.SetActive(true);
            if (language)
            {
                title.text = titles[index];
            } else
            {
                title.text = titlesFR[index];
            }
            exitImage.sprite = operationSprite[index];
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
            await youtubePlayer.PlayVideoAsync(YoutubeLinks[index]);
            
            m_index = index;
        }
    }

    public void SwitchLang()
    {
        language = !language;
    }

}
