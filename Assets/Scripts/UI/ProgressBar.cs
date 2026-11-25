using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Levels")]
    [SerializeField] private int[] maxPointsPerLevel = { 100, 250, 500, 1000 };

    [Header("Save")]
    [SerializeField] private string saveKey = "ProgressData";

    [Header("Debug")]
    [SerializeField] private int debugAddAmount = 50;

    private int currentLevel;
    private int currentPoints;
    private int overflowPoints;

    public int CurrentLevel => currentLevel;
    public int CurrentPoints => currentPoints;
    public int OverflowPoints => overflowPoints;
    public int MaxPointsForCurrentLevel => GetMaxForLevel(currentLevel);
    public bool IsMaxLevel => GetMaxForLevel(currentLevel) < 0;

    public event Action<int> OnLevelUp;
    public event Action<int, int> OnPointsChanged;

    private GameConfig config;

    public void AddPoints(int amount)
    {
        if (amount <= 0) return;

        if (config && config.isHiddenSourceScreen)
        {
            amount *= 2;
        }    

        if (IsMaxLevel)
        {
            overflowPoints += amount;
            Save();
            RefreshUI();
            return;
        }

        currentPoints += amount;

        int max = GetMaxForCurrentLevel();
        while (max > 0 && currentPoints >= max)
        {
            currentPoints -= max;
            currentLevel++;

            int nextMax = GetMaxForLevel(currentLevel);
            if (nextMax < 0)
            {
                overflowPoints += currentPoints;
                currentPoints = 0;
                OnLevelUp?.Invoke(currentLevel);
                break;
            }

            max = nextMax;
            OnLevelUp?.Invoke(currentLevel);
        }

        Save();
        RefreshUI();
    }

    public void SetPoints(int level, int points, int overflow = 0)
    {
        currentLevel = Mathf.Max(0, level);
        currentPoints = Mathf.Max(0, points);
        overflowPoints = Mathf.Max(0, overflow);
        Save();
        RefreshUI();
    }

    [ContextMenu("Debug: Add Points")]
    public void DebugAddPoints()
    {
        AddPoints(debugAddAmount);
    }

    [ContextMenu("Debug: Reset Progress")]
    public void DebugResetProgress()
    {
        currentLevel = 0;
        currentPoints = 0;
        overflowPoints = 0;
        Save();
        RefreshUI();
    }

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        config = FindAnyObjectByType<GameConfig>(FindObjectsInactive.Include);

        RefreshUI();
    }

    private int GetMaxForLevel(int level)
    {
        if (maxPointsPerLevel == null) return -1;
        if (level < 0 || level >= maxPointsPerLevel.Length) return -1;
        return maxPointsPerLevel[level];
    }

    private int GetMaxForCurrentLevel() => GetMaxForLevel(currentLevel);

    private void RefreshUI()
    {
        int max = GetMaxForCurrentLevel();

        if (fillImage != null)
        {
            fillImage.fillAmount = max <= 0
                ? 1f
                : Mathf.Clamp01((float)currentPoints / max);
        }

        if (progressText != null)
        {
            progressText.text = max <= 0
                ? (overflowPoints > 0 ? $"MAX (+{overflowPoints})" : "MAX")
                : $"{currentPoints} / {max}";
        }

        if (levelText != null)
        {
            levelText.text = max <= 0
                ? $"Уровень {currentLevel + 1} (MAX)"
                : $"Уровень {currentLevel + 1}";
        }

        OnPointsChanged?.Invoke(currentPoints, max);
    }

    private void Save()
    {
        var data = new SaveData
        {
            level = currentLevel,
            points = currentPoints,
            overflow = overflowPoints
        };
        PlayerPrefs.SetString(saveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);
            var data = JsonUtility.FromJson<SaveData>(json);
            if (data != null)
            {
                currentLevel = Mathf.Max(0, data.level);
                currentPoints = Mathf.Max(0, data.points);
                overflowPoints = Mathf.Max(0, data.overflow);
                return;
            }
        }
        currentLevel = 0;
        currentPoints = 0;
        overflowPoints = 0;
    }

    [Serializable]
    private class SaveData
    {
        public int level;
        public int points;
        public int overflow;
    }
}