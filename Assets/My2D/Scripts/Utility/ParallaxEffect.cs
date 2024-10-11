using UnityEngine;

namespace My2D
{
    //�÷��̾� �̵� ���� ����ȿ�� �Ÿ� ���ϱ�
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public Camera camera;           //ī�޶�
        public Transform followTarget;  //�÷��̾�

        //������ġ
        private Vector2 startingPosition;    //���� ��ġ (���, ī�޶�)
        private float startingZ;            //�����Ҷ� ����� z�� ��ġ��

        //������������ ������ ī�޶� �ִ� ��ġ������ �Ÿ�
        private Vector2 CamMoveSinceStart => startingPosition - (Vector2)camera.transform.position;

        //���� �÷��̾���� z�� �Ÿ�
        private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
        //
        private float ClippingPlane => camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane:camera.nearClipPlane);
        //���� �Ÿ� factor
        private float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / ClippingPlane;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            startingPosition = transform.position;
            startingZ = transform.position.z;
        }

        private void Update()
        {
            Vector2 newPosition = startingPosition + CamMoveSinceStart * ParallaxFactor;
            transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }

    }
}
