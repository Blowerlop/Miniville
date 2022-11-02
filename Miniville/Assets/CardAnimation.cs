using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 initialScale;
    private Vector3 initialRotation;

    [SerializeField] private float scaleForce;
    [SerializeField] private float scaleDuration;

    [SerializeField] private float rotateForce;
    [SerializeField] private float rotateDuration;

    private void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation.eulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(scaleForce, scaleDuration);
        transform.DORotate(transform.forward * -rotateForce, rotateDuration);

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.DOScale(initialScale, scaleDuration);
        transform.DORotate(initialRotation, rotateDuration);

    }
}
