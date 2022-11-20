using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public SwerveMovement SwerveMovement;
    public SwerveInputSystem SwerveInputSystem;
    public PlayerMover PlayerMover;

    public RectTransform WinUI;
    public RectTransform FailUI;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void ActivateWinUI()
    {
        WinUI.gameObject.SetActive(true);
        Vector3 defaultScale = WinUI.transform.localScale;

        WinUI.transform.localScale = Vector3.one * 0.0001f;
        WinUI.DOScale(defaultScale, 1f).SetEase(Ease.OutBounce);
    }

    public void ActivateFailUI()
    {
        FailUI.gameObject.SetActive(true);
        Vector3 defaultScale = FailUI.transform.localScale;

        FailUI.transform.localScale = Vector3.one * 0.0001f;
        FailUI.DOScale(defaultScale, 1f).SetEase(Ease.OutBounce);
    }

    public void StartGame()
    {
        int index = Random.Range(1,3);
        SceneManager.LoadScene(index);
    }
}
