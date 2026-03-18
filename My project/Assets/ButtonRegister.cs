using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class ButtonRegister : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private CancellationTokenSource tokenSource;
	private int timeToDoubleClick = 250;
	private float longClickTime = 2f;
	private bool isLongClick;
	private bool isDoubleClick;
	private float timeClicked;
	private int clickCount;
	private bool isCalculating;

	public void OnPointerUp(PointerEventData eventData)
	{
		tokenSource.Cancel();
	}
	public async void OnPointerDown(PointerEventData eventData) {

		clickCount++;
        if (isCalculating == true)
        {
			return;
        }
        tokenSource = new CancellationTokenSource();
        isCalculating = true;
        await Click(tokenSource.Token);
		
	}




	private async Task Click(CancellationToken tokenSource)
	{
		while (!tokenSource.IsCancellationRequested) {
			timeClicked += Time.deltaTime;
			await Task.Yield();
		}
		if (timeClicked >= longClickTime) {

			Debug.Log("LongClick");
		}
		else if (timeClicked <= longClickTime){
			await Task.Delay(timeToDoubleClick);
			if (clickCount >= 2)
			{
				Debug.Log("DoubleClick");
			}
			else
			{
                Debug.Log("Click");
            }

        }

		isCalculating = false;
		clickCount = 0;
		timeClicked = 0;
	}

}
