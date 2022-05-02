using EasyConsoleFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Demo.Models
{
    public class StupidEntity
    {
        public int Id { get; }

        public string Name { get; }

        public DateTime ThatMoment { get; }

        public decimal Revenue { get; }

        public PreferredTopic PreferredTopic { get; }

        public StupidEntity(int id, string name, DateTime moment, decimal revenue, PreferredTopic topic)
        {
            Id = id;
            Name = name;
            ThatMoment = moment;
            Revenue = revenue;
            PreferredTopic = topic;
        }

        public StupidEntity()
        {
            Id = Randomized.RandomId();
            Name = Randomized.RandomName();
            ThatMoment = Randomized.RandomDateTime();
            Revenue = Randomized.RandomMoney();
            PreferredTopic = PreferredTopic.Philosophy;
        }
    }

    public enum PreferredTopic : byte
    {
        Philosophy,
        ProgrammingLanguages,
        Literature,
        Videogames,
        Psychology,
        MartialArts
    }
}