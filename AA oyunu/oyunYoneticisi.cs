using UnityEngine;

public class oyunYoneticisi : MonoBehaviour
{
    GameObject anaCember;
    GameObject donenCember;
    public Animator animator;

    void Start()
    {
        // Sahnedeki objeleri tag ile bulur
        anaCember = GameObject.FindGameObjectWithTag("anaCember");
        donenCember = GameObject.FindGameObjectWithTag("donenCember");
    }

    public void OyunBitti()
    {
        // Donme olayýný ve atýþ yapmayý durdurur
        donenCember.GetComponent<donenCemberKod>().enabled = false;
        anaCember.GetComponent<anaCember>().enabled = false;

        // "oyunbitti" animasyonunu tetikler
        animator.SetTrigger("oyunbitti");
    }

    public void oyunKazandi()
    {
        Debug.Log("oyun kazandi.");
        Time.timeScale = 0; // Oyunu durdurur
    }
}
