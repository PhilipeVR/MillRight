﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
# endif

public class VideoManager2 : MonoBehaviour
{
    [Tooltip("Language Manager for scenes")]
    [SerializeField] private LanguageSceneSwitcher languageSceneManager;

    [Tooltip("Youtube Player from Youtube Video Player + Youtube API from asset store")]
    [SerializeField] private YoutubePlayer youtubePlayer;

    [Tooltip("Video Player used by Youtube Player")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Tooltip("Youtube Subtitle Reader from youtube player")]
    [SerializeField] private YoutubeSubtitlesReader youtubeSubtitlesReader;

    [Tooltip("Panel that displays whole video")]
    [SerializeField] private GameObject VideoPanel;

    [Tooltip("Text Component which holds title")]
    [SerializeField] private Text TitleDisplay;

    [Tooltip("Message displayed with youtube link")]
    [SerializeField] private string LinkMessage;

    [Tooltip("Message displayed in french with youtube link")]
    [SerializeField] private string LinkMessageFR;

    [Tooltip("Input Field that displays link")]
    [SerializeField] private InputField YoutubeLinkDisplay;

    [Tooltip("Number of Videos")]
    [SerializeField] private int numberOfVids;

    [Tooltip("List of links for youtube videos in english")]
    [SerializeField] private string[] youtubeLinks;

    [Tooltip("List of links for youtube videos in french")]
    [SerializeField] private string[] youtubeLinksFR;

    [Tooltip("List of titles for youtube videos in english")]
    [SerializeField] private string[] titlesEN;

    [Tooltip("List of titles for youtube videos in french")]
    [SerializeField] private string[] titlesFR;    
    
    [Tooltip("List of subtitles for youtube videos in english")]
    [SerializeField] private TextAsset[] textAssets;

    [Tooltip("List of subtitles for youtube videos in french")]
    [SerializeField] private TextAsset[] textAssetsFR;


    [Tooltip("Turn true if using animations with video")]
    [SerializeField] private bool isUsingAnimation = false;

    [Tooltip("If true video at index VideoIndexOnStart will play on start")]
    [SerializeField] private bool playOnAwake = false;

    [HideInInspector] [SerializeField] private int videoIndexOnStart;
    [HideInInspector] [SerializeField] private ProcessAnimationController processController;
    [HideInInspector] [SerializeField] private DialogueTrigger dialogueInterface;

    [Tooltip("Index for animator, won't be used if isUsingAnimation is false")]
    [SerializeField] private int[] animatorIndex;

    [Tooltip("Index for dialog, won't be used if isUsingAnimation is false")]
    [SerializeField] private int[] dialogueIndex;

    [Tooltip("Video indexes exception to animator indexes")]
    [SerializeField] private bool[] exceptionList;   

    private int m_videoIndex = -1;


    void Start()
    {
        if (PlayOnAwake)
        {
            PlayYoutubePlayer(VideoIndexOnStart);
        }
    }

    // Update is called once per frame
    public void PlayYoutubePlayer(int index)
    {
        if (index < 0 || index >= NumberOfVideos) return;
        VideoPanel.SetActive(true);
        bool lang = !languageSceneManager.languageScene.getLanguage();
        if (lang)
        {
            youtubeSubtitlesReader.Captions = Subtitles[index];
            youtubeSubtitlesReader.LoadSubtitles();
            youtubePlayer.Play(Links[index]);
            TitleDisplay.text = Titles[index];
            YoutubeLinkDisplay.text = LinkMessage + " " + Links[index];
        }
        else
        {
            youtubeSubtitlesReader.Captions = SubtitlesFR[index];
            youtubeSubtitlesReader.LoadSubtitles();
            youtubePlayer.Play(LinksFR[index]);
            TitleDisplay.text = TitlesFR[index];
            YoutubeLinkDisplay.text = LinkMessageFR + " " + LinksFR[index];
        }
        m_videoIndex = index;

    }

    public void ExitVideo()
    {
        if (VideoIndex > -1 && VideoIndex < NumberOfVideos)
        {
            VideoPanel.SetActive(false);
            if (IsUsingAnimation & exceptionList[VideoIndex])
            {
                AnimationController.ChangeAnimator(animatorIndex[VideoIndex]); //Trigger operation animation
                DialogueInterface.TriggerDialogue(dialogueIndex[VideoIndex]); //Trigger operation dialogue
            }
            m_videoIndex = -1; //reset index
        }
    }

