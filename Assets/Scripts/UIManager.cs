using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image underlayPanel;
    public Image dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        //DOTween.SetTweensCapacity(100, 50);
        //StartCoroutine(WinDialog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        GameEvents.onLevelCompleted += TriggerWinDialog;
    }
    public void OnDisable()
    {
        GameEvents.onLevelCompleted -= TriggerWinDialog;
    }

    public void TriggerWinDialog()
    {
        StartCoroutine(WinDialog());
    }

    public IEnumerator WinDialog()
    {
        underlayPanel.gameObject.SetActive(true);
        underlayPanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.4f);
        dialogBox.gameObject.SetActive(true);
        dialogBox.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
        StopCoroutine(WinDialog());
    }

    public IEnumerator OpenSettings(GameObject settingsPanel)
    {
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<CanvasGroup>().DOFade(1, 1);
        yield return null;
    }
    public IEnumerator CloseSettings(GameObject settingsPanel)
    {
        settingsPanel.GetComponent<CanvasGroup>().DOFade(0, 1);
        yield return new WaitForSeconds(1.1f) ;
        settingsPanel.SetActive(false);
    }

    public void OpenControls(GameObject settingsPanel)
    {
        StartCoroutine(OpenSettings(settingsPanel));
    }
    public void CloseControls(GameObject settingsPanel)
    {
        StartCoroutine(CloseSettings(settingsPanel));
    }
}
