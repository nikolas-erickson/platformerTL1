using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class levelButton : MonoBehaviour
{
    private Color grey = new Color(10, 10, 10);
    private Color yellow = new Color(255, 255, 0);
    private Color green = new Color(0, 255, 0);
    [SerializeField] private int levelNum;
    [SerializeField] gameSaveData saveData;
    private TextMeshProUGUI textObj;
    // Start is called before the first frame update
    void Start()
    {
        textObj = GetComponentInChildren<TextMeshProUGUI>();
        textObj.text = levelNum.ToString("D2");
        levelState l = saveData.getLevelState(levelNum);
        Button btn = GetComponent<Button>();
        Image img = GetComponent<Image>();
        if (l == levelState.locked)
        {
            img.color = grey;
        }
        else if (l == levelState.completed)
        {
            img.color = green;
            btn.onClick.AddListener(delegate { OpenScene(levelNum); });
        }
        else if (l == levelState.unlocked)
        {
            img.color = yellow;
            btn.onClick.AddListener(delegate { OpenScene(levelNum); });
        }
        else
        {
            img.color = grey;
        }

    }

    public void OpenScene(int levelNum)
    {
        saveData.setCurrentLevel(levelNum);
        SceneManager.LoadScene("level" + levelNum.ToString("D2"));
    }
}
