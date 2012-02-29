﻿using System;
using System.Linq;
using System.Reflection;
using FluentIL.Cecil;
using Mono.Cecil;
using _OpCodes = System.Reflection.Emit.OpCodes;

namespace CecilUsingFluentIL
{
    internal class Program
    {
        private static void Main()
        {
            AssemblyDefinition assembly = AssemblyDefinition
                .ReadAssembly("ConsoleProgramThatWillBeChanged.exe");

            TypeDefinition type = assembly.MainModule.Types
                .First(t => t.Name == "Program");

            ModifyDoSomethingMethod(type);
            ModifyAddMethod(type);

            assembly.Write("ConsoleProgramThatWillBeChanged.Patched.exe");
        }

        private static void ModifyDoSomethingMethod(TypeDefinition type)
        {
            MethodDefinition method = type.Methods
                .First(m => m.Name == "DoSomething");

            method.ReplaceWith()
                .Ldstr("Hello World from modified program")
                .Ret();
        }

        private static void ModifyAddMethod(TypeDefinition type)
        {
            MethodDefinition method = type.Methods
                .First(m => m.Name == "Add");

            MethodInfo minfo = typeof (Console).GetMethod(
                "WriteLine",
                new[] {typeof (string), typeof (int)});

            method.InsertBefore()
                .Ldstr("Value of First Parameter is {0}")
                .Ldarg(0)
                .Box(typeof (int))
                .Call(minfo)
                .Ldstr("Value of Second Parameter is {0}")
                .Ldarg(1)
                .Box(typeof (int))
                .Call(minfo);
        }
    }
}