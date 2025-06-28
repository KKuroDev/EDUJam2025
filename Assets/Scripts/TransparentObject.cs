using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public float fadeSpeed = 2f;
    private Material material;
    private bool isPlayerNear = false;
    private bool areAnyDevilsNear = false;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        isPlayerNear = IsPlayerNear();
        areAnyDevilsNear = AreAnyDevilsNear();


        if (isPlayerNear || areAnyDevilsNear)
            FadeOut();
        else
            FadeIn();
    }

    private bool IsPlayerNear()
    {
        GameObject player = GameObject.FindWithTag("Angel");
        float distance = Vector3.Distance(transform.position, player.transform.position);

        return (distance < 1.5f && transform.position.y < player.transform.position.y);
    }
    
    private bool AreAnyDevilsNear()
    {
        GameObject[] devils = GameObject.FindGameObjectsWithTag("Devil");

        foreach (GameObject devil in devils)
        {
            float distance = Vector3.Distance(transform.position, devil.transform.position);

            if (distance < 1.5f && transform.position.y < devil.transform.position.y)
                return true;
        }

        return false;
    }

    private void FadeOut()
    {
        
        Color targetColor = material.color;
        targetColor.a = Mathf.Lerp(targetColor.a, 0.1f, fadeSpeed * Time.deltaTime);
        material.color = targetColor;
        GetComponent<Renderer>().material.color = material.color;
    }

    private void FadeIn()
    {
        Color targetColor = material.color;
        targetColor.a = Mathf.Lerp(targetColor.a, 1f, fadeSpeed * Time.deltaTime);
        material.color = targetColor;
        GetComponent<Renderer>().material.color = material.color;
    }
}