using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Manager : MonoBehaviour
    {
        private const float Speed = 5f;

        [SerializeField] private GameObject originObject;
        [SerializeField] private GameObject destObject;
        [SerializeField] private GameObject movingObject;
        [SerializeField] private InputField originXInputField;
        [SerializeField] private InputField originYInputField;
        [SerializeField] private InputField destXInputField;
        [SerializeField] private InputField destYInputField;
        [SerializeField] private InputField speedInputField;

        private PathFinder pathFinder;
        private Coroutine currentCoroutine;

        public void Start()
        {
            pathFinder = new PathFinder();
        }

        public void BeginMovingObject()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            var originCoordinate = Vector2Int.zero;
            var destCoordinate = new Vector2Int(5, 5);

            if (int.TryParse(originXInputField.text, out int originX))
            {
                originCoordinate.x = originX;
            }
            else
            {
                originXInputField.text = "0";
            }

            if (int.TryParse(originYInputField.text, out int originY))
            {
                originCoordinate.y = originY;
            }
            else
            {
                originYInputField.text = "0";
            }

            if (int.TryParse(destXInputField.text, out int destX))
            {
                destCoordinate.x = destX;
            }
            else
            {
                destXInputField.text = "5";
            }

            if (int.TryParse(destYInputField.text, out int destY))
            {
                destCoordinate.y = destY;
            }
            else
            {
                destYInputField.text = "5";
            }

            if (!int.TryParse(speedInputField.text, out int speed))
            {
                speed = 5;
                speedInputField.text = "5";
            }

            originObject.transform.position = new Vector3(originCoordinate.x, originCoordinate.y);
            destObject.transform.position = new Vector3(destCoordinate.x, destCoordinate.y);

            Vector2Int[] pathCoordinates = pathFinder.FindPath(originCoordinate, destCoordinate);

            currentCoroutine = StartCoroutine(pathFinder.TracePath(movingObject, pathCoordinates, speed));
        }
    }
}