using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int currScore = 0;
    [SerializeField] int previousScore;
    LivesDisplay livesDisplay;
   
   private void Awake()
   {
       //Singleton Creation 
       int GameSessions = FindObjectsOfType<GameSession>().Length;
       if(GameSessions > 1)
       {
           Destroy(gameObject);
       }
       else
       {
           DontDestroyOnLoad(gameObject);
       }
   }

   private void Start()
   {
    
   }

   public void processPlayerDeath()
   {
        if(lives > 1)
        {
            lives--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            FindObjectOfType<LivesDisplay>().updateDisplay();
            
        }
        else
        {
            ResetSession();
        }
   }

   public int getLives()
   {
       return lives;
   }

   public void ResetSession()
   {
       SceneManager.LoadScene(0); //menu screen
       Destroy(gameObject);
   }

   public void changeScore(int score)
   {
       currScore += score;
   }
}
