using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text bile�eni i�in gerekli

public class kusKontrol : MonoBehaviour
{
    public Sprite[] kusSprites;
    SpriteRenderer spriteRenderer;
    bool ileriHareket = true;
    int kusSayacHareket = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D rigidbody_;

    public Text sayacText;
    int sayacPuan = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody_ = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        KusAnimasyon();
        KusHareket();
    }

    void KusAnimasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.1f) // Animasyon h�z�
        {
            kusAnimasyonZaman = 0;
            if (ileriHareket)
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];
                kusSayacHareket++;
                if (kusSayacHareket == 3)
                {
                    kusSayacHareket--;
                    ileriHareket = false;
                }
            }
            else
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];
                kusSayacHareket--;
                if (kusSayacHareket == 0)
                {
                    kusSayacHareket++;
                    ileriHareket = true;
                }
            }
        }
    }

    void KusHareket()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�k veya dokunma
        {
            rigidbody_.linearVelocity = Vector2.zero;
            rigidbody_.AddForce(new Vector2(0, 200));
        }

        // Ku� yukar� bakarken/d��erken a�� de�i�tirme
        if (rigidbody_.linearVelocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "puan")
        {
            sayacPuan++;
            sayacText.text = "Puan = " + sayacPuan;
        }
    }
}
