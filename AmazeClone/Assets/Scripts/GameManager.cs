using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentLevelIndex = 1; 
    private int[] requiredColoredWallsPerLevel = { 0, 24, 33, 30, 32, 33, 32, 34, 29, 33, 34 }; // Her seviyedeki gereken boyanmýþ duvar sayýsý
    public int RequiredColoredWalls { get { return CalculateRequiredColoredWalls(); } }

    
    [SerializeField] private GameObject levelupParticleEffectPrefab1;
    [SerializeField] private GameObject levelupParticleEffectPrefab2;
    [SerializeField] private GameObject levelupParticleEffectPrefab3;
    
    [SerializeField] private GameObject levelUpText;
    [SerializeField] private GameObject finalGameText;
    
    private ParticleSystem levelupParticleEffect1;
    private ParticleSystem levelupParticleEffect2;
    private ParticleSystem levelupParticleEffect3;

    [SerializeField] private AudioSource levelPassAudio;
    [SerializeField] private AudioSource finishGameAudio;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // Oluþturulacak efektleri sahnede oluþtur
        InstantiateParticleEffects();
    }

    
    private void InstantiateParticleEffects()
    {
        // Efektleri sahnede oluþtur
        levelupParticleEffect1 = Instantiate(levelupParticleEffectPrefab1).GetComponent<ParticleSystem>();
        levelupParticleEffect2 = Instantiate(levelupParticleEffectPrefab2).GetComponent<ParticleSystem>();
        levelupParticleEffect3 = Instantiate(levelupParticleEffectPrefab3).GetComponent<ParticleSystem>();

        // Oluþturulan efektleri GameManager altýnda düzenle
        levelupParticleEffect1.transform.parent = transform;
        levelupParticleEffect2.transform.parent = transform;
        levelupParticleEffect3.transform.parent = transform;

        // Oluþturulan efektleri etkisiz yap
        levelupParticleEffect1.Stop();
        levelupParticleEffect2.Stop();
        levelupParticleEffect3.Stop();
    }

    
    public void LevelPass()
    {
        currentLevelIndex++;
        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Yüklü efektleri oynat
            levelupParticleEffect1.Play();
            levelupParticleEffect2.Play();
            levelupParticleEffect3.Play();
            levelPassAudio.Play();
            SetLevelUpTextActive(true);
            Invoke("DeactivateLevelUpText", 3);
            Invoke("NextSceneLevelFNC", 3);

            SaveLastLevelIndex(currentLevelIndex);
        }
        else
        {
            SetFinalGameText(true);
            levelupParticleEffect1.Play();
            levelupParticleEffect2.Play();
            levelupParticleEffect3.Play();
            finishGameAudio.Play();
            Invoke("DeactiveFinalGameText",10);
            Debug.Log("All levels completed!");
        }
    }

    // Bir sonraki seviyede gereken boyanmýþ duvar sayýsýný hesapla
    private int CalculateRequiredColoredWalls()
    {
        if (currentLevelIndex < requiredColoredWallsPerLevel.Length)
        {
            return requiredColoredWallsPerLevel[currentLevelIndex];
        }
        else
        {
            Debug.LogError("There is no required colored walls defined for level " + currentLevelIndex);
            return 0;
        }
    }

    private void NextSceneLevelFNC()
    {
        SceneManager.LoadScene(currentLevelIndex);
        Debug.Log("Level passed! Next level loaded.");
    }

    public void SetLevelUpTextActive(bool isActive)
    {
        levelUpText.gameObject.SetActive(isActive);
    }

    private void DeactivateLevelUpText()
    {
        SetLevelUpTextActive(false); 
    }
   
    
    public void SetFinalGameText(bool isActive)
    {
        finalGameText.gameObject.SetActive(isActive);
    }
    private void DeactiveFinalGameText()
    {
        SetFinalGameText(false);
    }


    public void SaveLastLevelIndex(int levelIndex)
    {
        PlayerPrefs.SetInt("LastLevelIndex", levelIndex);
        PlayerPrefs.Save(); 
    }

    public void SetCurrentLevelIndex(int index)
    {
        currentLevelIndex = index;
    }
}
