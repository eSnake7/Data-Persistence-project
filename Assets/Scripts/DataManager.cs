using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public InputField nameInputField;
    public string playerName, highScoreName;
    public int score, highScore;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class HighScore
    {
        public int highScore;
        public string highScoreName;
    }

    public void SaveHighScore(int score, string playerName)
    {
        HighScore data = new HighScore();
        data.highScore = score;
        data.highScoreName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }

    public void StartGame()
    {
        if(nameInputField.text == "")
        {
            playerName = "nameless";
        }
        else
        {
            playerName = nameInputField.text;
        }
        score = 0;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
