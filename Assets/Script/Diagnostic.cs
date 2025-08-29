using System.Collections;
using UnityEngine;

public class Diagnostic : MonoBehaviour
{
    public float explosionRadius = 5f;
    public ParticleSystem explosionEffect;

    public AudioClip buildingSound;
    public AudioClip explosionSound;
    public AudioClip afterEffectSound;

    public AudioSource audioSource;
    private bool isTriggered;

    void Start()
    {
        Debug.Log("=== TrapDiagnostic START ===");

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("[TrapDiag] AudioSource not found!");
            return;
        }

        Debug.Log($"[TrapDiag] source.enabled={audioSource.enabled}, mute={audioSource.mute}, vol={audioSource.volume}, " +
                  $"pitch={audioSource.pitch}, spatialBlend={audioSource.spatialBlend}, playOnAwake={audioSource.playOnAwake}");

        if (buildingSound != null)
        {
            Debug.Log($"[TrapDiag] buildingSound='{buildingSound.name}' length={buildingSound.length:F2}s");
            CheckIfClipSilent(buildingSound);
            audioSource.PlayOneShot(buildingSound);
            Debug.Log("[TrapDiag] Played buildingSound via PlayOneShot");
        }
        else
        {
            Debug.LogWarning("[TrapDiag] buildingSound == null");
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("skelet") && !isTriggered)
        {
            Debug.Log("[TrapDiag] Triggered by skelet");

            if (explosionSound != null)
            {
                Debug.Log($"[TrapDiag] explosionSound='{explosionSound.name}' length={explosionSound.length:F2}s");
                CheckIfClipSilent(explosionSound);

                // через AudioSource
                audioSource.spatialBlend = 0f; // чтобы 3D не мешал
                audioSource.PlayOneShot(explosionSound);
                Debug.Log("[TrapDiag] Called audioSource.PlayOneShot(explosionSound)");

                // fallback через PlayClipAtPoint
                AudioSource.PlayClipAtPoint(explosionSound,
                    Camera.main ? Camera.main.transform.position : Vector3.zero);
                Debug.Log("[TrapDiag] Also called PlayClipAtPoint(explosionSound)");
            }
            else
            {
                Debug.LogWarning("[TrapDiag] explosionSound == null");
            }

            isTriggered = true;
            if (explosionEffect != null) explosionEffect.Play();

            await Awaitable.WaitForSecondsAsync(2);
            if (afterEffectSound != null)
            {
                CheckIfClipSilent(afterEffectSound);
                audioSource.PlayOneShot(afterEffectSound);
                Debug.Log("[TrapDiag] Played afterEffectSound");
            }
        }
    }

    private void CheckIfClipSilent(AudioClip clip)
    {
        if (clip == null) { Debug.Log("[TrapDiag] Clip == null"); return; }
        int samplesToCheck = Mathf.Min(4096, clip.samples);
        float[] data = new float[samplesToCheck * clip.channels];
        clip.GetData(data, 0);
        float max = 0f;
        for (int i = 0; i < data.Length; i++)
            if (Mathf.Abs(data[i]) > max) max = Mathf.Abs(data[i]);
        Debug.Log($"[TrapDiag] Clip '{clip.name}' maxSampleAbs={max:F5} (0 = тихий/пустой, ~0.5-1 = норм)");
    }
}
