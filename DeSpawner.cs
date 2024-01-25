using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawner : MonoBehaviour
{
    private Coroutine co;
    [SerializeField] private float deSpawnTime;

    private void Start()
    {
        co = StartCoroutine(DeSpawnText());
    }

    public IEnumerator DeSpawnText()
    {
        yield return new WaitForSeconds(deSpawnTime);
        StopCo();
    }

    public void StopCo()
    {
        StopCoroutine(co);
        Destroy(this.transform.gameObject);
    }
}
