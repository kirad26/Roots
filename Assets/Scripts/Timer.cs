using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] private Slider uFill;

    public int Duration;
    private int remainingDuration;

    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
        return true;

    }
    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            uFill.value = Mathf.InverseLerp(0,Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }
    private void OnEnd()
    {
        uFill.value = 0;
        Being(Duration);
    }
}
