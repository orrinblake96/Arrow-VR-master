using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameobjects : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DisableObject());
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);
    }
}
