using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public GameObject levels;
    public GameObject Sphere;
    public GameObject Cylinder;

    public void ShowInstructions()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        mainMenu.SetActive(false);
        levels.SetActive(false);
        instructions.SetActive(true);
    }

    public void ShowMainMenu()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void showLevels()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void sphereLevel()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(true);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void cylinderLevel()
    {
        Cylinder.SetActive(true);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void game()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }
}
