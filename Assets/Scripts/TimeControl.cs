using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private PacStudentController pacStudent;
    [SerializeField] private GhostController[] ghosts;

    private PlayerInput playerInput;

    private bool fastForwarding = false;
    private float slowMotionCooldown = 15.0f;
    private float ultraSlowMotionCooldown = 30.0f;

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
}
