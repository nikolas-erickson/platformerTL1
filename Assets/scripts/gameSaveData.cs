using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum levelState
{
    unlocked,
    locked,
    completed
}

[CreateAssetMenu]
public class gameSaveData : ScriptableObject
{
    private List<levelState> _levels;
    private static int _numLevels = 8;
    private int currentLevel;
    void OnEnable()
    {
        while (_levels.Count < _numLevels)
        {
            _levels.Add(levelState.locked);
        }
        if (_levels[0] == levelState.locked)
        {
            _levels[0] = levelState.unlocked;
        }
    }


    public levelState getLevelState(int levelNum)
    {
        return _levels[levelNum - 1];
    }

    public int getnumLevels()
    {
        return _numLevels;
    }

    public void completeLevel()
    {
        Debug.Log("cur lvl " + currentLevel);
        _levels[currentLevel - 1] = levelState.completed;
        if (currentLevel < _numLevels)
        {
            if (_levels[currentLevel] == levelState.locked)
            {
                _levels[currentLevel] = levelState.unlocked;
            }
        }
    }

    public void setCurrentLevel(int l)
    {
        Debug.Log("set lvl " + l);
        currentLevel = l;
    }
}
