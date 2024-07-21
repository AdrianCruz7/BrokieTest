using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField] int instancesOfHealing;
    [SerializeField] float delayInHeal;
    [SerializeField] float healthPerDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Regenerate());
            var status = gameObject.GetComponent<MeshRenderer>();
            status.enabled = false;
        }
    }

    IEnumerator Regenerate()
    {
        while (instancesOfHealing > 0)
        {
            GameManager.Instance.SignalPlayerHealthChange(healthPerDelay);
            instancesOfHealing--;
            yield return new WaitForSeconds(delayInHeal);
        }

        Destroy(gameObject);
    }
}
