using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField] private Light ThunderLight;
    [SerializeField] private AudioSource ThunderSound;
    [SerializeField] private int sampleDataLength = 1024;
    [SerializeField] private float updateStep = 0.1f;
    private float[] clipSampleData;
    private float currentUpdateTime = 0f;
    private float clipLoudness;
    private Coroutine thunderCoroutine;
    private void Awake() {
        if (!ThunderSound) {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
    }

    private void Update() {  
        currentUpdateTime += Time.deltaTime;
         if (currentUpdateTime >= updateStep) {
             currentUpdateTime = 0f;
             ThunderSound.clip.GetData(clipSampleData, ThunderSound.timeSamples);
             clipLoudness = 0f;
             foreach (var sample in clipSampleData) {
                 clipLoudness += Mathf.Abs(sample);
             }
             clipLoudness /= sampleDataLength;

            Debug.Log(clipLoudness);

            if (clipLoudness > 0.2f) {
                Debug.Log("TONNERRE");
                if(thunderCoroutine != null) {
                    StopCoroutine(thunderCoroutine);
                }
                thunderCoroutine = StartCoroutine(ThunderDelay());
            }
         }
    }

    private IEnumerator ThunderDelay() {
        ThunderLight.intensity = 3000;
        yield return new WaitForSeconds(0.1f);
        ThunderLight.intensity = 300;
    }
}
