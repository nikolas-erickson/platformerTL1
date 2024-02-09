using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public levelState[] levels;
    public int deaths;

    public GameData()
    {
        levels = new levelState[8];
        levels[0] = levelState.unlocked;
        for (int i = 1; i < 8; i++)
        {
            levels[i] = levelState.locked;
        }

        deaths = 0;
    }
}
