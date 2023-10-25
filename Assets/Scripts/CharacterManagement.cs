using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playerAnim;
    [Range(0,15)]
    public float speed;
    public Transform targetPosition;
    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveForward();
        }
    }

    void MoveForward ()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, speed * Time.time);
        playerAnim.SetBool("isRunning", true);
    }
    void StopMovement()
    {
        speed = 0;
        playerAnim.SetBool("isRunning", false);
    }
}
