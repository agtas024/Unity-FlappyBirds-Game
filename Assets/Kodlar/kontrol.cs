using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class kontrol : MonoBehaviour
{
    public Sprite []kussprite; //kusun kanat seviyesini atmak için sprite dizisi (üst-orta-alt)
    SpriteRenderer sprite_renderer;
    Boolean ilerigerikontrol = true; //kanat kontrolü için
    int kussayaci = 0, puan = 0;
    public int enbpuan=0;
    float kanathizi;
    Rigidbody2D fizik;
    public Text puan_text;
    oyun_kontrol oyunkontrol;
    bool kus_kontrol = true;
    public Boolean carpmaseskontrol = true;
    AudioSource []sesler;
    void Start()
    {
        sesler = GetComponents<AudioSource>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        puan_text.text = "SCORE: " + puan;
        oyunkontrol = GameObject.FindGameObjectWithTag("oyunkontrolTAG").GetComponent<oyun_kontrol>();
        enbpuan = PlayerPrefs.GetInt("kayit");
    }

    void Update()
    {
        if (Input.GetButton("Fire1")&&kus_kontrol)
        {
            sesler[0].Play();
            fizik.velocity = new Vector2(0, 0); //hız sıfırlandı
            fizik.AddForce(new Vector2(0, 145)); //kuvvet uygulandı
            fizik.position = new Vector3(math.clamp(fizik.position.x, fizik.transform.position.x-1, fizik.transform.position.x+1), math.clamp(fizik.position.y, -3, 1.85f), 0.0f);
        }
        if (fizik.velocity.y > 0) //kuvvet uygulanırsa
        {
            transform.eulerAngles = new Vector3(0, 0, 35); //kuş 35 derece kafasını kaldırır
        }
        else if (fizik.velocity.y < 0) // yer çekimi kuvvet uygularsa
        {
            transform.eulerAngles = new Vector3(0, 0, -45); // 45 derece kuş başını yere eğer
        }
        Animasyon();
    }
    void Animasyon()
    {
        kanathizi += Time.deltaTime;
        if (kanathizi > 0.5f)
        { //yarım saniye de bi kanat hareket etsin
            if (ilerigerikontrol) //kanat üstten alta gidecek true mi
            {
                sprite_renderer.sprite = kussprite[kussayaci++]; //ilk önce sprite icine kussayaci ndaki sprite atılır sonra kussayaci 1artırılır.
                if (kussayaci == kussprite.Length) //sayac dizi boyuna ulasti ise 
                {
                    ilerigerikontrol = false; //kontrolu false yap alttan uste için
                    kussayaci--; // 2. resmi yukarda bi kez oynattı ikinci kez oynatmasın diye 1 e çek
                }
            }
            else
            {
                sprite_renderer.sprite = kussprite[--kussayaci]; //önce kussayaci 1 azaltılır ve değerine es olan sprite ı sprite a atılıe
                if (kussayaci == 0) // sayac 0 olunca
                {
                    ilerigerikontrol = true; //kontrol true olur
                    kussayaci++; //0. resmi bi kez oynattı ikinci kez oynatmasın diye artır 1 kez
                }
            }
        }
    }

    void OnTriggerEnter2D (Collider2D carpisma)
    {
        if(carpisma.tag == "engelTAG" || carpisma.tag=="zeminTAG")
        {
            kus_kontrol = false;
            oyunkontrol.oyun_bitti();
        }
        else if (carpisma.tag == "puanTAG")
        {
            puan += 5;
            sesler[1].Play();
            puan_text.text = "SCORE: " + puan;
        }
        if (puan > enbpuan)
        {
            enbpuan = puan;
            PlayerPrefs.SetInt("kayit", enbpuan);
        }
    }
}
