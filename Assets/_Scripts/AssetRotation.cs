using UnityEngine;

public class AssetRotation : MonoBehaviour
{
   [SerializeField] float rotationStrength;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationStrength * Time.fixedDeltaTime);
    }
}
