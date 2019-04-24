using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartFirstLevel()
    {
       SceneManager.LoadScene(1);
    }
}
