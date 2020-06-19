using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(true);
    }
  public void SetCountDown()
  {
      GameManager.instance.countDownDone = true;
      GameManager.instance.StartCoroutine ("PlayTimer");

      this.gameObject.SetActive(false);
  }
}
