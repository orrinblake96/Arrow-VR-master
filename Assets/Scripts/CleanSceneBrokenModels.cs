using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSceneBrokenModels : MonoBehaviour
{
    public GameObject _wholeModel;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3);
        
        Destroy(gameObject);
        
        yield return new WaitForSeconds(2);

        Instantiate(_wholeModel, gameObject.transform.position, gameObject.transform.rotation);
    }
}
