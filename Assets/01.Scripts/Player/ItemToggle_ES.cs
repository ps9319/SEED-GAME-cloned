using UnityEngine;
using UnityEngine.UI;

public class ItemToggle_ES: MonoBehaviour
{
    [SerializeField] Image ItemToggleBar2;
    [SerializeField] Image ItemToggleBar1;

    //무기들
    [SerializeField] private Sprite fistSprite;
    [SerializeField] private Sprite pencilcaseSprite;

    //무기
    public string currentState = "fist";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 스프라이트 전환
            Sprite temp = ItemToggleBar1.sprite;
            ItemToggleBar1.sprite = ItemToggleBar2.sprite;
            ItemToggleBar2.sprite = temp;

            // 상태 업데이트
            UpdateState();
        }
    }

    void UpdateState()
    {
        if (ItemToggleBar1.sprite == fistSprite)
        {
            currentState = "fist";
        }
        else if (ItemToggleBar1.sprite == pencilcaseSprite)
        {
            currentState = "pencilcase";
        }
        else
        {
            currentState = "unknown";
        }
    }

}
