using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}