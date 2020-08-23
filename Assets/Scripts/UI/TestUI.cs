using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     todo delete this
/// </summary>
public class TestUI : MonoBehaviour
{
    public int starting;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = starting;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value++;
        }
    }
}
