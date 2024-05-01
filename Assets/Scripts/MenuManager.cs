using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuManager;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public string playerName;
    public BestPlayer bestPlayer;
    private InputField inputName;
    private Text scoreText;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        inputName = GameObject.Find("Canvas/InputField").GetComponent<InputField>();
        playerName = inputName.text;
        scoreText = GameObject.Find("Canvas/BestScoreText").GetComponent<Text>();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public class BestPlayer
    {
        public string playerName;
        public int score;
    }

    public void SaveBestPlayer(int m_Points)
    {
        bestPlayer = new BestPlayer();
        bestPlayer.playerName = playerName;
        bestPlayer.score = m_Points;

        string json = JsonUtility.ToJson(bestPlayer);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bestPlayer = JsonUtility.FromJson<BestPlayer>(json);
        }
        else
            bestPlayer = new BestPlayer();
    }
    public void UpdateScore()
    {
        LoadBestPlayer();
        scoreText.text = "Best Score : " + bestPlayer.playerName + " : " + bestPlayer.score;
    }

    public void StartGame()
    {
        playerName = inputName.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
