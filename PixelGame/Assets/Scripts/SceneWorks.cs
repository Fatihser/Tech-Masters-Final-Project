using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWorks : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
    }
    public GameObject startC;
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void destroyStartCanvas()
    {
        Destroy(startC);
        Time.timeScale = 1;
    }
}
