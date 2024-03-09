using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;

    private void Start()
    {
        pausedMenu.SetActive(false);
        Button devamEtButton = GetComponentInChildren<Button>();
        
    }

    public void StoppedButton()
    {
        pausedMenu.SetActive(true);
    }

    public void PlayContinueGame()
    {
        pausedMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Scene1()
    {
        pausedMenu.SetActive(false );
        SceneManager.LoadScene(1);
        GameManager.instance.SetCurrentLevelIndex(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        int lastLevelIndex = PlayerPrefs.GetInt("LastLevelIndex", 1);

        // Oyuncu daha �nce hi�bir seviyeyi tamamlamam��sa veya sonraki seviye indeksi hatal�ysa
        if (lastLevelIndex < 1 || lastLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            // Varsay�lan olarak ilk seviyeye ge�
            lastLevelIndex = 1;
        }

        // Sonraki seviyeye ge�
        GameManager.instance.SetCurrentLevelIndex(lastLevelIndex);
        SceneManager.LoadScene(lastLevelIndex);
    }




}
