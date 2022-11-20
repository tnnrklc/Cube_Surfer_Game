using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCubeManager : MonoBehaviour
{
    private float stepLength = 0.026f;

    private float playerStepLengthValue = 0.0045f;

    public TextMeshProUGUI scoreFinal;

    public Detector Detector;

    public List<CubeBehoviour> listOfCubeBehoviour = new List<CubeBehoviour>();

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static PlayerCubeManager Instance;

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #endregion

    public void GetCube(CubeBehoviour cubeBehoviour)
    {
        listOfCubeBehoviour.Add(cubeBehoviour);
        cubeBehoviour.isStacked = true;

        cubeBehoviour.transform.parent = transform;

        int indexOfCube = listOfCubeBehoviour.Count - 1;

        ReorderCubes();

        var playerTransform = PlayerBehoviour.Instance.transform;
        var yValue = indexOfCube * stepLength + playerStepLengthValue;
        var playerTarget = new Vector3(0f, yValue, 0f);

        playerTransform.DOLocalMove(playerTarget, 0.05f);

    }

    public void DropCube(CubeBehoviour cubeBehaviour)
    {
        cubeBehaviour.transform.parent = null;
        cubeBehaviour.isStacked = false;

        listOfCubeBehoviour.Remove(cubeBehaviour);

        var playerTransform = PlayerBehoviour.Instance.transform;

        if (listOfCubeBehoviour.Count < 1)
        {
            scoreFinal.text = Detector.collectedBonus.ToString();

            PlayerBehoviour.Instance.DeathAnimaton();

            Detector.sounds[2].Play();

            Vector3 groundPosition = new Vector3(0f, -0.0215f, -0.14f);

            playerTransform.DOLocalJump(groundPosition, 0.05f, 1, 0.5f);

            return;
        }

        int indexOfCube = listOfCubeBehoviour.Count - 1;
        var yValue = indexOfCube * stepLength + playerStepLengthValue;
        var playerTarget = new Vector3(0f, yValue, 0f);

        playerTransform.DOLocalMove(playerTarget, 0.05f);
    }

    private void ReorderCubes()
    {
        int index = listOfCubeBehoviour.Count - 1;

        foreach(var cube in listOfCubeBehoviour)
        {
            Vector3 target = new Vector3(0f, index * stepLength, 0f);
            cube.transform.DOLocalMove(target, 0.05f);
            index--;
        }
    }
}
