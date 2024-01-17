using System.Collections;
using UnityEngine;

public class Stage2_1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float rotationSpeed = 180f;

    void Start()
    {
        StartCoroutine(ActivateBulletAfterDelay(2f));
    }

    IEnumerator ActivateBulletAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(RotateAndDeactivate(bulletPrefab, new Vector3(0f, 0f, 1f), rotationSpeed, 3f));
    }

    IEnumerator RotateAndDeactivate(GameObject obj, Vector3 axis, float speed, float duration)
    {
        obj.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.transform.Rotate(axis * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.SetActive(false);

        yield return new WaitForSeconds(3f);
        StartCoroutine(ActivateBulletAfterDelay(0f));
    }
}
