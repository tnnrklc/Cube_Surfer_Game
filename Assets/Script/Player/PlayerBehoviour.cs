using UnityEngine;

public class PlayerBehoviour : MonoBehaviour
{
    [SerializeField]
    private Animator animotorOfPlayer;

    public PlayerMover PlayerMover;

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static PlayerBehoviour Instance;

    private void Singleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #endregion

    public void DeathAnimaton()
    {
        PlayerMover.velocityOfPlayer = 0;
        animotorOfPlayer.SetTrigger("Death");
        GameManager.Instance.ActivateFailUI();
    }

    public void SuccessAnimaton()
    {
        PlayerMover.velocityOfPlayer = 0;
        animotorOfPlayer.SetTrigger("Success");
    }
}
