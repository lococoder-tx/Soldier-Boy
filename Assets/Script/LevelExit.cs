using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float LevelExitSlowMo = .2f;
    private void OnTriggerEnter2D(Collider2D col)
    {

    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMo;
        yield return null;
    }
}
