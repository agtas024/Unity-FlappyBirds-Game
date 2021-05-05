using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class reklam : MonoBehaviour
{
    void Start()
    {
            #if UNITY_ANDROİD
                string appId ="ca-app-pub-9990507867151963~2678952935";
            #endif

        MobileAds.Initialize(initStatus => { });
}


    void Update()
    {
        
    }
}
