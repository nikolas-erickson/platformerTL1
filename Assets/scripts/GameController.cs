using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private Text _pointsText;
    private int score;
    private int lives;
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
        lives = 0;
    }
    public void addToPoints(int amount)
    {
        score += amount;
        _pointsText.text = "Score: " + score;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
