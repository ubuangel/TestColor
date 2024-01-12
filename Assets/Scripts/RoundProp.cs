using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct ColorData
{
    public ColorData(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public string Name { get; }
    public Color Color { get; }
}

public class RoundProp : MonoBehaviour
{
    [SerializeField] private GameObject _colorTextBackgroundObject;
    [SerializeField] private GameObject _option1Button;
    [SerializeField] private GameObject _option2Button;
    [SerializeField] private GameObject _option3Button;

    public ColorData RoundText { get; private set; }
    public ColorData RoundColor { get; private set; }
    public ColorData[] RoundColors { get; private set; } = new ColorData[3];
    
    private ColorData _prevText;
    private ColorData _prevColor;
    private int _untilGuarantee = 3;
    
    private static readonly ColorData[] ColorList =
    {
        new("Rojo", new Color(0.824f, 0.059f, 0.224f)),
        new("Amarillo", new Color(0.875f, 0.768f, 0.114f)),
        new("Verde", new Color(0.251f, 0.627f, 0.169f)),
        new("Azul", new Color(0.118f, 0.4f, 0.961f)),
        new("Morado", new Color(0.533f, 0.224f, 0.937f))
    };

    private void Awake()
    {
        _prevText = RandomColor();
        _prevColor = RandomColor();
    }

    public static ColorData RandomColor()
    {
        return ColorList[Random.Range(0, ColorList.Length)];
    }
    
    // around 1 correct match for every 1.7 incorrect matches
    public void ChooseColors()
    {
        RoundColor = RandomColor();
        if (_colorTextBackgroundObject != null)
        {
            RoundText = RandomColor();
            _colorTextBackgroundObject.GetComponent<Image>().color = 0.5f * RandomColor().Color;
        }
        if ((_option1Button != null) && (_option2Button != null) && (_option3Button != null))
        {
            System.Random random = new System.Random();

            ColorData[] newColorList = ColorList.ToArray();
            Shuffle(newColorList);

            RoundColors[0] = newColorList[0];
            RoundColors[1] = newColorList[1];
            RoundColors[2] = newColorList[2];
            RoundText = RoundColors[random.Next(3)];

            _option1Button.GetComponent<Image>().color = RoundColors[0].Color;
            _option2Button.GetComponent<Image>().color = RoundColors[1].Color;
            _option3Button.GetComponent<Image>().color = RoundColors[2].Color;
        }

        if (!RoundText.Equals(RoundColor))
        {
            _untilGuarantee -= 1;
        }
        
        if (_untilGuarantee == 0)
        {
            RoundColor = RoundText;
            _untilGuarantee = Random.Range(2, 8);
        }
        else
        {
            while (_prevText.Equals(RoundText) && _prevColor.Equals(RoundColor))
            {
                RoundText = RandomColor();
                RoundColor = RandomColor();
            }
        }
        
        _prevText = RoundText;
        _prevColor = RoundColor;
    }

    private void Shuffle<T>(T[] data)
    {
        System.Random random = new System.Random();
        for (int i = 0; i < (data.Length - 1); ++i)
        {
            int r = random.Next(i, data.Length);
            (data[r], data[i]) = (data[i], data[r]);
        }
    }
}
