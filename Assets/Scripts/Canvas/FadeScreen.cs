using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Canvas
{
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] GameObject fadeScreen;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                fadeScreen.SetActive(true);

                StartCoroutine(NextScene());
            }
        }

        private IEnumerator NextScene()
        {
            yield return new WaitForSeconds(2.3f);
            SceneManager.LoadScene(2);
        }
    }
}
