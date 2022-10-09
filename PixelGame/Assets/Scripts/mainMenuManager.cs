using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject storyPanel;
    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void storyButton()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(false);
        }
        storyPanel.SetActive(true);
    }
    
    public void storyBackButton()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(true);
        }
        storyPanel.SetActive(false);
    }

    public void exitButton()
    {
        Application.Quit();
    }
}
