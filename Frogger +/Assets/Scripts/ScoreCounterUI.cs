using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class ScoreCounterUI : MonoBehaviour
{

[SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform scoreTextContainer;
    [SerializeField] private float duration;
    private float containerInitPosition;
    private float moveAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Ease animationCurve;
    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }


    public void UpdateScore(int score)
    {
        toUpdate.SetText($"{score}");
        scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetCoinContainer(score));
    }

        private IEnumerator ResetCoinContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}"); 
        Vector3 localPosition = scoreTextContainer.localPosition;
        scoreTextContainer.localPosition = new Vector3(localPosition.x,
        containerInitPosition, localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
