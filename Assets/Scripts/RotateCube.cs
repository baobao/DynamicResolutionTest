using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private float _angle;

    void Update()
    {
        _angle = Time.deltaTime * 20f;
        transform.Rotate(new Vector3(0, 1f, 0), _angle);
    }
}
