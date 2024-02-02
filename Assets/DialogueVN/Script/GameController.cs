using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{   
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public ChooseController chooseController;
    public AudioController audioController;


    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }
    // Start is called before the first frame update
    void Start()
    {    
        UnlockCursor();

        if (currentScene is StoryScene)
        {   
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
            backgroundController.SetImage(storyScene.background);
            PlayAudio(storyScene.sentences[0]);
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
   /*  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {   
                if (bottomBar.IsLastSentence())
                {
                    PlayScene((currentScene as StoryScene).nextScene);
                Debug.Log("Lanjut Ke Permainan");
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    } */
//? Code Reference : AD Works
void Update()
{   
    if(state == State.IDLE) {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {   
           
            if (bottomBar.IsCompleted())
            {   
                bottomBar.StopTyping();
                if (bottomBar.IsLastSentence())
                {
                    StoryScene storyScene = currentScene as StoryScene;
                    if (storyScene != null)
                    {   
                        if(storyScene.levelUnlock != 0)
                        {   
                            Debug.Log(storyScene.levelUnlock);
                            PlayerPrefs.SetInt("levelAt", storyScene.levelUnlock);
                            PlayerPrefs.Save();
                        }

                        // Cek apakah sceneToLoad terisi
                        if (storyScene.sceneToLoad != null  && storyScene.sceneToLoad != "")
                        {
                            
                            SceneManager.LoadScene(storyScene.sceneToLoad);
                        }
                        else
                        {
                            // Jika sceneToLoad tidak terisi, lanjutkan ke nextScene
                            PlayScene(storyScene.nextScene);
                        }
                    }
                    else
                    {
                        Debug.LogError("currentScene is not a StoryScene");
                    }
                }
                else
                {
                    bottomBar.PlayNextSentence();
                    PlayAudio((currentScene as StoryScene)
                                .sentences[bottomBar.GetSentenceIndex()]);
                }
            }
            else
            {
                bottomBar.SpeedUp();
            }
        }
    }
}

    
    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {   
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {   StoryScene storyScene = scene as StoryScene;
            backgroundController.SwitchImage(storyScene.background);
            PlayAudio(storyScene.sentences[0]);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
            bottomBar.Hide();
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound);
    }
}
