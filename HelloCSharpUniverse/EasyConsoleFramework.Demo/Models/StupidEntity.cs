using EasyConsoleFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Demo.Models
{
    internal class StupidEntity
    {
        internal int Id { get; }

        internal string Name { get; }

        internal DateTime ThatMoment { get; }

        internal decimal Revenue { get; }

        internal PreferredTopic PreferredTopic { get; }

        internal StupidEntity(int id, string name, DateTime moment, decimal revenue, PreferredTopic topic)
        {
            Id = id;
            Name = name;
            ThatMoment = moment;
            Revenue = revenue;
            PreferredTopic = topic;
        }

        internal StupidEntity()
        {
            Id = Randomized.RandomId();
            Name = Randomized.RandomName();
            ThatMoment = Randomized.RandomDateTime();
            Revenue = Randomized.RandomMoney();
            PreferredTopic = PreferredTopic.Philosophy;
        }
    }

    internal enum PreferredTopic : byte
    {
        Philosophy,
        ProgrammingLanguages,
        Literature,
        Videogames,
        Psychology,
        MartialArts
    }
}