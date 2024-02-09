using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] gameSaveData saveData;
    public static GameController Instance { get; private set; }
    [SerializeField] private Text _pointsText;
    private int score;
    //private int lives;
    private float timerAmt;
    private bool timerActive;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // dont allow 2 GameControllers
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timerActive = false;
        //lives = 0;
    }
    public void addToPoints(int amount)
    {
        score += amount;
        _pointsText.text = "Score: " + score;

    }

    public void storeLevelComplete()
    {
        saveData.completeLevel();
    }

    public void startTmrReturnToLvlSelect()
    {
        timerAmt = 2f;
        timerActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timerAmt -= Time.deltaTime;
            if (timerAmt < 0)
            {
                SceneManager.LoadScene("levelSelect");
            }
        }

    }
}
