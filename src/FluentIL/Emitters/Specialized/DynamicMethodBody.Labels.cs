﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;

namespace FluentIL.Emitters
{
    partial class DynamicMethodBody
    {
        private readonly Dictionary<string, Label> labelsField = new Dictionary<string, Label>();

        public DynamicMethodBody MarkLabel(Label label)
        {
#if DEBUG
            Debug.Print("IL_{0}:", label.GetHashCode());
#endif

            methodInfoField.GetILEmitter()
                .MarkLabel(label);

            return this;
        }

        public DynamicMethodBody MarkLabel(string label)
        {
            Label lbl = GetLabel(label);
#if DEBUG
            Debug.Print("IL_{0}:", lbl.GetHashCode());
#endif

            methodInfoField.GetILEmitter()
                .MarkLabel(GetLabel(label));

            return this;
        }

        private Label GetLabel(string label)
        {
            if (!labelsField.ContainsKey(label))
                labelsField.Add(label, methodInfoField.GetILEmitter().DefineLabel());

            return labelsField[label];
        }
    }
}