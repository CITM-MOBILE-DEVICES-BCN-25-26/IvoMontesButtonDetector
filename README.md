# IvoMontesButtonDetector

I used the IPointerDownHandler and IPointerUpHandler interfaces to detect exactly when the user presses and releases the button (as recommended by my fantastic colleague Pau Mora).

- When I click, there is a counter that counts the clicks.

- I use isCalculating to know if the task is running so I don't trigger it again and simply use the counter.
  
- I use the CancellationToken to know when the user has stopped pressing, I cancel it in OnPointerUp.

- In await Click, the execution pauses to wait for the result.

- In the While (!tokenSource.IsCancellationRequested), I measure how much time the button remains pressed. The While stops when tokenSource.Cancel is called.

- I use await Task.Yield(); (recommendation of my fantastic colleague Pau Mora) so that the frame ends and the loop does not stay in the same frame all the time.

- In the Ifs, I compare the clicked time with the time assigned to LongClick to know if it is a LongClick. If the time is less, I use await Task.Delay(250) to wait 250 milliseconds to give time if there is another click, if after the wait the clickCount is 2 or more, then it detects it as a double click.

- At the end of everything, I set all variables to 0 to be able to register clicks again as many times as I want.

