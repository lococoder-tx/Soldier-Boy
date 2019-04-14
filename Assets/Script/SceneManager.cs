using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void StartFirstLeve()
    {
       SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }
}
