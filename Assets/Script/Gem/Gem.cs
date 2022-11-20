using UnityEngine;

public class Gem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(eulers: new Vector3(x: 0, y: 30, z: 0) * Time.deltaTime * 5f);
    }
}