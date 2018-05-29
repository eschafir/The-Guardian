using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
        MusicPlayer.PlayGameMusic();
    }

    public void QuitRequest() {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public static int ActualScene() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(ActualScene() + 1);
        MusicPlayer.PlayGameMusic();
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
        MusicPlayer.PlayMenuMusic();
    }

    public void Lose() {
        SceneManager.LoadScene("Lose");
        MusicPlayer.PlayLoseMusic();
    }

    public void Win() {
        SceneManager.LoadScene("Win");
        MusicPlayer.PlayWinMusic();
    }

    public void StartGame() {
        ScoreKeeper.Reset();
        SceneManager.LoadScene(1);
        MusicPlayer.PlayGameMusic();
    }

}
