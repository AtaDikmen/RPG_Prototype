using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.Canvas
{
    public class MainMenu : MonoBehaviour
    {
        public Text progressText;
        public Image loadBar;
        public GameObject loadPanel;

        void Start()
        {

        }
        public void StartGame()
        {
            StartCoroutine(LoadingScreen());
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public IEnumerator LoadingScreen()
        {
            loadPanel.SetActive(true);

            AsyncOperation Operation = SceneManager.LoadSceneAsync(1);
            
            while (!Operation.isDone)
            {
                float progress = Mathf.Clamp01(Operation.progress / 0.9f);
                loadBar.fillAmount = progress;
                progressText.text = "%" + Math.Round((progress * 100), 1);

                yield return null;
            }
        }
    }
}
