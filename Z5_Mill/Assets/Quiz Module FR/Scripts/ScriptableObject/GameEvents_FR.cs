using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents_FR", menuName = "Quiz_FR/new GameEvents_FR")]
public class GameEvents_FR : ScriptableObject {

    public delegate void    UpdateQuestionUICallback            (Question_FR question);
    public                  UpdateQuestionUICallback            UpdateQuestionUI                = null;

    public delegate void    UpdateSolutionUICallback            (Question_FR question);
    public                  UpdateSolutionUICallback            UpdateSolutionUI                = null;

    public delegate void    ShowSolutionCallback                (int index, bool b);
    public                  ShowSolutionCallback                ShowSolution                    = null;

    public delegate void    UpdateQuestionAnswerCallback        (AnswerData_FR pickedAnswer);
    public                  UpdateQuestionAnswerCallback        UpdateQuestionAnswer            = null;

    public delegate void    DisplayResolutionScreenCallback     (UIManager_FR.ResolutionScreenType type, int score);
    public                  DisplayResolutionScreenCallback     DisplayResolutionScreen         = null;

    public delegate void    ScoreUpdatedCallback();
    public                  ScoreUpdatedCallback                ScoreUpdated                    = null;

    [HideInInspector]
    public                  int                                 CurrentFinalScore               = 0;
    [HideInInspector]
    public                  int                                 StartupHighscore                = 0;
}