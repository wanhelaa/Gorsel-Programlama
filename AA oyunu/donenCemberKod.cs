using UnityEngine;

public class donenCemberKod : MonoBehaviour
{
    public float donmeHizi = 100f;

    void Update()
    {
        // Çemberi Z ekseninde döndürür
        transform.Rotate(0, 0, donmeHizi * Time.deltaTime);
    }
}