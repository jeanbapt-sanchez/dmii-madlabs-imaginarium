using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{
    private Vector2 uvOffset = Vector2.zero;

    [SerializeField] private float ScrollX = 0.5f;
    [SerializeField] private Light spotlight;

    private void Update() {
        float OffsetX = Time.time * ScrollX;
        //GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, 0);
        Debug.Log(spotlight.cookie);
    }
}
