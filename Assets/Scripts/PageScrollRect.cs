using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageScrollRect : ScrollRect
{
    private float pageWidth;
    private int prevPageIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        StopMovement();

        int pageIndex = Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);
        if(pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 5){
            pageIndex += (int)Mathf.Sign(eventData.delta.x);
        }

        float destX = pageIndex * pageWidth;
        content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);

        prevPageIndex = pageIndex;
    }
}
