using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleCommandBase
{
    private string _commandId;
    private string _commandDesc;
    private string _commandFormat;

    public string commandId
    {
        get
        {
            return _commandId;
        }
    }

    public string commandDesc
    {
        get
        {
            return _commandDesc;
        }
    }

    public string commandFormat
    {
        get
        {
            return _commandFormat;
        }
    }

    public ConsoleCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDesc = description;
        _commandFormat = format;
    }
}

public class ConsoleCommand : ConsoleCommandBase
{
    public Action command;
    public ConsoleCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}