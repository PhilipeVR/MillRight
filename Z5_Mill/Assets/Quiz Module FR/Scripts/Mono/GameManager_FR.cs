using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_FR : MonoBehaviour {

    #region Variables

    [SerializeField]    ProgressBar_FR         _progressBar            = null; // mine
    [SerializeField]    Button              _nextButton             = null; // mine
    [SerializeField]    Button              _continueButton         = null; // mine

    private             Question_FR[]          _questions              = null;
    public              Question_FR[]          Questions               { get { return _questions; } }

    [SerializeField]    GameEvents_FR          events                  = null;

    [SerializeField]    Animator            timerAnimtor            = null;
    [SerializeField]    TextMeshProUGUI     timerText               = null;
    [SerializeField]    Color               timerHalfWayOutColor    = Color.yellow;
    [SerializeField]    Color               timerAlmostOutColor     = Color.red;
    private             Color               timerDefaultColor       = Color.white;

    private             List<AnswerData_FR>    PickedAnswers           = new List<AnswerData_FR>();
    private             List<int>           FinishedQuestions       = new List<int>();
    private             int                 currentQuestion         = 0;

    private             int                 timerStateParaHash      = 0;

    private             IEnumerator         IE_WaitTillNextRound                 = null;
    private             IEnumerator         IE_StartTimer                        = null;
    private             IEnumerator         IE_WaitTillNextRound_FromSolution    = null; // mine

    private             bool                IsFinished
    {
        get
        {
            return (FinishedQuestions.Count < Questions.Length) ? false : true;
        }
    }
    [SerializeField] Button continueButton;

    #endregion

    #region Default Unity methods

    // Function that is called when the object becomes enabled and active
    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }

    // Function that is called when the behaviour becomes disabled
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    // Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    void Awake()
    {
        events.CurrentFinalScore = 0;
    }

    // Function that is called when the script instance is being loaded.
    void Start()
    {
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility_FR.SavePrefKey);

        timerDefaultColor = timerText.color;
        LoadQuestions();

        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

        Display();
    }

    #endregion

    // Function that is called to update new selected answer.
    public void UpdateAnswers(AnswerData_FR newAnswer)
    {
        if (Questions[currentQuestion].GetAnswerType == Question_FR.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    // Function that is called to clear PickedAnswers list.
    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData_FR>();
    }

    // Function that is called to display new question.
    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        } else { Debug.LogWarning("Something went wrong while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method."); }

        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }

        UpdateProgressBar(FinishedQuestions.Count, Questions.Length);
    }

    // Function that is called to accept picked answers and check/display the result.
    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        if (isCorrect)
        {
            UpdateScore(Questions[currentQuestion].AddScore);
        }


        if (IsFinished)
        {
            SetHighscore();
        }

        var type 
            = (IsFinished) // Call IsFinished property
            ? UIManager_FR.ResolutionScreenType.Finish // If IsFinished==true, then type =  UIManager.ResolutionScreenType.Finish
            : (isCorrect) ? UIManager_FR.ResolutionScreenType.Correct // If IsFinished != true, check if answers are correct
            : UIManager_FR.ResolutionScreenType.Incorrect; // If isCorrect != true, then type = UIManager.ResolutionScreenType.Incorrect

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        //AudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

        if (type == UIManager_FR.ResolutionScreenType.Incorrect) // >>> Show solution after incorrect resolution screen
        {
            

            StartCoroutine(ShowSolution()); // Adds a delay so SolutionUI elements are not added until after animation
            StopCoroutine(ShowSolution());

            StartCoroutine(SolutionScreen());
            StopCoroutine(SolutionScreen());
            
        }

        if ((type != UIManager_FR.ResolutionScreenType.Finish) && (type != UIManager_FR.ResolutionScreenType.Incorrect))
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }       
    }

    #region Timer Methods

    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimtor.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }

                timerAnimtor.SetInteger(timerStateParaHash, 1);
                break;
        }
    }
    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            timeLeft--;

            AudioManager.Instance.PlaySound("CountdownSFX");

            if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostOutColor;
            }

            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Accept();
    }
    #endregion

    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        Display();
    }

    IEnumerator WaitTillNextRound_FromSolution()
    {
        yield return new WaitForSeconds(0);
        _nextButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
        Display();
    }

    IEnumerator ShowSolution()
    {
        yield return new WaitForSeconds(1);

        events.UpdateSolutionUI(Questions[currentQuestion]);

        List<int> c = Questions[currentQuestion].GetCorrectAnswers();
        List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

        for(int i = 0; i<Questions[currentQuestion].Answers.Count(); i++)
        {
            if ( c.Contains(i) ) 
            {
                events.ShowSolution(i, true); // enable checkmark
            }
            else 
            {
                events.ShowSolution(i, false); // enable xmark
            }
        }

        _nextButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(true);
    }

    IEnumerator SolutionScreen()
    {
        var waitForButton = new WaitForUIButtons(continueButton);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == continueButton)
        {            
            if (IE_WaitTillNextRound_FromSolution != null)
            {
                StopCoroutine(IE_WaitTillNextRound_FromSolution);
            }
            IE_WaitTillNextRound_FromSolution = WaitTillNextRound_FromSolution();
            StartCoroutine(IE_WaitTillNextRound_FromSolution);            
        }                
    }

    
    // Function that is called to check currently picked answers and return the result.
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }

    // Function that is called to compare picked answers with question correct answers.
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> c = Questions[currentQuestion].GetCorrectAnswers();
            List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var f = c.Except(p).ToList(); // Remove all elements from list except the elements that can be found in list "p"
            var s = p.Except(c).ToList(); // Remove all elements from list except the elements that can be found in list "c"

            return !f.Any() && !s.Any(); // If "f" and "s" contains elements, return false
        }
        return false;
    }

    // Function that is called to load all questions from the Resource folder.
    void LoadQuestions()
    {
        Object[] objs = Resources.LoadAll("Questions_FR", typeof(Question_FR));
        _questions = new Question_FR[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _questions[i] = (Question_FR)objs[i];
        }
    }

    // Function that is called restart the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function that is called to quit the application.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Function that is called to set new highscore if game score is higher.
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }

    // Function that is called update the score and update the UI.
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }

    private void UpdateProgressBar(int current, int max)
    {
        _progressBar.UpdateProgressBar(current, max);
    }

    #region Getters

    Question_FR GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }

    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

    #endregion
}