    public int VideoIndex
    {
        get => m_videoIndex;
    }

    public YoutubePlayer Player
    {
        get => youtubePlayer;
        set => youtubePlayer = value;

    }

    public string[] Titles
    {
        get => titlesEN;
        set => titlesEN = value;

    }

    public string[] TitlesFR
    {
        get => titlesFR;
        set => titlesFR = value;

    }

    public string[] Links
    {
        get => youtubeLinks;
        set => youtubeLinks = value;
    }

    public string[] LinksFR
    {
        get => youtubeLinksFR;
        set => youtubeLinksFR = value;

    }    
    public TextAsset[] Subtitles
    {
        get => textAssets;
        set => textAssets = value;
    }

    public TextAsset[] SubtitlesFR
    {
        get => textAssetsFR;
        set => textAssetsFR = value;

    }

    public int NumberOfVideos
    {
        get => numberOfVids;
        set => numberOfVids = value;
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
    }

    public int VideoIndexOnStart
    {
        get => videoIndexOnStart;
        set => videoIndexOnStart = value;
    }

    public bool IsUsingAnimation
    {
        get => isUsingAnimation;
    }

    public ProcessAnimationController AnimationController
    {
        get => processController;
        set => processController = value;
    }
    public DialogueTrigger DialogueInterface
    {
        get => dialogueInterface;
        set => dialogueInterface = value;
    }

    public int[] AnimatorIndex
    {
        get => animatorIndex;
        set => animatorIndex = value;
    }

    public int[] DialogueIndex
    {
        get => dialogueIndex;
        set => dialogueIndex = value;
    }

    public bool[] Exceptions
    {
        get => exceptionList;
        set => exceptionList = value;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(VideoManager2))]
public class VideoManagerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        VideoManager2 script = (VideoManager2)target;


        if (script != null)
        {
            if (script.PlayOnAwake)
            {
                script.VideoIndexOnStart = EditorGUILayout.IntField("Video Index On Start", script.VideoIndexOnStart);
            }
            if (script.IsUsingAnimation)
            {
                script.AnimationController = EditorGUILayout.ObjectField("Process Controller", script.AnimationController, typeof(ProcessAnimationController), true) as ProcessAnimationController;
                script.DialogueInterface = EditorGUILayout.ObjectField("Trigger Dialogue Interface", script.DialogueInterface, typeof(DialogueTrigger), true) as DialogueTrigger;
            }

            int maxLength = script.NumberOfVideos;
            if (maxLength > 0)
            {
                string[] link = script.Links;
                string[] linkFR = script.LinksFR;
                string[] title = script.Titles;
                string[] titleFR = script.TitlesFR;
                TextAsset[] subs = script.Subtitles;
                TextAsset[] subsFR = script.SubtitlesFR;
                bool[] exception = script.Exceptions;

                script.Links = new string[maxLength];
                script.LinksFR = new string[maxLength];
                script.TitlesFR = new string[maxLength];
                script.Titles = new string[maxLength];
                script.Subtitles = new TextAsset[maxLength];
                script.SubtitlesFR = new TextAsset[maxLength];
                script.Exceptions = new bool[maxLength];

                if (link.Length > maxLength)
                {
                    System.Array.Copy(link, script.Links, maxLength);
                    System.Array.Copy(linkFR, script.LinksFR, maxLength);
                    System.Array.Copy(title, script.Titles, maxLength);
                    System.Array.Copy(titleFR, script.TitlesFR, maxLength);
                    System.Array.Copy(subs, script.Subtitles, maxLength);
                    System.Array.Copy(subsFR, script.SubtitlesFR, maxLength);
                    System.Array.Copy(exception, script.Exceptions, maxLength);
                }
                else
                {
                    link.CopyTo(script.Links, 0);
                    linkFR.CopyTo(script.LinksFR, 0);
                    title.CopyTo(script.Titles, 0);
                    titleFR.CopyTo(script.TitlesFR, 0);
                    subs.CopyTo(script.Subtitles, 0);
                    subsFR.CopyTo(script.SubtitlesFR, 0);
                    exception.CopyTo(script.Exceptions, 0);
                }
            }
        }

    }
}
#endif
