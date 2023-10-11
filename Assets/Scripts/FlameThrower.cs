using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private float flameRate = 4f;
    private bool isFlameActive = true;

    private void Start()
    {
        flamePrefab.SetActive(isFlameActive);
        StartCoroutine(ToggleFlameState());
    }

    private IEnumerator ToggleFlameState()
    {
        while (true)
        {
            yield return new WaitForSeconds(flameRate);
            isFlameActive = !isFlameActive;
            flamePrefab.SetActive(isFlameActive);
        }
    }

}
