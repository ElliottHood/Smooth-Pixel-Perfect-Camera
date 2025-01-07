using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private bool twoDimensional = false;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float changeDirectionInterval = 1f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    System.Collections.IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            targetRotation = GetRandomRotation();
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    Quaternion GetRandomRotation()
    {
        if (twoDimensional)
        {
            float randomZ = Random.Range(0f, 360f);

            return Quaternion.Euler(0, 0, randomZ);
        }
        else
        {
            float randomX = Random.Range(0f, 360f);
            float randomY = Random.Range(0f, 360f);
            float randomZ = Random.Range(0f, 360f);

            return Quaternion.Euler(randomX, randomY, randomZ);
        }
    }
}
