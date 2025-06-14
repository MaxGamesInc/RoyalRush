using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
public class CameraController : MonoBehaviour
{
    CinemachineCamera cinemachineCamera;
    [Header("Camera Settings")]
    [SerializeField] float defaultFov = 60f;
    [SerializeField] float minFov = 30f;
    [SerializeField] float maxFov = 60f;
    [SerializeField] float zoomDuration = 0.5f;
    [SerializeField] float zoomAmount = 10f;
    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();

    }
    public void ChangeFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float TargetFOV = Mathf.Clamp(startFOV + (speedAmount * zoomAmount), minFov, maxFov);
        float timeelapsed = 0f;
        while (timeelapsed < zoomDuration)
        {

            timeelapsed += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, TargetFOV, timeelapsed / zoomDuration);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = TargetFOV;
    }
}
