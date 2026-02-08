using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public static ManageScenes instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Coroutine for moving to the next scene (based on the build index)
    /// </summary>
    /// <returns></returns>
    public IEnumerator SwitchScene()
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    /// <summary>
    /// Function used for UI button in Hierarchy
    /// </summary>
    public void NextScene()
    {
        StartCoroutine(SwitchScene());
    }

    /// <summary>
    /// Coroutine for switching to any scene based on scene name
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator ChangeScene(string sceneName)
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Function used for UI button in Hierarchy
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneChange(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }
}
