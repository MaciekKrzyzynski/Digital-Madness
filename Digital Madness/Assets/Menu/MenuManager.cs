using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public GameObject levels;

    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        levels.SetActive(false);
        instructions.SetActive(true);
    }

    public void ShowMainMenu()
    {
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void showLevels()
    {
        instructions.SetActive(false);
        levels.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void game()
    {
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }
}
