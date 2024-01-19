using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private StageManager stageManager;
    private PlayerController playerController;

    public float rotationSpeed;
    public float moveSpeed;

    private void Start()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (stageManager.mainStage == 4)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else if (stageManager.mainStage == 6)
        {
            MoveToLastClickPosition();
        }
    }

    private void MoveToLastClickPosition()
    {
        Vector3 targetPosition = playerController.GetLastClickPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
