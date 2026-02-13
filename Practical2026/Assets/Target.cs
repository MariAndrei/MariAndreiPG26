using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Renderer rend;
    private Color startColor;
    private Color endColor = Color.red;
    private float flashDuration = 0.3f;
    float DeFlashDuration = 0.1f;
    private float health = 50f;
    float effectTimer;
    private void Start()
    {
        startColor = rend.material.color;
    }
    enum TargetState { Normal, Flashing, DeFlashing}
    TargetState state = TargetState.Normal;


    private void Update()
    {
        switch (state)
        {
            case TargetState.Normal:


                break;
            case TargetState.Flashing:

                rend.material.color = Color.Lerp(startColor, endColor, effectTimer/flashDuration);
                effectTimer += Time.deltaTime;
                if (effectTimer > flashDuration)
                {
                    effectTimer = 0;
                    state = TargetState.DeFlashing;
                }
                break;

            case TargetState.DeFlashing:

                rend.material.color = Color.Lerp(endColor, startColor, effectTimer / DeFlashDuration);
                effectTimer += Time.deltaTime;
                if (effectTimer > DeFlashDuration) 
                    state = TargetState.Normal;
                    break;
        }
    }
    public void takeDamage(float amount)
    {
        
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

        state = TargetState.Flashing;
        effectTimer = 0;

    }


    void Die()
    {

        Destroy(gameObject);
    }

}
