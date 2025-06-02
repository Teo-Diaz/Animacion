using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject tutorialUI;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform tutorialCameraTarget; // Empty GameObject en la posición/rotación del tutorial

    [Header("Camera Transition")]
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private Ease transitionEase = Ease.InOutSine;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void ShowTutorial()
    {
        mainMenuPanel.SetActive(false);

        // Transición de cámara al punto del tutorial
        cameraTransform.DOMove(tutorialCameraTarget.position, transitionDuration).SetEase(transitionEase);
        cameraTransform.DORotateQuaternion(tutorialCameraTarget.rotation, transitionDuration).SetEase(transitionEase);

        // Al final de la transición, mostrar el UI del tutorial
        DOVirtual.DelayedCall(transitionDuration, () =>
        {
            if (tutorialUI != null)
                tutorialUI.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
