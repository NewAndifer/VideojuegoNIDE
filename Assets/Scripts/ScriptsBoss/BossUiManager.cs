using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BossUIManager : MonoBehaviour
{
    public static BossUIManager Instance;

    private UIDocument uiDocument;
    private VisualElement[] hearts = new VisualElement[3];

    private Label timeCountLabel;
    private float timeRemaining = 60f; 
    private bool isTimerActive = false;

    public float GetTimeRemaining() { return timeRemaining; }
    public bool GetIsTimerActive() { return isTimerActive; }

    public string FormatearTiempo(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null) return;

        VisualElement root = uiDocument.rootVisualElement;
        if (root == null) return;

        hearts[0] = root.Q<VisualElement>("Life1");
        hearts[1] = root.Q<VisualElement>("Life2");
        hearts[2] = root.Q<VisualElement>("Life3");

        timeCountLabel = root.Q<Label>("TimeCount");

        UpdateTimerText(timeRemaining);
        StartTimer();
    }

    private void Update()
    {
        if (isTimerActive && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; 
            UpdateTimerText(timeRemaining);

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerActive = false;
                UpdateTimerText(timeRemaining); 
                OnTimeOut();
            }
        }
    }

    public void StartTimer()
    {
        timeRemaining = 60f; 
        isTimerActive = true;
    }

    public void StopTimer()
    {
        isTimerActive = false;
    }

   private void UpdateTimerText(float timeInSeconds)
    {
        if (timeCountLabel == null) return;
        
        timeCountLabel.text = FormatearTiempo(timeInSeconds);
    }

    private void OnTimeOut()
    {
        SceneManager.LoadScene("BossCombat");        
    }

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;

            if (i < currentLives)
            {
                hearts[i].style.display = DisplayStyle.Flex; 
            }
            else
            {
                hearts[i].style.display = DisplayStyle.None; 
            }
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene("Mainmap_01");
        }
    }
}