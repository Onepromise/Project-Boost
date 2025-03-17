using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
   private void Update() 
   {
        Quit();    
   }

   void Quit()
   {
    if (Keyboard.current.escapeKey.wasPressedThisFrame)
    {
        Debug.Log("I just quit!");
        Application.Quit();
    }
    
   }
}
