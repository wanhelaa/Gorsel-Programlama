using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public GameObject gokyuzu1, gokyuzu2;
    Rigidbody2D rigidbody1_, rigidbody2_;
    public float arkaPlanHiz = -1.5f;
    float uzunluk;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject[] engeller;
    float zamanSayac = 0;
    int sayac = 0;

    void Start()
    {
        rigidbody1_ = gokyuzu1.GetComponent<Rigidbody2D>();
        rigidbody2_ = gokyuzu2.GetComponent<Rigidbody2D>();

        rigidbody1_.linearVelocity = new Vector2(arkaPlanHiz, 0);
        rigidbody2_.linearVelocity = new Vector2(arkaPlanHiz, 0);

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector3(-20, -20, 0), Quaternion.identity);
            Rigidbody2D rbEngel = engeller[i].AddComponent<Rigidbody2D>();
            rbEngel.gravityScale = 0;
            rbEngel.linearVelocity = new Vector2(arkaPlanHiz, 0);
        }
    }

    void Update()
    {
        ArkaPlanHareket();
        EngelHareket();
    }

    void ArkaPlanHareket()
    {
        if (gokyuzu1.transform.position.x <= -uzunluk)
        {
            gokyuzu1.transform.position += new Vector3(uzunluk * 2, 0, 0);
        }
        if (gokyuzu2.transform.position.x <= -uzunluk)
        {
            gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0, 0);
        }
    }

    void EngelHareket()
    {
        zamanSayac += Time.deltaTime;
        if (zamanSayac >= 2f) // Her 2 saniyede bir yeni engel
        {
            zamanSayac = 0;
            float yEkseni = Random.Range(-0.1f, 1.24f);
            engeller[sayac].transform.position = new Vector3(3.13f, yEkseni, 0);

            sayac++;
            if (sayac >= engeller.Length)
            {
                sayac = 0;
            }
        }
    }
}
