using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //ĳ���Ϳ� ���õ� �̺�Ʈ �Լ� �����ϴ� Ŭ����

    public class CharacterEvents
    {
        //ĳ���Ͱ� �������� ������ ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> charaterDamaged;

        //ĳ���Ͱ� ���Ҷ� ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> charaterHealed;

        //...
    }
}
