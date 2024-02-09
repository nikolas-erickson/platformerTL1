using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;

public enum levelState
{
    unlocked,
    locked,
    completed
}

[CreateAssetMenu]
public class gameSaveData : ScriptableObject, iDataPersistence
{
    private levelState[] _levels;
    private static int _numLevels = 8;
    private int currentLevel;
    private bool _loaded = false;

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

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadData(GameData data)
    {
        if (!_loaded)
        {

            _levels = new levelState[_numLevels];
            for (int i = 0; i < _numLevels; i++)
            {
                _levels[i] = data.levels[i];
            }
            _loaded = true;
        }
    }
    public void saveData(ref GameData data)
    {
        for (int i = 0; i < _numLevels; i++)
        {
            data.levels[i] = _levels[i];
        }
    }

    public void printData()
    {
        Debug.Log("printing");
        foreach (levelState l in _levels)
        {
            Debug.Log("state is " + l);
        }
    }


}
