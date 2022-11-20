using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [SerializeField] //make what is private accessible from unity.
    private SwerveInputSystem swerveInputSystem;

    [SerializeField]
    private float swerveSpeed = 0.5f;

    [SerializeField]
    private float maxSwerveAmount = 1f;

    //input management should be made in update loop.
    private void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * swerveInputSystem.MoveFactoryX;
        swerveAmount = Mathf.Clamp(value: swerveAmount, min: -maxSwerveAmount, max: maxSwerveAmount);
        transform.Translate(x: swerveAmount, y: 0f, z: 0f);
    }
}
