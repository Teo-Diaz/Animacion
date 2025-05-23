using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    [SerializeField] private CanvasGroup deathPanel;
    [SerializeField] private float fadeDuration = 1.5f;

    public void ShowDeathPanel()
    {
        deathPanel.gameObject.SetActive(true);
        deathPanel.alpha = 0;
        deathPanel.interactable = false;
        deathPanel.blocksRaycasts = false;

        // Espera 0.5 segundos antes de hacer el fade
        DOTween.Sequence()
            .AppendInterval(0.5f) // tiempo de espera
            .Append(deathPanel.DOFade(1f, fadeDuration))
            .OnStart(() =>
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; 
                
                deathPanel.interactable = true;
                deathPanel.blocksRaycasts = true;
            });
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Tutorial"); 
    }
}
