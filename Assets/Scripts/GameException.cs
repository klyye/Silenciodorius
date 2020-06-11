using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

/// <summary>
///     A general exception class to indicate that an exception was thrown from scripts that were
///     written by "us". (??? I don't know how to phrase this better, just stuff that doesn't come
///     from Unity itself).
/// </summary>
public class GameException : SystemException
{
    public GameException(string msg) : base(msg) {
        
    }

    /// <summary>
    ///     Gives an exception with a formatted message.
    /// </summary>
    /// <param name="msgFormat">The string according to which we format the message.</param>
    /// <param name="args">Additional objects that we can format into the message.</param>
    /// <returns>An exception with the message.</returns>
    public static GameException Error(string msgFormat, params object[] args)
    {
        return new GameException(string.Format(msgFormat, args));
    }
}
