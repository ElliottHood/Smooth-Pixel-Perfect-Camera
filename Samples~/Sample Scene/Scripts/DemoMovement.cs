using UnityEngine;

public class DemoMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3;

    void Update()
    {
        transform.position += Time.deltaTime * movementSpeed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    }
}
