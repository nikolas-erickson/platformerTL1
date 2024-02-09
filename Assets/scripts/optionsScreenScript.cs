using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsScreenScript : MonoBehaviour
{
    //private int soundLevel;
    //private bool bcMode;
    public void discardChanges()
    {
        // ignore local changes
        SceneManager.LoadScene("Title");
    }
    public void saveChanges()
    {
        // TODO - save local changes to playerPrefs
        SceneManager.LoadScene("Title");
    }
}
