using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Carga la escena especificada por su nombre

        //AudioManager.instance.PlaySFX(0); // Reproduce un efecto de sonido al cambiar al siguiente personaje
    }

    public void QuitGame()
    {
        Application.Quit(); // Cierra la aplicación o el juego

        //AudioManager.instance.PlaySFX(0); // Reproduce un efecto de sonido al cambiar al siguiente personaje
    }
}
