using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayLength = 120f; 
    public float nightIntensity = 0.2f; 
    public float dayIntensity = 1f; 

    private float timeOfDay = 0f; 
    private float rotationSpeed; 

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = RenderSettings.sun; 
        }
        rotationSpeed = 360f / dayLength; 
    }

    void Update()
    {
        timeOfDay += Time.deltaTime / dayLength;
        if (timeOfDay > 1f)
            timeOfDay = 0f;

        float sunRotation = timeOfDay * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(sunRotation, 0f, 0f);

        if (timeOfDay > 0.2f && timeOfDay < 0.8f) 
        {
            directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, (timeOfDay - 0.2f) / 0.6f);
        }
        else // Nighttime
        {
            directionalLight.intensity = nightIntensity;
        }

        RenderSettings.skybox.SetFloat("_Rotation", sunRotation);
    }
}
