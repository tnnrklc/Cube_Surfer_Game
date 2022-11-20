using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Detector : MonoBehaviour
{
    public int collectedBonus = 0;

    public PlayerMover PlayerMover;

    public TextMeshProUGUI text;

    public TextMeshProUGUI scoreFinal;

    public TextMeshProUGUI highestScore;

    public RectTransform Level2;

    public RectTransform highScoreUI;

    public int highScore = 0;

    string highScoreKey = "HighScore";

    public ParticleSystem pickup;

    //private AudioSource sourceGem;

    public AudioSource[] sounds;

    void Start()
    {
        //sourceGem = GetComponent<AudioSource>();

        sounds = GetComponents<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cube"))
        {
            var cubeBehoviour = collision.gameObject.GetComponent<CubeBehoviour>();

            if (!cubeBehoviour.isStacked)
            {
                PlayerCubeManager.Instance.GetCube(cubeBehoviour);
            }

            sounds[1].Play();
        }

        if (collision.gameObject.CompareTag("GreenGem"))
        {
            collectedBonus += 25;
            text.text = collectedBonus.ToString();

            pickup.Play();

            //sourceGem.Play();

            sounds[0].Play();

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PurpleGem"))
        {
            collectedBonus += 50;
            text.text = collectedBonus.ToString();

            pickup.Play();

            //sourceGem.Play();

            sounds[0].Play();

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Level2")
        {

            PlayerMover.velocityOfPlayer += 1f;
            StartCoroutine(ActivationRoutine());
            /*else
            {
                Vector3 groundPosition = new Vector3(0f, -0.0215f, -0.14f);
                var playerTransform = PlayerBehoviour.Instance.transform;
                playerTransform.DOLocalJump(groundPosition, 0.05f, 1, 0.5f);
                PlayerBehoviour.Instance.DeathAnimaton();
            }*/
        }

        if (other.gameObject.name == "Finish")
        {
            sounds[3].Play();
            scoreFinal.text = collectedBonus.ToString();
            highScore = PlayerPrefs.GetInt(highScoreKey, 0);
            AccessEndPoint();

            if (collectedBonus > highScore)
            {
                PlayerPrefs.SetInt(highScoreKey, collectedBonus);
                highestScore.text = collectedBonus.ToString();
                PlayerPrefs.Save();

                highScoreUI.gameObject.SetActive(true);
                Vector3 defaultScale = highScoreUI.transform.localScale;

                highScoreUI.transform.localScale = Vector3.one * 0.0001f;
                highScoreUI.DOScale(defaultScale, 1f).SetEase(Ease.OutBounce);
            }

            PlayerBehoviour.Instance.SuccessAnimaton();
            Vector3 groundPosition = new Vector3(0f, -0.0215f, -0.14f);
            var playerTransform = PlayerBehoviour.Instance.transform;
            playerTransform.DOLocalJump(groundPosition, 0.05f, 1, 0.5f);
        }

    }

    private IEnumerator ActivationRoutine()
    {
        Level2.gameObject.SetActive(true);
        Vector3 defaultScale = Level2.transform.localScale;

        Level2.transform.localScale = Vector3.one * 0.0001f;
        Level2.DOScale(defaultScale, 1f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(2);

        Level2.gameObject.SetActive(false);
    }

    public void AccessEndPoint()
    {
        DOTween.To(() => PlayerMover.velocityOfPlayer, x => PlayerMover.velocityOfPlayer = x, 0, 0.2f)
            .OnUpdate(() =>
            {
                Debug.Log("DOTween update");
            }).OnComplete(action: () =>
            {
                PlayerMover.CanMotion = false;
                //Effect.gameObject.SetActive(true);

                GameManager.Instance.ActivateWinUI();
            });
    }
}
