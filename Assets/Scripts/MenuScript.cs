using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject gameMenu;

    private void Awake()
    {
        gameMenu = GameObject.Find("GameMenu");
    }

    private void Start()
    {
        gameMenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { LoadScene(1); });
        gameMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { LoadScene(1); });
        gameMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { OnQuitBtn(); });
    }

    private void OnQuitBtn()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        gameMenu.transform.GetChild(0).gameObject.SetActive(false);
        gameMenu.transform.GetChild(1).gameObject.SetActive(false);
        gameMenu.transform.GetChild(2).gameObject.SetActive(false);
        gameMenu.transform.GetChild(3).gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            //Debug.Log("111111111");

            gameMenu.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = progressValue;

            
            yield return null;
        }


        //if (operation.isDone)
        //{
        //    Debug.Log("ooooooooooo");

        //}



        // Scene loading is complete. Now find the UIControllerScript and call OnGameStart().
        //UIControllerScript uiController = FindObjectOfType<UIControllerScript>();
        //if (uiController == null)
        //{
        //    Debug.Log("*******");
        //}
        //else
        //{
        //    uiController.OnGameStart();
        //}

        //Debug.Log("Here********");
    }
}
