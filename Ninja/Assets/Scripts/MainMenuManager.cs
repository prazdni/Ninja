using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenGameplayScene() 
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
