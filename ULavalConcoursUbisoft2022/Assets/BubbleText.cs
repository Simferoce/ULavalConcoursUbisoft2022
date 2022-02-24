using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private TextMeshProUGUI _text = null;
    [SerializeField] private Vector3 _offSet = Vector3.zero;

    private Camera _camera = null;
    private float _endMessage = 0.0f;

    public enum Source
    {
        Unknow
    }

    public struct Message
    {
        public int Id;
        public Source Source;
        public string Text;
        public float Duration;
    }

    public static Dictionary<int, Message> messages = new Dictionary<int, Message>()
    {
        {0, new Message() {Id = 0, Source = Source.Unknow, Text = "What ?!? A new assignment !?", Duration = 3.0f } },
        {1, new Message() {Id = 1, Source = Source.Unknow, Text = "Don't you think we did enough !? We are exhauted of all those SPECIALS measures. ", Duration = 3.0f } },
        {2, new Message() {Id = 2, Source = Source.Unknow, Text = "I cannot ... conti...", Duration = 2.0f } },
        {3, new Message() {Id = 3, Source = Source.Unknow, Text = "ARGGGGHHH !!!", Duration = 2.0f } },
    };

    private void Start()
    {
        _camera = Camera.main;

        AlignToCameraAngleYZAxis();
        _canvas.transform.localPosition = _canvas.transform.localRotation * _offSet;
    }

    private void AlignToCameraAngleYZAxis()
    {
        _canvas.transform.rotation = Quaternion.Euler(Vector3.Scale((Quaternion.Inverse(_camera.transform.rotation)).eulerAngles, new Vector3(0, 1, 1)));
    }

    public void ShowMessage(int id, float duration = -1)
    {
        Message message;
        if(messages.TryGetValue(id, out message))
        {
            _canvas.gameObject.SetActive(true);
            _text.SetText(message.Text);
            _endMessage = Time.time + (duration == -1 ? message.Duration : duration);
        }
    }

    private void Update()
    {
        AlignToCameraAngleXAxis();
        RepositionateBubbleText();

        if (Time.time > _endMessage)
        {
            _canvas.gameObject.SetActive(false);
        }
    }

    private void RepositionateBubbleText()
    {
        _canvas.transform.position = _canvas.transform.parent.position + _canvas.transform.parent.localRotation * _offSet;

        //Rect cavasRect = _canvas.GetComponent<RectTransform>().rect;
        //Vector3 pointA = _camera.WorldToViewportPoint(_canvas.transform.position +  _canvas.transform.localRotation * new Vector3(-cavasRect.width / 2, -cavasRect.height / 2, 0));
        //Vector3 pointAClamp = new Vector3(Mathf.Clamp01(pointA.x), Mathf.Clamp01(pointA.y));
        //Vector3 pointADiff = pointA - pointAClamp;
        //Vector3 pointB = _camera.WorldToViewportPoint(_canvas.transform.position +  _canvas.transform.localRotation * new Vector3(cavasRect.width / 2, -cavasRect.height / 2, 0));
        //Vector3 pointBClamp = new Vector3(Mathf.Clamp01(pointB.x), Mathf.Clamp01(pointB.y));
        //Vector3 pointBDiff = pointB - pointBClamp;
        //Vector3 pointC = _camera.WorldToViewportPoint(_canvas.transform.position + _canvas.transform.localRotation * new Vector3(-cavasRect.width / 2, cavasRect.height / 2, 0));
        //Vector3 pointCClamp = new Vector3(Mathf.Clamp01(pointC.x), Mathf.Clamp01(pointC.y));
        //Vector3 pointCDiff = pointC - pointCClamp;
        //Vector3 pointD = _camera.WorldToViewportPoint(_canvas.transform.position +  _canvas.transform.localRotation * new Vector3(cavasRect.width / 2, cavasRect.height / 2, 0));
        //Vector3 pointDClamp = new Vector3(Mathf.Clamp01(pointD.x), Mathf.Clamp01(pointD.y));
        //Vector3 pointDDiff = pointD - pointDClamp;

        //float minX = Mathf.Min(pointADiff.x, pointBDiff.x, pointCDiff.x, pointDDiff.x);
        //float maxX = Mathf.Max(pointADiff.x, pointBDiff.x, pointCDiff.x, pointDDiff.x);
        //float minY = Mathf.Min(pointADiff.y, pointBDiff.y, pointCDiff.y, pointDDiff.y);
        //float maxY = Mathf.Max(pointADiff.y, pointBDiff.y, pointCDiff.y, pointDDiff.y);

        //float maxXAbs = Mathf.Abs(minX) > Mathf.Abs(maxX) ? minX : maxX;
        //float maxYAbs = Mathf.Abs(minY) > Mathf.Abs(maxY) ? minY : maxY;

        //Vector3 point = _camera.WorldToViewportPoint(_canvas.transform.position);
        //point.x = point.x - maxXAbs;
        //point.y = point.y - maxYAbs;
        //_canvas.transform.position = _camera.ViewportToWorldPoint(point);
    }

    private void AlignToCameraAngleXAxis()
    {
        _canvas.transform.rotation = Quaternion.Euler(Vector3.Scale((_camera.transform.rotation).eulerAngles, new Vector3(1, 0, 0)));
    }
}
