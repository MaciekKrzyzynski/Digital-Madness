using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class MenuTextEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public enum MenuAction
    {
        None,
        StartGame,
        Instructions,
        StartScreen
    }
    public MenuAction menuAction;
    private Vector3 originalScale;
    private Coroutine animationCoroutine;

    private float hoverScale = 1.1f;
    private float animationSpeed = 0.15f;
    public MenuManager menuManager;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 target = originalScale * hoverScale;
        animationCoroutine = StartCoroutine(ScaleAnimation(target));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 target = originalScale;
        animationCoroutine = StartCoroutine(ScaleAnimation(target));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 target = originalScale * 0.9f;
        animationCoroutine = StartCoroutine(ScaleAnimation(target));
        StartCoroutine(ClickBounce());
    }

    IEnumerator ScaleAnimation(Vector3 target)
    {
        Vector3 start = transform.localScale;
        float time = 0;

        while (time < animationSpeed)
        {
            time += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(start, target, time / animationSpeed);
            yield return null;
        }

        transform.localScale = target;
    }

    IEnumerator ClickBounce()
    {
        //Srink
        yield return new WaitForSecondsRealtime(0.1f);

        Vector3 target = originalScale * hoverScale;
        animationCoroutine = StartCoroutine(ScaleAnimation(target));
        switch (menuAction)
        {
            case MenuAction.StartGame:
                menuManager.game();
                break;

            case MenuAction.Instructions:
                menuManager.ShowInstructions();
                break;

            case MenuAction.StartScreen:
                menuManager.ShowMainMenu();
                break;
        }
    }
}
