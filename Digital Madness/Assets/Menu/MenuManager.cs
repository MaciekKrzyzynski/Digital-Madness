using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;

    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }

    public void ShowMainMenu()
    {
        instructions.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void game()
    {
        instructions.SetActive(false);
        mainMenu.SetActive(false);
    }
}
