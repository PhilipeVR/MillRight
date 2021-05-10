using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationGatherer : MonoBehaviour
{
    [SerializeField] public string scene;
    [SerializeField] private LanguageSceneToggle languageScene;
    [SerializeField] private SceneDisplayToggle sceneDisplayToggle;
    [DrawIf("Discover", true)] [SerializeField] private ComponentHint componentHint;
    [DrawIf("MainMenu", true)] [SerializeField] private LoadSceneScript loadSceneScript;
    [SerializeField] private LanguageSceneSwitcher languageSceneSwitcher;
    [DrawIf("AnimScene", true)] [DrawIf("Discover", true)] [SerializeField] private VideoManager2 videoManager;
    [DrawIf("Discover", true)][SerializeField] private DialogueTrigger trigger;
    [DrawIf("SimScene", true)] [DrawIf("AnimScene", true)] [SerializeField] private DialogueTrigger triggerDialogue;
    [DrawIf("AnimScene", true)] [SerializeField] private InteractableManager interactionManager;
    [DrawIf("SimScene", true)] [SerializeField] private ProcessChecker processChecker;
    [SerializeField] private bool AnimScene, Discover, MainMenu, SimScene;

    private string levelToLoad;
    
    // Start is called before the first frame update
    public void Awake()
    {
        LoadInformation();
    }

    public void SaveInformation()
    {
        if (Discover)
        {
            SaveSystem.SaveData(languageScene.Name, languageScene.Number, languageScene.getLanguage(), scene, sceneDisplayToggle.getTutorial(), componentHint);
        }
        else if (AnimScene)
        {
            SaveSystem.SaveData(languageScene.Name, languageScene.Number, languageScene.getLanguage(), scene, sceneDisplayToggle.getTutorial(), interactionManager.CurrentAnim);
        }
        else if (SimScene)
        {
            SaveSystem.SaveData(languageScene.Name, languageScene.Number, languageScene.getLanguage(), scene, sceneDisplayToggle.getTutorial(), processChecker.CompletedOperations);
        }
    }

    public void LoadInformation()
    {
        DataHandler data = SaveSystem.LoadData();
        if(data != null)
        {
            if (scene.Equals("MainMenu"))
            {
                languageScene.setName(data.StudentName);
                languageScene.setNumber(data.StudentNumber);
                languageScene.setLanguage(data.Language);
                sceneDisplayToggle.setTutorial(data.IsTutorial);
                levelToLoad = data.savedLevel;
                Debug.Log(data.StudentName);
                Debug.Log(data.StudentNumber);
                if (data.StudentName.Equals("CEED ADMIN") && data.StudentNumber.Equals("1234567"))
                {
                    Debug.Log("ADMIN MODE");
                    sceneDisplayToggle.AdminMode = true;
                }
                if (languageScene.getLanguage())
                {
                    languageSceneSwitcher.toggleOnStart();
                }
            }
            else if (scene.Equals("IntroTutorial"))
            {
                if (sceneDisplayToggle.getTutorial())
                {
                    if (data.SavedAnim != anim.NA)
                    {
                        interactionManager.SetupAnims(data.SavedAnim);
                        videoManager.VideoWatched = true;
                        triggerDialogue.SentenceTrigger = true;
                        Debug.Log(sceneDisplayToggle.AdminMode);
                    }
                }
            }
            else if(scene.Equals("ComponentDiscovery"))
            {
                if (sceneDisplayToggle.getTutorial())
                {
                    foreach (SetupOnHover part in componentHint.parts)
                    {
                        if (data.DetailIndexes.Contains(part.DetailIndex))
                        {
                            Debug.Log("Contains");
                            part.savedPartClicked = true;
                        }
                    }
                    videoManager.VideoWatched = true;
                    trigger.SentenceTrigger = true;
                }
            }
            else if (scene.Equals("Mill Simulation Scene"))
            {
                if (sceneDisplayToggle.getTutorial())
                {
                    Debug.Log(data.Completed[0]);
                    processChecker.ReloadFinishedOperations(data.Completed);
                    triggerDialogue.SentenceTrigger = true;
                }
            }


        }
    }

    public void ContinueTraining()
    {
        loadSceneScript.loadlevel(levelToLoad);
    }
}
