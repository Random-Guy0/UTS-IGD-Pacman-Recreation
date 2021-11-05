using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private PacStudentController pacStudent;

    [SerializeField] private GameObject slowMotionCountdown;
    [SerializeField] private TMP_Text slowMotionCountdownText;

    [SerializeField] private GameObject ultraSlowMotionCountdown;
    [SerializeField] private TMP_Text ultraSlowMotionCountdownText;

    private PlayerInput playerInput;

    private bool fastForwarding = false;
    private int slowMotionCooldown = 15;
    private int ultraSlowMotionCooldown = 30;

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
        if(!slowMotion && !ultraSlowMotion && canSlowMotion)
        {
            slowMotion = true;
            Time.timeScale *= 0.2f;
            pacStudent.speed *= 5.0f;

            yield return new WaitForSecondsRealtime(5.0f);

            slowMotion = false;
            Time.timeScale *= 5.0f;
            pacStudent.speed *= 0.2f;

            canSlowMotion = false;
            slowMotionCountdown.SetActive(true);
            slowMotionCountdownText.text = slowMotionCooldown.ToString();
            StartCoroutine(SlowMotionCountdown());
        }
    }

    private IEnumerator SlowMotionCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        slowMotionCooldown--;

        if(slowMotionCooldown == 0)
        {
            canSlowMotion = true;
            slowMotionCountdown.SetActive(false);
        }
        else
        {
            slowMotionCountdownText.text = slowMotionCooldown.ToString();
            StartCoroutine(SlowMotionCountdown());
        }
    }

    private IEnumerator UltraSlowMotion()
    {
        if(!slowMotion && !ultraSlowMotion && canSlowMotion)
        {
            ultraSlowMotion = true;
            Time.timeScale *= 0.08f;
            pacStudent.speed *= 12.5f;

            yield return new WaitForSecondsRealtime(5.0f);

            ultraSlowMotion = false;
            Time.timeScale *= 12.5f;
            pacStudent.speed *= 0.08f;
        }
    }
}
