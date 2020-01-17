using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayerShowModule
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicShow : MonoBehaviour
    {
        private AudioSource audioSource;
        private float[] samples = new float[128];
        public LineRenderer lineRenderer;
        public float po;
        private Vector3 middleCubePostion;
        public float _timeout;
        public float t;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            lineRenderer.positionCount = samples.Length;
            t = _timeout;
        }

        void Update()
        {
            if (_timeout > 0f)
            {
                _timeout -= Time.deltaTime;
                if (_timeout <= 0f)
                {
                    _timeout = t;

                    audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
                    int j = samples.Length / 2;
                    int n = 0;
                    for (int i = samples.Length / 2; i < samples.Length; i++)
                    {
                        j--;
                        lineRenderer.SetPosition(i, new Vector3(0.1f * (i - samples.Length / 2 + po) - 2, (samples[n]) * 10, 0));
                        lineRenderer.SetPosition(j, new Vector3(0.1f * (j - samples.Length / 2) - 2, (samples[n]) * 10, 0));
                        n++;
                    }
                }
            }
        }
    }
}