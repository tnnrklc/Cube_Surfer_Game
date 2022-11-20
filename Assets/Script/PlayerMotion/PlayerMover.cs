using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    private bool canMotion = true;

    public bool CanMotion { get => canMotion; set => canMotion = value; }

    public float velocityOfPlayer;

    public RectTransform Level1;

    private void Start()
    {
        StartCoroutine(ActivationRoutine());
    }

    private IEnumerator ActivationRoutine()
    {
        Level1.gameObject.SetActive(true);
        Vector3 defaultScale = Level1.transform.localScale;

        Level1.transform.localScale = Vector3.one * 0.0001f;
        Level1.DOScale(defaultScale, 1f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(2);

        Level1.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!canMotion)
            return;

        transform.position += new Vector3(x: 0f, y: 0f, z: 1f) * Time.deltaTime * velocityOfPlayer;

        if (transform.position.x > 0.147f)
        {
            transform.position = new Vector3(x: 0.147f, y: transform.position.y, z: transform.position.z);
        }
        if (transform.position.x < -0.1633f)
        {
            transform.position = new Vector3(x: -0.1633f, y: transform.position.y, z: transform.position.z);
        }
    }

}
