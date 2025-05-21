using UnityEngine;
using UnityEngine.UI;

public class ItemToggle_ES: MonoBehaviour
{
    [SerializeField] Image ItemToggleBar2;
    [SerializeField] Image ItemToggleBar1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ��������Ʈ ���� ��ȯ
            Sprite temp = ItemToggleBar1.sprite;
            ItemToggleBar1.sprite = ItemToggleBar2.sprite;
            ItemToggleBar2.sprite = temp;
        }
    }
}
