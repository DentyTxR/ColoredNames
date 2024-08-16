using Exiled.API.Features;
using System;
using UnityEngine;

namespace ColoredNames.Features.Components
{
    //
    //Code from https://github.com/NotIntense/RainbowTags
    //
    public class RainbowBadgeComponent : MonoBehaviour
    {
        private Player _player;
        private int _position;
        private string[] _colors = Sequences;
        private float _timer;
        public double Interval;

        public string[] Colors
        {
            get => _colors ?? Array.Empty<string>();
            set
            {
                _colors = value ?? Array.Empty<string>();
                _position = 0;
            }
        }

        private void Awake()
        {
            _timer = 0;
            _player = Player.Get(gameObject);

            _player.RankName = " ";
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= Interval)
            {
                string text = RollNext();

                if (!string.IsNullOrEmpty(text))
                    _player.RankColor = text;

                _timer = 0;
            }
        }

        private string RollNext()
        {
            if (_colors.Length == 0)
                return string.Empty;

            _position = (_position + 1) % _colors.Length;
            return _colors[_position];
        }

        public static string[] Sequences { get; set; } = new[]
        {
            "pink",
            "red",
            "brown",
            "silver",
            "light_green",
            "crimson",
            "cyan",
            "aqua",
            "deep_pink",
            "tomato",
            "yellow",
            "magenta",
            "blue_green",
            "orange",
            "lime",
            "green",
            "emerald",
            "carmine",
            "nickel",
            "mint",
            "army_green",
            "pumpkin"
        };
    }
}