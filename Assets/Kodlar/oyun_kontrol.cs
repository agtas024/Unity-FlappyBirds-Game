using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oyun_kontrol : MonoBehaviour
{
    public GameObject gök_1,gök_2,engel;
    Rigidbody2D fizik_1, fizik_2;
    float uzunluk,değişimzaman;
    public int kaç_tane_engel;
    GameObject[] engeller;
    int sayaç=0;
    AudioSource çarpmasesi;
    void Start()
    {
        çarpmasesi = GetComponent<AudioSource>();

        fizik_1 = gök_1.GetComponent<Rigidbody2D>();
        fizik_2 = gök_2.GetComponent<Rigidbody2D>();

        fizik_1.velocity = new Vector2(-1, 0);
        fizik_2.velocity = new Vector2(-1, 0);

        uzunluk = gök_1.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kaç_tane_engel];

        for(int i=0; i< engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-30, -30), Quaternion.identity);
            Rigidbody2D fizikengel = engeller[i].AddComponent<Rigidbody2D>();
            fizikengel.gravityScale = 0;
            fizikengel.velocity = new Vector2(-1, 0);
        }
    }

    void Update()
    {
        if (gök_1.transform.position.x <= -uzunluk-(uzunluk/2))
        {
            gök_1.transform.position += new Vector3(uzunluk*2, 0); ;
        }
        if(gök_2.transform.position.x <= -uzunluk-(uzunluk/2))
        {
            gök_2.transform.position += new Vector3(uzunluk*2, 0); ;
        }

        //--------------------------------------------------------------------------------------------------------------
        değişimzaman += Time.deltaTime;
        if (değişimzaman > 2.4f)
        {
            değişimzaman = 0;
            float Y_ekseni = Random.Range(1.77f, 3.45f);
            engeller[sayaç++].transform.position = new Vector3(11.6f, Y_ekseni);

            if (sayaç >= engeller.Length) sayaç = 0;
        } 
    }

    public void oyun_bitti()
    {
        StartCoroutine(oyunbitticagirdi());
    }
    public IEnumerator oyunbitticagirdi()
    {
        if (GameObject.FindGameObjectWithTag("kontrolTAG").GetComponent<kontrol>().carpmaseskontrol)
        {
            çarpmasesi.Play();
            GameObject.FindGameObjectWithTag("kontrolTAG").GetComponent<kontrol>().carpmaseskontrol = false;
        }
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fizik_1.velocity = Vector2.zero;
            fizik_2.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Ana menu");
    }
}

