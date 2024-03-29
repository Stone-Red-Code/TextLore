﻿namespace TextLore.Shared.Logic;

public interface IBuilder<out T> where T : class
{
    T Build();
}