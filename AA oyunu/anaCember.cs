using UnityEngine;
using UnityEngine.UI;
using System;

public class anaCember : MonoBehaviour
{
    public GameObject kucukCember;
    public GameObject donenCember; // Müfettiþten atamayý unutma
    public GameObject anaCember_;
    public GameObject oyunuYoneticisi;
    public int atisSayisi = 8;

    void Start()
    {
        // Tag ile oyun yöneticisini buluyoruz
        oyunuYoneticisi = GameObject.FindGameObjectWithTag("oyunYoneticisi");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Firlat();
        }
    }

    void Firlat()
    {
        // Yeni küçük çemberi oluþturur
        Instantiate(kucukCember, transform.position, transform.rotation);

        // Senin döngün: Mevcut çocuk objelerdeki sayýlarý 1 eksiltir
        for (int i = 0; i < transform.childCount; i++)
        {
            // Yazýyý alýr, sayýya çevirir ve azaltýr
            int sayi = Convert.ToInt32(transform.GetChild(i).GetComponentInChildren<Text>().text);
            sayi--;

            if (sayi > 0)
            {
                transform.GetChild(i).GetComponentInChildren<Text>().text = sayi.ToString();
            }
            else
            {
                Destroy(transform.GetChild(i).gameObject); // Sayý biterse objeyi yok et
            }
        }

        atisSayisi--;

        // Eðer atýþlar bittiyse (senin kodundaki mantýkla) oyun yöneticisine haber ver
        if (atisSayisi <= 0)
        {
            oyunuYoneticisi.GetComponent<oyunYoneticisi>().oyunKazandi();
        }
    }
}