using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private PacStudentController pacStudent;

    private PlayerInput playerInput;

    private bool fastForwarding = false;
    private float slowMotionCooldown = 15.0f;
    private float ultraSlowMotionCooldown = 30.0f;

    private bool slowMotion = false;
    private bool ultraSlowMotion = false;

    private bool canSlowMotion = true;
    private bool canUltraSlowMotion = true;

    private void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void Update()
    {
        if(playerInput.TimePowers.FastForward.triggered)
        {
            FastForward();
        }

        if(playerInput.TimePowers.SlowMotion.triggered)
        {
            StartCoroutine(SlowMotion());
        }

        if(playerInput.TimePowers.UltraSlowMotion.triggered)
        {
            StartCoroutine(UltraSlowMotion());
        }
    }

    private void FastForward()
    {
        if (!fastForwarding)
        {
            Time.timeScale *= 2.0f;
            fastForwarding = true;
        }
        else
        {
            Time.timeScale *= 0.5f;
            fastForwarding = false;
        }
    }

    private IEnumerator SlowMotion()
    {
        if(!slowMotion && !ultraSlowMotion)
        {
            slowMotion = true;
            Time.timeScale *= 0.2f;
            pacStudent.speed *= 5.0f;

            yield return new WaitForSecondsRealtime(5.0f);

            slowMotion = false;
            Time.timeScale *= 5.0f;
            pacStudent.speed *= 0.2f;
        }
    }

    private IEnumerator UltraSlowMotion()
    {
        if(!slowMotion && !ultraSlowMotion)
        {
            yield return new WaitForSecondsRealtime(0.0f);
        }
    }
}
